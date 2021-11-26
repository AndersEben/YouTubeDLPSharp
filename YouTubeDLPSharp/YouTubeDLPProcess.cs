using System;
using System.Diagnostics;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using YouTubeDLPSharp.Process;
using YouTubeDLPSharp.Options;
using System.IO;

namespace YouTubeDLPSharp
{
    public class YouTubeDLPProcess
    {


        // the regex used to match the currently downloaded video of a playlist.
        private static readonly Regex rgxPlaylist = new Regex(@"Downloading video (\d+) of (\d+)", RegexOptions.Compiled);
        // the regex used for matching download progress information.
        private static readonly Regex rgxProgress = new Regex(
            @"\[download\]\s+(?:(?<percent>[\d\.]+)%(?:\s+of\s+\~?(?<total>[\d\.\w]+))?\s+at\s+(?:(?<speed>[\d\.\w]+\/s)|[\w\s]+)\s+ETA\s(?<eta>[\d\:]+))?",
            RegexOptions.Compiled
        );
        // the regex used to match the beginning of post-processing.
        private static readonly Regex rgxPost = new Regex(@"\[ffmpeg\]\s+", RegexOptions.Compiled);

        // the regex used to match the beginning of post-processing.
        private static readonly Regex rgxYoutube = new Regex(@"\[youtube\]\s+(?<id>[A-Za-z0-9]\w+)?:\s(?<mess>.*)", RegexOptions.Compiled);
        // the regex used to match the beginning of post-processing.
        private static readonly Regex rgxInfo = new Regex(@"\[info\]\s+(?<mess>.*)", RegexOptions.Compiled);
        // the regex used to match the beginning of post-processing.
        private static readonly Regex rgxConverter = new Regex(@"\[VideoConvertor\]\s+(?<mess>.*)", RegexOptions.Compiled);
        // the regex used to match the beginning of post-processing.
        private static readonly Regex rgxMerger = new Regex(@"\[Merger\]\s+(?<mess>.*)", RegexOptions.Compiled);



        /// <summary>
        /// Path to the youtube-dlp.exe
        /// </summary>
        public string ExecutablePath { get; set; }

        /// <summary>
        /// youtube-dlp writes to the standard output
        /// </summary>
        public event EventHandler<DataReceivedEventArgs> OutputReceived;

        /// <summary>
        /// youtube-dlp writes to the error output.
        /// </summary>
        public event EventHandler<DataReceivedEventArgs> ErrorReceived;

        /// <summary>
        /// Creates a new instance of the YoutubeDLProcess class.
        /// </summary>
        /// <param name="executablePath">Path to the youtube-dlp.exe</param>
        public YouTubeDLPProcess(string executablePath = "yt-dlp.exe")
        {
            if (!File.Exists(executablePath))
                throw new InvalidOperationException(message: "cannot find yt-dlp.exe");
        
            this.ExecutablePath = executablePath;
        }


        internal string ConvertToArgs(string[] urls, OptionSet options) => (urls != null ? String.Join(" ", urls) : String.Empty) + options.ToString();


        /// <summary>
        /// Invokes youtube-dlp with specific parameters and options.
        /// </summary>
        /// <param name="urls">The video URLs to youtube-dlp.</param>
        /// <param name="options">options to be passed to youtube-dlp.</param>
        /// <returns>The exit code of the youtube-dlp process.</returns>
        public async Task<int> RunAsync(string[] urls, OptionSet options) => await RunAsync(urls, options, CancellationToken.None);

