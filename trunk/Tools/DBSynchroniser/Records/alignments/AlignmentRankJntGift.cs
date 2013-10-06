 


// Generated on 10/06/2013 18:02:16
using System;
using System.Collections.Generic;
using Stump.Core.IO;
using Stump.DofusProtocol.D2oClasses;
using Stump.DofusProtocol.D2oClasses.Tools.D2o;
using Stump.ORM;
using Stump.ORM.SubSonic.SQLGeneration.Schema;

namespace DBSynchroniser.Records
{
    [TableName("AlignmentRankJntGift")]
    [D2OClass("AlignmentRankJntGift", "com.ankamagames.dofus.datacenter.alignments")]
    public class AlignmentRankJntGiftRecord : ID2ORecord
    {
        int ID2ORecord.Id
        {
            get { return (int)Id; }
        }
        private const String MODULE = "AlignmentRankJntGift";
        public int id;
        public List<int> gifts;
        public List<int> parameters;
        public List<int> levels;

        [D2OIgnore]
        [PrimaryKey("Id", false)]
        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        [D2OIgnore]
        [Ignore]
        public List<int> Gifts
        {
            get { return gifts; }
            set
            {
                gifts = value;
                m_giftsBin = value == null ? null : value.ToBinary();
            }
        }

        private byte[] m_giftsBin;
        [D2OIgnore]
        public byte[] GiftsBin
        {
            get { return m_giftsBin; }
            set
            {
                m_giftsBin = value;
                gifts = value == null ? null : value.ToObject<List<int>>();
            }
        }

        [D2OIgnore]
        [Ignore]
        public List<int> Parameters
        {
            get { return parameters; }
            set
            {
                parameters = value;
                m_parametersBin = value == null ? null : value.ToBinary();
            }
        }

        private byte[] m_parametersBin;
        [D2OIgnore]
        public byte[] ParametersBin
        {
            get { return m_parametersBin; }
            set
            {
                m_parametersBin = value;
                parameters = value == null ? null : value.ToObject<List<int>>();
            }
        }

        [D2OIgnore]
        [Ignore]
        public List<int> Levels
        {
            get { return levels; }
            set
            {
                levels = value;
                m_levelsBin = value == null ? null : value.ToBinary();
            }
        }

        private byte[] m_levelsBin;
        [D2OIgnore]
        public byte[] LevelsBin
        {
            get { return m_levelsBin; }
            set
            {
                m_levelsBin = value;
                levels = value == null ? null : value.ToObject<List<int>>();
            }
        }

        public virtual void AssignFields(object obj)
        {
            var castedObj = (AlignmentRankJntGift)obj;
            
            Id = castedObj.id;
            Gifts = castedObj.gifts;
            Parameters = castedObj.parameters;
            Levels = castedObj.levels;
        }
        
        public virtual object CreateObject(object parent = null)
        {
            
            var obj = parent != null ? (AlignmentRankJntGift)parent : new AlignmentRankJntGift();
            obj.id = Id;
            obj.gifts = Gifts;
            obj.parameters = Parameters;
            obj.levels = Levels;
            return obj;
        
        }
    }
}