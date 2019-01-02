using System;
using System.IO;

using Realms;

using Relation.Model;

namespace Relation
{
    class Program
    {
        static void Main(string[] args)
        {
            var config = new RealmConfiguration(Path.Combine(
                Directory.GetCurrentDirectory(), "simple.realm"));
            Realm.DeleteRealm(config);

            var realm = Realm.GetInstance(config);
            realm.Write(() =>
            {
                var insertObject = new Table1
                {
                    Name = "Table1-1",
                    Object1 = new Table2 { Name = "Table2-1" },
                };
                insertObject.ObjectN.Add(new Table2 { Name = "Table2-2" });
                insertObject.ObjectN.Add(new Table2 { Name = "Table2-3" });
                realm.Add(insertObject);
            });

            foreach (var i in realm.All<Table1>())
            {
                Console.WriteLine(i);
            }
        }
    }
}
