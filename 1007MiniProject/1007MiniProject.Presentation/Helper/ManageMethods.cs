using _1007MiniProject.Application.interfaces.services;
using _1007MiniProject.Presentation.UI;
using System;

namespace _1007MiniProject.Presentation.Helper
{
    public class ManageMethods
    {
        private readonly IGenreService _genreservice;
        private readonly IMovieService _movieservice;
        private readonly IActorService _actorService;
        private readonly IMovieActorService _movieActorService;

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
            MenuUI.SplashScreen();

            while (true)
            {
                MenuUI.MainMenuHeader();
                MenuUI.PrintMenuOptions();

                string choice = MenuUI.PromptChoice();

                switch (choice)
                {
                    case "1": _genreservice.CreateGenre(); break;
                    case "2": _actorService.CreateActor(); break;
                    case "3": _movieservice.CreateMovie(); break;
                    case "4": _genreservice.ShowAllGenres(); break;
                    case "5": _actorService.ShowAllActors(); break;
                    case "6": _movieservice.ShowAllMovies(); break;
                    case "7": _movieservice.ShowMovieDetails(); break;
                    case "8": _movieActorService.AssignActorToMovie(); break;
                    case "9": _movieActorService.ShowMovieActors(); break;
                    case "10": _movieservice.SearchMovie(); break;
                    case "11": _movieservice.MovieStatistics(); break;
                    case "12": _movieservice.DeleteMovie(); break;
                    case "13": _movieservice.RestoreMovie(); break;
                    case "0": MenuUI.ExitAnimation(); return;
                    default: MenuUI.InvalidOption(); break;
                }
            }
        }
    }
}