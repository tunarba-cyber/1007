using _1007MiniProject.Application.interfaces.services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1007MiniProject.Presentation.Helper
{
    public class ManageMethods
    {
        private readonly IGenreService _genreservice;
        private readonly IMovieService _movieservice;
        private readonly IActorService _actorService;
        private readonly IMovieActorService _movieActorService;  // was IMovieService

        public ManageMethods(
            IGenreService genreservice,
            IMovieService movieservice,
            IActorService actorService,
            IMovieActorService movieActorService)
        {
            _genreservice = genreservice;
            _movieservice = movieservice;
            _actorService = actorService;
            _movieActorService = movieActorService;
        }
        public void MainMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("========== MOVIE MANAGEMENT SYSTEM ==========");
                Console.WriteLine("1.  Create Genre");
                Console.WriteLine("2.  Create Actor");
                Console.WriteLine("3.  Create Movie");
                Console.WriteLine("4.  Show All Genres");
                Console.WriteLine("5.  Show All Actors");
                Console.WriteLine("6.  Show All Movies");
                Console.WriteLine("7.  Show Movie Details");
                Console.WriteLine("8.  Assign Actor To Movie");
                Console.WriteLine("9.  Show Movie Actors");
                Console.WriteLine("10. Search Movie");
                Console.WriteLine("11. Movie Statistics");
                Console.WriteLine("12. Delete Movie (Soft Delete)");
                Console.WriteLine("13. Restore Movie");
                Console.WriteLine("14. Update Genre");
                Console.WriteLine("0.  Exit");
                Console.WriteLine("==============================================");
                Console.Write("Select an option: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        _genreservice.CreateGenre();
                        break;
                    case "2":
                        _actorService.CreateActor();
                        break;
                    case "3":
                        _movieservice.CreateMovie();
                        break;
                    case "4":
                        _genreservice.ShowAllGenres();
                        break;
                    case "5":
                        _actorService.ShowAllActors();
                        break;
                    case "6":
                        _movieservice.ShowAllMovies();
                        break;
                    case "7":
                        _movieservice.ShowMovieDetails();
                        break;
                    case "8":
                        _movieActorService.AssignActorToMovie();
                        break;
                    case "9":
                        _movieActorService.ShowMovieActors();
                        break;
                    case "10":
                        _movieservice.SearchMovie();
                        break;
                    case "11":
                        _movieservice.MovieStatistics();
                        break;
                    case "12":
                        _movieservice.DeleteMovie();
                        break;
                    case "13":
                        _movieservice.RestoreMovie();
                        break;
                    case "0":
                        Console.WriteLine("Exiting application. Goodbye!");
                        return;
                    default:
                        Console.WriteLine("Invalid option! Press Enter to try again.");
                        Console.ReadLine();
                        break;
                }
            }
        }
    }
}
