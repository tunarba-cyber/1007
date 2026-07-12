using _1007MiniProject.Core.Entities.common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1007MiniProject.Core.Entities
{
    public class Genre : BaseEntity
    {
        public string Name { get; set; }
        public List<Movie> Movies { get; set; }

    }
}
