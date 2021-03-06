﻿using AE.HabitatSystemsPanel.Buildable;
using AE.HabitatSystemsPanel.Configuration;
using FCSCommon.Utilities;
using Harmony;
using Oculus.Newtonsoft.Json;
using System;
using System.IO;
using System.Reflection;
using UnityEngine;

namespace AE.HabitatSystemsPanel
{
    public class QPatch
    {
        public static AssetBundle GlobalBundle { get; set; }

        public static void Patch()
        {
            Version = QuickLogger.GetAssemblyVersion(Assembly.GetExecutingAssembly());
            QuickLogger.Info($"Started patching. Version: {Version}");


#if DEBUG
            QuickLogger.DebugLogsEnabled = true;
            QuickLogger.Debug("Debug logs enabled");
#endif

            try
            {

                GlobalBundle = FCSTechFabricator.QPatch.Bundle;

                if (GlobalBundle == null)
                {
                    QuickLogger.Error("Global Bundle has returned null stopping patching");
                    throw new FileNotFoundException("Bundle failed to load");
                }

                LoadConfiguration();

                HSPBuildable.PatchSMLHelper();

                var harmony = HarmonyInstance.Create("com.seacooker.fcstudios");
                harmony.PatchAll(Assembly.GetExecutingAssembly());

                QuickLogger.Info("Finished patching");
            }
            catch (Exception ex)
            {
                QuickLogger.Error(ex);
            }
        }

        private static void LoadConfiguration()
        {
            // == Load Configuration == //
            string configJson = File.ReadAllText(Mod.ConfigurationFile().Trim());

            JsonSerializerSettings settings = new JsonSerializerSettings();
            settings.MissingMemberHandling = MissingMemberHandling.Ignore;

            //LoadData
            Configuration = JsonConvert.DeserializeObject<ModConfiguration>(configJson, settings);
        }

        public static ModConfiguration Configuration { get; set; }

        public static string Version { get; set; }
    }
}
