﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YouTubeDLPSharp.Options
{
    public partial class OptionSet
    {
        private Option<bool> quiet = new Option<bool>("-q", "--quiet");
        private Option<bool> noWarnings = new Option<bool>("--no-warnings");
        private Option<bool> simulate = new Option<bool>("-s", "--simulate");
        private Option<bool> skipDownload = new Option<bool>("--skip-download");

        private Option<bool> dumpJson = new Option<bool>("-j", "--dump-json");
        private Option<bool> dumpSingleJson = new Option<bool>("-J", "--dump-single-json");

        private Option<bool> newline = new Option<bool>("--newline");
        private Option<bool> noProgress = new Option<bool>("--no-progress");
        private Option<bool> consoleTitle = new Option<bool>("--console-title");
        private Option<bool> verbose = new Option<bool>("-v", "--verbose");
        private Option<bool> dumpPages = new Option<bool>("--dump-pages");
        private Option<bool> writePages = new Option<bool>("--write-pages");
        private Option<bool> printTraffic = new Option<bool>("--print-traffic");

        /// <summary>
        /// Activate quiet mode
        /// </summary>
        public bool Quiet { get => quiet.Value; set => quiet.Value = value; }
        /// <summary>
        /// Ignore warnings
        /// </summary>
        public bool NoWarnings { get => noWarnings.Value; set => noWarnings.Value = value; }
        /// <summary>
        /// Do not download the video and do not
        /// write anything to disk
        /// </summary>
        public bool Simulate { get => simulate.Value; set => simulate.Value = value; }
        /// <summary>
        /// Do not download the video
        /// </summary>
        public bool SkipDownload { get => skipDownload.Value; set => skipDownload.Value = value; }
        /// <summary>
        /// Simulate, quiet but print JSON
        /// information. See the &quot;OUTPUT TEMPLATE&quot;
        /// for a description of available keys.
        /// </summary>
        public bool DumpJson { get => dumpJson.Value; set => dumpJson.Value = value; }
        /// <summary>
        /// Simulate, quiet but print JSON
        /// information for each command-line
        /// argument. If the URL refers to a
        /// playlist, dump the whole playlist
        /// information in a single line.
        /// </summary>
        public bool DumpSingleJson { get => dumpSingleJson.Value; set => dumpSingleJson.Value = value; }
        /// <summary>
        /// Output progress bar as new lines
        /// </summary>
        public bool Newline { get => newline.Value; set => newline.Value = value; }
        /// <summary>
        /// Do not print progress bar
        /// </summary>
        public bool NoProgress { get => noProgress.Value; set => noProgress.Value = value; }
        /// <summary>
        /// Display progress in console titlebar
        /// </summary>
        public bool ConsoleTitle { get => consoleTitle.Value; set => consoleTitle.Value = value; }
        /// <summary>
        /// Print various debugging information
        /// </summary>
        public bool Verbose { get => verbose.Value; set => verbose.Value = value; }
        /// <summary>
        /// Print downloaded pages encoded using
        /// base64 to debug problems (very verbose)
        /// </summary>
        public bool DumpPages { get => dumpPages.Value; set => dumpPages.Value = value; }
        /// <summary>
        /// Write downloaded intermediary pages to
        /// files in the current directory to debug
        /// problems
        /// </summary>
        public bool WritePages { get => writePages.Value; set => writePages.Value = value; }
        /// <summary>
        /// Display sent and read HTTP traffic
        /// </summary>
        public bool PrintTraffic { get => printTraffic.Value; set => printTraffic.Value = value; }

    }

}
