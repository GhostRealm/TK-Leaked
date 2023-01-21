using NLog;
using System;
using System.Collections.Generic;
using System.IO;

namespace common.resources
{
    public class Resources : IDisposable
    {
        public XmlData GameData;
        public string ResourcePath;
        public AppSettings Settings;
        public Dictionary<string, byte[]> WebFiles = new Dictionary<string, byte[]>();
        public WorldData Worlds;

        private static readonly Logger Log = LogManager.GetCurrentClassLogger();

        public Resources(string resourcePath, bool? wServer = false, Action<float, float, string, bool> progress = null)
        {
            if (wServer.HasValue) Log.Info("Loading resources...");

            ResourcePath = resourcePath;
            GameData = new XmlData(resourcePath + "/xml", !wServer.HasValue, progress);

            if (!wServer.HasValue) return;

            Settings = new AppSettings(resourcePath + "/data/init.xml");

            if (!wServer.Value) webFiles(resourcePath + "/web");
            else Worlds = new WorldData(resourcePath + "/worlds", GameData);
        }

        public IDictionary<string, byte[]> Languages { get; private set; }

        public void Dispose()
        {
            ResourcePath = null;
            GameData = null;
            WebFiles = null;
            Languages = null;
            Worlds = null;
            Settings = null;

            GC.SuppressFinalize(this);
        }

        private void webFiles(string dir)
        {
            Log.Info("Loading web data...");

            var files = Directory.EnumerateFiles(dir, "*", SearchOption.AllDirectories);
            foreach (var file in files)
            {
                var webPath = file.Substring(dir.Length, file.Length - dir.Length)
                    .Replace("\\", "/");

                WebFiles[webPath] = File.ReadAllBytes(file);
            }
        }
    }
}
