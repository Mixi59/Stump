using System;
using System.Collections.Generic;
using System.Linq;
using Stump.Core.Cache;
using Stump.Core.Extensions;
using Stump.DofusProtocol.Enums;
using Stump.DofusProtocol.Types;
using Stump.Server.WorldServer.Database.Items;
using Stump.Server.WorldServer.Database.Items.Templates;
using Stump.Server.WorldServer.Database.World;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Characters;
using Stump.Server.WorldServer.Game.Effects;
using Stump.Server.WorldServer.Game.Effects.Instances;

namespace Stump.Server.WorldServer.Game.Items.Player
{
    public abstract class BasePlayerItem : Item<PlayerItemRecord>
    {
        #region Fields

        public Character Owner
        {
            get;
            private set;
        }


        #endregion

        #region Constructors

        public BasePlayerItem(Character owner, PlayerItemRecord record)
            : base(record)
        {
            m_objectItemValidator = new ObjectValidator<ObjectItem>(BuildObjectItem);

            Owner = owner;
        }

        #endregion

        #region Functions

        public virtual bool AreConditionFilled(Character character)
        {
            try
            {
                return Template.CriteriaExpression == null ||
                    Template.CriteriaExpression.Eval(character);
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        ///   Check if the given item can be stacked with the actual item (without comparing his position)
        /// </summary>
        /// <param name = "compared"></param>
        /// <returns></returns>
        public virtual bool IsStackableWith(BasePlayerItem compared)
        {
            return (compared.Template.Id == Template.Id &&
                    compared.Position == CharacterInventoryPositionEnum.INVENTORY_POSITION_NOT_EQUIPED &&
                    compared.Effects.CompareEnumerable(Effects));
        }

        /// <summary>
        ///   Check if the given item must be stacked with the actual item
        /// </summary>
        /// <param name = "compared"></param>
        /// <returns></returns>
        public virtual bool MustStackWith(BasePlayerItem compared)
        {
            return (compared.Template.Id == Template.Id &&
                    compared.Position == CharacterInventoryPositionEnum.INVENTORY_POSITION_NOT_EQUIPED &&
                    compared.Position == Position &&
                    compared.Effects.CompareEnumerable(Effects));
        }

        public virtual bool IsLinked()
        {
            if (Template.IsLinkedToOwner)
                return true;

            if (Template.Type.SuperType == ItemSuperTypeEnum.SUPERTYPE_QUEST)
                return true;

            if (IsTokenItem())
                return true;

            return Effects.Any(x => x.EffectId == EffectsEnum.Effect_NonExchangeable_981 ||
                                    x.EffectId == EffectsEnum.Effect_NonExchangeable_982);
        }

        public bool IsTokenItem()
        {
            return Inventory.ActiveTokens && Template.Id == Inventory.TokenTemplateId;
        }

        public virtual bool IsUsable()
        {
            return Template.Usable;
        }

        public virtual bool IsEquiped()
        {
            return Position != CharacterInventoryPositionEnum.INVENTORY_POSITION_NOT_EQUIPED;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>True whenever the item can be added</returns>
        public virtual bool OnAddItem()
        {
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>True whenever the item can be removed</returns>
        public virtual bool OnRemoveItem()
        {
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="character"></param>
        /// <param name="item"></param>
        /// <param name="amount"></param>
        /// <param name="targetCell"></param>
        /// <param name="target"></param>
        /// <returns>Returns the amount of items to remove</returns>
        public virtual uint UseItem(uint amount = 1, Cell targetCell = null, Character target = null)
        {
            uint removed = 0;
            foreach (var effect in Effects)
            {
                var handler = EffectManager.Instance.GetUsableEffectHandler(effect, target ?? Owner, this);
                handler.NumberOfUses = amount;
                handler.TargetCell = targetCell;

                if (handler.Apply())
                    removed = Math.Max(handler.UsedItems, removed);
            }

            return removed;
        }

        public virtual bool OnEquipItem(bool unequip)
        {
            return true;
        }

        public virtual bool AllowFeeding
        {
            get
            {
                return false;
            }
        }

        public virtual bool Feed(BasePlayerItem food)
        {
            return false;
        }

        public virtual bool AllowDropping
        {
            get
            {
                return false;
            }
        }

        public virtual bool Drop(BasePlayerItem dropOnItem)
        {
            return false;
        }

        #region ObjectItem

        private readonly ObjectValidator<ObjectItem> m_objectItemValidator;

        protected virtual ObjectItem BuildObjectItem()
        {
            return new ObjectItem(
                (byte) Position,
                (short) Template.Id,
                0, // todo : power rate
                false, // todo : over max
                Effects.Where(entry => !entry.Hidden).Select(entry => entry.GetObjectEffect()),
                Guid,
                (int)Stack);
        }

        public ObjectItem GetObjectItem()
        {
            return m_objectItemValidator;
        }

        /// <summary>
        /// Call it each time you modify part of the item
        /// </summary>
        public virtual void Invalidate()
        {
            m_objectItemValidator.Invalidate();
        }

        #endregion

        #endregion

        #region Properties

        public override int Guid
        {
            get { return base.Guid; }
            protected set
            {
                base.Guid = value;
                Invalidate();
            }
        }

        public override ItemTemplate Template
        {
            get { return base.Template; }
            protected set
            {
                base.Template = value;
                Invalidate();
            }
        }

        public override uint Stack
        {
            get { return base.Stack; }
            set
            {
                base.Stack = value;
                Invalidate();
            }
        }


        public override List<EffectBase> Effects
        {
            get { return base.Effects; }
            protected set
            {
                base.Effects = value;
                Invalidate();
            }
        }

        public virtual CharacterInventoryPositionEnum Position
        {
            get { return Record.Position; }
            set
            {
                Record.Position = value;
                Invalidate();
            }
        }

        public virtual uint AppearanceId
        {
            get
            {
                return Template.AppearanceId;
            }
        }

        public virtual int Weight
        {
            get { return (int) (Template.RealWeight*Stack); }
        }

        #endregion
    }
}