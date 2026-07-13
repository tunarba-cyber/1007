using _1007MiniProject.Application.interfaces.repositories;
using _1007MiniProject.Application.interfaces.services;
using _1007MiniProject.Core.Entities;
using _1007MiniProject.Persistance.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace _1007MiniProject.Persistance.implementations.services
{
    public class GenreService : IGenreService
    {
        private readonly IRepository<Genre> _genres;
        private const int GenreNameMaxLength = 100;
        private static readonly Regex LettersOnlyRegex = new Regex(@"^[A-Za-z]+$");

        public GenreService(IRepository<Genre> genres)
        {
            _genres = genres;
        }

        public void CreateGenre()
        {
            while (true)
            {
                ServiceUI.Header("Create Genre — Champion Class");
                Console.Write("Enter genre name (0 = back, 00 = menu): ");
                string name = Console.ReadLine();

                if (name == "0" || name == "00") return;

                if (string.IsNullOrWhiteSpace(name))
                {
                    ServiceUI.Error("Genre name cannot be empty!");
                    continue;
                }

                if (name.Length > GenreNameMaxLength)
                {
                    ServiceUI.Error($"Genre name cannot exceed {GenreNameMaxLength} characters!");
                    continue;
                }

                if (!LettersOnlyRegex.IsMatch(name))
                {
                    ServiceUI.Error("Genre name can only contain letters (no numbers, spaces, or symbols)!");
                    continue;
                }

                if (_genres.Any(g => g.Name == name))
                {
                    ServiceUI.Error("A genre with this name already exists!");
                    continue;
                }

                try
                {
                    ServiceUI.Loading("Forging a new genre in the Nexus forge");
                    _genres.Add(new Genre { Name = name });
                    _genres.SaveChanges();
                    ServiceUI.Success("Genre created successfully.");
                }
                catch (Exception ex)
                {
                    ServiceUI.Error(ex.Message);
                }

                Console.Write("Press Enter to create another, or 00 to return to menu: ");
                if (Console.ReadLine() == "00") return;
            }
        }

        public void ShowAllGenres()
        {
            ServiceUI.Header("All Genres — Champion Roster");
            ServiceUI.Loading("Scouting the Rift for genres");
            var genres = _genres.GetAll();

            if (!genres.Any())
            {
                ServiceUI.Empty("genres");
            }
            else
            {
                foreach (var genre in genres)
                {
                    Console.WriteLine($"{genre.Id} - {genre.Name}");
                }
            }

            ServiceUI.Pause();
        }
    }
}