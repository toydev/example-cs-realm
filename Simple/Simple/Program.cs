using System;
using System.IO;

using Realms;

using Simple.Model;

namespace Simple
{
    class Program
    {
        static void Main(string[] args)
        {
            // カレントディレクトリに「db.realm」という名前でファイルを作る設定にする。
            // ファイル名を指定しなかった場合は、「C:\Users\xxx\default.realm」になる。
            var config = new RealmConfiguration(Path.Combine(
                Directory.GetCurrentDirectory(), "db.realm"));

            // Realm インスタンスを取得する。
            var realm = Realm.GetInstance(config);

            // realm.Write でトランザクションを開始する。
            realm.Write(() =>
            {
                // オブジェクトを書き込む。
                realm.Add(new Table1
                {
                    Column1 = "Value1",
                    Column2 = "Value2",
                    Column3 = "Value3"
                });
            });

            // オブジェクトを読み込む。
            // 書き込みは次回実行に引き継がれるので、繰り返し実行すると行がどんどん増えていく。
            foreach (var row in realm.All<Table1>())
            {
                Console.WriteLine("Column1: {0}, Column2: {1}, Column3: {3}",
                    row.Column1, row.Column2, row.Column3);
            }
        }
    }
}
