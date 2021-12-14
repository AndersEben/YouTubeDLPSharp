using Newtonsoft.Json;
using System;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using YouTubeDLPSharp.Process;
using YouTubeDLPSharp.Data;
using YouTubeDLPSharp.Options;
using System.IO;
using System.Collections.Generic;

namespace YouTubeDLPSharp
{
    /// <summary>
    /// A class providing methods for downloading videos using youtube-dl.
    /// </summary>
    public class YouTubeDLP
    {

        private static Regex rgxFile = new Regex("echo\\s\\\"?(.*)\\\"?", RegexOptions.Compiled);

        protected ProcessRun runner;

        /// <summary>
        /// Path to the youtube-dlp executable.
        /// </summary>
        public string YoutubeDLPPath { get; set; } = "yt-dlp.exe";
        /// <summary>
        /// Path to the FFmpeg executable.
        /// </summary>
        public string FFmpegPath { get; set; } = "ffmpeg.exe";
        /// <summary>
        /// Path of the folder where items will be downloaded to.
        /// </summary>
        public string OutputFolder { get; set; } = Environment.CurrentDirectory;
        /// <summary>
        /// Template of the name of the downloaded file on youtube-dlp style.
        /// See https://github.com/yt-dlp/yt-dlp#output-template.
        /// </summary>
        public string OutputFileTemplate { get; set; } = "%(title)s.%(ext)s";
        /// <summary>
        /// If set to true, file names a re restricted to ASCII characters.
        /// </summary>
        public bool RestrictFilenames { get; set; } = false;


        public bool OverwriteFiles { get; set; } = true;

        /// <summary>
        /// If set to true, download errors are ignored and downloading is continued.
        /// </summary>
        public bool IgnoreDownloadErrors { get; set; } = true;

        /// <summary>
        /// Creates a new instance of the YoutubeDLP class.
        /// </summary>
        /// <param name="maxNumberOfProcesses">The maximum number of concurrent youtube-dlp processes.</param>
        public YouTubeDLP(byte maxNumberOfProcesses = 4)
        {
            runner = new ProcessRun(maxNumberOfProcesses);
        }

        /// <summary>
        /// Sets the maximal number of parallel download processes.
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        public async Task SetMaxNumberOfProcesses(byte count) => await runner.SetTotalCount(count);



        #region Process methods

        /// <summary>
        /// Runs a fetch of information for the given video without downloading the video.
        /// </summary>
        /// <param name="url">The URL of the video to fetch information for.</param>
        /// <param name="ct">A CancellationToken used to cancel the process.</param>
        /// <param name="flat">If set to true, does not extract information for each video in a playlist.</param>
        /// <param name="overrideOptions">Override options of the default option set for this run.</param>
        /// <returns>A RunResult object containing a VideoData object with the requested video information.</returns>
        public async Task<RunResult<VideoData>> RunVideoDataFetch(string url, CancellationToken ct = default, bool flat = true, OptionSet overrideOptions = null)
        {
            var opts = new OptionSet()
            {
                IgnoreErrors = this.IgnoreDownloadErrors,
                IgnoreConfig = true
            };

            if (overrideOptions != null)
            {
                opts = opts.OverrideOptions(overrideOptions);
            }

            opts.DumpSingleJson = true;
            opts.FlatPlaylist = flat;
            opts.SkipDownload = true;

            VideoData videoData = null;
            var process = new YouTubeDLPProcess(YoutubeDLPPath);
            process.OutputReceived += (o, e) => videoData = JsonConvert.DeserializeObject<VideoData>(e.Data);
            (int code, string[] errors) = await runner.RunThrottled(process, new[] { url }, opts, ct);
            return new RunResult<VideoData>(code == 0, errors, videoData);
        }

