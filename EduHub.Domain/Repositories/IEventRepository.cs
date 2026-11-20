using EduHub.Domain.Entities;

namespace EduHub.Domain.Repositories
{
    public interface IEventRepository
    {
        void Add(Event ev);
    }
}