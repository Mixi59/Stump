using System;
using System.Collections.Generic;
using System.Linq;
using Stump.Core.Pool;
using Stump.Core.Reflection;
using Stump.Server.BaseServer.Initialization;
using Stump.Server.WorldServer.Database.Interactives;

namespace Stump.Server.WorldServer.Worlds.Interactives
{
    public class InteractiveManager : Singleton<InteractiveManager>
    {
        private readonly UniqueIdProvider m_idProvider = new UniqueIdProvider();
        private Dictionary<int, InteractiveSpawn> m_interactivesSpawns;
        private Dictionary<int, InteractiveTemplate> m_interactivesTemplates;

        [Initialization(InitializationPass.Fourth)]
        public void Initialize()
        {
            m_interactivesTemplates = InteractiveTemplate.FindAll().ToDictionary(entry => entry.Id);
            m_interactivesSpawns = InteractiveSpawn.FindAll().ToDictionary(entry => entry.Id);
        }

        public int PopSkillId()
        {
            return m_idProvider.Pop();
        }

        public void FreeSkillId(int id)
        {
            m_idProvider.Push(id);
        }

        public IEnumerable<InteractiveSpawn> GetInteractiveSpawns()
        {
            return m_interactivesSpawns.Values;
        }

        public InteractiveSpawn GetOneSpawn(Predicate<InteractiveSpawn> predicate)
        {
            return m_interactivesSpawns.Values.Where(entry => predicate(entry)).FirstOrDefault();
        }

        public InteractiveTemplate GetTemplate(int id)
        {
            InteractiveTemplate template;
            if (m_interactivesTemplates.TryGetValue(id, out template))
                return template;

            return template;
        }

        public void AddInteractiveSpawn(InteractiveSpawn spawn)
        {
            spawn.Save();

            m_interactivesSpawns.Add(spawn.Id, spawn);
        }
    }
}