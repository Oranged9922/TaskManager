using Application.Common.Interfaces;
using Domain.UserAggregate;
using ErrorOr;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
namespace Infrastructure.Persistance.Repositories
{
    public class UserRepository(TaskOrganizerDbContext dbContext) : IUserRepository
    {
        private readonly TaskOrganizerDbContext _context = dbContext;

        public ErrorOr<User> GetCurrentUser(string jwtToken)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtSecurityToken = tokenHandler.ReadToken(jwtToken) as JwtSecurityToken;

            var userIdClaim = jwtSecurityToken?.Claims.First(claim => claim.Type == "id");
            var roleClaim = jwtSecurityToken?.Claims.First(claim => claim.Type == "role");

            if (userIdClaim == null || roleClaim == null)
            {
                return Domain.Common.Errors.Validation.InvalidToken;
            }

            var userId = new UserId(Guid.Parse(userIdClaim.Value));

            var user = _context.Users.First(u => u.Id == userId);

            if (user is null)
            {
                return Domain.Common.Errors.Repository.EntityDoesNotExist;
            }
            return user;
        }

        public UserId Add(User user)
        {
            _context.Add(user);
            _context.SaveChanges();
            return user.Id;
        }

        public User? Get(UserId id)
        {
            return _context.Users
                .Include(u => u.AssignedTasks)
                .Include(u => u.CreatedTasks)
                .SingleOrDefault(u => u.Id == id);
        }

        public User? GetByUsername(string username)
        {
            return _context.Users
                .Include(u => u.AssignedTasks)
                .Include(u => u.CreatedTasks)
                .SingleOrDefault(u => u.Username == username);
        }

        public void Update(User user)
        {
            _context.Update(user);
            _context.SaveChanges();
        }

        public void Delete(User user)
        {
            _context.Remove(user);
            _context.SaveChanges();
        }

        public void Delete(UserId id)
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == id);
            if (user is not null)
            {
                _context.Remove(user);
                _context.SaveChanges();
            }
        }

        public bool Exists(UserId id)
        {
            return _context.Users.Any(u => u.Id == id);
        }

        public bool Exists(string username)
        {
            return _context.Users.Any(u => u.Username == username);
        }
    }
