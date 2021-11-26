
namespace YouTubeDLPSharp.Options
{
    public partial class OptionSet
    {
        private Option<string> batchFile = new Option<string>("-a", "--batch-file");

        private Option<string> paths = new Option<string>("-P", "--paths");

        private Option<string> output = new Option<string>("-o", "--output");
        private Option<string> outputNaPlaceholder = new Option<string>("--output-na-placeholder");
        private Option<bool> restrictFilenames = new Option<bool>("--restrict-filenames");

        private Option<bool> windowsFilenames = new Option<bool>("--windows-filenames");
        private Option<int?> trimFilenames = new Option<int?>("--trim-filenames");

        private Option<bool> noOverwrites = new Option<bool>("-w", "--no-overwrites");

        private Option<bool> forceOverwrites = new Option<bool>("--force-overwrites");

        private Option<bool> doContinue = new Option<bool>("-c", "--continue");
        private Option<bool> noContinue = new Option<bool>("--no-continue");
        private Option<bool> noPart = new Option<bool>("--no-part");
        private Option<bool> noMtime = new Option<bool>("--no-mtime");
        private Option<bool> writeDescription = new Option<bool>("--write-description");
        private Option<bool> writeInfoJson = new Option<bool>("--write-info-json");
        private Option<bool> writeAnnotations = new Option<bool>("--write-annotations");
        private Option<string> loadInfoJson = new Option<string>("--load-info-json");
        private Option<string> cookies = new Option<string>("--cookies");
        private Option<string> cacheDir = new Option<string>("--cache-dir");
        private Option<bool> noCacheDir = new Option<bool>("--no-cache-dir");
        private Option<bool> rmCacheDir = new Option<bool>("--rm-cache-dir");

        /// <summary>
        /// File containing URLs to download (&#x27;-&#x27;
        /// for stdin), one URL per line. Lines
        /// starting with &#x27;#&#x27;, &#x27;;&#x27; or &#x27;]&#x27; are
        /// considered as comments and ignored.
        /// </summary>
        public string BatchFile { get => batchFile.Value; set => batchFile.Value = value; }
        /// <summary>
        /// File containing URLs to download (&#x27;-&#x27;
        /// for stdin), one URL per line. Lines
        /// starting with &#x27;#&#x27;, &#x27;;&#x27; or &#x27;]&#x27; are
        /// considered as comments and ignored.
        /// </summary>
        public string Paths { get => paths.Value; set => paths.Value = value; }
        /// <summary>
        /// Output filename template, see the
        /// &quot;OUTPUT TEMPLATE&quot; for all the info
        /// </summary>
        public string Output { get => output.Value; set => output.Value = value; }
        /// <summary>
        /// ER  Placeholder value for unavailable meta
        /// fields in output filename template
        /// (default is &quot;NA&quot;)
        /// </summary>
        public string OutputNaPlaceholder { get => outputNaPlaceholder.Value; set => outputNaPlaceholder.Value = value; }
        /// <summary>
        /// Restrict filenames to only ASCII
        /// characters, and avoid &quot;&amp;&quot; and spaces in
        /// filenames
        /// </summary>
        public bool RestrictFilenames { get => restrictFilenames.Value; set => restrictFilenames.Value = value; }
        /// <summary>
        /// Do not overwrite files
        /// </summary>
        public bool NoOverwrites { get => noOverwrites.Value; set => noOverwrites.Value = value; }
        /// <summary>
        /// Force resume of partially downloaded
        /// files. By default, youtube-dl will
        /// resume downloads if possible.
        /// </summary>
        public bool Continue { get => doContinue.Value; set => doContinue.Value = value; }
        /// <summary>
        /// Do not resume partially downloaded
        /// files (restart from beginning)
        /// </summary>
        public bool NoContinue { get => noContinue.Value; set => noContinue.Value = value; }
        /// <summary>
        /// Do not use .part files - write directly
        /// into output file
        /// </summary>
        public bool NoPart { get => noPart.Value; set => noPart.Value = value; }
        /// <summary>
        /// Do not use the Last-modified header to
        /// set the file modification time
        /// </summary>
        public bool NoMtime { get => noMtime.Value; set => noMtime.Value = value; }
        /// <summary>
        /// Write video description to a
        /// .description file
        /// </summary>
        public bool WriteDescription { get => writeDescription.Value; set => writeDescription.Value = value; }
        /// <summary>
        /// Write video metadata to a .info.json
        /// file
        /// </summary>
        public bool WriteInfoJson { get => writeInfoJson.Value; set => writeInfoJson.Value = value; }
        /// <summary>
        /// Write video annotations to a
        /// .annotations.xml file
        /// </summary>
        public bool WriteAnnotations { get => writeAnnotations.Value; set => writeAnnotations.Value = value; }
        /// <summary>
        /// JSON file containing the video
        /// information (created with the &quot;--write-
        /// info-json&quot; option)
        /// </summary>
        public string LoadInfoJson { get => loadInfoJson.Value; set => loadInfoJson.Value = value; }
        /// <summary>
        /// File to read cookies from and dump
        /// cookie jar in
        /// </summary>
        public string Cookies { get => cookies.Value; set => cookies.Value = value; }
        /// <summary>
        /// Location in the filesystem where
        /// youtube-dl can store some downloaded
        /// information permanently. By default
        /// $XDG_CACHE_HOME/youtube-dl or ~/.cache
        /// /youtube-dl . At the moment, only
        /// YouTube player files (for videos with
        /// obfuscated signatures) are cached, but
        /// that may change.
        /// </summary>
        public string CacheDir { get => cacheDir.Value; set => cacheDir.Value = value; }
        /// <summary>
        /// Disable filesystem caching
        /// </summary>
        public bool NoCacheDir { get => noCacheDir.Value; set => noCacheDir.Value = value; }
        /// <summary>
        /// Delete all filesystem cache files
        /// </summary>
        public bool RemoveCacheDir { get => rmCacheDir.Value; set => rmCacheDir.Value = value; }
    }

}
