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
        //public static string GetApplicationSetting(string key, bool throwIfMissingOrEmpty = true)
        //{
        //    string value = System.Configuration.ConfigurationManager.AppSettings[key];
            
        //    if (throwIfMissingOrEmpty)
        //    {
        //        if (string.IsNullOrEmpty(value))
        //        {
        //            throw new ConfigurationErrorsException(key);
        //        }
        //    }

        //    return value;
        //}

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
