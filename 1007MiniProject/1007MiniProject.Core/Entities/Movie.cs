using _1007MiniProject.Core.Entities.common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1007MiniProject.Core.Entities
{
    public class Movie : BaseEntity
    {
        public string Title { get; set; }
        public DateTime ReleaseYear { get; set; }
        public decimal Duration { get; set; }
        public decimal Budget { get; set; }
        public bool IsDeleted { get; set; }
        public int GenreId { get; set; }
        public Genre Genre { get; set; }
        public List<MovieActor> MovieActors { get; set; }

    }
}
