using _1007MiniProject.Application.interfaces.repositories;
using _1007MiniProject.Application.interfaces.services;
using _1007MiniProject.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1007MiniProject.Persistance.implementations.services
{
    public class MovieService
    {
        private readonly IRepository<Genre> _genres;
        private readonly IRepository<Actor> _actors;
        private readonly IRepository<Movie> _movies;
        private readonly IRepository<MovieActor> _movieActors;
        private readonly IGenreService _genreservice;
        private readonly IMovieService _movieservice;
        private readonly IActorService _actorService;
        

        public void CreateMovie()
        {
            int step = 1;
            string title = null;
            DateTime releaseYear = default;
            decimal duration = 0, budget = 0;
            int genreId = 0;

            while (true)
            {
                switch (step)
                {
                    case 1:
                        Console.Write("Enter movie title (00 = menu): ");
                        string titleInput = Console.ReadLine();
                        if (titleInput == "00") return;
                        if (string.IsNullOrWhiteSpace(titleInput))
                        {
                            Console.WriteLine("Error: Title cannot be empty!");
                            continue;
                        }
                        title = titleInput;
                        step = 2;
                        continue;

                    case 2:
                        Console.Write("Enter release year yyyy-MM-dd (0 = back, 00 = menu): ");
                        string releaseInput = Console.ReadLine();
                        if (releaseInput == "00") return;
                        if (releaseInput == "0") { step = 1; continue; }
                        if (!DateTime.TryParse(releaseInput, out releaseYear))
                        {
                            Console.WriteLine("Error: Invalid release year!");
                            continue;
                        }
                        step = 3;
                        continue;

                    case 3:
                        Console.Write("Enter duration in minutes (0 = back, 00 = menu): ");
                        string durationInput = Console.ReadLine();
                        if (durationInput == "00") return;
                        if (durationInput == "0") { step = 2; continue; }
                        if (!decimal.TryParse(durationInput, out duration) || duration <= 0)
                        {
                            Console.WriteLine("Error: Invalid duration!");
                            continue;
                        }
                        step = 4;
                        continue;

                    case 4:
                        Console.Write("Enter budget (0 = back, 00 = menu): ");
                        string budgetInput = Console.ReadLine();
                        if (budgetInput == "00") return;
                        if (budgetInput == "0") { step = 3; continue; }
                        if (!decimal.TryParse(budgetInput, out budget) || budget < 0)
                        {
                            Console.WriteLine("Error: Invalid budget!");
                            continue;
                        }
                        step = 5;
                        continue;

                    case 5:
                        _genreservice.ShowAllGenres();
                        Console.Write("Enter genre ID (0 = back, 00 = menu): ");
                        string genreIdInput = Console.ReadLine();
                        if (genreIdInput == "00") return;
                        if (genreIdInput == "0") { step = 4; continue; }
                        if (!int.TryParse(genreIdInput, out genreId) || _genres.GetById(genreId) == null)
                        {
                            Console.WriteLine("Error: Invalid genre ID!");
                            continue;
                        }
                        step = 6;
                        continue;

                    case 6:
                        var movie = new Movie
                        {
                            Title = title,
                            ReleaseYear = releaseYear,
                            Duration = duration,
                            Budget = budget,
                            GenreId = genreId
                        };
                        try
                        {
                            _movies.Add(movie);
                            _movies.SaveChanges();
                            Console.WriteLine("Movie created successfully.");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Error: {ex.Message}");
                        }

                        Console.Write("Press Enter to create another, or 00 to return to menu: ");
                        if (Console.ReadLine() == "00") return;
                        step = 1;
                        continue;
                }
            }
        }
        public void ShowAllMovies()
        {
            Console.WriteLine("--- All Movies ---");
            var movies = _movies.GetAll().Where(m => !m.IsDeleted).ToList();

            if (!movies.Any())
            {
                Console.WriteLine("No movies found.");
                return;
            }

            foreach (var movie in movies)
            {
                var genre = _genres.GetById(movie.GenreId);
                Console.WriteLine($"{movie.Id} - {movie.Title} ({movie.ReleaseYear:yyyy}) - Genre: {genre?.Name ?? "Unknown"}");
            }
        }
        public void ShowMovieDetails()
        {
            Console.WriteLine("--- Movie Details ---");
            ShowAllMovies();

            Console.Write("Enter movie ID: ");
            string idInput = Console.ReadLine();
            if (!int.TryParse(idInput, out int movieId))
            {
                Console.WriteLine("Error: Invalid movie ID!");
                return;
            }

            var movie = _movies.GetById(movieId);
            if (movie == null || movie.IsDeleted)
            {
                Console.WriteLine("Error: Movie not found!");
                return;
            }

            var genre = _genres.GetById(movie.GenreId);

            Console.WriteLine($"Title: {movie.Title}");
            Console.WriteLine($"Release Year: {movie.ReleaseYear:yyyy}");
            Console.WriteLine($"Duration: {movie.Duration} minutes");
            Console.WriteLine($"Budget: {movie.Budget:C}");
            Console.WriteLine($"Genre: {genre?.Name ?? "Unknown"}");
        }
        public void SearchMovie()
        {
            while (true)
            {
                Console.Write("Enter title keyword (00 = menu): ");
                string keyword = Console.ReadLine();
                if (keyword == "00") return;

                if (string.IsNullOrWhiteSpace(keyword))
                {
                    Console.WriteLine("Error: Search keyword cannot be empty!");
                    continue;
                }

                var results = _movies.GetAll()
                    .Where(m => !m.IsDeleted && m.Title.Contains(keyword, StringComparison.OrdinalIgnoreCase))
                    .ToList();

                if (!results.Any())
                    Console.WriteLine("No movies found matching your search.");
                else
                    foreach (var movie in results)
                        Console.WriteLine($"{movie.Id} - {movie.Title} ({movie.ReleaseYear:yyyy})");

                Console.Write("Press Enter to search again, or 00 to return to menu: ");
                if (Console.ReadLine() == "00") return;
            }
        }
        public void MovieStatistics()
        {
            Console.WriteLine("--- Movie Statistics ---");
            var movies = _movies.GetAll().Where(m => !m.IsDeleted).ToList();

            if (!movies.Any())
            {
                Console.WriteLine("No movies found.");
                return;
            }

            Console.WriteLine($"Total Movies: {movies.Count}");
            Console.WriteLine($"Average Duration: {movies.Average(m => m.Duration):F2} minutes");
            Console.WriteLine($"Average Budget: {movies.Average(m => m.Budget):C}");
            Console.WriteLine($"Highest Budget Movie: {movies.OrderByDescending(m => m.Budget).First().Title}");
            Console.WriteLine($"Lowest Budget Movie: {movies.OrderBy(m => m.Budget).First().Title}");

            Console.WriteLine("Movies per Genre:");
            var genreGroups = movies.GroupBy(m => m.GenreId);
            foreach (var group in genreGroups)
            {
                var genre = _genres.GetById(group.Key);
                Console.WriteLine($"- {genre?.Name ?? "Unknown"}: {group.Count()}");
            }
        }
        public void DeleteMovie()
        {
            while (true)
            {
                ShowAllMovies();
                Console.Write("Enter movie ID to delete (00 = menu): ");
                string idInput = Console.ReadLine();
                if (idInput == "00") return;

                if (!int.TryParse(idInput, out int movieId))
                {
                    Console.WriteLine("Error: Invalid movie ID!");
                    continue;
                }

                var movie = _movies.GetById(movieId);
                if (movie == null || movie.IsDeleted)
                {
                    Console.WriteLine("Error: Movie not found!");
                    continue;
                }

                movie.IsDeleted = true;

                try
                {
                    _movies.Update(movie);
                    _movies.SaveChanges();
                    Console.WriteLine("Movie deleted successfully.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }

                Console.Write("Press Enter to delete another, or 00 to return to menu: ");
                if (Console.ReadLine() == "00") return;
            }
        }
        public void RestoreMovie()
        {
            while (true)
            {
                var deletedMovies = _movies.GetAll().Where(m => m.IsDeleted).ToList();

                if (!deletedMovies.Any())
                {
                    Console.WriteLine("No deleted movies found.");
                    Console.Write("Press Enter to check again, or 00 to return to menu: ");
                    if (Console.ReadLine() == "00") return;
                    continue;
                }

                foreach (var m in deletedMovies)
                    Console.WriteLine($"{m.Id} - {m.Title}");

                Console.Write("Enter movie ID to restore (00 = menu): ");
                string idInput = Console.ReadLine();
                if (idInput == "00") return;

                if (!int.TryParse(idInput, out int movieId))
                {
                    Console.WriteLine("Error: Invalid movie ID!");
                    continue;
                }

                var movie = deletedMovies.FirstOrDefault(m => m.Id == movieId);
                if (movie == null)
                {
                    Console.WriteLine("Error: Movie not found in deleted list!");
                    continue;
                }

                movie.IsDeleted = false;

                try
                {
                    _movies.Update(movie);
                    _movies.SaveChanges();
                    Console.WriteLine("Movie restored successfully.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }

                Console.Write("Press Enter to restore another, or 00 to return to menu: ");
                if (Console.ReadLine() == "00") return;
            }
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
                        _movieservice.ShowAllMovies();
                        Console.Write("Enter movie ID (00 = menu): ");
                        string movieIdInput = Console.ReadLine();
                        if (movieIdInput == "00") return;
                        if (!int.TryParse(movieIdInput, out movieId) || _movies.GetById(movieId) == null)
                        {
                            Console.WriteLine("Error: Invalid movie ID!");
                            continue;
                        }
                        step = 2;
                        continue;

                    case 2:
                        _actorService.ShowAllActors();
                        Console.Write("Enter actor ID (0 = back, 00 = menu): ");
                        string actorIdInput = Console.ReadLine();
                        if (actorIdInput == "00") return;
                        if (actorIdInput == "0") { step = 1; continue; }
                        if (!int.TryParse(actorIdInput, out actorId) || _actors.GetById(actorId) == null)
                        {
                            Console.WriteLine("Error: Invalid actor ID!");
                            continue;
                        }

                        if (_movieActors.Any(ma => ma.MovieId == movieId && ma.ActorId == actorId))
                        {
                            Console.WriteLine("Error: This actor is already assigned to this movie!");
                            continue;
                        }

                        try
                        {
                            _movieActors.Add(new MovieActor { MovieId = movieId, ActorId = actorId });
                            _movieActors.SaveChanges();
                            Console.WriteLine("Actor assigned to movie successfully.");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Error: {ex.Message}");
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
            Console.WriteLine("--- Movie Actors ---");
            _movieservice.ShowAllMovies();

            Console.Write("Enter movie ID: ");
            string movieIdInput = Console.ReadLine();
            if (!int.TryParse(movieIdInput, out int movieId))
            {
                Console.WriteLine("Error: Invalid movie ID!");
                return;
            }

            var links = _movieActors.GetAll().Where(ma => ma.MovieId == movieId).ToList();
            if (!links.Any())
            {
                Console.WriteLine("No actors assigned to this movie.");
                return;
            }

            foreach (var link in links)
            {
                var actor = _actors.GetById(link.ActorId);
                if (actor != null)
                {
                    Console.WriteLine($"{actor.Id} - {actor.Name} {actor.Surname}");
                }
            }
        }
    }
}
