using Microsoft.EntityFrameworkCore;
using SplitUp.Web.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SplitUp.Web.Services
{
    public class UserService : IUserService
    {
        public readonly DataContext _dbContext;

        public UserService(DataContext _dataContext)
        {
            _dbContext = _dataContext;
        }

        public User GetUserByEmail(string email) => _dbContext.Users.FirstOrDefault(u => u.Email == email);

        public User GetUserById(int userId) => _dbContext.Users.FirstOrDefault(u => u.Id == userId);


        public User Login(string email, string password)
        {
            var user = GetUserByEmail(email);
            if (user != null)
            {
                return (user.Password == password) ? user : null;
            }

            return null;
        }

        public void Register(User user)
        {
            user.Token = Guid.NewGuid().ToString();
            _dbContext.Add(user);
            _dbContext.SaveChanges();
        }

        public IEnumerable<string> GetAllUsersEmail() => _dbContext.Users.Select(u => u.Email).ToList();
    }
}