        /// <summary>
        /// Invokes youtube-dlp with specific parameters and options.
        /// </summary>
        /// <param name="urls">The video URLs to be passed to youtube-dlp.</param>
        /// <param name="options">An OptionSet specifying the options to be passed to youtube-dlp.</param>
        /// <param name="ct">CancellationToken used to cancel the download.</param>
        /// <param name="progress">A progress provider used to get download progress information.</param>
        /// <returns>The exit code of the youtube-dlp process.</returns>
        public async Task<int> RunAsync(string[] urls, OptionSet options, CancellationToken ct, IProgress<DownloadProgress> progress = null)
        {
            var tcs = new TaskCompletionSource<int>();
            var process = new System.Diagnostics.Process();

            var startInfo = new ProcessStartInfo()
            {
                CreateNoWindow = true,
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true

            };

            startInfo.FileName = ExecutablePath;
            startInfo.Arguments = ConvertToArgs(urls, options);
            
            process.EnableRaisingEvents = true;
            process.StartInfo = startInfo;

            var tcsOut = new TaskCompletionSource<bool>();
            process.OutputDataReceived += (o, e) =>
            {
                if (e.Data == null)
                {
                    tcsOut.SetResult(true);
                    return;
                }
                Match match;
                if ((match = rgxProgress.Match(e.Data)).Success)
                {
                    if (match.Groups.Count > 1 && match.Groups[1].Length > 0)
                    {
                        float progValue = float.Parse(match.Groups[1].ToString(), CultureInfo.InvariantCulture) / 100.0f;
                        Group totalGroup = match.Groups["total"];
                        string total = totalGroup.Success ? totalGroup.Value : null;
                        Group speedGroup = match.Groups["speed"];
                        string speed = speedGroup.Success ? speedGroup.Value : null;
                        Group etaGroup = match.Groups["eta"];
                        string eta = etaGroup.Success ? etaGroup.Value : null;
                        //Debug.WriteLine(e.Data);
                        progress?.Report(new DownloadProgress(DownloadState.Downloading, progress: progValue, totalDownloadSize: total, downloadSpeed: speed, eta: eta));
                    }
                    else
                    {
                        progress?.Report(new DownloadProgress(DownloadState.Downloading));
                    }
                }
                else if ((match = rgxPost.Match(e.Data)).Success)
                {
                    //progress?.Report(new DownloadProgress(DownloadState.PostProcessing, 1));
                }
                else if ((match = rgxPlaylist.Match(e.Data)).Success)
                {
                    var index = int.Parse(match.Groups[1].Value);
                    //progress?.Report(new DownloadProgress(DownloadState.PreProcessing, index: index));
                }
                else if ((match = rgxInfo.Match(e.Data)).Success)
                {
                    Group mess = match.Groups["mess"];
                    string all_mess = mess.Success ? mess.Value : null;
                    Debug.WriteLine(all_mess);
                }
                else if ((match = rgxYoutube.Match(e.Data)).Success)
                {
                    Group id = match.Groups["id"];
                    string all_id = id.Success ? id.Value : null;
                    Group mess = match.Groups["mess"];
                    string all_mess = mess.Success ? mess.Value : null;
                    Debug.WriteLine(all_id);
                    Debug.WriteLine(all_mess);
                }
                else if ((match = rgxMerger.Match(e.Data)).Success)
                {
                    Group mess = match.Groups["mess"];
                    string all_mess = mess.Success ? mess.Value : null;
                    Debug.WriteLine(all_mess);
                }
                else if ((match = rgxConverter.Match(e.Data)).Success)
                {
                    Group mess = match.Groups["mess"];
                    string all_mess = mess.Success ? mess.Value : null;
                    Debug.WriteLine(all_mess);
                }


                Debug.WriteLine("[youtube-dlp] : " + e.Data);
                OutputReceived?.Invoke(this, e);
            };
            var tcsError = new TaskCompletionSource<bool>();
            process.ErrorDataReceived += (o, e) =>
            {
                if (e.Data == null)
                {
                    tcsError.SetResult(true);
                    return;
                }
                Debug.WriteLine("[youtube-dlp ERROR] " + e.Data);
                progress?.Report(new DownloadProgress(DownloadState.Error, data: e.Data));
                ErrorReceived?.Invoke(this, e);
            };
            process.Exited += async (sender, args) =>
            {
                // Wait for output and error streams to finish
                await tcsOut.Task;
                await tcsError.Task;
                tcs.TrySetResult(process.ExitCode);
                process.Dispose();
            };
            ct.Register(() =>
            {
                if (!tcs.Task.IsCompleted)
                    tcs.TrySetCanceled();
                try { if (!process.HasExited) process.KillTree(); }
                catch { }
            });
            Debug.WriteLine("[youtube-dlp] Arguments: " + process.StartInfo.Arguments);
            if (!process.Start())
                tcs.TrySetException(new InvalidOperationException("Failed to start youtube-dlp process."));
            process.BeginOutputReadLine();
            process.BeginErrorReadLine();
            progress?.Report(new DownloadProgress(DownloadState.PreProcessing));
            return await tcs.Task;
        }
    }
}
