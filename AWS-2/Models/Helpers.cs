using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace AWS_2.Models
{
    public class Helpers
    {
        public static string GetRDSConnectionString()
        {
            var appConfig = ConfigurationManager.AppSettings;

            string dbname = appConfig["ebdb"];

            if (string.IsNullOrEmpty(dbname)) return null;

            string username = appConfig["root"];
            string password = appConfig["rootroot"];
            string hostname = appConfig["aa5zzsxwobqgca.ctw4zf9zzkge.eu-west-1.rds.amazonaws.com"];
            string port = appConfig["3306"];

            return "Data Source=" + hostname + ";Initial Catalog=" + dbname + ";User ID=" + username + ";Password=" + password + ";";
        }

    }
}