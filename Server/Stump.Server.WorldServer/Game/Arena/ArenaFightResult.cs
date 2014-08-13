﻿using Stump.DofusProtocol.Enums;
using Stump.DofusProtocol.Types;
using Stump.Server.WorldServer.Game.Actors.Fight;
using Stump.Server.WorldServer.Game.Fights.Results;
using FightLoot = Stump.Server.WorldServer.Game.Fights.Results.FightLoot;

namespace Stump.Server.WorldServer.Game.Arena
{
    public class ArenaFightResult : FightResult<CharacterFighter>
    {
        public ArenaFightResult(CharacterFighter fighter, FightOutcomeEnum outcome, FightLoot loot, int rank)
            : base(fighter, outcome, loot)
        {
            Rank = rank;
        }

        public int Rank
        {
            get;
            private set;
        }

        public override FightResultListEntry GetFightResultListEntry()
        {
            var amount = Fighter.Character.ComputeWonArenaTokens(Rank);
            var loot = new DofusProtocol.Types.FightLoot(new[] { (short) ItemIdEnum.Kolizeton, (short)amount }, 0);

            return new FightResultPlayerListEntry((short) Outcome, loot, Id, Alive, (byte)Level,
                new FightResultAdditionalData[0]);
        }

        public override void Apply()
        {
            Fighter.Character.UpdateArenaProperties(Rank, Outcome == FightOutcomeEnum.RESULT_VICTORY);
        }
    }
}