using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SplitUp.Web.Services
{
    public interface IUserService
    {
        User GetUserByEmail(string email);

        User Login(String email, String Password);

        void Register(User user);

        IEnumerable<string> GetAllUsersEmail();

        User GetUserById(int userId);
    }
}
