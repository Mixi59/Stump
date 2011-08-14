using System;
using System.Collections.Generic;
using Stump.Core.Cache;
using Stump.DofusProtocol.Enums;
using Stump.DofusProtocol.Types;
using Stump.Server.WorldServer.Core.Network;
using Stump.Server.WorldServer.Database.Breeds;
using Stump.Server.WorldServer.Database.Characters;
using Stump.Server.WorldServer.Database.World;
using Stump.Server.WorldServer.Worlds.Actors.Interfaces;
using Stump.Server.WorldServer.Worlds.Actors.Stats;
using Stump.Server.WorldServer.Worlds.Breeds;
using Stump.Server.WorldServer.Worlds.Dialog;
using Stump.Server.WorldServer.Worlds.Items;
using Stump.Server.WorldServer.Worlds.Maps;
using Stump.Server.WorldServer.Worlds.Maps.Cells;
using Stump.Server.WorldServer.Worlds.Notifications;
using Stump.Server.WorldServer.Worlds.Parties;

namespace Stump.Server.WorldServer.Worlds.Actors.RolePlay.Characters
{
    public sealed class Character : Humanoid,
        IStatsOwner, IInventoryOwner
    {
        private readonly CharacterRecord m_record;

        public Character(CharacterRecord record, WorldClient client)
        {
            m_record = record;
            Client = client;

            LoadRecord();
        }

        #region Events
        public event Action<Character> LoggedIn;

        public void NotifyLoggedIn()
        {
            Action<Character> handler = LoggedIn;
            if (handler != null) handler(this);
        }

        public event Action<Character> LoggedOut;

        public void NotifyLoggedOut()
        {
            Action<Character> handler = LoggedOut;
            if (handler != null) handler(this);
        }

        #endregion

        #region Properties

        public WorldClient Client
        {
            get;
            private set;
        }

        public bool InWorld
        {
            get;
            private set;
        }

        #region Identifier
        public override int Id
        {
            get { return m_record.Id; }
            protected set
            {
                m_record.Id = value;
                base.Id = value;
            }
        }

        public override string Name
        {
            get { return m_record.Name; }
            protected set
            {
                m_record.Name = value;
                base.Name = value;
            }
        }
        #endregion

        #region Inventory
        public Inventory Inventory
        {
            get;
            private set;
        }

        #endregion

        #region Position

        public Map Map
        {
            get { return Position.Map; }
            set { Position.Map = value; }
        }

        public Cell Cell
        {
            get { return Position.Cell; }
            set { Position.Cell = value; }
        }

        public DirectionsEnum Direction
        {
            get { return Position.Direction; }
            set { Position.Direction = value; }
        }

        #endregion

        #region Dialog
        public IDialogRequest DialogRequest
        {
            get;
            private set;
        }

        public bool IsInDialogRequest
        {
            get { return DialogRequest != null; }
        }

        #endregion

        #region Party

        private readonly Dictionary<int, PartyInvitation> m_partyInvitations
            = new Dictionary<int, PartyInvitation>();

        public Party Party
        {
            get;
            private set;
        }

        public bool IsInParty
        {
            get { return Party != null; }
        }

        public bool IsPartyLeader
        {
            get { return IsInParty && Party.Leader == this; }
        }

        #endregion

        #region Apparence

        public bool CustomLookActivated
        {
            get;
            set;
        }

        public EntityLook CustomLook
        {
            get;
            set;
        }

        public EntityLook RealLook
        {
            get
            {
                return m_record.EntityLook;
            }
            protected set
            {
                m_record.EntityLook = value;
                base.Look = value;
            }
        }

        public override EntityLook Look
        {
            get
            {
                return ( CustomLookActivated && CustomLook != null ? CustomLook : RealLook );
            }
        }

        public SexTypeEnum Sex
        {
            get { return m_record.Sex; }
            private set { m_record.Sex = value; }
        }

        public PlayableBreedEnum BreedId
        {
            get { return m_record.Breed; }
            private set
            {
                m_record.Breed = value; 
                Breed = BreedManager.Instance.GetBreed(value);
            }
        }

        public Breed Breed
        {
            get;
            private set;
        }

        #endregion

        #region Stats

        public delegate void LevelChangedHandler(Character character, byte currentLevel, int difference);
        public event LevelChangedHandler LevelChanged;

        public void NotifyLevelChanged(byte currentlevel, int difference)
        {
            LevelChangedHandler handler = LevelChanged;
            if (handler != null)
                handler(this, currentlevel, difference);
        }

        public byte Level
        {
            get;
            private set;
        }

        public long Experience
        {
            get { return m_record.Experience; }
            set
            {
                m_record.Experience = value;
                if (value >= UpperBoundExperience)
                {
                    var lastLevel = Level;

                    Level = ExperienceManager.Instance.GetCharacterLevel(m_record.Experience);

                    LowerBoundExperience = ExperienceManager.Instance.GetCharacterLevelExperience(Level);
                    UpperBoundExperience = ExperienceManager.Instance.GetCharacterNextLevelExperience(Level);

                    var difference = Level - lastLevel;

                    NotifyLevelChanged(Level, difference);
                }
            }
        }

        public long LowerBoundExperience
        {
            get;
            private set;
        }

        public long UpperBoundExperience
        {
            get;
            private set;
        }

        public ushort StatsPoints
        {
            get { return m_record.StatsPoints; }
            set { m_record.StatsPoints = value; }
        }

        public ushort SpellsPoints
        {
            get { return m_record.SpellsPoints; }
            set { m_record.SpellsPoints = value; }
        }

        public short EnergyMax
        {
            get { return m_record.EnergyMax; }
            set { m_record.EnergyMax = value; }
        }

        public short Energy
        {
            get { return m_record.Energy; }
            set { m_record.Energy = value; }
        }

        public StatsFields Stats
        {
            get;
            private set;
        }

        public int LifePoints
        {
            get { return Stats[CaracteristicsEnum.Health].Total; }
        }

        public int MaxLifePoints
        {
            get { return ((StatsHealth) Stats[CaracteristicsEnum.Health]).TotalMax; }
        }

        #endregion

        #region Alignment

        public AlignmentSideEnum AlignmentSide
        {
            get;
            private set;
        }

        public byte AlignmentValue
        {
            get;
            private set;
        }

        public byte AlignmentGrade
        {
            get;
            private set;
        }

        public ushort Dishonor
        {
            get;
            private set;
        }

        public int CharacterPower
        {
            get { return Id + Level; }
        }

        #endregion

        #endregion

        #region Actions
        
        #region Dialog
        public void DisplayNotification(Notification notification)
        {
            notification.Display();
        }

        public void OpenDialogRequest(IDialogRequest dialogRequest)
        {
            if (dialogRequest.Source != this &&
                dialogRequest.Target != this)
                return;

            dialogRequest.Source.DialogRequest = dialogRequest;
            dialogRequest.Target.DialogRequest = dialogRequest;
        }

        public void CloseDialogRequest()
        {
            if (!IsInDialogRequest) 
                return;

            var dialog = DialogRequest;

            dialog.Target.DialogRequest = null;
            dialog.Source.DialogRequest = null;
        }

        public void AcceptRequest()
        {
            if (!IsInDialogRequest)
                return;

            if (DialogRequest.Target == this)
                DialogRequest.AcceptDialog();
        }

        public void DeniedRequest()
        {
            if (!IsInDialogRequest)
                return;

            if (DialogRequest.Target == this)
                DialogRequest.DeniedDialog();
        }

        public void CancelRequest()
        {
            if (!IsInDialogRequest)
                return;

            if (DialogRequest.Source == this)
                DialogRequest.CancelDialog();
        }

        #endregion

        #region Party
        public void Invite(Character target)
        {
            if (!IsInParty)
            {
                var party = PartyManager.Instance.Create(this);

                EnterParty(party);
            }

            if (target.m_partyInvitations.ContainsKey(Party.Id))
                return; // already invited

            var invitation = new PartyInvitation(Party, this, target);
            target.m_partyInvitations.Add(Party.Id, invitation);

            Party.AddGuest(target);
            invitation.Display();
        }

        public PartyInvitation GetInvitation(int id)
        {
            return m_partyInvitations.ContainsKey(id) ? m_partyInvitations[id] : null;
        }

        public bool RemoveInvitation(PartyInvitation invitation)
        {
            return m_partyInvitations.Remove(invitation.Party.Id);
        }

        public void EnterParty(Party party)
        {
            if (IsInParty)
                LeaveParty();

            if (m_partyInvitations.ContainsKey(party.Id))
                m_partyInvitations.Remove(party.Id);

            foreach (var partyInvitation in m_partyInvitations)
            {
                partyInvitation.Value.Deny();
            }

            Party = party;
            Party.MemberRemoved += OnPartyMemberRemoved;
            Party.PartyDeleted += OnPartyDeleted;

            if (party.IsMember(this))
                return;

            if (!party.PromoteGuestToMember(this))
            {
                Party.MemberRemoved -= OnPartyMemberRemoved;
                Party.PartyDeleted -= OnPartyDeleted;
                Party = null;
            }
        }

        public void LeaveParty()
        {
            if (!IsInParty)
                return;

            Party.MemberRemoved -= OnPartyMemberRemoved;
            Party.PartyDeleted -= OnPartyDeleted;
            Party.RemoveMember(this);
            Party = null;
        }

        private void OnPartyMemberRemoved(Party party, Character member, bool kicked)
        {
            if (member != this)
                return;

            Party.MemberRemoved -= OnPartyMemberRemoved;
            Party.PartyDeleted -= OnPartyDeleted;
            Party = null;
        }

        private void OnPartyDeleted(Party party)
        {
            Party.MemberRemoved -= OnPartyMemberRemoved;
            Party.PartyDeleted -= OnPartyDeleted;
            Party = null;
        }


        #endregion

        #endregion

        #region Save & Load

        /// <summary>
        ///   Spawn the character on the map. It can be called once.
        /// </summary>
        public void LogIn()
        {
            if (InWorld)
                return;

            Map.Enter(this);
            World.Instance.Enter(this);

            // todo: send MOTD

            InWorld = true;
        }

        public void LogOut()
        {
            if (InWorld)
            {
                if (Map != null)
                    Map.Leave(this);

                if (IsInParty)
                    LeaveParty();

                World.Instance.Leave(this);
            }

            SaveLater();
        }

        public void SaveLater()
        {
            WorldServer.Instance.IOTaskPool.EnqueueTask(SaveNow);
        }

        public void SaveNow()
        {
            m_record.MapId = Map.Id;
            m_record.CellId = Cell.Id;
            m_record.Direction = Direction;

            m_record.AP = (ushort) Stats[CaracteristicsEnum.AP].Base;
            m_record.MP = (ushort) Stats[CaracteristicsEnum.MP].Base;
            m_record.Strength = Stats[CaracteristicsEnum.Strength].Base;
            m_record.Agility = Stats[CaracteristicsEnum.Agility].Base;
            m_record.Chance = Stats[CaracteristicsEnum.Chance].Base;
            m_record.Intelligence = Stats[CaracteristicsEnum.Intelligence].Base;
            m_record.Wisdom = Stats[CaracteristicsEnum.Wisdom].Base;
            m_record.BaseHealth = (ushort) (Stats[CaracteristicsEnum.Health] as StatsHealth).Base;
            m_record.DamageTaken = (ushort) ( Stats[CaracteristicsEnum.Health] as StatsHealth ).DamageTaken;


            m_record.Save();
        }

        private void LoadRecord()
        {
            Breed = BreedManager.Instance.GetBreed(BreedId);

            Map map = World.Instance.GetMap(m_record.MapId);
            Position = new ObjectPosition(
                map,
                map.Cells[m_record.CellId],
                m_record.Direction);

            Stats = new StatsFields(this, m_record);
            Level = ExperienceManager.Instance.GetCharacterLevel(m_record.Experience);
            LowerBoundExperience = ExperienceManager.Instance.GetCharacterLevelExperience(Level);
            UpperBoundExperience = ExperienceManager.Instance.GetCharacterNextLevelExperience(Level);

            Inventory = new Inventory(this, m_record.Inventory);
        }

        #endregion

        #region Network

        #region GameRolePlayCharacterInformations
        public override GameContextActorInformations GetGameContextActorInformations()
        {
            return new GameRolePlayCharacterInformations(
                Id,
                Look,
                GetEntityDispositionInformations(),
                Name,
                GetHumanInformations(),
                GetActorAlignmentInformations());
        }
        #endregion

        #region ActorAlignmentInformations

        public ActorAlignmentInformations GetActorAlignmentInformations()
        {
            return new ActorAlignmentInformations(
                (byte)AlignmentSide,
                AlignmentValue,
                AlignmentGrade,
                Dishonor,
                CharacterPower);
        }

        #endregion

        #region ActorExtendedAlignmentInformations

        public ActorExtendedAlignmentInformations GetActorAlignmentExtendInformations()
        {
            return new ActorExtendedAlignmentInformations(
                            (byte)AlignmentSide,
                            AlignmentValue,
                            AlignmentGrade,
                            Dishonor,
                            CharacterPower,
                            0,
                            0,
                            0,
                            false
                            );
        }

        #endregion

        #region CharacterBaseInformations

        public CharacterBaseInformations GetCharacterBaseInformations()
        {
            return new CharacterBaseInformations(
                Id,
                Level,
                Name,
                Look,
                (byte) BreedId,
                Sex == SexTypeEnum.SEX_FEMALE);
        }

        #endregion

        #region PartyMemberInformations

        public PartyInvitationMemberInformations GetPartyInvitationMemberInformations()
        {
            return new PartyInvitationMemberInformations(
                Id,
                Level,
                Name,
                Look,
                (byte)BreedId,
                Sex == SexTypeEnum.SEX_FEMALE,
                (short) Map.Position.X,
                (short) Map.Position.Y,
                Map.Id);
        }

        public PartyMemberInformations GetPartyMemberInformations()
        {
            return new PartyMemberInformations(
                Id,
                Level,
                Name,
                Look,
                LifePoints,
                MaxLifePoints,
                (short) Stats[CaracteristicsEnum.Prospecting].Total,
                0,
                (short) Stats[CaracteristicsEnum.Initiative].Total,
                false,
                0);
        }

        public PartyGuestInformations GetPartyGuestInformations(Party party)
        {
            if (!m_partyInvitations.ContainsKey(party.Id))
                return new PartyGuestInformations();

            var invitation = m_partyInvitations[party.Id];

            return new PartyGuestInformations(
                Id,
                invitation.Source.Id,
                Name,
                Look);
        }

        #endregion

        #endregion
    }
}