using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MESH_v2
{
    public class User
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Access { get; set; }

        public User(int id, string login, string password, string access)
        {
            this.Id = id;
            this.Login = login;
            this.Password = password;
            this.Access = access;
        }

       
    }
}
