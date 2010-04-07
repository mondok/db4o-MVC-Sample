using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Db4objects.Db4o;
using Db4objects.Db4o.CS;
using Db4objects.Db4o.CS.Config;
using DbShared;
using Db4objects.Db4o.Linq;

namespace Sandbox
{
    public class Db4oSample : IDisposable
    {
        private IObjectContainer _database;

        public Db4oSample(bool useClientServer)
        {
            IClientConfiguration config = Db4oClientServer.NewClientConfiguration();
            config.Common.ObjectClass(typeof(ClothingType)).CascadeOnUpdate(true);
            config.Common.ObjectClass(typeof(ClothingType)).CascadeOnActivate(true);

            config.Common.ObjectClass(typeof(ClothingType)).GenerateUUIDs(true);

            // client/server configuration
            if (useClientServer)
            {
                _database = Db4oClientServer.OpenClient(config, DbConfig.HOST, DbConfig.PORT, DbConfig.USER, DbConfig.PASSWORD);
            }
            else
            {
                _database = Db4oEmbedded.OpenFile(DbConfig.DB_PATH);
            }
        }

        public void Run()
        {
            ClearDatabase();

            AddEntities();

            QueryByExample();

            QueryByPredicate();

            QueryByLinq();
        }

        private void ClearDatabase()
        {
            var clothes = _database.Query(typeof(ClothingType));
            var pets = _database.Query(typeof(Pet));

            foreach (var clothingArticle in clothes)
                _database.Delete(clothingArticle);

            foreach (var pet in pets)
                _database.Delete(pet);

            _database.Commit();
        }

        private void QueryByExample()
        {
            Debug.WriteLine("Querying by Example");

            IObjectSet clothes = _database.QueryByExample(new ClothingType { Color = "Blue" });

            foreach (ClothingType c in clothes.Cast<ClothingType>())
            {
                Debug.WriteLine(c.ToString());
            }
        }

        private void QueryByPredicate()
        {
            Debug.WriteLine("Querying by Predicate");

            IList<Pet> pets = _database.Query<Pet>(p => p.PetType == "dog");

            foreach (Pet pet in pets)
            {
                Debug.WriteLine(pet.ToString());
            }
        }

        private void QueryByLinq()
        {
            Debug.WriteLine("Querying by Linq");
            IDb4oLinqQuery<ClothingType> clothes = from ClothingType c in _database
                                                   where c.DatePurchased.Year < 2008
                                                   select c;

            foreach (var clothingType in clothes)
            {
                Debug.WriteLine(clothingType.ToString());
            }
        }

        private void AddEntities()
        {
            _database.Store(new ClothingType() { Color = "Red", DatePurchased = DateTime.Now, Description = "Mesh shorts", Id = Guid.NewGuid().ToString(), Name = "Shorts" });
            _database.Store(new ClothingType() { Color = "Blue", DatePurchased = DateTime.Parse("1/1/2007"), Description = "Jeans", Id = Guid.NewGuid().ToString(), Name = "Blue Jeans" });
            _database.Store(new ClothingType() { Color = "Blue", DatePurchased = DateTime.Parse("7/15/2002"), Description = "Shoes", Id = Guid.NewGuid().ToString(), Name = "Running shoes" });

            _database.Store(new Pet { Id = Guid.NewGuid().ToString(), PetName = "Rover", PetType = "dog" });
            _database.Store(new Pet { Id = Guid.NewGuid().ToString(), PetName = "Max", PetType = "dog" });
            _database.Store(new Pet { Id = Guid.NewGuid().ToString(), PetName = "Chirp", PetType = "bird" });

            _database.Commit();
        }

        public void Dispose()
        {
            if (_database != null)
                _database.Close();
        }
    }
}
