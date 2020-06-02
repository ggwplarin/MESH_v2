using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MESH_v2
{
    public class StudentMark
    {
        public int stId { get; set; }
        public DateTimeOffset Date { get; set; }
        public int DisciplineId { get; set; }
        public string Description { get; set; }



        public StudentMark(int studentId, DateTimeOffset date, int disciplineId, string disciplinesIds,string description)
        {
            this.stId = studentId;
            this.Date = date;
            this.DisciplineId = disciplineId;
            this.Description = description;




        }
    }
}
