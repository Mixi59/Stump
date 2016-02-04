﻿using Stump.DofusProtocol.Enums;
using Stump.Server.WorldServer.Game.Actors.Fight;
using Stump.Server.WorldServer.Game.Fights;
using Stump.Server.WorldServer.Game.Fights.Teams;
using Stump.Server.WorldServer.Game.Spells;

namespace Stump.Server.WorldServer.AI.Fights.Brain.Custom.Invocations
{
    [BrainIdentifier((int)MonsterIdEnum.ARBRE_282)]
    public class TreeBrain : Brain
    {
        private static Spell m_transformationSpell;

        public TreeBrain(AIFighter fighter)
            : base(fighter)
        {
            if (m_transformationSpell == null)
                m_transformationSpell = new Spell((int) SpellIdEnum.FEUILLAGE, 1);

            fighter.Team.FighterAdded += OnFighterAdded;
        }

        private void OnFighterAdded(FightTeam team, FightActor fighter)
        {
            if (Fighter != fighter)
                return;

            Fighter.CastSpell(m_transformationSpell, Fighter.Cell, true);
            fighter.Team.FighterAdded -= OnFighterAdded;
        }

        public override void Play()
        {
        }
    }
}