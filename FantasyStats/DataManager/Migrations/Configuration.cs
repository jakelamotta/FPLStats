namespace DataManager.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<DataManager.Models.FPLStatsContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(DataManager.Models.FPLStatsContext context)
        {
            context.Settings.Add(new Models.Setting
            {
                Key = "PathToDataFiles",
                Value = "D:\\repos\\FPLStats\\FPLStats\\FantasyStats\\Data\\filedata"
            });

            context.Settings.Add(new Models.Setting
            {
                Key = "PlayerScript",
                Value = "understats2.py"
            });

            context.Settings.Add(new Models.Setting
            {
                Key = "TeamScript",
                Value = "usteam.py"
            });

            context.Settings.Add(new Models.Setting
            {
                Key = "PathTopythonExe",
                Value = "C:\\Python27\\python.exe"
            });

            context.Settings.Add(new Models.Setting
            {
                Key = "WorkingDirectory",
                Value = "D:\\repos\\FPLStats\\FPLStats\\FantasyStats\\Data\\Scraping"
            });

            context.Settings.Add(new Models.Setting
            {
                Key = "PrevSeasonWeight",
                Value = "0.5"
            });

            context.Settings.Add(new Models.Setting
            {
                Key = "PrevSeasonDiffTeamWeight",
                Value = "0.2"
            });

            context.Settings.Add(new Models.Setting
            {
                Key = "PointsGoalsDefender",
                Value = "6"
            });

            context.Settings.Add(new Models.Setting
            {
                Key = "PointsCleanSheetDefender",
                Value = "4"
            });

            context.Settings.Add(new Models.Setting
            {
                Key = "PointsGoalsMidfielder",
                Value = "5"
            });

            context.Settings.Add(new Models.Setting
            {
                Key = "PointsCleanSheetGoalKeeper",
                Value = "4"
            });

            context.Settings.Add(new Models.Setting
            {
                Key = "PointsGoalsGoalkeeper",
                Value = "6"
            });

            context.Settings.Add(new Models.Setting
            {
                Key = "PointsCleanSheetMidfielder",
                Value = "1"
            });

            context.Settings.Add(new Models.Setting
            {
                Key = "PointsGoalsForward",
                Value = "4"
            });

            context.Settings.Add(new Models.Setting
            {
                Key = "PointsCleanSheetForward",
                Value = "0"
            });

            context.SaveChanges();
        }
    }
}