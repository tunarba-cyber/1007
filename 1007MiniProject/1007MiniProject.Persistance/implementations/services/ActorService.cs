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
    public class ActorService : IActorService
    {

        private readonly IRepository<Actor> _actors;

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
                Console.Clear();
                Console.WriteLine("--- Create Actor ---");
                switch (step)
                {
                    case 1:
                        Console.Write("Enter actor name (00 = menu): ");
                        string nameInput = Console.ReadLine();
                        if (nameInput == "00") return;
                        if (string.IsNullOrWhiteSpace(nameInput))
                        {
                            Console.WriteLine("Error: Name cannot be empty!");
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
                            Console.WriteLine("Error: Surname cannot be empty!");
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
                            Console.WriteLine("Error: Invalid birth date!");
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
                            Console.WriteLine("Error: Country cannot be empty!");
                            continue;
                        }
                        country = countryInput;
                        step = 5;
                        continue;

                    case 5:
                        var actor = new Actor { Name = name, Surname = surname, BirthDate = birthDate, Country = country };
                        try
                        {
                            _actors.Add(actor);
                            _actors.SaveChanges();
                            Console.WriteLine("Actor created successfully.");
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
        public void ShowAllActors()
        {
            Console.Clear();
            Console.WriteLine("--- All Actors ---");
            var actors = _actors.GetAll();

            if (!actors.Any())
            {
                Console.WriteLine("No actors found.");
                return;
            }

            foreach (var actor in actors)
            {
                Console.WriteLine($"{actor.Id} - {actor.Name} {actor.Surname} ({actor.Country}, born {actor.BirthDate:yyyy-MM-dd})");
            }
        }

    }
}