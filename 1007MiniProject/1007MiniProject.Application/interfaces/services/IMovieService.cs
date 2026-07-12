using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1007MiniProject.Application.interfaces.services
{
    public interface IMovieService
    {
        void CreateMovie();
        void ShowAllMovies();
        void ShowMovieDetails();
        void SearchMovie();
        void DeleteMovie();
        void MovieStatistics();
        void RestoreMovie();
        void AssignActorToMovie();
        void ShowMovieActors();
    }
}
