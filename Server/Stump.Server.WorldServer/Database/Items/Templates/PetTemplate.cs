﻿using System.Collections.Generic;
using System.Linq;
using Stump.Core.IO;
using Stump.DofusProtocol.D2oClasses.Tools.D2o;
using Stump.DofusProtocol.D2oClasses;
using Stump.ORM;
using Stump.ORM.SubSonic.SQLGeneration.Schema;
using Stump.Server.WorldServer.Database.Interactives;
using Stump.Server.WorldServer.Database.Items.Pets;
using Stump.Server.WorldServer.Game.Effects;
using Stump.Server.WorldServer.Game.Effects.Instances;

namespace Stump.Server.WorldServer.Database.Items.Templates
{
    public class PetTemplateRelator
    {
        public static string FetchQuery = "SELECT * FROM items_pets LEFT JOIN items_pets_foods ON items_pets_foods.PetId = items_pets.Id";

        private PetTemplate m_current;

        public PetTemplate Map(PetTemplate pet, PetFoodRecord food)
        {
            if (pet == null)
                return m_current;

            if (m_current?.Id == pet.Id)
            {
                m_current.Foods.Add(food);
                return null;
            }

            var previous = m_current;

            m_current = pet;
            m_current.Foods.Add(food);
            return previous;
        }
    }

    [TableName("items_pets")]
    [D2OClass("Pet", "com.ankamagames.dofus.datacenter.pets")]
    public class PetTemplate : IAutoGeneratedRecord, IAssignedByD2O, ISaveIntercepter
    {
        private int[] m_foodItems;
        private string m_foodItemsCSV;
        private int[] m_foodTypes;
        private string m_foodTypesCSV;
        private byte[] m_possibleEffectsBin;
        private List<EffectBase> m_possibleEffects;

        [PrimaryKey("Id", false)]
        public int Id
        {
            get;
            set;
        }

        public string FoodItemsCSV
        {
            get { return m_foodItemsCSV; }
            set
            {
                m_foodItemsCSV = value;
                m_foodItems = value.FromCSV<int>(",");
            }
        }

        [Ignore]
        public int[] FoodItems
        {
            get { return m_foodItems; }
            set
            {
                m_foodItems = value;
                m_foodItemsCSV = value.ToCSV(",");
            }
        }

        public string FoodTypesCSV
        {
            get { return m_foodTypesCSV; }
            set
            {
                m_foodTypesCSV = value;
                m_foodTypes = value.FromCSV<int>(",");
            }
        }

        [Ignore]
        public int[] FoodTypes
        {
            get { return m_foodTypes; }
            set
            {
                m_foodTypes = value;
                m_foodTypesCSV = value.ToCSV(",");
            }
        }

        public int MinDurationBeforeMeal
        {
            get;
            set;
        }
        public int MaxDurationBeforeMeal
        {
            get;
            set;
        }

        [Ignore]
        public List<PetFoodRecord> Foods
        {
            get;
            set;
        } = new List<PetFoodRecord>();

        public byte[] PossibleEffectsBin
        {
            get { return m_possibleEffectsBin; }
            set
            {
                m_possibleEffectsBin = value;
                m_possibleEffects = value?.ToObject<List<EffectBase>>();
            }
        }

        [Ignore]
        public List<EffectBase> PossibleEffects
        {
            get { return m_possibleEffects; }
            set
            {
                m_possibleEffects = value;
                m_possibleEffectsBin = value?.ToBinary();
            }
        }

        public int? GhostItemId
        {
            get;
            set;
        }

        #region IAssignedByD2O Members

        public void AssignFields(object d2oObject)
        {
            var pet = (Pet) d2oObject;
            Id = pet.id;
            FoodItems = pet.foodItems.ToArray();
            FoodTypes = pet.foodTypes.ToArray();
            MinDurationBeforeMeal = pet.MinDurationBeforeMeal;
            MaxDurationBeforeMeal = pet.MaxDurationBeforeMeal;
            PossibleEffects = pet.PossibleEffects.Select(EffectManager.Instance.ConvertExportedEffect).ToList();
        }

        #endregion

        #region ISaveIntercepter Members

        public void BeforeSave(bool insert)
        {
            m_foodItemsCSV = m_foodItems.ToCSV(",");
            m_foodTypesCSV = m_foodTypes.ToCSV(",");
        }

        #endregion
    }
}