        /// <summary>
        /// Runs a download of the specified video with an optional conversion afterwards.
        /// </summary>
        /// <param name="url">The URL of the video to be downloaded.</param>
        /// <param name="format">A format selection string in youtube-dlp style.</param>
        /// <param name="mergeFormat">If a merge is required, the container format of the merged downloads.</param>
        /// <param name="recodeFormat">The video format the output will be recoded to after download.</param>
        /// <param name="ct">A CancellationToken used to cancel the download.</param>
        /// <param name="progress">A progress provider used to get download progress information.</param>
        /// <param name="output">A progress provider used to capture the standard output.</param>
        /// <param name="overrideOptions">Override options of the default option set for this run.</param>
        /// <param name="WriteJson">Neu</param>
        /// <returns>A RunResult object containing the path to the downloaded and converted video.</returns>
        public async Task<RunResult<string>> RunVideoDownload(string url,
            string format = "bestvideo*+bestaudio/best",
            DownloadMergeFormat mergeFormat = DownloadMergeFormat.Unspecified,
            VideoRecodeFormat recodeFormat = VideoRecodeFormat.None,
            CancellationToken ct = default, IProgress<DownloadProgress> progress = null,
            IProgress<string> output = null, OptionSet overrideOptions = null,bool WriteJson = false)
        {
            var opts = GetDownloadOptions();
            if (overrideOptions != null)
            {
                opts = opts.OverrideOptions(overrideOptions);
            }

            opts.Format = format;
            opts.MergeOutputFormat = mergeFormat;
            opts.RecodeVideo = recodeFormat;

            if(WriteJson)
            {
                opts.WriteInfoJson = true;
            }

            string outputFile = String.Empty;
            var process = new YouTubeDLPProcess(YoutubeDLPPath);
            // Report the used ytdlp args
            output?.Report($"Arguments: {process.ConvertToArgs(new[] { url }, opts)}\n");
            process.OutputReceived += (o, e) =>
            {
                var match = rgxFile.Match(e.Data);
                if (match.Success)
                {
                    outputFile = match.Groups[1].ToString().Trim('"');
                    progress?.Report(new DownloadProgress(DownloadState.Success, data: outputFile));
                }
                output?.Report(e.Data);
            };
            (int code, string[] errors) = await runner.RunThrottled(process, new[] { url }, opts, ct, progress);
            return new RunResult<string>(code == 0, errors, outputFile);
        }

        /// <summary>
        /// Runs a download of the specified video with and converts it to an audio format afterwards.
        /// </summary>
        /// <param name="url">The URL of the video to be downloaded.</param>
        /// <param name="format">The audio format the video will be converted to after downloaded.</param>
        /// <param name="ct">A CancellationToken used to cancel the download.</param>
        /// <param name="progress">A progress provider used to get download progress information.</param>
        /// <param name="output">A progress provider used to capture the standard output.</param>
        /// <param name="overrideOptions">Override options of the default option set for this run.</param>
        /// <param name="postprocess">Neu</param>
        /// <returns>A RunResult object containing the path to the downloaded and converted video.</returns>
        public async Task<RunResult<string>> RunAudioDownload(string url, AudioConversionFormat format,
            CancellationToken ct = default, IProgress<DownloadProgress> progress = null,
            IProgress<string> output = null, OptionSet overrideOptions = null, string postprocess = null)
        {
            var opts = GetDownloadOptions();
            if (overrideOptions != null)
            {
                opts = opts.OverrideOptions(overrideOptions);
            }
            opts.Format = "bestaudio/best";
            opts.ExtractAudio = true;
            opts.AudioFormat = format;

            //opts.PostprocessorArgs = "-ss 20 -to 120";
            if (postprocess != null)
            {
                opts.PostprocessorArgs = postprocess;
            }


            string outputFile = String.Empty;
            var error = new List<string>();
            var process = new YouTubeDLPProcess(YoutubeDLPPath);
            // Report the used ytdlp args
            output?.Report($"Arguments: {process.ConvertToArgs(new[] { url }, opts)}\n");
            process.OutputReceived += (o, e) =>
            {
                var match = rgxFile.Match(e.Data);
                if (match.Success)
                {
                    outputFile = match.Groups[1].ToString().Trim('"');
                    progress?.Report(new DownloadProgress(DownloadState.Success, data: outputFile));
                }
                output?.Report(e.Data);
            };
            (int code, string[] errors) = await runner.RunThrottled(process, new[] { url }, opts, ct, progress);
            return new RunResult<string>(code == 0, errors, outputFile);
        }


        /// <summary>
        /// Returns an option set with default options used for most downloading operations.
        /// </summary>
        protected virtual OptionSet GetDownloadOptions()
        {
            return new OptionSet()
            {
                IgnoreErrors = this.IgnoreDownloadErrors,
                IgnoreConfig = true,
                NoPlaylist = true,
                //ExternalDownloaderArgs = "-nostats -loglevel debug",
                Output = Path.Combine(OutputFolder, OutputFileTemplate),
                RestrictFilenames = this.RestrictFilenames,
                NoContinue = this.OverwriteFiles,
                NoOverwrites = !this.OverwriteFiles,
                NoPart = true,
                FfmpegLocation = Utils.GetFullPath(this.FFmpegPath),
                //PostprocessorArgs = "",
                Exec = "echo {}"
            };
        }

        #endregion
    }
}
