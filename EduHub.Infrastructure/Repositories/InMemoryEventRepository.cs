using System.Collections.Generic;
using EduHub.Domain.Entities;
using EduHub.Domain.Repositories;

namespace EduHub.Infrastructure.Repositories
{
    public class InMemoryEventRepository : IEventRepository
    {
        private readonly List<Event> _events = new List<Event>();

        public void Add(Event ev)
        {
            _events.Add(ev);
        }
    }
}