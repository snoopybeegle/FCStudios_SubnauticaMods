﻿using FCS_DeepDriller.Ores;
using FCSCommon.Utilities;
using Harmony;
using System;
using System.IO;
using System.Reflection;
using FCS_DeepDriller.Buildable.MK1;
using FCS_DeepDriller.Configuration;
using FCSTechFabricator;
using FCSTechFabricator.Components;
using FCSTechFabricator.Craftables;
using QModManager.API.ModLoading;
using SMLHelper.V2.Handlers;
using SMLHelper.V2.Utility;
using UnityEngine;


namespace FCS_DeepDriller
{
    [QModCore]
    public static class QPatch
    {
        public static DeepDrillerCfg Configuration { get; private set; }

        [QModPatch]
        public static void Patch()
        {
            var assembly = Assembly.GetExecutingAssembly();
            QuickLogger.Info("Started patching. Version: " + QuickLogger.GetAssemblyVersion(assembly));

#if DEBUG
            QuickLogger.DebugLogsEnabled = true;
            QuickLogger.Debug("Debug logs enabled");
#endif
#if USE_ExStorageDepot
            QuickLogger.Info("Ex-Storage Version");
#endif
            try
            {
                
                Configuration = Mod.LoadConfiguration();
                Configuration.Convert();

                OptionsPanelHandler.RegisterModOptions(new Options());
                
                AddItemsToTechFabricator();

                FCSDeepDrillerBuildable.PatchHelper();

                SandSpawnable.PatchHelper();

                var harmony = HarmonyInstance.Create("com.fcsdeepdriller.fcstudios");
                
                harmony.PatchAll(Assembly.GetExecutingAssembly());

                QuickLogger.Info("Finished patching");
            }
            catch (Exception ex)
            {
                QuickLogger.Error(ex);
            }
        }

        private static void AddItemsToTechFabricator()
        {
            var icon = ImageUtils.LoadSpriteFromFile(Path.Combine(Mod.GetAssetFolder(), "FCSDeepDriller.png"));
            var craftingTab = new CraftingTab(Mod.DeepDrillerTabID, Mod.ModFriendlyName, icon);
            
            var deepDrillerKit = new FCSKit(Mod.DeepDrillerKitClassID, Mod.DeepDrillerKitFriendlyName, craftingTab, Mod.DeepDrillerKitIngredients);
            deepDrillerKit.Patch(FcTechFabricatorService.PublicAPI, FcAssetBundlesService.PublicAPI);

            var assetsFolder = Mod.GetAssetFolder();

            var focusAttachmentKit = new FCSKit(Mod.FocusAttachmentKitClassID, Mod.FocusAttachmentFriendlyName, craftingTab, Mod.FocusAttachmentKitIngredients);
            focusAttachmentKit.ChangeIconLocation(assetsFolder, "FocusAttachment_DD");
            focusAttachmentKit.Patch(FcTechFabricatorService.PublicAPI, FcAssetBundlesService.PublicAPI);
            
            var batteryAttachmentKit = new FCSKit(Mod.BatteryAttachmentKitClassID, Mod.BatteryAttachmentFriendlyName, craftingTab, Mod.BatteryAttachmentKitIngredients);
            batteryAttachmentKit.ChangeIconLocation(assetsFolder, "BatteryAttachment_DD");
            batteryAttachmentKit.Patch(FcTechFabricatorService.PublicAPI, FcAssetBundlesService.PublicAPI);

            var solarAttachmentKit = new FCSKit(Mod.SolarAttachmentKitClassID, Mod.SolarAttachmentFriendlyName, craftingTab, Mod.SolarAttachmentKitIngredients);
            solarAttachmentKit.ChangeIconLocation(assetsFolder, "SolarAttachment_DD");
            solarAttachmentKit.Patch(FcTechFabricatorService.PublicAPI, FcAssetBundlesService.PublicAPI);

            var drillerMK1Module = new FCSModule(Mod.DrillerMK1ModuleClassID, Mod.DrillerMK1ModuleFriendlyName, Mod.MK1Description, craftingTab, Mod.DrillerMK1Ingredients);
            drillerMK1Module.ChangeIconLocation(assetsFolder, Mod.DrillerMK1ModuleClassID);
            drillerMK1Module.Patch(FcTechFabricatorService.PublicAPI, FcAssetBundlesService.PublicAPI);

            var drillerMK2Module = new FCSModule(Mod.DrillerMK2ModuleClassID, Mod.DrillerMK2ModuleFriendlyName, Mod.MK2Description, craftingTab, Mod.DrillerMK2Ingredients);
            drillerMK2Module.ChangeIconLocation(assetsFolder, Mod.DrillerMK2ModuleClassID);
            drillerMK2Module.Patch(FcTechFabricatorService.PublicAPI, FcAssetBundlesService.PublicAPI);

            var drillerMK3Module = new FCSModule(Mod.DrillerMK3ModuleClassID, Mod.DrillerMK3ModuleFriendlyName, Mod.MK3Description, craftingTab, Mod.DrillerMK3Ingredients);
            drillerMK3Module.ChangeIconLocation(assetsFolder, Mod.DrillerMK3ModuleClassID);
            drillerMK3Module.Patch(FcTechFabricatorService.PublicAPI, FcAssetBundlesService.PublicAPI);
        }
    }
}
