namespace Contracts.TOTask.CreateTOTask
{
    public record CreateTOTaskRequest(string Name, string Description, string ProjectId = "", string UserId = "");
}
