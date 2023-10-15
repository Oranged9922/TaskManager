using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.User.CreateUser
{
    public record CreateUserRequest(string Username, string Password, string Email);
}
