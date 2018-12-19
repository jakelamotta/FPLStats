using Common.Dtos;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public static class Utility
    {
        private static List<SettingDto> AllSettings;

        public static T GetApplicationSetting<T>(string key, bool throwIfMissingOrEmpty = true)
        {
            string value = AllSettings.FirstOrDefault(allS => allS.Key.Equals(key))?.Value;

            if (throwIfMissingOrEmpty)
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ConfigurationErrorsException(key);
                }
            }

            T result = (T)Convert.ChangeType(value, typeof(T));

            return result;
        }

        public static void SetAllSettings(List<SettingDto> settings)
        {
            AllSettings = settings;
        }

        public static List<String> GetFileData(string filePath)
        {            
            return System.IO.File.ReadLines(filePath).ToList();
        }


        public static List<string> CsvRowToList(string row)
        {
            var list = row.Split(new char[] { ',' }).ToList();

            return list;
        }

        public static string StripLineBreaks(this string value)
        {
            var array = value.Split(new string[] { "\r\n"},StringSplitOptions.None);

            return array[0];
        }

        public static string StripXValues(this string value)
        {
            var toReturn = "";
            var sequence =  "+";

            if (value.Contains("-"))
            {
                sequence = "-";
            }

            var strList = value.Split(new string[] { sequence }, StringSplitOptions.None);

            toReturn = strList[0];

            return toReturn;
        }

        public static LinearRegressionDto GetLinearRegressionModel(IEnumerable<double> xValues, IEnumerable<double> yValues) 
        {
            if (xValues.Count() != yValues.Count())
            {
                throw new Exception();
            }

            var result = new LinearRegressionDto();
            var dataPoints = xValues.Count();

            var y = 0.0;
            var x = 0.0;
            var xy = 0.0;
            var x2 = 0.0;
            var y2 = 0.0;

            for (var i = 0; i < xValues.Count(); i++)
            {
                var curX = xValues.ElementAt(i);
                var curY = yValues.ElementAt(i);
                x += curX;
                y += curY;

                xy += curX * curY;
                x2 += curX * curX;
                y2 += curY * curY;
            }

            result.Alpha = (y * x2 - x * xy) / (dataPoints * x2 - (x * x));
            result.Beta = (dataPoints * xy - x * y) / (dataPoints*x2 -(x*x));

            return result;
        }
    }
}
