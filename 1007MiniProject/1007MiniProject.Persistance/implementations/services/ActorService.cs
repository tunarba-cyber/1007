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
    public class ActorService : IActorService
    {
        private readonly IRepository<Actor> _actors;
        private static readonly Regex LettersOnlyRegex = new Regex(@"^[A-Za-z]+$");

        public ActorService(IRepository<Actor> actors)
        {
            _actors = actors;
        }

        public void CreateActor()
        {
            int step = 1;
            string name = null, surname = null, country = null;
            DateTime birthDate = default;

            while (true)
            {
                ServiceUI.Header("Create Actor — Summoner Registration");
                switch (step)
                {
                    case 1:
                        Console.Write("Enter actor name (00 = menu): ");
                        string nameInput = Console.ReadLine();
                        if (nameInput == "00") return;
                        if (string.IsNullOrWhiteSpace(nameInput))
                        {
                            ServiceUI.Error("Name cannot be empty!");
                            continue;
                        }
                        if (!LettersOnlyRegex.IsMatch(nameInput))
                        {
                            ServiceUI.Error("Name can only contain letters (no numbers, spaces, or symbols)!");
                            continue;
                        }
                        name = nameInput;
                        step = 2;
                        continue;

                    case 2:
                        Console.Write("Enter actor surname (0 = back, 00 = menu): ");
                        string surnameInput = Console.ReadLine();
                        if (surnameInput == "00") return;
                        if (surnameInput == "0") { step = 1; continue; }
                        if (string.IsNullOrWhiteSpace(surnameInput))
                        {
                            ServiceUI.Error("Surname cannot be empty!");
                            continue;
                        }
                        if (!LettersOnlyRegex.IsMatch(surnameInput))
                        {
                            ServiceUI.Error("Surname can only contain letters (no numbers, spaces, or symbols)!");
                            continue;
                        }
                        surname = surnameInput;
                        step = 3;
                        continue;

                    case 3:
                        Console.Write("Enter birth date yyyy-MM-dd (0 = back, 00 = menu): ");
                        string birthInput = Console.ReadLine();
                        if (birthInput == "00") return;
                        if (birthInput == "0") { step = 2; continue; }
                        if (!DateTime.TryParse(birthInput, out birthDate))
                        {
                            ServiceUI.Error("Invalid birth date!");
                            continue;
                        }
                        if (birthDate > DateTime.Today)
                        {
                            ServiceUI.Error("Birth date cannot be in the future!");
                            continue;
                        }
                        step = 4;
                        continue;

                    case 4:
                        Console.Write("Enter country (0 = back, 00 = menu): ");
                        string countryInput = Console.ReadLine();
                        if (countryInput == "00") return;
                        if (countryInput == "0") { step = 3; continue; }
                        if (string.IsNullOrWhiteSpace(countryInput))
                        {
                            ServiceUI.Error("Country cannot be empty!");
                            continue;
                        }
                        if (!LettersOnlyRegex.IsMatch(countryInput))
                        {
                            ServiceUI.Error("Country can only contain letters (no numbers, spaces, or symbols)!");
                            continue;
                        }
                        country = countryInput;
                        step = 5;
                        continue;

                    case 5:
                        var actor = new Actor { Name = name, Surname = surname, BirthDate = birthDate, Country = country };
                        try
                        {
                            ServiceUI.Loading("Registering summoner with the League");
                            _actors.Add(actor);
                            _actors.SaveChanges();
                            ServiceUI.Success("Actor created successfully.");
                        }
                        catch (Exception ex)
                        {
                            ServiceUI.Error(ex.Message);
                        }

                        Console.Write("Press Enter to create another, or 00 to return to menu: ");
                        if (Console.ReadLine() == "00") return;
                        step = 1;
                        continue;
                }
            }
        }

        public void ShowAllActors()
        {
            ServiceUI.Header("All Actors — Summoner Leaderboard");
            ServiceUI.Loading("Pulling match history for all summoners");
            PrintActors();
            ServiceUI.Pause();
        }

        public void ShowAllActorsInline()
        {
            ServiceUI.Header("All Actors — Summoner Leaderboard");
            PrintActors();
        }

        private void PrintActors()
        {
            var actors = _actors.GetAll();

            if (!actors.Any())
            {
                ServiceUI.Empty("actors");
            }
            else
            {
                foreach (var actor in actors)
                {
                    Console.WriteLine($"{actor.Id} - {actor.Name} {actor.Surname} ({actor.Country}, born {actor.BirthDate:yyyy-MM-dd})");
                }
            }
        }
    }
}