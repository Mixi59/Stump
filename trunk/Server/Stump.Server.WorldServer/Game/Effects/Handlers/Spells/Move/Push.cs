using Stump.Core.Threading;
using Stump.DofusProtocol.Enums;
using Stump.Server.WorldServer.Database.World;
using Stump.Server.WorldServer.Game.Actors.Fight;
using Stump.Server.WorldServer.Game.Effects.Instances;
using Stump.Server.WorldServer.Game.Maps.Cells;
using Stump.Server.WorldServer.Game.Spells;
using Stump.Server.WorldServer.Handlers.Actions;

namespace Stump.Server.WorldServer.Game.Effects.Handlers.Spells.Move
{
    [EffectHandler(EffectsEnum.Effect_PushBack)]
    public class Push : SpellEffectHandler
    {
        public Push(EffectDice effect, FightActor caster, Spell spell, Cell targetedCell, bool critical)
            : base(effect, caster, spell, targetedCell, critical)
        {
        }

        public override void Apply()
        {
            var integerEffect = Effect.GenerateEffect(EffectGenerationContext.Spell) as EffectInteger;

            if (integerEffect == null)
                return;

            foreach (FightActor actor in GetAffectedActors())
            {
                if (CastPosition.Id == actor.Cell.Id)
                    continue; // in case of traps it is possible

                var castPoint = new MapPoint(CastPosition);
                var pushDirection = castPoint.OrientationTo(actor.Position.Point, false);
                var startCell = actor.Position.Point;
                var lastCell = startCell;

                for (int i = 0; i < integerEffect.Value; i++)
                {
                    var nextCell = lastCell.GetNearestCellInDirection(pushDirection);

                    if (nextCell == null || !Fight.IsCellFree(Map.Cells[nextCell.CellId]))
                    {
                        actor.InflictDamage((short) new AsyncRandom().Next(1, 5), EffectSchoolEnum.Unknown, Caster);
                        break;
                    }

                    lastCell = nextCell;
                }

                var endCell = lastCell;
                var actorCopy = actor;
                Fight.ForEach(entry => ActionsHandler.SendGameActionFightSlideMessage(entry.Client, Caster, actorCopy, startCell.CellId, endCell.CellId));

                actor.Position.Cell = Map.Cells[endCell.CellId];
            }
        }
    }
}