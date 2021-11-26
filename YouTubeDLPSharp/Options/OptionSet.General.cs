﻿
namespace YouTubeDLPSharp.Options
{
    public partial class OptionSet
    {
        private Option<bool> help = new Option<bool>("-h", "--help");
        private Option<bool> version = new Option<bool>("--version");
        private Option<bool> update = new Option<bool>("-U", "--update");
        private Option<bool> ignoreErrors = new Option<bool>("-i", "--ignore-errors");
        private Option<bool> abortOnError = new Option<bool>("--abort-on-error");
        private Option<bool> noabortOnError = new Option<bool>("--no-abort-on-error");

        private Option<bool> dumpUserAgent = new Option<bool>("--dump-user-agent");
        private Option<bool> listExtractors = new Option<bool>("--list-extractors");
        private Option<bool> extractorDescriptions = new Option<bool>("--extractor-descriptions");
        private Option<bool> forceGenericExtractor = new Option<bool>("--force-generic-extractor");

        private Option<string> defaultSearch = new Option<string>("--default-search");

        private Option<bool> ignoreConfig = new Option<bool>("--ignore-config", "--no-config");

        private Option<string> configLocation = new Option<string>("--config-location");
        private Option<bool> flatPlaylist = new Option<bool>("--flat-playlist");
        private Option<bool> noflatPlaylist = new Option<bool>("-no--flat-playlist");

        private Option<bool> markWatched = new Option<bool>("--mark-watched");
        private Option<bool> noMarkWatched = new Option<bool>("--no-mark-watched");
        private Option<bool> noColor = new Option<bool>("--no-color");

        /// <summary>
        /// Print this help text and exit
        /// </summary>
        public bool Help { get => help.Value; set => help.Value = value; }
        /// <summary>
        /// Print program version and exit
        /// </summary>
        public bool Version { get => version.Value; set => version.Value = value; }
        /// <summary>
        /// Update this program to latest version.
        /// Make sure that you have sufficient
        /// permissions (run with sudo if needed)
        /// </summary>
        public bool Update { get => update.Value; set => update.Value = value; }
        /// <summary>
        /// Continue on download errors, for
        /// example to skip unavailable videos in a
        /// playlist
        /// </summary>
        public bool IgnoreErrors { get => ignoreErrors.Value; set => ignoreErrors.Value = value; }
        /// <summary>
        /// Abort downloading of further videos (in
        /// the playlist or the command line) if an
        /// error occurs
        /// </summary>
        public bool AbortOnError { get => abortOnError.Value; set => abortOnError.Value = value; }
        /// <summary>
        /// Resume downloading of further videos (in
        /// the playlist or the command line) if an
        /// error occurs
        /// </summary>
        public bool NoAbortOnError { get => noabortOnError.Value; set => noabortOnError.Value = value; }
        /// <summary>
        /// Display the current browser
        /// identification
        /// </summary>
        public bool DumpUserAgent { get => dumpUserAgent.Value; set => dumpUserAgent.Value = value; }
        /// <summary>
        /// List all supported extractors
        /// </summary>
        public bool ListExtractors { get => listExtractors.Value; set => listExtractors.Value = value; }
        /// <summary>
        /// Output descriptions of all supported
        /// extractors
        /// </summary>
        public bool ExtractorDescriptions { get => extractorDescriptions.Value; set => extractorDescriptions.Value = value; }
        /// <summary>
        /// Force extraction to use the generic
        /// extractor
        /// </summary>
        public bool ForceGenericExtractor { get => forceGenericExtractor.Value; set => forceGenericExtractor.Value = value; }
        /// <summary>
        /// Use this prefix for unqualified URLs.
        /// For example &quot;gvsearch2:&quot; downloads two
        /// videos from google videos for youtube-
        /// dl &quot;large apple&quot;. Use the value &quot;auto&quot;
        /// to let youtube-dl guess (&quot;auto_warning&quot;
        /// to emit a warning when guessing).
        /// &quot;error&quot; just throws an error. The
        /// default value &quot;fixup_error&quot; repairs
        /// broken URLs, but emits an error if this
        /// is not possible instead of searching.
        /// </summary>
        public string DefaultSearch { get => defaultSearch.Value; set => defaultSearch.Value = value; }
        /// <summary>
        /// Do not read configuration files. When
        /// given in the global configuration file
        /// /etc/youtube-dl.conf: Do not read the
        /// user configuration in ~/.config
        /// /youtube-dl/config (%APPDATA%/youtube-
        /// dl/config.txt on Windows)
        /// </summary>
        public bool IgnoreConfig { get => ignoreConfig.Value; set => ignoreConfig.Value = value; }
        /// <summary>
        /// Location of the configuration file;
        /// either the path to the config or its
        /// containing directory.
        /// </summary>
        public string ConfigLocation { get => configLocation.Value; set => configLocation.Value = value; }
        /// <summary>
        /// Do not extract the videos of a
        /// playlist, only list them.
        /// </summary>
        public bool FlatPlaylist { get => flatPlaylist.Value; set => flatPlaylist.Value = value; }
        /// <summary>
        /// Do extract the videos of a
        /// playlist, only list them.
        /// </summary>
        public bool NoFlatPlaylist { get => noflatPlaylist.Value; set => noflatPlaylist.Value = value; }
        /// <summary>
        /// Mark videos watched (YouTube only)
        /// </summary>
        public bool MarkWatched { get => markWatched.Value; set => markWatched.Value = value; }
        /// <summary>
        /// Do not mark videos watched (YouTube
        /// only)
        /// </summary>
        public bool NoMarkWatched { get => noMarkWatched.Value; set => noMarkWatched.Value = value; }
        /// <summary>
        /// Do not emit color codes in output
        /// </summary>
        public bool NoColor { get => noColor.Value; set => noColor.Value = value; }
    }

}
