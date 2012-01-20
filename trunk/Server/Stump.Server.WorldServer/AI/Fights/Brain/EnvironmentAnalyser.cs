using System;
using System.Collections.Generic;
using System.Linq;
using Stump.Core.Extensions;
using Stump.Core.Threading;
using Stump.Server.WorldServer.AI.Fights.Actions;
using Stump.Server.WorldServer.Database.World;
using Stump.Server.WorldServer.Game.Actors.Fight;
using Stump.Server.WorldServer.Game.Fights;
using Stump.Server.WorldServer.Game.Maps.Cells;
using Stump.Server.WorldServer.Game.Maps.Cells.Shapes;
using Stump.Server.WorldServer.Game.Spells;

namespace Stump.Server.WorldServer.AI.Fights.Brain
{
    public class EnvironmentAnalyser
    {
        public EnvironmentAnalyser(AIFighter fighter)
        {
            Fighter = fighter;
            CellInformationProvider = new AIFightCellsInformationProvider(Fighter.Fight, Fighter);
        }

        public AIFightCellsInformationProvider CellInformationProvider
        {
            get;
            private set;
        }

        public AIFighter Fighter
        {
            get;
            private set;
        }

        public Fight Fight
        {
            get { return Fighter.Fight; }
        }

        public Cell GetCellToCastSpell(FightActor target, Spell spell)
        {
            var cell = target.Position.Point.GetAdjacentCells(CellInformationProvider.IsCellWalkable).OrderBy(entry => entry.DistanceToCell(Fighter.Position.Point)).FirstOrDefault();

            if (cell == null)
                return default(Cell);

            return CellInformationProvider.GetCellInformation(cell.CellId).Cell;
        }

        public Cell GetCellToFlee()
        {
            var rand = new AsyncRandom();
            var movementsCells = GetMovementCells();
            var fighters = Fight.GetAllFighters(entry => entry.IsEnnemyWith(Fighter));

            var currentCellIndice = fighters.Sum(entry => entry.Position.Point.DistanceToCell(Fighter.Position.Point)); 
            var betterCell = default(Cell);
            long betterCellIndice = 0;
            for (int i = 0; i < movementsCells.Length; i++)
            {
                if (!CellInformationProvider.IsCellWalkable(movementsCells[i].Id))
                    continue;

                long indice = fighters.Sum(entry => entry.Position.Point.DistanceToCell(new MapPoint(movementsCells[i])));

                if (betterCellIndice < indice)
                {
                    betterCellIndice = indice;
                    betterCell = movementsCells[i];
                }
                else if (betterCellIndice == indice && rand.Next(2) == 0)
                    // random factory
                {
                    betterCellIndice = indice;
                    betterCell = movementsCells[i];
                }
            }

            if (currentCellIndice == betterCellIndice)
                return Fighter.Cell;

            return betterCell;
        }

        public Cell[] GetMovementCells()
        {
            return GetMovementCells(Fighter.MP);
        }

        public Cell[] GetMovementCells(int mp)
        {
            var circle = new Lozenge(0, (uint) mp);

            return circle.GetCells(Fighter.Cell, Fight.Map);
        }

        public FightActor GetNearestFighter()
        {
            return GetNearestFighter(entry => true);
        }

        public FightActor GetNearestAlly()
        {
            return GetNearestFighter(entry => entry.IsFriendlyWith(Fighter));
        }

        public FightActor GetNearestEnnemy()
        {
            return GetNearestFighter(entry => entry.IsEnnemyWith(Fighter));
        }

        public FightActor GetNearestFighter(Predicate<FightActor> predicate)
        {
            return Fight.GetAllFighters(entry => predicate(entry) && entry.IsAlive()).
                OrderBy(entry => entry.Position.Point.DistanceToCell(Fighter.Position.Point)).
                RandomElementOrDefault();
        }
    }
}