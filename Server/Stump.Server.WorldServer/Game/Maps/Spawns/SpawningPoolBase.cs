using System;
using System.Collections.Generic;
using Stump.Core.Timers;
using Stump.Server.WorldServer.Game.Actors.RolePlay;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Monsters;

namespace Stump.Server.WorldServer.Game.Maps.Spawns
{
    public enum SpawningPoolState
    {
        Stoped,
        Running,
        Paused
    }

    public abstract class SpawningPoolBase
    {
        protected SpawningPoolBase(Map map)
        {
            Map = map;
            Map.ActorLeave += OnMapActorLeave;
            Spawns = new List<MonsterGroup>();
        }

        protected SpawningPoolBase(Map map, int interval)
            : this(map)
        {
            Interval = interval;
        }

        public Map Map
        {
            get;
            protected set;
        }

        public int Interval
        {
            get;
            protected set;
        }

        public int RemainingTime
        {
            get { return State != SpawningPoolState.Running ? 0 : (int)(SpawnTimer.NextTick - DateTime.Now).TotalMilliseconds; }
        }

        protected List<MonsterGroup> Spawns
        {
            get;
            set;
        }

        public int SpawnsCount
        {
            get { return Spawns.Count; }
        }

        protected MonsterGroup NextGroup
        {
            get;
            set;
        }

        protected TimedTimerEntry SpawnTimer
        {
            get;
            set;
        }

        public SpawningPoolState State
        {
            get;
            private set;
        }

        public bool AutoSpawnEnabled
        {
            get { return State != SpawningPoolState.Stoped; }
        }

        public void StartAutoSpawn()
        {
            lock (this)
            {
                if (!Map.CanSpawnMonsters())
                {
                    return;
                }

                if (AutoSpawnEnabled)
                    return;

                ResetTimer();
                State = SpawningPoolState.Running;
                OnAutoSpawnEnabled();
            }
        }

        protected virtual void OnAutoSpawnEnabled()
        {
            SpawnNextGroup();
        }

        public void StopAutoSpawn()
        {
            lock (this)
            {
                if (!AutoSpawnEnabled)
                    return;


                if (SpawnTimer != null)
                    SpawnTimer.Dispose();

                State = SpawningPoolState.Stoped;
                OnAutoSpawnDisabled();
            }
        }

        protected virtual void OnAutoSpawnDisabled()
        {

        }

        protected void PauseAutoSpawn()
        {
            lock (this)
            {
                if (State != SpawningPoolState.Running)
                    return;

                if (SpawnTimer != null)
                    SpawnTimer.Dispose();

                State = SpawningPoolState.Paused;
            }
        }

        protected void ResumeAutoSpawn()
        {
            lock (this)
            {
                if (State != SpawningPoolState.Paused)
                    return;

                ResetTimer();
                State = SpawningPoolState.Running;
                OnAutoSpawnEnabled();
            }
        }

        private void TimerCallBack()
        {
            if (IsLimitReached())
                PauseAutoSpawn();
            else
            {
                lock (this)
                {
                    SpawnNextGroup();
                }

                if (IsLimitReached())
                    PauseAutoSpawn();
                else
                    ResetTimer();
            }
        }

        private void ResetTimer()
        {
            SpawnTimer = Map.Area.CallDelayed(GetNextSpawnInterval(), TimerCallBack);
        }

        public bool SpawnNextGroup()
        {
            var group = DequeueNextGroupToSpawn();

            if (group == null)
                return false;

            Map.Enter(group);

            OnGroupSpawned(group);

            return true;
        }

        public void SetTimer(int time)
        {
            lock (this)
            {
                Interval = time;

                ResetTimer();
            }
        }

        protected abstract bool IsLimitReached();
        protected abstract int GetNextSpawnInterval();

        protected virtual MonsterGroup DequeueNextGroupToSpawn()
        {
            return NextGroup;
        }

        public virtual void SetNextGroupToSpawn(IEnumerable<Monster> monsters)
        {
            NextGroup = new MonsterGroup(Map.GetNextContextualId(), Map.GetRandomFreePosition());

            foreach (var monster in monsters)
            {
                NextGroup.AddMonster(monster);
            }
        }

        private void OnMapActorLeave(Map map, RolePlayActor actor)
        {
            if (actor is MonsterGroup && (Spawns.Contains((MonsterGroup) actor)))
                OnGroupUnSpawned((MonsterGroup) actor);
        }

        public event Action<SpawningPoolBase, MonsterGroup> Spawned;

        protected virtual void OnGroupSpawned(MonsterGroup group)
        {
            lock (Spawns)
                Spawns.Add(group);

            NextGroup = null;

            var handler = Spawned;
            if (handler != null)
                handler(this, group);
        }

        protected virtual void OnGroupUnSpawned(MonsterGroup monster)
        {
            lock (Spawns)
                Spawns.Remove(monster);

            if (!IsLimitReached() && State == SpawningPoolState.Paused)
                ResumeAutoSpawn();
        }
    }
}