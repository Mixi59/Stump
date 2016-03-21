﻿using System.Linq;
using Stump.DofusProtocol.Enums;
using Stump.Server.WorldServer.Database.World;
using Stump.Server.WorldServer.Game.Actors.Fight;
using Stump.Server.WorldServer.Game.Effects.Handlers.Spells.Targets;

namespace Stump.Server.WorldServer.Game.Spells.Casts.Roublard
{
    [SpellCastHandler(SpellIdEnum.EXPLOSION_ROUBLARDE)]
    [SpellCastHandler(SpellIdEnum.AVERSE_ROUBLARDE)]
    [SpellCastHandler(SpellIdEnum.TORNADE_ROUBLARDE)]
    public class ExplosionCastHandler : DefaultSpellCastHandler
    {
        public ExplosionCastHandler(FightActor caster, Spell spell, Cell targetedCell, bool critical)
            : base(caster, spell, targetedCell, critical)
        {
        }

        public override bool Initialize()
        {
            if (base.Initialize())
            {
                foreach (var handler in Handlers.Where(x => x.Targets.OfType<StateCriterion>().Any(y => y.State == (int) SpellStatesEnum.KABOOM_92)))
                    handler.DefaultDispellableStatus = FightDispellableEnum.DISPELLABLE_BY_STRONG_DISPEL;
                return true;
            }
            return false;
        }
    }
}