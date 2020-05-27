using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MESH_v2
{
    class Discipline
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int TeacherId { get; set; }
        public bool Inactive { get; set; }


        public Discipline(int id, string title, int teacherId, bool inactive)
        {
            this.Id = id;
            this.Title = title;
            this.TeacherId = teacherId;
            this.Inactive = inactive;



        }
    }
}
