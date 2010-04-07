using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Db4objects.Db4o;
using Db4objects.Db4o.CS;
using Db4objects.Db4o.CS.Config;
using Db4objects.Db4o.Linq;
using DbShared;

namespace Sandbox
{
    class Program
    {
        static void Main(string[] args)
        {
            IClientConfiguration config = Db4oClientServer.NewClientConfiguration();
            config.Common.ObjectClass(typeof(ClothingType)).CascadeOnUpdate(true);
            config.Common.ObjectClass(typeof(ClothingType)).CascadeOnActivate(true);

            config.Common.ObjectClass(typeof(ClothingType)).GenerateUUIDs(true);

            IObjectContainer db =
                Db4oClientServer.OpenClient(config, DbConfig.HOST, DbConfig.PORT, DbConfig.USER, DbConfig.PASSWORD);
            //Db4oEmbedded.OpenFile(@"C:\Applications\Testing\CodeCamp2010\Db4oDemo\DbServer\bin\Debug\clothes.yap");

            //db.Store(new ClothingType() { Color = "Red", DatePurchased = DateTime.Now, Description = "Mesh shorts", Id = "1", Name = "Shorts" });

            //db.Commit();

            var clothesLinq = from ClothingType c in db
                      where c.Id == "1"
                      select c;
            var t = db.Query<ClothingType>(p => p.Id == "1").ToList();
            var clinq = clothesLinq.ToList();

            var clothes = db.Query<ClothingType>().ToList();
        }
    }
}
