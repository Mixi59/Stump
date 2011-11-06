using Stump.DofusProtocol.Enums;
using Stump.DofusProtocol.Messages;
using Stump.Server.WorldServer.Core.Network;
using Stump.Server.WorldServer.Database.World;
using Stump.Server.WorldServer.Worlds.Actors.Fight;

namespace Stump.Server.WorldServer.Handlers.Actions
{
    public partial class ActionsHandler : WorldHandlerContainer
    {
        public static void SendGameActionFightDeathMessage(WorldClient client, FightActor fighter)
        {
            client.Send(new GameActionFightDeathMessage(
                            (short) ActionsEnum.ACTION_CHARACTER_DEATH,
                            fighter.Id, fighter.Id
                            ));
        }

        public static void SendGameActionFightPointsVariationMessage(WorldClient client, ActionsEnum action,
                                                                     FightActor source,
                                                                     FightActor target, short delta)
        {
            client.Send(new GameActionFightPointsVariationMessage(
                            (short) action,
                            source.Id, target.Id, delta
                            ));
        }

        public static void SendGameActionFightLifePointsVariationMessage(WorldClient client, FightActor source,
                                                                         FightActor target, short delta)
        {
            client.Send(new GameActionFightLifePointsVariationMessage(
                            (short) ActionsEnum.ACTION_CHARACTER_LIFE_POINTS_LOST,
                            source.Id, target.Id, delta
                            ));
        }

        public static void SendGameActionFightReduceDamagesMessage(WorldClient client, FightActor source, FightActor target, int amount)
        {
            client.Send(new GameActionFightReduceDamagesMessage(105, source.Id, target.Id, amount));
        }

        public static void SendGameActionFightReflectSpellMessage(WorldClient client, FightActor source, FightActor target)
        {
            client.Send(new GameActionFightReflectSpellMessage((short)ActionsEnum.ACTION_CHARACTER_SPELL_REFLECTOR, source.Id, target.Id));
        }

        public static void SendGameActionFightTeleportOnSameMapMessage(WorldClient client, FightActor source, FightActor target, Cell destination)
        {
            client.Send(new GameActionFightTeleportOnSameMapMessage((short)ActionsEnum.ACTION_CHARACTER_TELEPORT_ON_SAME_MAP, source.Id, target.Id, destination.Id));
        }

        public static void SendGameActionFightSlideMessage(WorldClient client, FightActor source, FightActor target, short startCellId, short endCellId)
        {
            client.Send(new GameActionFightSlideMessage((short)ActionsEnum.ACTION_CHARACTER_PUSH, source.Id, target.Id, startCellId, endCellId));
        }
    }
}