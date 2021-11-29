﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace YouTubeDLPSharp.Options
{
    public partial class OptionSet : ICloneable
    {
        /// <summary>
        /// The default option set (if no options are explicitly set).
        /// </summary>
        public static readonly OptionSet Default = new OptionSet();

        /// <summary>
        /// Writes all options to a config file with the specified path.
        /// </summary>
        public void WriteConfigFile(string path)
        {
            File.WriteAllLines(path, GetOptionFlags());
        }

        public override string ToString() => " " + String.Join(" ", GetOptionFlags());

        /// <summary>
        /// Returns an enumerable of all option flags.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<string> GetOptionFlags()
        {
            foreach (var opt in GetOptions())
            {
                var value = opt.ToString();
                if (!String.IsNullOrWhiteSpace(value))
                    yield return value;
            }
        }

        internal IEnumerable<IOption> GetOptions()
        {
            return this.GetType().GetRuntimeFields()
                .Where(p => p.FieldType.IsGenericType && p.FieldType.GetGenericTypeDefinition() == typeof(Option<>))
                .Select(p => p.GetValue(this)).Cast<IOption>();
        }

        /// <summary>
        /// Creates a clone of this option set and overrides all options with non-default values set in the given option set.
        /// </summary>
        /// <param name="overrideOptions">All non-default option values of this option set will be copied to the cloned option set.</param>
        /// <returns>A cloned option set with all specified options overriden.</returns>
        public OptionSet OverrideOptions(OptionSet overrideOptions)
        {
            OptionSet cloned = (OptionSet)this.Clone();
            var overrideFields = overrideOptions.GetType().GetRuntimeFields().Where(p => p.FieldType.IsGenericType && p.FieldType.GetGenericTypeDefinition() == typeof(Option<>));
            foreach (var field in overrideFields)
            {
                IOption fieldValue = (IOption)field.GetValue(overrideOptions);
                if (fieldValue.IsSet)
                {
                    cloned.GetType()
                        .GetField(field.Name, BindingFlags.NonPublic | BindingFlags.Instance)
                        .SetValue(cloned, fieldValue);
                }
            }
            return cloned;
        }

        /// <summary>
        /// Creates an option set from an array of command-line option strings.
        /// </summary>
        /// <param name="lines">An array containing one command-line option string per item.</param>
        /// <returns>The parsed OptionSet.</returns>
        public static OptionSet FromString(IEnumerable<string> lines)
        {
            OptionSet optSet = new OptionSet();
            var options = optSet.GetOptions();
            int i = 0;
            foreach (string rawLine in lines)
            {
                i++;
                string line = rawLine.Trim();
                // skip comments
                if (line.StartsWith("#") || String.IsNullOrWhiteSpace(line))
                    continue;
                string flag = line.Split(' ')[0];
                IOption option = options.Where(o => o.OptionStrings.Contains(flag)).FirstOrDefault();
                if (option != null)
                {
                    option.SetFromString(line);
                }
                else throw new FormatException($"Invalid option in line {i}: {line}");
            }
            return optSet;
        }

        /// <summary>
        /// Loads an option set from a youtube-dlp config file.
        /// </summary>
        /// <param name="path">The path to the config file.</param>
        /// <returns>The loaded OptionSet.</returns>
        public static OptionSet LoadConfigFile(string path)
        {
            return FromString(File.ReadAllLines(path));
        }

        public object Clone()
        {
            return OptionSet.FromString(this.GetOptionFlags());
        }
    }
}
