﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FCSTechFabricator.Interfaces
{
    public interface IFCSStorage
    {
       int GetContainerFreeSpace { get; }
       bool IsFull { get;}
       bool CanBeStored(int amount);
       bool AddItemToContainer(InventoryItem item);
       bool IsAllowedToAdd(Pickupable pickupable, bool verbose);
    }
}
