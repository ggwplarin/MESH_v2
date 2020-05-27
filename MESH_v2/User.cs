using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Documents;

namespace MESH_v2
{
    public class User
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public string Group { get; set; }
       

        public User(int id, string login, string password,string role, string group)
        {
            this.Id = id;
            this.Login = login;
            this.Password = password;
            this.Role = role;
            this.Group = group;
            


        }

       
    }
}
