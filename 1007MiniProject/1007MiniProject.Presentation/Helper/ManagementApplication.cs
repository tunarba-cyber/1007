using _1007MiniProject.Application.interfaces.repositories;
using _1007MiniProject.Application.interfaces.services;
using _1007MiniProject.Core.Entities;
using _1007MiniProject.Persistance.contexts;
using _1007MiniProject.Persistance.implementations.repositories;
using _1007MiniProject.Persistance.implementations.services;

namespace _1007MiniProject.Presentation.Helper
{
    internal class ManagementApplication
    {
        private readonly ManageMethods _manageMethods;

        public ManagementApplication()
        {
            var context = new AppDbContext();

            IRepository<Genre> genreRepository = new Repository<Genre>(context);
            IRepository<Actor> actorRepository = new Repository<Actor>(context);
            IRepository<Movie> movieRepository = new Repository<Movie>(context);
            IRepository<MovieActor> movieActorRepository = new Repository<MovieActor>(context);

            IGenreService genreService = new GenreService(genreRepository);
            IActorService actorService = new ActorService(actorRepository);
            IMovieService movieService = new MovieService(genreRepository, actorRepository, movieRepository, movieActorRepository, genreService, actorService);
            IMovieActorService movieActorService = new MovieActorService(actorRepository, movieRepository, movieActorRepository, movieService, actorService);

            _manageMethods = new ManageMethods(genreService, movieService, actorService, movieActorService);
        }

        public void Run()
        {
            _manageMethods.MainMenu();
        }
    }
}