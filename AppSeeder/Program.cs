using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using System.Data.Common;

using Seido.Utilities.SeedGenerator;

using Configuration;
using Models;
using DbContext;

namespace AppConsole
{
    static class MyLinqExtensions
    {
        public static void Print<T>(this IEnumerable<T> collection)
        {
            collection.ToList().ForEach(item => Console.WriteLine(item));
        }
    }


    class Program
    {
        const int nrItemsSeed = 1000;
        static void Main(string[] args)
        {
            #region run below to test the model only

            Console.WriteLine($"\nSeeding the Model...");
            var modelList = SeedModel(nrItemsSeed);

            Console.WriteLine($"\nTesting Model...");
            WriteModel(modelList);
            #endregion


            #region  run below only when Database i created
            Console.WriteLine($"\nConnecting to database...");
            Console.WriteLine($"Database type: {AppConfig.DbSetActive.DbServer}");
            Console.WriteLine($"Connection used: {AppConfig.DbSetActive.DbConnection}");
  
            Console.WriteLine($"\nSeeding database...");
            try
            {
                SeedDataBase(modelList).Wait();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nError: Database could not be seeded. Ensure the database is correctly created");
                Console.WriteLine($"\nError: {ex.Message}");
                Console.WriteLine($"\nError: {ex.InnerException.Message}");
                return;
            }

            Console.WriteLine("\nQuery database...");
            QueryDatabaseAsync().Wait();
            #endregion

        }


        #region Update to reflect you new Model
<<<<<<< HEAD
        private static void WriteModel(List<MusicGroup> _modelList)
        {
            Console.WriteLine($"Nr of great music bands: {_modelList.Count()}");
            Console.WriteLine($"Total nr of albums produced: {_modelList.Sum(b => b.Albums.Count)}");
            Console.WriteLine($"Total nr of music band members: {_modelList.Sum(b => b.Members.Count)}");

            Console.WriteLine($"First Music group: {_modelList.First()}");
            _modelList.First().Albums.ForEach(album => Console.WriteLine($"  - {album.Name}"));

            Console.WriteLine($"Last Music group: {_modelList.Last()}");
            _modelList.Last().Albums.ForEach(album => Console.WriteLine($"  - {album.Name}"));

        }

        private static List<MusicGroup> SeedModel(int nrItems)
        {
            var _seeder = new SeedGenerator();

            //Create a list of 20 great bands
            var _musicgroups = _seeder.ItemsToList<MusicGroup>(nrItems);
            var _artists = _seeder.ItemsToList<Artist>(nrItems*8);

            _musicgroups.ForEach(m => {

                //pick 4 to 8 members from the list of _artists
                m.Members = _seeder.UniqueIndexPickedFromList(_seeder.Next(4, 9), _artists);

                //Create between 5 and 16 Albums
                m.Albums = new List<Album>();
                for (int i = 5; i < _seeder.Next(6, 17); i++)
                {
                    m.Albums.Add(new Album().Seed(_seeder));
                }

                m.EstablishedYear = m.Albums.Min(a => a.ReleaseYear);
            });

            return _musicgroups;
        }

        private static async Task SeedDataBase(List<MusicGroup> _modelList)
=======
        private static void WriteModel(List<Friend> modelList)
        {
            Console.WriteLine($"NrOfFriends: {modelList.Count()}");
            Console.WriteLine($"NrOfFriends without any pets: {modelList.Count(
                f => f.Pets == null || f.Pets?.Count == 0)}");
            Console.WriteLine($"NrOfFriends without an adress: {modelList.Count(
                f => f.Address == null)}");
               
            Console.WriteLine($"First Friend: {modelList.First()}");
            Console.WriteLine($"Last Friend: {modelList.Last()}");
        }

        private static List<Friend> SeedModel(int nrItems)
        {
            var seeder = new SeedGenerator();
            
            //Create a list of friends, adresses and pets
            var goodfriends = seeder.ItemsToList<Friend>(nrItems);
            var addresses = seeder.ItemsToList<Address>(nrItems);

            var _quotes = seeder.AllQuotes.Select (q => new Quote() { QuoteText = q.Quote, Author = q.Author}).ToList(); 

            //Assign adress and pet to friends
            for (int i = 0; i < nrItems; i++)
            {
                //assign an address randomly
                goodfriends[i].Address = (seeder.Bool) ? seeder.FromList(addresses) :null;

                //Create between 0 and 3 pets
                var _pets = new List<Pet>();
                for (int c = 0; c < seeder.Next(0,4); c++)
                {
                    _pets.Add(new Pet().Seed(seeder)); 
                }
                goodfriends[i].Pets = (_pets.Count > 0) ? _pets : null;

                //Quotes
                goodfriends[i].Quotes = new List<Quote>();
                for (int c = 0; c < seeder.Next(0,6); c++)
                {
                    var q = seeder.FromList(_quotes); 
                    goodfriends[i].Quotes.Add(q);
                }
            }
            return goodfriends;
        }
        private static async Task SeedDataBase(List<Friend> _modelList)
>>>>>>> origin/main_friends
        {
            using (var db = MainDbContext.DbContext())
            {
                #region move the seeded model into the database using EFC
                foreach (var item in _modelList)
                {
                    db.Friends.Add(item);
                }
                #endregion

                await db.SaveChangesAsync();
            }
        }

        private static async Task QueryDatabaseAsync()
        {
            Console.WriteLine("--------------");
            using (var db = MainDbContext.DbContext())
            {
                #region Reading the database using EFC
                var _modelList = await db.Friends
                    .Include(item => item.Address)
                    .Include(item => item.Pets)
                    .Include(item => item.Quotes)
                    .ToListAsync();                
                #endregion

                WriteModel(_modelList);
            }
        }
        #endregion
    }
}
