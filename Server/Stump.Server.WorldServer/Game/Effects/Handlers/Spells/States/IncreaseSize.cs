﻿using Stump.DofusProtocol.Enums;
using Stump.Server.WorldServer.Database.World;
using Stump.Server.WorldServer.Game.Actors.Fight;
using Stump.Server.WorldServer.Game.Effects.Instances;
using Stump.Server.WorldServer.Game.Fights.Buffs.Customs;using Stump.Server.WorldServer.Game.Spells.Casts;
using System.Linq;

namespace Stump.Server.WorldServer.Game.Effects.Handlers.Spells.States
{
    [EffectHandler(EffectsEnum.Effect_IncreaseSize)]
    public class IncreaseSize : SpellEffectHandler
    {
        public IncreaseSize(EffectDice effect, FightActor caster, SpellCastHandler castHandler, Cell targetedCell, bool critical)
            : base(effect, caster, castHandler, targetedCell, critical)
        {
        }

        protected override bool InternalApply()
        {
            foreach (var actor in GetAffectedActors())
            {
                var look = actor.Look.Clone();
                var buffLook = actor.GetBuffs(x => x is SkinBuff).LastOrDefault() as SkinBuff;

                if (buffLook != null)
                    look = buffLook.Look.Clone();

                look.Rescale((Dice.DiceNum / 100.0) + 1);

                var buff = new SkinBuff(actor.PopNextBuffId(), actor, Caster, this, look, Spell, FightDispellableEnum.DISPELLABLE_BY_DEATH);
                actor.AddBuff(buff);
            }

            return true;
        }
    }
}
