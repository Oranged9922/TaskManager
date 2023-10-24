using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationTests.Common
{
    public static class Endpoint
    {
        public static class UserController
        {
            private static string Base { get => "/user"; }
            public static string CreateUser { get => Base + "/create"; }
            public static string LoginUser { get => Base + "/login"; }
        }
    }
}
