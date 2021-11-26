﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YouTubeDLPSharp.Options
{
    public partial class OptionSet
    {
        private Option<string> format = new Option<string>("-f", "--format");

        private Option<bool> allFormats = new Option<bool>("--all-formats");
        private Option<bool> preferFreeFormats = new Option<bool>("--prefer-free-formats");

        private Option<bool> listFormats = new Option<bool>("-F", "--list-formats");

        private Option<bool> youtubeSkipDashManifest = new Option<bool>("--youtube-skip-dash-manifest");

        private Option<DownloadMergeFormat> mergeOutputFormat = new Option<DownloadMergeFormat>("--merge-output-format");

        /// <summary>
        /// Video format code, see the &quot;FORMAT
        /// SELECTION&quot; for all the info
        /// </summary>
        public string Format { get => format.Value; set => format.Value = value; }
        /// <summary>
        /// Download all available video formats
        /// </summary>
        public bool AllFormats { get => allFormats.Value; set => allFormats.Value = value; }
        /// <summary>
        /// Prefer free video formats unless a
        /// specific one is requested
        /// </summary>
        public bool PreferFreeFormats { get => preferFreeFormats.Value; set => preferFreeFormats.Value = value; }
        /// <summary>
        /// List all available formats of requested
        /// videos
        /// </summary>
        public bool ListFormats { get => listFormats.Value; set => listFormats.Value = value; }
        /// <summary>
        /// Do not download the DASH manifests and
        /// related data on YouTube videos
        /// </summary>
        public bool YoutubeSkipDashManifest { get => youtubeSkipDashManifest.Value; set => youtubeSkipDashManifest.Value = value; }
        /// <summary>
        /// If a merge is required (e.g.
        /// bestvideo+bestaudio), output to given
        /// container format. One of mkv, mp4, ogg,
        /// webm, flv. Ignored if no merge is
        /// required
        /// </summary>
        public DownloadMergeFormat MergeOutputFormat { get => mergeOutputFormat.Value; set => mergeOutputFormat.Value = value; }
    }

}
