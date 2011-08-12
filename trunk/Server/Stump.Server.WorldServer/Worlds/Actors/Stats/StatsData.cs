using System;
using Stump.DofusProtocol.Enums;
using Stump.DofusProtocol.Types;
using Stump.Server.WorldServer.Worlds.Actors.Interfaces;

namespace Stump.Server.WorldServer.Worlds.Actors.Stats
{
    public class StatsData
    {
        protected Func<IStatsOwner, int, int, int, int, int> m_formule;
        protected short m_valueBase;
        protected short m_valueBonus;
        protected short m_valueEquiped;
        protected short m_valueGiven;

        public StatsData(IStatsOwner owner, CaracteristicsEnum name, short valueBase)
            : this(
                owner, name, valueBase,
                delegate(IStatsOwner _owner, int valuebase, int valueequiped, int valuegiven, int valuebonus)
                    {
                        if (_owner == null)
                            throw new ArgumentNullException("_owner");

                        return valuebase + valueequiped + valuegiven + valuebonus;
                    })
        {
        }

        public StatsData(IStatsOwner owner, CaracteristicsEnum name, short valueBase, Func<IStatsOwner, int, int, int, int, int> formule)
        {
            m_valueBase = valueBase;
            m_formule = formule;
            Name = name;
            Owner = owner;
        }

        public IStatsOwner Owner
        {
            get;
            protected set;
        }

        public CaracteristicsEnum Name
        {
            get;
            protected set;
        }

        public short Base
        {
            get { return m_valueBase; }
            set { m_valueBase = value; }
        }

        public short Equiped
        {
            get { return m_valueEquiped; }
            set { m_valueEquiped = value; }
        }

        public short Given
        {
            get { return m_valueGiven; }
            set { m_valueGiven = value; }
        }

        public short Bonus
        {
            get { return m_valueBonus; }
            set { m_valueBonus = value; }
        }

        public virtual int Total
        {
            get
            {
                if (m_formule != null)
                {
                    int result = m_formule(Owner, m_valueBase, m_valueEquiped, m_valueGiven, m_valueBonus);

                    return result;
                }

                return 0;
            }
        }

        /// <summary>
        ///   TotalSafe can't be lesser than 0
        /// </summary>
        public virtual int TotalSafe
        {
            get
            {
                if (m_formule != null)
                {
                    int result = m_formule(Owner, m_valueBase, m_valueEquiped, m_valueGiven, m_valueBonus);

                    return result < 0 ? 0 : result;
                }

                return 0;
            }
        }

        public static int operator +(int i1, StatsData s1)
        {
            return i1 + s1.Total;
        }

        public static int operator +(StatsData s1, StatsData s2)
        {
            return s1.Total + s2.Total;
        }

        public static int operator -(int i1, StatsData s1)
        {
            return i1 - s1.Total;
        }

        public static int operator -(StatsData s1, StatsData s2)
        {
            return s1.Total - s2.Total;
        }

        public static int operator *(int i1, StatsData s1)
        {
            return i1*s1.Total;
        }

        public static int operator *(StatsData s1, StatsData s2)
        {
            return s1.Total*s2.Total;
        }

        public static double operator /(StatsData s1, double d1)
        {
            return s1.Total/d1;
        }

        public static double operator /(StatsData s1, StatsData s2)
        {
            return s1.Total/(double) s2.Total;
        }

        public static implicit operator CharacterBaseCharacteristic(StatsData s1)
        {
            return new CharacterBaseCharacteristic(s1.Base, s1.Equiped, s1.Given, s1.Bonus);
        }
    }
}