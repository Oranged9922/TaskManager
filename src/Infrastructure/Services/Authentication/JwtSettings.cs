using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services.Authentication
{
    public sealed class JwtSettings
    {
        public string Secret { get; init; } = string.Empty;

        public int ExpiryMinutes { get; init; } = 0;

        public string Issuer { get; init; } = string.Empty;

        public string Audience { get; init; } = string.Empty;
    }
}
