﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FCS_HydroponicHarvesters.Configuration;
using FCSCommon.Utilities;
using SMLHelper.V2.Handlers;

namespace FCS_HydroponicHarvesters.Buildables
{
    internal partial class HydroponicHarvestersBuildable
    {
        private const string MaxKey = "HH_Max";
        private const string HighKey = "HH_High";
        private const string MinKey = "HH_Min";
        private const string LowKey = "HH_Low";
        private const string OffKey = "HH_Off";
        private const string CannotDeleteDNAItemKey = "HH_CannotDeleteDNAItem";
        private const string NotDirtyKey = "HH_NotDirty";
        private const string HMSTimeKey = "HH_HMSTime";
        private const string AmountOfItemsKey = "HH_Items";
        private const string HasItemsMessageKey = "HH_HasItemsMessage";
        internal string BuildableName { get; set; }

        internal TechType TechTypeID { get; set; }

        private void AdditionalPatching()
        {
            QuickLogger.Debug("Additional Properties");
            BuildableName = FriendlyName;
            TechTypeID = TechType;

            LanguageHandler.SetLanguageLine(MaxKey, "MAX");
            LanguageHandler.SetLanguageLine(HighKey, "HIGH");
            LanguageHandler.SetLanguageLine(MinKey, "MIN");
            LanguageHandler.SetLanguageLine(LowKey, "LOW");
            LanguageHandler.SetLanguageLine(OffKey, "OFF");
            LanguageHandler.SetLanguageLine(CannotDeleteDNAItemKey, "Cannot delete DNA {0} because there are items still in the harvestor.");
            LanguageHandler.SetLanguageLine(NotDirtyKey, "Cannot clean! This harvester isn't dirty enough.");
            LanguageHandler.SetLanguageLine(HMSTimeKey, "Time Left Until Dirty: {0}");
            LanguageHandler.SetLanguageLine(AmountOfItemsKey, "{0} Items");
            LanguageHandler.SetLanguageLine(HasItemsMessageKey, "Hydroponic Harvester is not empty.");
        }

        internal static string Max()
        {
            return Language.main.Get(MaxKey);
        }

        internal static string High()
        {
            return Language.main.Get(HighKey);
        }

        internal static string Min()
        {
            return Language.main.Get(MinKey);
        }

        internal static string Low()
        {
            return Language.main.Get(LowKey);
        }

        internal static string Off()
        {
            return Language.main.Get(OffKey);
        }

        internal static string CannotDeleteDNAItem(string itemName)
        {
            return string.Format(Language.main.Get(CannotDeleteDNAItemKey), itemName);
        }

        internal static string UnitIsntDirty()
        {
            return Language.main.Get(NotDirtyKey);
        }

        internal static string HMSTime()
        {
            return Language.main.Get(HMSTimeKey);
        }
        internal static string AmountOfItems()
        {
            return Language.main.Get(AmountOfItemsKey);
        }

        internal static string NotAllowedItem()
        {
            return Language.main.Get("TimeCapsuleItemNotAllowed");
        }

        internal static string HasItemsMessage()
        {
            return Language.main.Get(HasItemsMessageKey);
        }
    }
}