using Common.Dtos;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class Utility
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
    }
}
