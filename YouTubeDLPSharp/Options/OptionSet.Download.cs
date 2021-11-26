
namespace YouTubeDLPSharp.Options
{
    public partial class OptionSet
    {
        private Option<int?> currentFragments = new Option<int?>("-N", "--concurrent-fragments");
        private Option<long?> limitRate = new Option<long?>("-r", "--limit-rate");
        private Option<long?> throttledRate = new Option<long?>("--throttled-rate");
        private Option<int?> retries = new Option<int?>("-R", "--retries");
        private Option<int?> fragmentRetries = new Option<int?>("--fragment-retries");
        private Option<bool> skipUnavailableFragments = new Option<bool>("--skip-unavailable-fragments");
        private Option<bool> abortOnUnavailableFragment = new Option<bool>("--abort-on-unavailable-fragment");
        private Option<bool> keepFragments = new Option<bool>("--keep-fragments");
        private Option<long?> bufferSize = new Option<long?>("--buffer-size");
        private Option<bool> noResizeBuffer = new Option<bool>("--no-resize-buffer");
        private Option<long?> httpChunkSize = new Option<long?>("--http-chunk-size");
        private Option<bool> playlistReverse = new Option<bool>("--playlist-reverse");
        private Option<bool> playlistRandom = new Option<bool>("--playlist-random");
        private Option<bool> xattrSetFilesize = new Option<bool>("--xattr-set-filesize");
        private Option<bool> hlsUseMpegts = new Option<bool>("--hls-use-mpegts");

        /// <summary>
        /// Number of fragments of a dash/hlsnative
        /// video that should be download concurrently
        /// (default is 1)
        /// </summary>
        public int? CurrentFragments { get => currentFragments.Value; set => currentFragments.Value = value; }
        /// <summary>
        /// Maximum download rate in bytes per
        /// second (e.g. 50K or 4.2M)
        /// </summary>
        public long? LimitRate { get => limitRate.Value; set => limitRate.Value = value; }
        /// <summary>
        /// Minimum download rate in bytes per second
        /// below which throttling is assumed and the
        /// video data is re-extracted(e.g. 100K)
        /// </summary>
        public long? ThrottledRate { get => throttledRate.Value; set => throttledRate.Value = value; }
        /// <summary>
        /// Number of retries (default is 10), or
        /// &quot;infinite&quot;.
        /// </summary>
        public int? Retries { get => retries.Value; set => retries.Value = value; }
        /// <summary>
        /// Number of retries for a fragment
        /// (default is 10), or &quot;infinite&quot; (DASH,
        /// hlsnative and ISM)
        /// </summary>
        public int? FragmentRetries { get => fragmentRetries.Value; set => fragmentRetries.Value = value; }
        /// <summary>
        /// Skip unavailable fragments (DASH,
        /// hlsnative and ISM)
        /// </summary>
        public bool SkipUnavailableFragments { get => skipUnavailableFragments.Value; set => skipUnavailableFragments.Value = value; }
        /// <summary>
        /// Abort downloading when some fragment is
        /// not available
        /// </summary>
        public bool AbortOnUnavailableFragment { get => abortOnUnavailableFragment.Value; set => abortOnUnavailableFragment.Value = value; }
        /// <summary>
        /// Keep downloaded fragments on disk after
        /// downloading is finished; fragments are
        /// erased by default
        /// </summary>
        public bool KeepFragments { get => keepFragments.Value; set => keepFragments.Value = value; }
        /// <summary>
        /// Size of download buffer (e.g. 1024 or
        /// 16K) (default is 1024)
        /// </summary>
        public long? BufferSize { get => bufferSize.Value; set => bufferSize.Value = value; }
        /// <summary>
        /// Do not automatically adjust the buffer
        /// size. By default, the buffer size is
        /// automatically resized from an initial
        /// value of SIZE.
        /// </summary>
        public bool NoResizeBuffer { get => noResizeBuffer.Value; set => noResizeBuffer.Value = value; }
        /// <summary>
        /// Size of a chunk for chunk-based HTTP
        /// downloading (e.g. 10485760 or 10M)
        /// (default is disabled). May be useful
        /// for bypassing bandwidth throttling
        /// imposed by a webserver (experimental)
        /// </summary>
        public long? HttpChunkSize { get => httpChunkSize.Value; set => httpChunkSize.Value = value; }
        /// <summary>
        /// Download playlist videos in reverse
        /// order
        /// </summary>
        public bool PlaylistReverse { get => playlistReverse.Value; set => playlistReverse.Value = value; }
        /// <summary>
        /// Download playlist videos in random
        /// order
        /// </summary>
        public bool PlaylistRandom { get => playlistRandom.Value; set => playlistRandom.Value = value; }
        /// <summary>
        /// Set file xattribute ytdl.filesize with
        /// expected file size
        /// </summary>
        public bool XattrSetFilesize { get => xattrSetFilesize.Value; set => xattrSetFilesize.Value = value; }
        /// <summary>
        /// Use the mpegts container for HLS
        /// videos, allowing to play the video
        /// while downloading (some players may not
        /// be able to play it)
        /// </summary>
        public bool HlsUseMpegts { get => hlsUseMpegts.Value; set => hlsUseMpegts.Value = value; }
    }

}
