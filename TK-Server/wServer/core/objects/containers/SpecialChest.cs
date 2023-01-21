﻿using common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wServer.core.objects.containers
{
    internal class SpecialChest : Container
    {
        public SpecialChest(CoreServerManager manager, ushort objType, int? life, bool dying, RInventory dbLink = null) : base(manager, objType, life, dying, dbLink)
        {
        }
    }
}
