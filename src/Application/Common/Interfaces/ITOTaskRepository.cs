
using Domain.TOTaskAggregate;

namespace Application.Common.Interfaces
{
    public interface ITOTaskRepository
    {
        List<TOTask> GetAllTasks();
    }
}
