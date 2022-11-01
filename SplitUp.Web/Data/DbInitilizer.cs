using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SplitUp.Web.Data
{
    public class DbInitilizer
    {
        public static void SeedData(DataContext dataContext)
        {
            _SeedUsers(dataContext);
        }

        public static void _SeedUsers(DataContext dataContext)
        {
            var users = dataContext.Set<User>();
            if (users.Any())
            {
                return;
            }
            users.Add(new User {
                Id = 1,
                FullName = "Bibesh",
                Email = "bibesh.kc@selu.edu",
                Password = "bafal123",
                Token = Guid.NewGuid().ToString(),
            });
            dataContext.SaveChanges();
        }
    }
}
