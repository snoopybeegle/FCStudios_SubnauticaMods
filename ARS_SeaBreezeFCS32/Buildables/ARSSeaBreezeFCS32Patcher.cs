﻿using ARS_SeaBreezeFCS32.Configuration;
using ARS_SeaBreezeFCS32.Mono;
using FCSCommon.Helpers;
using FCSCommon.Utilities;
using FCSCommon.Extensions;
using SMLHelper.V2.Assets;
using SMLHelper.V2.Crafting;
using System;
using System.Collections.Generic;
using System.IO;
using FCSCommon.Controllers;
using UnityEngine;

namespace ARS_SeaBreezeFCS32.Buildables
{
    internal partial class ARSSeaBreezeFCS32Buildable : Buildable
    {
        #region Public Properties

        internal static readonly ARSSeaBreezeFCS32Buildable Singleton = new ARSSeaBreezeFCS32Buildable();
        public override TechGroup GroupForPDA { get; } = TechGroup.InteriorModules;
        public override TechCategory CategoryForPDA { get; } = TechCategory.InteriorModule;
        public override string AssetsFolder { get; } = $"FCS_ARSSeaBreeze/Assets";
        public override string IconFileName => "ARSSeaBreeze.png";

        #endregion

        public ARSSeaBreezeFCS32Buildable() : base(Mod.ClassID, Mod.FriendlyName, Mod.Description)
        {
            OnFinishedPatching += AdditionalPatching;
        }

        public static void PatchHelper()
        {
            if (!Singleton.GetPrefabs())
            {
                throw new FileNotFoundException($"Failed to retrieve the {Singleton.FriendlyName} prefab from the asset bundle");
            }
            
            Singleton.Patch();
        }

        public override GameObject GetGameObject()
        {
            try
            {
                var prefab = GameObject.Instantiate(_Prefab);

                //========== Allows the building animation and material colors ==========// 

                Shader shader = Shader.Find("MarmosetUBER");
                Renderer[] renders = prefab.GetComponentsInChildren<Renderer>();
                foreach (Renderer renderer in renders)
                {
                    renderer.material.shader = shader;
                }

                SkyApplier skyApplier = prefab.EnsureComponent<SkyApplier>();
                skyApplier.renderers = renders;
                skyApplier.anchorSky = Skies.Auto;

                //========== Allows the building animation and material colors ==========// 

                // Add constructible
                var constructable = prefab.EnsureComponent<Constructable>();
                constructable.allowedOnWall = false;
                constructable.allowedOnGround = true;
                constructable.allowedInSub = true;
                constructable.allowedInBase = true;
                constructable.allowedOnCeiling = false;
                constructable.allowedOutside = false;
                constructable.rotationEnabled = true;
                constructable.model = prefab.FindChild("model");
                constructable.techType = TechType;

                //Get the Size and Center of a box ow collision around the mesh that will be used as bounds
                var center = new Vector3(0.05496028f, 1.019654f, 0.05290359f);
                var size = new Vector3(0.9710827f, 1.908406f, 0.4202727f);
                
                //Create or get the constructable bounds
                GameObjectHelpers.AddConstructableBounds(prefab,size,center);
                
                prefab.EnsureComponent<PrefabIdentifier>().ClassId = this.ClassID;
                prefab.EnsureComponent<AnimationManager>();
                prefab.EnsureComponent<ARSolutionsSeaBreezeController>();
                
                return prefab;
            }
            catch (Exception e)
            {
                QuickLogger.Error(e.Message);
                return null;
            }
        }

#if SUBNAUTICA
        
        protected override TechData GetBlueprintRecipe()
        {
            QuickLogger.Debug($"Creating recipe...");
            // Create and associate recipe to the new TechType
            var customFabRecipe = new TechData()
            {
                craftAmount = 1,
                Ingredients = new List<Ingredient>()
                {
                    new Ingredient(Mod.SeaBreezeKitClassID.ToTechType(),1)
                }
            };
            QuickLogger.Debug($"Created Ingredients");
            return customFabRecipe;
        }
#elif BELOWZERO
        protected override RecipeData GetBlueprintRecipe()
        {
            QuickLogger.Debug($"Creating recipe...");
            // Create and associate recipe to the new TechType
            var customFabRecipe = new RecipeData()
            {
                craftAmount = 1,
                Ingredients = new List<Ingredient>()
                {
                    new Ingredient(Mod.SeaBreezeKitClassID.ToTechType(), 1)
                }
            };
            QuickLogger.Debug($"Created Ingredients");
            return customFabRecipe;
        }

#endif
    }
}
