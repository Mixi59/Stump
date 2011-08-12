﻿using System.Collections.Generic;
using System.Linq;
using Stump.Core.Reflection;
using Stump.DofusProtocol.D2oClasses;
using Stump.DofusProtocol.Enums;
using Stump.Server.BaseServer.Initialization;
using Stump.Server.WorldServer.Database.Effects;
using Stump.Server.WorldServer.Worlds.Effects.Instances;

namespace Stump.Server.WorldServer.Worlds.Effects
{
    public class EffectManager : Singleton<EffectManager>
    {
        private Dictionary<short, EffectRecord> m_effects = new Dictionary<short, EffectRecord>();

        [Initialization(InitializationPass.Third)]
        public void Intialize()
        {
            m_effects = EffectRecord.FindAll().ToDictionary(entry => (short) entry.Id);
        }

        /// <summary>
        ///   D2O Effect class to stump effect class
        /// </summary>
        /// <param name = "effect"></param>
        /// <returns></returns>
        public EffectBase ConvertExportedEffect(EffectInstance effect)
        {
            if (effect is EffectInstanceLadder)
                return new EffectLadder(effect as EffectInstanceLadder);
            if (effect is EffectInstanceCreature)
                return new EffectCreature(effect as EffectInstanceCreature);
            if (effect is EffectInstanceDate)
                return new EffectDate(effect as EffectInstanceDate);
            if (effect is EffectInstanceDice)
                return new EffectDice(effect as EffectInstanceDice);
            if (effect is EffectInstanceDuration)
                return new EffectDuration(effect as EffectInstanceDuration);
            if (effect is EffectInstanceMinMax)
                return new EffectMinMax(effect as EffectInstanceMinMax);
            if (effect is EffectInstanceMount)
                return new EffectMount(effect as EffectInstanceMount);
            if (effect is EffectInstanceString)
                return new EffectString(effect as EffectInstanceString);
            if (effect is EffectInstanceInteger)
                return new EffectInteger(effect as EffectInstanceInteger);

            return new EffectBase(effect);
        }

        public IEnumerable<EffectBase> ConvertExportedEffect(IEnumerable<EffectInstance> effects)
        {
            return effects.Select(ConvertExportedEffect);
        }

        public EffectRecord GetTemplate(short id)
        {
            return !m_effects.ContainsKey(id) ? null : m_effects[id];
        }

        public bool IsEffectRandomable(EffectsEnum effect)
        {
            return m_randomablesEffects.Contains(effect);
        }

        public EffectInstance GuessRealEffect(EffectInstance effect)
        {
            if (!(effect is EffectInstanceDice))
                return effect;

            var effectDice = effect as EffectInstanceDice;

            if (effectDice.value == 0 && effectDice.diceNum > 0 && effectDice.diceSide > 0)
            {
                return new EffectInstanceMinMax
                           {
                               duration = effectDice.duration,
                               effectId = effectDice.effectId,
                               max = effectDice.diceSide,
                               min = effectDice.diceNum,
                               modificator = effectDice.modificator,
                               random = effectDice.random,
                               targetId = effectDice.targetId,
                               trigger = effectDice.trigger,
                               zoneShape = effectDice.zoneShape,
                               zoneSize = effectDice.zoneSize
                           };
            }

            if (effectDice.value == 0 && effectDice.diceNum == 0 && effectDice.diceSide > 0)
            {
                return new EffectInstanceMinMax
                           {
                               duration = effectDice.duration,
                               effectId = effectDice.effectId,
                               max = effectDice.diceSide,
                               min = effectDice.diceNum,
                               modificator = effectDice.modificator,
                               random = effectDice.random,
                               targetId = effectDice.targetId,
                               trigger = effectDice.trigger,
                               zoneShape = effectDice.zoneShape,
                               zoneSize = effectDice.zoneSize
                           };
            }

            return effect;
        }

        #region Randomable Effects

