﻿using common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace wServer.core.objects
{
    public class Container : StaticObject, IContainer
    {
        private const int BagSize = 8;

        public Container(CoreServerManager manager, ushort objType, int? life, bool dying, RInventory dbLink = null) : base(manager, objType, life, false, dying, false) => Initialize(dbLink);

        public Container(CoreServerManager manager, ushort id) : base(manager, id, null, false, false, false) => Initialize(null);

        public int[] BagOwners { get; set; }
        public RInventory DbLink { get; private set; }
        public Inventory Inventory { get; private set; }
        public int[] SlotTypes { get; private set; }

        public override bool HitByProjectile(Projectile projectile, TickData time) => false;

        public override void Tick(TickData time)
        {
            if (Inventory == null)
                return;

            if (ObjectType == 0x504)    //Vault chest
                return;

            var hasItem = false;

            foreach (var i in Inventory)
                if (i != null)
                {
                    hasItem = true;
                    break;
                }

            if (!hasItem)
                Owner.LeaveWorld(this);

            base.Tick(time);
        }

        protected override void ExportStats(IDictionary<StatDataType, object> stats)
        {
            if (Inventory == null)
                return;

            stats[StatDataType.Inventory0] = Inventory[0]?.ObjectType ?? -1;
            stats[StatDataType.Inventory1] = Inventory[1]?.ObjectType ?? -1;
            stats[StatDataType.Inventory2] = Inventory[2]?.ObjectType ?? -1;
            stats[StatDataType.Inventory3] = Inventory[3]?.ObjectType ?? -1;
            stats[StatDataType.Inventory4] = Inventory[4]?.ObjectType ?? -1;
            stats[StatDataType.Inventory5] = Inventory[5]?.ObjectType ?? -1;
            stats[StatDataType.Inventory6] = Inventory[6]?.ObjectType ?? -1;
            stats[StatDataType.Inventory7] = Inventory[7]?.ObjectType ?? -1;
            stats[StatDataType.InventoryData0] = Inventory.Data[0]?.GetData() ?? "{}";
            stats[StatDataType.InventoryData1] = Inventory.Data[1]?.GetData() ?? "{}";
            stats[StatDataType.InventoryData2] = Inventory.Data[2]?.GetData() ?? "{}";
            stats[StatDataType.InventoryData3] = Inventory.Data[3]?.GetData() ?? "{}";
            stats[StatDataType.InventoryData4] = Inventory.Data[4]?.GetData() ?? "{}";
            stats[StatDataType.InventoryData5] = Inventory.Data[5]?.GetData() ?? "{}";
            stats[StatDataType.InventoryData6] = Inventory.Data[6]?.GetData() ?? "{}";
            stats[StatDataType.InventoryData7] = Inventory.Data[7]?.GetData() ?? "{}";
            stats[StatDataType.OwnerAccountId] = BagOwners.Length == 1 ? BagOwners[0] : -1;

            base.ExportStats(stats);
        }

        private void Initialize(RInventory dbLink)
        {
            Inventory = new Inventory(this);
            BagOwners = new int[0];
            DbLink = dbLink;

            var node = CoreServerManager.Resources.GameData.ObjectTypeToElement[ObjectType];

            SlotTypes = Utils.ResizeArray(node.Element("SlotTypes").Value.CommaToArray<int>(), BagSize);

            var eq = node.Element("Equipment");

            if (eq != null)
            {
                var inv = eq.Value.CommaToArray<ushort>().Select(_ => _ == 0xffff ? null : CoreServerManager.Resources.GameData.Items[_]).ToArray();

                Array.Resize(ref inv, BagSize);

                Inventory.SetItems(inv);
            }
        }
    }
}
