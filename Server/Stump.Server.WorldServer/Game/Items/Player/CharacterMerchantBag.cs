﻿using System.Collections.Generic;
using System.Linq;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Characters;
using Stump.Server.WorldServer.Handlers.Inventory;

namespace Stump.Server.WorldServer.Game.Items.Player
{
    public sealed class CharacterMerchantBag : PersistantItemsCollection<MerchantItem>
    {
        public CharacterMerchantBag(Character owner)
        {
            Owner = owner;
        }

        public Character Owner
        {
            get;
            private set;
        }

        public bool IsLoaded
        {
            get;
            private set;
        }

        internal void LoadMerchantBag(MerchantBag bag)
        {
            if (IsLoaded)
                return;

            Items = bag.ToDictionary(x => x.Guid);
            IsLoaded = true;
        }

        internal void LoadMerchantBag()
        {
            if (IsLoaded)
                return;

            var records = ItemManager.Instance.FindPlayerMerchantItems(Owner.Id);
            Items = records.Select(entry => new MerchantItem(entry)).ToDictionary(entry => entry.Guid);
            IsLoaded = true;
        }

        private void UnLoadMerchantBag()
        {
            Items.Clear();
        }

        public int GetMerchantTax()
        {
            var resultTax = Items.Aggregate<KeyValuePair<int, MerchantItem>, double>(0, (current, item) => current + (item.Value.Price*item.Value.Stack));

            resultTax = (resultTax * 0.1);

            return (int)resultTax;
        }

        public bool StoreItem(BasePlayerItem item, int amount, uint price)
        {
            if (!Owner.Inventory.HasItem(item) || amount <= 0)
                return false;

            if (item.IsLinkedToPlayer() || item.IsLinkedToAccount())
                return false;

            if (amount > item.Stack)
                amount = (int)item.Stack;

            var merchantItem = ItemManager.Instance.CreateMerchantItem(Owner, item, amount, price);
            AddItem(merchantItem);

            Owner.Inventory.RemoveItem(item, amount);

            return true;
        }

        public bool TakeBack(MerchantItem item, int quantity)
        {
            if (quantity <= 0)
                return false;

            if (quantity > item.Stack)
                quantity = (int)item.Stack;

            RemoveItem(item, quantity);
            var newItem = ItemManager.Instance.CreatePlayerItem(Owner, item.Template, quantity,
                                                                       item.Effects);

            Owner.Inventory.AddItem(newItem);

            WorldServer.Instance.IOTaskPool.AddMessage(Save);

            return true;
        }

        public bool ModifyItem(MerchantItem item, int quantity, uint price)
        {
            item.Price = price;
            InventoryHandler.SendExchangeShopStockMovementUpdatedMessage(Owner.Client, item);

            if (quantity > item.Stack)
            {
                var playerItem = Owner.Inventory.TryGetItem(item.Template);
                if (playerItem != null)
                    StoreItem(playerItem, (int) (quantity - item.Stack), price);
            }

            if (quantity > 0 && quantity < item.Stack)
                TakeBack(item, (int) (item.Stack - quantity));

            WorldServer.Instance.IOTaskPool.AddMessage(Save);

            return true;
        }

        protected override void OnItemAdded(MerchantItem item)
        {
            InventoryHandler.SendExchangeShopStockMovementUpdatedMessage(Owner.Client, item);

            base.OnItemAdded(item);
        }

        protected override void OnItemRemoved(MerchantItem item)
        {
            InventoryHandler.SendExchangeShopStockMovementRemovedMessage(Owner.Client, item);

            base.OnItemRemoved(item);
        }

        protected override void OnItemStackChanged(MerchantItem item, int difference)
        {
            InventoryHandler.SendExchangeShopStockMovementUpdatedMessage(Owner.Client, item);

            base.OnItemStackChanged(item, difference);
        }
    }
}