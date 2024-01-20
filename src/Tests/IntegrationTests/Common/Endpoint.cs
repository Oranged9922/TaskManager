
namespace IntegrationTests.Common
{
    /// <summary>
    /// This class contains all the endpoints for the API.
    /// Feel free to add more endpoints as needed.
    /// </summary>
    public static class Endpoint
    {

        public static class ErrorController
        {
            private static string Base { get => "/error"; }
            public static string Error { get => Base; }
        }

        /// <summary>
        /// This class contains all the endpoints for the UserController.
        /// </summary>
        public static class UserController
        {
            private static string Base { get => "/user"; }
            public static string CreateUser { get => $"{Base}/create"; }
            public static string LoginUser { get => $"{Base}/login"; }
        }

        /// <summary>
        /// This class contains all the endpoints for the TOProjectController.
        /// </summary>
        public static class TOProjectController
        {
            private static string Base { get => "/project"; }
            public static string CreateProject { get => $"{Base}/create"; }
        }

        public static class TOTaskController
        {
            private static string Base(Guid projectId) => $"/project/{projectId}/task";
            public static string CreateTask(Guid projectId) => $"{Base(projectId)}/create";
            public static string GetTask(Guid projectId) => $"{Base(projectId)}/get";
            public static string UpdateTask(Guid projectId) => $"{Base(projectId)}/update";
            public static string DeleteTask(Guid projectId) => $"{Base(projectId)}/delete";
        }
    }
}
