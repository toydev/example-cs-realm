using System;
using System.IO;
using System.Threading;

using Realms;

using Transaction.Model;

namespace Transaction
{
    class Program
    {
        static void Main(string[] args)
        {
            var config = new RealmConfiguration(Path.Combine(
                Directory.GetCurrentDirectory(), "simple.realm"));
            Realm.DeleteRealm(config);

            // 書き込みはトランザクション内で行う。
            var realm = Realm.GetInstance(config);
            realm.Write(() =>
            {
                // オブジェクトを書き込む。
                realm.Add(new Table1
                {
                    PrimaryKey = 1,
                    Column1 = "Value1-1",
                    Column2 = "Value1-2",
                    Column3 = "Value1-3"
                });
            });

            new Thread(() =>
            {
                // スレッド用に Realm オブジェクトを用意する。
                var threadRealm = Realm.GetInstance(config);

                var updateTarget = threadRealm.Find<Table1>(1);

                // 更新もトランザクション内で行う。
                threadRealm.Write(() =>
                {
                    updateTarget.Column1 = "NewValue";
                    Console.WriteLine("Updated: {0}", threadRealm.Find<Table1>(1));

                    // トランザクションの完了を少し待機する。
                    Thread.Sleep(2000);
                });
            }).Start();

            // 更新が済むまで少し待機する。
            Thread.Sleep(1000);

            // トランザクションが完了する前は変更内容が見えない。
            // Realm オブジェクトは毎回取得しなおす。
            realm = Realm.GetInstance(config);
            var noUpdateObject = realm.Find<Table1>(1);
            Console.WriteLine("NoUpdated: {0}", noUpdateObject);

            // トランザクションが完了するまで少し待機する。
            Thread.Sleep(3000);

            // トランザクションが完了したので変更内容が見える。
            realm = Realm.GetInstance(config);
            var updatedObject = realm.Find<Table1>(1);
            Console.WriteLine("Updated: {0}", updatedObject);
        }
    }
}