        /// <summary>
        ///   Effects that are random when a new item is generated
        /// </summary>
        private readonly EffectsEnum[] m_randomablesEffects =
            new[]
                {
                    EffectsEnum.Effect_AddMP,
                    EffectsEnum.Effect_AddGlobalDamageReduction_105,
                    EffectsEnum.Effect_AddDamageReflection,
                    EffectsEnum.Effect_AddHealth,
                    EffectsEnum.Effect_AddAP_111,
                    EffectsEnum.Effect_AddDamageBonus,
                    EffectsEnum.Effect_AddDamageMultiplicator,
                    EffectsEnum.Effect_AddCriticalHit,
                    EffectsEnum.Effect_SubRange,
                    EffectsEnum.Effect_AddRange,
                    EffectsEnum.Effect_AddStrength,
                    EffectsEnum.Effect_AddAgility,
                    EffectsEnum.Effect_AddAP_120,
                    EffectsEnum.Effect_AddDamageBonus_121,
                    EffectsEnum.Effect_AddCriticalMiss,
                    EffectsEnum.Effect_AddChance,
                    EffectsEnum.Effect_AddWisdom,
                    EffectsEnum.Effect_AddVitality,
                    EffectsEnum.Effect_AddIntelligence,
                    EffectsEnum.Effect_AddMP_128,
                    EffectsEnum.Effect_SubRange_135,
                    EffectsEnum.Effect_AddRange_136,
                    EffectsEnum.Effect_AddPhysicalDamage_137,
                    EffectsEnum.Effect_IncreaseDamage_138,
                    EffectsEnum.Effect_AddPhysicalDamage_142,
                    EffectsEnum.Effect_SubDamageBonus,
                    EffectsEnum.Effect_SubChance,
                    EffectsEnum.Effect_SubVitality,
                    EffectsEnum.Effect_SubAgility,
                    EffectsEnum.Effect_SubIntelligence,
                    EffectsEnum.Effect_SubWisdom,
                    EffectsEnum.Effect_SubStrength,
                    EffectsEnum.Effect_IncreaseWeight,
                    EffectsEnum.Effect_DecreaseWeight,
                    EffectsEnum.Effect_IncreaseAPAvoid,
                    EffectsEnum.Effect_IncreaseMPAvoid,
                    EffectsEnum.Effect_SubDodgeAPProbability,
                    EffectsEnum.Effect_SubDodgeMPProbability,
                    EffectsEnum.Effect_AddGlobalDamageReduction,
                    EffectsEnum.Effect_AddDamageBonusPercent,
                    EffectsEnum.Effect_SubAP,
                    EffectsEnum.Effect_SubMP,
                    EffectsEnum.Effect_SubCriticalHit,
                    EffectsEnum.Effect_SubMagicDamageReduction,
                    EffectsEnum.Effect_SubPhysicalDamageReduction,
                    EffectsEnum.Effect_AddInitiative,
                    EffectsEnum.Effect_SubInitiative,
                    EffectsEnum.Effect_AddProspecting,
                    EffectsEnum.Effect_SubProspecting,
                    EffectsEnum.Effect_AddHealBonus,
                    EffectsEnum.Effect_SubHealBonus,
                    EffectsEnum.Effect_AddSummonLimit,
                    EffectsEnum.Effect_AddMagicDamageReduction,
                    EffectsEnum.Effect_AddPhysicalDamageReduction,
                    EffectsEnum.Effect_SubDamageBonusPercent,
                    EffectsEnum.Effect_AddEarthResistPercent,
                    EffectsEnum.Effect_AddWaterResistPercent,
                    EffectsEnum.Effect_AddAirResistPercent,
                    EffectsEnum.Effect_AddFireResistPercent,
                    EffectsEnum.Effect_AddNeutralResistPercent,
                    EffectsEnum.Effect_SubEarthResistPercent,
                    EffectsEnum.Effect_SubWaterResistPercent,
                    EffectsEnum.Effect_SubAirResistPercent,
                    EffectsEnum.Effect_SubFireResistPercent,
                    EffectsEnum.Effect_SubNeutralResistPercent,
                    EffectsEnum.Effect_AddTrapBonus,
                    EffectsEnum.Effect_AddTrapBonusPercent,
                    EffectsEnum.Effect_AddEarthElementReduction,
                    EffectsEnum.Effect_AddWaterElementReduction,
                    EffectsEnum.Effect_AddAirElementReduction,
                    EffectsEnum.Effect_AddFireElementReduction,
                    EffectsEnum.Effect_AddNeutralElementReduction,
                    EffectsEnum.Effect_SubEarthElementReduction,
                    EffectsEnum.Effect_SubWaterElementReduction,
                    EffectsEnum.Effect_SubAirElementReduction,
                    EffectsEnum.Effect_SubFireElementReduction,
                    EffectsEnum.Effect_SubNeutralElementReduction,
                    EffectsEnum.Effect_AddPvpEarthResistPercent,
                    EffectsEnum.Effect_AddPvpWaterResistPercent,
                    EffectsEnum.Effect_AddPvpAirResistPercent,
                    EffectsEnum.Effect_AddPvpFireResistPercent,
                    EffectsEnum.Effect_AddPvpNeutralResistPercent,
                    EffectsEnum.Effect_SubPvpEarthResistPercent,
                    EffectsEnum.Effect_SubPvpWaterResistPercent,
                    EffectsEnum.Effect_SubPvpAirResistPercent,
                    EffectsEnum.Effect_SubPvpFireResistPercent,
                    EffectsEnum.Effect_SubPvpNeutralResistPercent,
                    EffectsEnum.Effect_AddPvpEarthElementReduction,
                    EffectsEnum.Effect_AddPvpWaterElementReduction,
                    EffectsEnum.Effect_AddPvpAirElementReduction,
                    EffectsEnum.Effect_AddPvpFireElementReduction,
                    EffectsEnum.Effect_AddPvpNeutralElementReduction,
                    EffectsEnum.Effect_AddGlobalDamageReduction_265,
                    EffectsEnum.Effect_AddPushDamageBonus,
                    EffectsEnum.Effect_SubPushDamageBonus,
                    EffectsEnum.Effect_AddPushDamageReduction,
                    EffectsEnum.Effect_SubPushDamageReduction,
                    EffectsEnum.Effect_AddCriticalDamageBonus,
                    EffectsEnum.Effect_SubCriticalDamageBonus,
                    EffectsEnum.Effect_AddCriticalDamageReduction,
                    EffectsEnum.Effect_SubCriticalDamageReduction,
                    EffectsEnum.Effect_AddEarthDamageBonus,
                    EffectsEnum.Effect_SubEarthDamageBonus,
                    EffectsEnum.Effect_AddFireDamageBonus,
                    EffectsEnum.Effect_SubFireDamageBonus,
                    EffectsEnum.Effect_AddWaterDamageBonus,
                    EffectsEnum.Effect_SubWaterDamageBonus,
                    EffectsEnum.Effect_AddAirDamageBonus,
                    EffectsEnum.Effect_SubAirDamageBonus,
                    EffectsEnum.Effect_AddNeutralDamageBonus,
                    EffectsEnum.Effect_SubNeutralDamageBonus,
                };

        #endregion
    }
}