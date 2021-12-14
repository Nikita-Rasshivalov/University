using Microsoft.AspNetCore.Identity;
using RadiostationWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RadiostationWeb.Data
{
    public class DbInitializer
    {
        public static void InitializeDb(BDLab1Context context)
        {
            context.Database.EnsureCreated();
            Random rnd = new Random();
            if (context.Records.Any())
                return;
            var recordsNumber = 1000;
            for (var i = 0; i < recordsNumber; i++)
            {
                var genreId = rnd.Next(1, context.Genres.Count() + 1);
                var performerId = rnd.Next(1, context.Performers.Count() + 1);
                var album = "Album " + rnd.Next(0, 4);
                DateTime start = new DateTime(2000, 1, 1);
                int range = (DateTime.Today - start).Days;
                DateTime recordDate = start.AddDays(rnd.Next(range));
                var lasting = rnd.Next(170, 300);
                var rating = rnd.Next(1, 45) / 10 + 0.5;
                var composName = "Compos Name " + rnd.Next(1, 1000);

                context.Records.Add(new Record
                {
                    PerformerId = performerId,
                    GenreId = genreId,
                    Album = album,
                    RecordDate = recordDate,
                    Lasting = lasting,
                    Rating = (decimal)rating,
                    ComposName = composName
                });
                context.SaveChanges();
            }

            if (context.Genres.Any())
                return;
            context.Genres.AddRange(
                new List<Genre> {
                    new Genre { GenreName = "Rock" , Description="Ritmic music" },
                    new Genre { GenreName = "Pop" , Description="Emphasis on vocals" },
                    new Genre { GenreName = "Rap" , Description="rhythmic text or poetry" } }
                );
            context.SaveChanges();
            if (context.Groups.Any())
                return;
            context.Groups.AddRange(
                new List<Group> {
                    new Group { Description = "Gribi" },
                    new Group { Description = "Agata Kristy"  },
                    new Group { Description = "Aria" } }
                );
            context.SaveChanges();

            if (context.Performers.Any())
                return;
            context.Performers.AddRange(
                new List<Performer> {
                    new Performer { Name = "Valery" , Surname ="Kipelov", GroupId = 1 },
                    new Performer { Name = "Vadim" , Surname ="Samoilov", GroupId = 2 },
                    new Performer { Name = "Illia" , Surname ="Kapustin", GroupId = 3 } }
                );
            context.SaveChanges();

            if (context.Employees.Any())
                return;
            context.Employees.AddRange(
                new List<Employee> {
                    new Employee { Name = "Nikita" , Surname ="Rasshivalov", Middlename = "Igorevich" },
                    new Employee { Name = "Doroshko" , Surname ="Denis", Middlename = "Denosovich" },
                    new Employee { Name = "Mataras" , Surname ="Atrem", Middlename = "Artemovich" } }
                );
            context.SaveChanges();

        }
    }
}

