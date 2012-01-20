using System;
using Castle.ActiveRecord;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Characters;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Npcs;

namespace Stump.Server.WorldServer.Database.Npcs.Replies
{
    [ActiveRecord(DiscriminatorValue = "EndDialog")]
    public class EndDialogReply : NpcReply
    {
        public override void Execute(Npc npc, Character character)
        {
            character.LeaveDialog();
        }


    }
}