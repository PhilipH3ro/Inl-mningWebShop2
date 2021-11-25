using Newtonsoft.Json;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DC2.UI.Model;

namespace DC2.UI.Data
{
    public class DataUser : IDataUser
    {
        private List<UserDTO> LoadUsers()
        {
            var path = @"Json\User.json";
            var jsonResponse = File.ReadAllText(path);
            var tmp = JsonConvert.DeserializeObject<IEnumerable<UserDTO>>(jsonResponse);
            return tmp.ToList();
        }

        private void SaveUsers(List<UserDTO> users)
        {
            var path = @"Json\User.json";
            var jsonResponse = JsonConvert.SerializeObject(users);
            File.WriteAllText(path, jsonResponse);
        }
        public List<UserDTO> GetAll()
        {
            var users = LoadUsers();
            return (from cust in users
                    orderby cust.UserId
                    select cust).ToList();
        }

        public UserDTO GetById(int id)
        {
            var users = LoadUsers();
            return users.FirstOrDefault(x => x.UserId == id);
        }

        public void AddUser(UserDTO user)
        {
            var users = LoadUsers();
            user.UserId = GetNewId();
            users.Add(user);

            SaveUsers(users);
        }
        public void RemoveUser(UserDTO removeUser)
        {
            var users = LoadUsers();
            var user = users.FirstOrDefault(x => x.UserId == removeUser.UserId);
            users.Remove(user);

            SaveUsers(users);
        }
        public void UpdateUser(UserDTO newUserDetails)
        {
            var users = LoadUsers();

            var user = users.FirstOrDefault(x => x.UserId == newUserDetails.UserId);
            if (user is not null)
            {
                users.Remove(user);
                users.Add(newUserDetails);
            }
            users = (from cust in users
                     orderby user.UserId
                     select cust).ToList();

            SaveUsers(users);
        }

        private int GetNewId()
        {
            var users = LoadUsers();
            var max = users.Max(x => x.UserId);
            return max + 1;
        }
    }
}
