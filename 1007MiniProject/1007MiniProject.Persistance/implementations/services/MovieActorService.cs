using _1007MiniProject.Application.interfaces.repositories;
using _1007MiniProject.Application.interfaces.services;
using _1007MiniProject.Core.Entities;
using _1007MiniProject.Persistance.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1007MiniProject.Persistance.implementations.services
{
    public class MovieActorService : IMovieActorService
    {
        private readonly IRepository<Actor> _actors;
        private readonly IRepository<Movie> _movies;
        private readonly IRepository<MovieActor> _movieActors;
        private readonly IMovieService _movieservice;
        private readonly IActorService _actorService;

        public MovieActorService(
            IRepository<Actor> actors,
            IRepository<Movie> movies,
            IRepository<MovieActor> movieActors,
            IMovieService movieservice,
            IActorService actorService)
        {
            _actors = actors;
            _movies = movies;
            _movieActors = movieActors;
            _movieservice = movieservice;
            _actorService = actorService;
        }

        public void AssignActorToMovie()
        {
            int step = 1;
            int movieId = 0, actorId = 0;

            while (true)
            {
                switch (step)
                {
                    case 1:
                        _movieservice.ShowAllMoviesInline();
                        Console.Write("Enter movie ID (00 = menu): ");
                        string movieIdInput = Console.ReadLine();
                        if (movieIdInput == "00") return;
                        if (!int.TryParse(movieIdInput, out movieId) || _movies.GetById(movieId) == null)
                        {
                            ServiceUI.Error("Invalid movie ID!");
                            continue;
                        }
                        step = 2;
                        continue;

                    case 2:
                        _actorService.ShowAllActorsInline();
                        Console.Write("Enter actor ID (0 = back, 00 = menu): ");
                        string actorIdInput = Console.ReadLine();
                        if (actorIdInput == "00") return;
                        if (actorIdInput == "0") { step = 1; continue; }
                        if (!int.TryParse(actorIdInput, out actorId) || _actors.GetById(actorId) == null)
                        {
                            ServiceUI.Error("Invalid actor ID!");
                            continue;
                        }

                        if (_movieActors.Any(ma => ma.MovieId == movieId && ma.ActorId == actorId))
                        {
                            ServiceUI.Error("This actor is already assigned to this movie!");
                            continue;
                        }

                        try
                        {
                            ServiceUI.Loading("Locking in the champion pick");
                            _movieActors.Add(new MovieActor { MovieId = movieId, ActorId = actorId });
                            _movieActors.SaveChanges();
                            ServiceUI.Success("Actor assigned to movie successfully.");
                        }
                        catch (Exception ex)
                        {
                            ServiceUI.Error(ex.Message);
                        }

                        Console.Write("Press Enter to assign another, or 00 to return to menu: ");
                        if (Console.ReadLine() == "00") return;
                        step = 1;
                        continue;
                }
            }
        }

        public void ShowMovieActors()
        {
            _movieservice.ShowAllMoviesInline();

            Console.Write("Enter movie ID: ");
            string movieIdInput = Console.ReadLine();
            if (!int.TryParse(movieIdInput, out int movieId))
            {
                ServiceUI.Error("Invalid movie ID!");
                ServiceUI.Pause();
                return;
            }

            var links = _movieActors.GetAll().Where(ma => ma.MovieId == movieId).ToList();
            if (!links.Any())
            {
                ServiceUI.Empty("actors assigned to this movie");
                ServiceUI.Pause();
                return;
            }

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("──── ROSTER ────");
            Console.ResetColor();

            foreach (var link in links)
            {
                var actor = _actors.GetById(link.ActorId);
                if (actor != null)
                {
                    Console.WriteLine($"{actor.Id} - {actor.Name} {actor.Surname}");
                }
            }

            ServiceUI.Pause();
        }
    }
}