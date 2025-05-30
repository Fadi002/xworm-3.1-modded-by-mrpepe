using System;
using System.Diagnostics;

namespace Xpepemod
{
    public enum LogLevel
    {
        Trace = 0,
        Debug = 1,
        Info = 2,
        Warning = 3,
        Error = 4,
        Critical = 5,
        None = 6
    }

    public class LogAPI
    {
        private LogLevel _minLevel = LogLevel.Info;
        private bool _useColors = true;
        private bool _showTimestamp = false;
        private bool _debug = false;

        public LogLevel MinimumLevel
        {
            get => _minLevel;
            set => _minLevel = value;
        }

        public bool debug
        {
            get => _debug;
            set => _debug = value;
        }

        public bool UseColors
        {
            get => _useColors;
            set => _useColors = value;
        }

        public bool ShowTimestamp
        {
            get => _showTimestamp;
            set => _showTimestamp = value;
        }

        public void Trace(string message) => Log(LogLevel.Trace, message);
        public void Debug(string message) => Log(LogLevel.Debug, message);
        public void Info(string message) => Log(LogLevel.Info, message);
        public void Warning(string message) => Log(LogLevel.Warning, message);
        public void Error(string message) => Log(LogLevel.Error, message);
        public void Critical(string message) => Log(LogLevel.Critical, message);

        private void Log(LogLevel level, string message)
        {
            if (!_debug && level == LogLevel.Debug)
            {
                return;
            }


            if (level < _minLevel)
                return;

            ConsoleColor originalColor = Console.ForegroundColor;

            try
            {
                if (_useColors)
                    Console.ForegroundColor = GetColorForLevel(level);

                string timestamp = _showTimestamp ? $"{DateTime.Now:HH:mm:ss.fff} " : "";
                string levelStr = $"[{level.ToString().ToUpper()}] ";

                Console.WriteLine($"{timestamp}[Xpepemod]{levelStr}{message}");
            }
            finally
            {
                if (_useColors)
                    Console.ForegroundColor = originalColor;
            }
        }

        private ConsoleColor GetColorForLevel(LogLevel level)
        {
            switch (level)
            {
                case LogLevel.Trace:
                    return ConsoleColor.DarkGray;
                case LogLevel.Debug:
                    return ConsoleColor.Gray;
                case LogLevel.Info:
                    return ConsoleColor.White;
                case LogLevel.Warning:
                    return ConsoleColor.Yellow;
                case LogLevel.Error:
                    return ConsoleColor.Red;
                case LogLevel.Critical:
                    return ConsoleColor.DarkRed;
                default:
                    return ConsoleColor.White;
            }
        }
    }
}
