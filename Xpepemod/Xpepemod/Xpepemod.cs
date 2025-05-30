using System;
using System.Collections.Generic;
using System.IO;
using System.Net;

namespace Xpepemod
{
    public static class Xpepemod
    {
        public static ThemeAPI Themes { get; private set; } = new ThemeAPI();
        public static EventAPI Events { get; private set; } = new EventAPI();
        public static LogAPI Log { get; private set; } = new LogAPI();

        private static readonly string _currentVersion = "1.0.0";
        private static readonly string _versionUrl = "https://raw.githubusercontent.com/Fadi002/xworm-3.1-modded-by-mrpepe/refs/heads/main/Xpepemod/version";
        public static void CheckModVersion()
        {
            try
            {
                using (WebClient wc = new WebClient())
                {
                    string onlineVersion = wc.DownloadString(_versionUrl).Trim();

                    if (_currentVersion != onlineVersion)
                    {
                        Xpepemod.Log.Warning($"New mod version available: {onlineVersion} (Current: {_currentVersion})");
                    }
                    else
                    {
                        Xpepemod.Log.Info("Mod is up-to-date.");
                    }
                }
            }
            catch (Exception ex)
            {
                Xpepemod.Log.Error($"Version check failed: {ex.Message}");
            }
        }
        public static void Init()
        {
            // Configure logging
            Log.MinimumLevel = LogLevel.Trace; // Show all log levels
            Log.UseColors = true;              // Enable colored output
            Log.ShowTimestamp = true;          // Show timestamps in logs
            Log.debug = true;                  // Show debugging messages


            var config = new Dictionary<string, string>();
            if (!File.Exists("xpepemod.ini"))
            {
                File.WriteAllText("xpepemod.ini", "[General]\r\nEnabled = true\r\nDebug = true\r\nShowTimestamp = true\r\nUseColors = true\r\nCheckHashes = true\r\nCheckUpdates = true");
            }
            foreach (var line in File.ReadAllLines("xpepemod.ini"))
            {
                if (!string.IsNullOrWhiteSpace(line) && !line.StartsWith("[") && line.Contains("="))
                {
                    var parts = line.Split('=');
                    config[parts[0].Trim()] = parts[1].Trim();
                }
            }

            if (config["Enabled"].ToLower() != "true")
            {
                return;
            }

            if (config["Debug"].ToLower() != "true") 
            {
                Log.debug = false;
            }

            if (config["ShowTimestamp"].ToLower() != "true")
            {
                Log.ShowTimestamp = false;
            }

            if (config["UseColors"].ToLower() != "true")
            {
                Log.UseColors = false;
            }

            if (config["CheckUpdates"].ToLower()  == "true")
            {
                CheckModVersion();
            }


            // Demonstrate different log levels
            Log.Trace("Starting initialization process");
            Log.Info("Xpepemod initialized");

            Log.Debug("Initializing theme manager");
            FormThemeManager.Init();
            Log.Debug("Theme manager initialized");

            Log.Info("Xpepemod Ready");

            Log.Debug("Loading mods");
            ModLoader.LoadMods(bool.Parse(config["CheckHashes"]));
            Log.Debug("Mods loaded");
        }
    }
}
