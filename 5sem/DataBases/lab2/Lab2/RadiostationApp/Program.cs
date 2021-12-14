using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using RadiostationApp.Models;
using System;
using System.Linq;

namespace RadiostationApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var connectionString = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build()["ConnectionStrings:RadoistationDb"];
            var dbOptions = new DbContextOptionsBuilder<RadiostationContext>()
                .UseSqlServer(connectionString).Options;
            var context = new RadiostationContext(dbOptions);
            var choice = 0;
            while ((choice = getChoice()) != 11)
            {
                switch (choice)
                {
                    case 1:
                        var groups = context.Groups;
                        Console.WriteLine("Id\t\tDescription");
                        foreach (var group in groups)
                        {
                            Console.WriteLine($"{group.Id}\t\t{group.Description}");
                        }
                        break;
                    case 2:
                        var recordsByRating = context.Records.Where(e => e.Rating > 3);
                        Console.WriteLine("Id\tPerformerId\tGenreId\tAlbum\tRecordDate\tLasting\tLasting\tRating\tCompositionName");
                        foreach (var record in recordsByRating)
                        {
                            Console.WriteLine($"{record.Id}\t{record.PerformerId}\t{record.GenreId}\t{record.Album}\t{record.RecordDate}\t{record.Lasting}\t{record.Rating}\t{record.ComposName}");
                        }
                        break;
                    case 3:
                        var recordsGroupPerformer = context.Records.AsEnumerable().GroupBy(e => e.PerformerId);
                        foreach (var rec in recordsGroupPerformer)
                        {
                            Console.WriteLine($"PerformerId:{rec.Key}");
                            Console.WriteLine($"Count of records:{rec.Count()}");
                        }
                        break;
                    case 4:
                        var recordsWithPerformers = context.Records.Join(context.Performers, e => e.PerformerId, t => t.Id, (e, t) => new { RecordId = e.Id, t.Name }).ToList();
                        Console.WriteLine("RecId     PerformerName");
                        foreach (var rec in recordsWithPerformers)
                        {
                            Console.WriteLine($"{rec.RecordId} {rec.Name}");
                        }

                        break;
                    case 5:
                        var recordsWithPerformerRating = context.Records
                            .Join(context.Performers, e => e.PerformerId, s => s.Id,
                            (e, s) => new { RecordId = e.Id, s.Name, Rating = e.Rating })
                            .Where(item => item.Rating > 3).ToList();
                        Console.WriteLine("RecordId     PerfName    Rating");
                        foreach (var rec in recordsWithPerformerRating)
                        {
                            Console.WriteLine($"{rec.RecordId} {rec.Name} {rec.Rating}");
                        }
                        break;
                    case 6:
                        Console.WriteLine("Input Genre name:");
                        var genreName = Console.ReadLine();
                        Console.WriteLine("Input Discription name:");
                        var discription = Console.ReadLine();
                        context.Genres.Add(new Genre { GenreName = genreName, Description = discription });
                        context.SaveChanges();
                        break;
                    case 7:
                        Console.WriteLine("Input Composition Name");
                        var composName = Console.ReadLine();
                        Console.WriteLine("Input rating:");
                        decimal rating;
                        decimal.TryParse(Console.ReadLine(), out rating);
                        Console.WriteLine("Input lasting of record:");
                        int lasting;
                        int.TryParse(Console.ReadLine(), out lasting);

                        Console.WriteLine("Input date of record:");
                        DateTime date;
                        DateTime.TryParse(Console.ReadLine(), out date);

                        Console.WriteLine("Input Album Name");
                        var album = Console.ReadLine();

                        Console.WriteLine("Existing genre(Id name):");

                        foreach (var genre in context.Genres.ToList())
                        {
                            Console.WriteLine($"{genre.Id} {genre.GenreName}");
                        }

                        int genreId;
                        int.TryParse(Console.ReadLine(), out genreId);


                        Console.WriteLine("Existing Performer(Id name):");

                        foreach (var perf in context.Performers.ToList())
                        {
                            Console.WriteLine($"{perf.Id} {perf.Name}");
                        }

                        int performerId;
                        int.TryParse(Console.ReadLine(), out performerId);

                        try
                        {
                            context.Records.Add(new Record
                            {
                                ComposName = composName,
                                Rating = rating,
                                GenreId = genreId,
                                PerformerId = performerId,
                                Lasting = lasting,
                                RecordDate = date,
                                Album = album


                            });
                            context.SaveChanges();

                        }
                        catch (Exception ex)
                        {

                            Console.WriteLine(ex.Message);
                        }
                        break;
                    case 8:
                        Console.WriteLine("Existing group(Id term):");
                        foreach (var group in context.Groups.AsNoTracking().ToList())
                        {
                            Console.WriteLine($"{group.Id} {group.Description}");
                        }
                        Console.WriteLine("Input group id:");
                        int groupId;
                        int.TryParse(Console.ReadLine(), out groupId);
                        if (context.Groups.AsNoTracking().AsEnumerable().Any(s => s.Id == groupId))
                        {
                            context.Groups.Remove(new Group { Id = groupId });
                            context.SaveChanges();
                        }
                        else
                        {
                            Console.WriteLine("There is no group with this id.");
                        }
                        break;
                    case 9:
                        Console.WriteLine("Existing performer(Id name):");
                        foreach (var perf in context.Performers.AsNoTracking().ToList())
                        {
                            Console.WriteLine($"{perf.Id} {perf.Name}");
                        }
                        Console.WriteLine("Input performer id:");
                        int.TryParse(Console.ReadLine(), out performerId);
                        if (context.Performers.AsNoTracking().AsEnumerable().Any(p => p.Id== performerId))
                        {
                            context.Performers.Remove(new Performer { Id = performerId });
                            context.SaveChanges();
                        }
                        else
                        {
                            Console.WriteLine("There is no position with this id.");
                        }
                        break;
                    case 10:
                        Console.WriteLine("Input new Composition name:");
                        var updatedName = Console.ReadLine();
                        var recordToUpdate = context.Records.Where(e => e.Rating < 2).ToList();
                        foreach (var record in recordToUpdate)
                        {
                            record.ComposName = updatedName;
                            context.Update(record);
                            context.SaveChanges();
                        }
                        break;
                }
            }
        }
        public static int getChoice()
        {
            var choice = 0;
            Console.WriteLine("1.Select all from groups\n" +
                "2.Select records whose rating more than 3\n" +
                "3.Records grouped by rating\n" +
                "4.Records with performers\n" +
                "5.Recrords with performer with rating lower than 3\n" +
                "6.Add genre\n" +
                "7.Add Record\n" +
                "8.Delete group by id\n" +
                "9.Delete performer by id\n" +
                "10. Update records whose rating lower 2\n" +
                "11.Exit");
            int.TryParse(Console.ReadLine(), out choice);
            return choice;
        }
    }
}

