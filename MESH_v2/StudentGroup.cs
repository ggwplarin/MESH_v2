using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MESH_v2
{
    public class StudentGroup
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string DisciplinesIds { get; set; }
        


        public StudentGroup(int id, string title, string disciplinesIds)
        {
            this.Id = id;
            this.Title = title;
            this.DisciplinesIds = disciplinesIds;
            



        }
    }
}
