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
    public class GenreService : IGenreService
    {
        private readonly IRepository<Genre> _genres;
        private const int GenreNameMaxLength = 100;

        public GenreService(IRepository<Genre> genres)
        {
            _genres = genres;
        }

        public void CreateGenre()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("--- Create Genre ---");
                Console.Write("Enter genre name (0 = back, 00 = menu): ");
                string name = Console.ReadLine();

                if (name == "0" || name == "00") return;

                if (string.IsNullOrWhiteSpace(name))
                {
                    Console.WriteLine("Error: Genre name cannot be empty!");
                    continue;
                }

                if (_genres.Any(g => g.Name == name))
                {
                    Console.WriteLine("Error: A genre with this name already exists!");
                    continue;
                }

                try
                {
                    _genres.Add(new Genre { Name = name });
                    _genres.SaveChanges();
                    Console.WriteLine("Genre created successfully.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }

                Console.Write("Press Enter to create another, or 00 to return to menu: ");
                if (Console.ReadLine() == "00") return;
            }
        }
        public void ShowAllGenres()
        {
            Console.Clear();
            Console.WriteLine("--- All Genres ---");
            var genres = _genres.GetAll();

            if (!genres.Any())
            {
                Console.WriteLine("No genres found.");
                return;
            }

            foreach (var genre in genres)
            {
                Console.WriteLine($"{genre.Id} - {genre.Name}");
            }
        }
    }
}