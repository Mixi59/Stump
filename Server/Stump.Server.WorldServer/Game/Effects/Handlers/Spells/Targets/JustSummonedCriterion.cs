﻿using System.Linq;
using Stump.Server.WorldServer.Game.Actors.Fight;
using Stump.Server.WorldServer.Game.Actors.Interfaces;

namespace Stump.Server.WorldServer.Game.Effects.Handlers.Spells.Targets
{
    public class JustSummonedCriterion : TargetCriterion
    {
        public override bool IsTargetValid(FightActor actor, SpellEffectHandler handler)
        {
            var summon = actor as ISummoned;
            return summon != null && handler.CastHandler.GetEffectHandlers()
                .Any(x => summon.SummoningEffect == x);
        }
    }
}