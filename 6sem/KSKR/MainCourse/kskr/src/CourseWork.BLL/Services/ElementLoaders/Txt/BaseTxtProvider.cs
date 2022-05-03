using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace CourseWork.BLL.Services.ElementLoaders.Txt
{
    public abstract class BaseTxtProvider
    {
        /// <summary>
        /// Loads values from txt file with ability to skip first info row.
        /// </summary>
        /// <param name="path">File path.</param>
        /// <param name="skipRows">Rows to skip.</param>
        /// <returns>List of rows values.</returns>
        protected List<string[]> GetValuesFromTextFile(string path, string separator = "\t", int skipRows = 1)
        {
            using var reader = new StreamReader(path);
            var text = reader.ReadToEnd();
            var currentSeparator = CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;
            var regex = new Regex(@"\.|,");
            text = regex.Replace(text, currentSeparator);
            text = text.Replace("\r\n", "\n");
            var rows = text.Split("\n").Skip(skipRows).Where(row => !string.IsNullOrEmpty(row));
            return rows.Select(r => r.Split(separator, StringSplitOptions.RemoveEmptyEntries)).ToList();
        }
    }
}
