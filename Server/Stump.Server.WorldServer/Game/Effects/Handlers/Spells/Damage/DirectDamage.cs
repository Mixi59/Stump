using System;
using Stump.DofusProtocol.Enums;
using Stump.Server.WorldServer.Database.World;
using Stump.Server.WorldServer.Game.Actors.Fight;
using Stump.Server.WorldServer.Game.Effects.Instances;
using Stump.Server.WorldServer.Game.Fights.Buffs;
using Stump.Server.WorldServer.Handlers.Actions;
using Spell = Stump.Server.WorldServer.Game.Spells.Spell;

namespace Stump.Server.WorldServer.Game.Effects.Handlers.Spells.Damage
{
    [EffectHandler(EffectsEnum.Effect_DamageWater)]
    [EffectHandler(EffectsEnum.Effect_DamageEarth)]
    [EffectHandler(EffectsEnum.Effect_DamageAir)]
    [EffectHandler(EffectsEnum.Effect_DamageFire)]
    [EffectHandler(EffectsEnum.Effect_DamageNeutral)]
    public class DirectDamage : SpellEffectHandler
    {
        public DirectDamage(EffectDice effect, FightActor caster, Spell spell, Cell targetedCell, bool critical)
            : base(effect, caster, spell, targetedCell, critical)
        {
            BuffTriggerType = BuffTriggerType.TURN_BEGIN;
        }

        public BuffTriggerType BuffTriggerType
        {
            get;
            set;
        }

        public override bool Apply()
        {
            foreach (var actor in GetAffectedActors())
            {
                if (Effect.Duration > 0)
                {
                    AddTriggerBuff(actor, true, BuffTriggerType, DamageBuffTrigger);
                }
                else
                {
                    // spell reflected
                    var buff = actor.GetBestReflectionBuff();
                    if (buff != null && buff.ReflectedLevel >= Spell.CurrentLevel && Spell.Template.Id != 0)
                    {
                        NotifySpellReflected(actor);
                        Caster.InflictDamage(new Fights.Damage(Dice, GetEffectSchool(Dice.EffectId), actor, Spell) {ReflectedDamages = true, MarkTrigger = MarkTrigger});

                        actor.RemoveAndDispellBuff(buff);
                    }
                    else
                    {
                        actor.InflictDamage(new Fights.Damage(Dice, GetEffectSchool(Dice.EffectId), Caster, Spell) {MarkTrigger = MarkTrigger});
                    }
                }
            }

            return true;
        }

        private void NotifySpellReflected(FightActor source)
        {
            ActionsHandler.SendGameActionFightReflectSpellMessage(Fight.Clients, source, Caster);
        }

        private static void DamageBuffTrigger(TriggerBuff buff, BuffTriggerType trigger, object token)
        {
            var damages = new Fights.Damage(buff.Dice, GetEffectSchool(buff.Dice.EffectId), buff.Caster, buff.Spell)
            {
                Buff = buff
            };

            buff.Target.InflictDamage(damages);

        }

        private static EffectSchoolEnum GetEffectSchool(EffectsEnum effect)
        {
            switch (effect)
            {
                case EffectsEnum.Effect_DamageWater:
                    return EffectSchoolEnum.Water;
                case EffectsEnum.Effect_DamageEarth:
                    return EffectSchoolEnum.Earth;
                case EffectsEnum.Effect_DamageAir:
                    return EffectSchoolEnum.Air;
                case EffectsEnum.Effect_DamageFire:
                    return EffectSchoolEnum.Fire;
                case EffectsEnum.Effect_DamageNeutral:
                    return EffectSchoolEnum.Neutral;
                default:
                    throw new Exception(string.Format("Effect {0} has not associated School Type", effect));
            }
        }
    }
}