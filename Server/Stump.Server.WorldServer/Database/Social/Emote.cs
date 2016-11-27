﻿using Stump.DofusProtocol.D2oClasses;
using Stump.DofusProtocol.D2oClasses.Tools.D2o;
using Stump.DofusProtocol.Enums;
using Stump.ORM;
using Stump.ORM.SubSonic.SQLGeneration.Schema;
using Stump.Server.WorldServer.Database.I18n;
using Stump.Server.WorldServer.Game.Actors.Look;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Characters;

namespace Stump.Server.WorldServer.Database.Social
{
    public class EmoteRelator
    {
        public static string FetchQuery = "SELECT * FROM emotes";
    }


    [TableName("emotes")]
    [D2OClass("Emoticon", "com.ankamagames.dofus.datacenter.communication")]
    public class Emote : IAutoGeneratedRecord, IAssignedByD2O
    {
        private string m_name;

        [PrimaryKey("Id")]
        public int Id
        {
            get;
            set;
        }

        public EmotesEnum EmoteId => (EmotesEnum) Id;

        public int NameId
        {
            get;
            set;
        }

        public string Name => m_name ?? (m_name = TextManager.Instance.GetText(NameId));

        public bool Persistancy
        {
            get;
            set;
        }

        public bool Eight_directions
        {
            get;
            set;
        }

        public bool Aura
        {
            get;
            set;
        }

        public int Cooldown
        {
            get;
            set;
        }

        public int Duration
        {
            get;
            set;
        }

        public void AssignFields(object d2oObject)
        {
            var emote = (Emoticon) d2oObject;
            Id = (int) emote.Id;
            NameId = (int) emote.NameId;
            Persistancy = emote.Persistancy;
            Eight_directions = emote.Eight_directions;
            Aura = emote.Aura;
            Cooldown = (int) emote.Cooldown;
            Duration = (int) emote.Duration;
        }

        public ActorLook ApplyEmoteLook(Character character, ActorLook look)
        {
            ActorLook auraLook = null;

            switch (EmoteId)
            {
                case EmotesEnum.EMOTE_AURA_DE_PUISSANCE:
                    auraLook = new ActorLook(character.Level == 200 ? (short) 170 : (short) 169);
                    break;

                case EmotesEnum.EMOTE_AURA_VAMPYRIQUE:
                    auraLook = new ActorLook(171);
                    break;

                case EmotesEnum.EMOTE_AURA_BLEUTÉE_DE_L_ORNITHORYNQUE_ANCESTRAL:
                    auraLook = new ActorLook(1465);
                    break;

                case EmotesEnum.EMOTE_AURA_DE_NELWEEN:
                    auraLook = new ActorLook(1501);
                    break;

                case EmotesEnum.EMOTE_GUILD:
                {
                    if (character.Guild == null)
                        break;

                    look.AddSkin((short) character.Guild.Emblem.Template.SkinId);

                    look.AddColor(8, character.Guild.Emblem.SymbolColor);
                    look.AddColor(7, character.Guild.Emblem.BackgroundColor);
                }
                    break;
            }

            if (auraLook != null)
                look.SetSubLook(new SubActorLook(0, SubEntityBindingPointCategoryEnum.HOOK_POINT_CATEGORY_BASE_FOREGROUND, auraLook));

            return look;
        }
    }
}