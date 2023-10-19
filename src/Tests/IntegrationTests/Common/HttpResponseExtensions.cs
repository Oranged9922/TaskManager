using System.Net.Http;
using System.Threading.Tasks;
using System.IdentityModel.Tokens.Jwt;
using Newtonsoft.Json;
using Application.UserLogic.Commands.CreateUser;

namespace IntegrationTests.Common
{

    public static class HttpResponseExtensions
    {
        public static async Task<JwtSecurityToken> ExtractAndParseJwtTokenAsync(this HttpResponseMessage response)
        {
            if (!response.IsSuccessStatusCode)
                throw new HttpRequestException($"Response status code does not indicate success: {(int)response.StatusCode} ({response.ReasonPhrase}).");

            var content = await response.Content.ReadAsStringAsync();
            var responseObject = JsonConvert.DeserializeObject<CreateUserCommandResponse>(content);

            var jwtToken = new JwtSecurityToken(responseObject!.JwtToken);
            return jwtToken;
        }
    }
}
