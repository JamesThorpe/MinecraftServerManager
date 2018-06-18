using System.Collections.Generic;
using System.Threading.Tasks;
using LiteDB;

namespace MSM.Core.Config {
    class UserRepository : IUserRepository {
        private readonly LiteDatabase _database;

        public UserRepository(LiteDatabase database)
        {
            _database = database;
        }

        public User RetrieveUser(string Id)
        {
            var users = GetUsers();
            return users.FindById(Id);
        }

        private LiteCollection<User> GetUsers()
        {
            return _database.GetCollection<User>("users");
        }

        public int CountUsers()
        {
            var users = GetUsers();
            return users.Count();
        }

        public void AddUser(User user)
        {
            var users = GetUsers();
            users.Insert(user.Id, user);
        }

        public IEnumerable<User> RetrieveUsers()
        {
            var users = GetUsers();
            return users.FindAll();
        }

        public void UpdateUser(User user)
        {
            var users = GetUsers();
            users.Update(user);
        }
    }
}