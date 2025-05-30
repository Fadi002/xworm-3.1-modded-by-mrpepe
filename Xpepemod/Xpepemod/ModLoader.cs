using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Xpepemod
{
    public static class ModLoader
    {
        private static HashSet<string> _hashList = null;

        private static bool HasInternet()
        {
            try
            {
                using (var ping = new Ping())
                {
                    var reply = ping.Send("8.8.8.8", 1000);
                    return reply.Status == IPStatus.Success;
                }
            }
            catch
            {
                return false;
            }
        }

        public static bool LoadHashList(string url)
        {
            if (_hashList != null)
                return true;

            if (!HasInternet())
            {
                Xpepemod.Log.Error("No internet connection.");
                return false;
            }

            try
            {
                using (WebClient wc = new WebClient())
                {
                    string json = wc.DownloadString(url);
                    List<string> hashes = JsonConvert.DeserializeObject<List<string>>(json);
                    _hashList = new HashSet<string>(hashes, StringComparer.OrdinalIgnoreCase);
                    Xpepemod.Log.Info("Loaded hash list from server.");
                    return true;
                }
            }
            catch (Exception ex)
            {
                Xpepemod.Log.Error("Failed to download hash list: " + ex.Message);
                return false;
            }
        }
        private static string ComputeSHA256(string filePath)
        {
            using (var sha256 = SHA256.Create())
            using (var stream = File.OpenRead(filePath))
            {
                byte[] hash = sha256.ComputeHash(stream);
                return BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
            }
        }
        private static bool Checkhash(string filePath)
        {
            if (!LoadHashList("https://raw.githubusercontent.com/Fadi002/xworm-3.1-mods-repo/refs/heads/main/hashes.json"))
                return false;

            string fileHash = ComputeSHA256(filePath);
            return _hashList.Contains(fileHash);
        }

        public static void LoadMods(bool checkhashes)
        {
            string modsPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Mods");

            if (!Directory.Exists(modsPath))
                Directory.CreateDirectory(modsPath);

            foreach (var dll in Directory.GetFiles(modsPath, "*.dll"))
            {
                if (!File.Exists(dll))
                {
                    Xpepemod.Log.Error($"File doesn't exists {dll}");
                    continue;
                }

                if (checkhashes)
                {
                    if (!Checkhash(dll))
                    {
                        Xpepemod.Log.Critical($"Hash didn't match for {dll}");
                    }
                }
                try
                {
                    var assembly = Assembly.LoadFrom(dll);

                    foreach (var type in assembly.GetTypes())
                    {
                        if (typeof(IMod).IsAssignableFrom(type) && !type.IsInterface && !type.IsAbstract)
                        {
                            IMod mod = (IMod)Activator.CreateInstance(type);
                            mod.OnLoad();
                        }
                    }
                }
                catch (Exception ex)
                {
                    Xpepemod.Log.Error($"ModLoader failed to load mod '{dll}': {ex.Message}");
                }
            }
        }
    }
}
