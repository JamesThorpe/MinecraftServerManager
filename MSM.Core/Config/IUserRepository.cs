using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MSM.Core.Config
{
    public interface IUserRepository
    {
        User RetrieveUser(string Id);
        int CountUsers();
        void AddUser(User user);
        IEnumerable<User> RetrieveUsers();
        void UpdateUser(User user);
    }
}
