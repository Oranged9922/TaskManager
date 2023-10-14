
using Domain.TOTaskAggregate;

namespace Application.Common.Interfaces
{
    public interface ITOTaskRepository
    {
        TOTaskId Add(TOTask task);
        void Delete(TOTask task);
        void Delete(TOTaskId id);
        List<TOTask> GetAllTasks();
        TOTask? GetTaskById(TOTaskId id);
        void Update(TOTask task);
    }
}
