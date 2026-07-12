using _1007MiniProject.Core.Entities.common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1007MiniProject.Core.Entities
{
    public class Actor : BaseEntity
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime BirthDate { get; set; }
        public string Country { get; set; }
        public List<MovieActor> MovieActors { get; set; }
    }
}
