using DC2.UI.Model;
using System.Collections.Generic;

namespace DC2.UI.Data
{
    public interface IDataUser
    {
        void AddUser(UserDTO user);
        List<UserDTO> GetAll();
        UserDTO GetById(int id);
        void RemoveUser(UserDTO removeUser);
        void UpdateUser(UserDTO newUserDetails);
    }
}