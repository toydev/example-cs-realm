﻿using System;
using System.IO;

using Realms;

using Simple.Model;

namespace Simple
{
    class Program
    {
        static void Main(string[] args)
        {
            // カレントディレクトリに「simple.realm」という名前でファイルを作る設定にする。
            // ファイル名を指定しなかった場合は、「C:\Users\xxx\default.realm」になる。
            var config = new RealmConfiguration(Path.Combine(
                Directory.GetCurrentDirectory(), "simple.realm"));

            // 繰り返し実行できるよう既にデータベースがある場合は削除する。
            Realm.DeleteRealm(config);

            // Realm インスタンスを取得する。
            var realm = Realm.GetInstance(config);

            // トランザクションを開始する。
            realm.Write(() =>
            {
                // オブジェクトを書き込む。
                realm.Add(new Table1
                {
                    PrimaryKey = 1,
                    Column1 = "Value1",
                    Column2 = "Value2",
                    Column3 = "Value3"
                });

                // オブジェクトを書き込む。
                realm.Add(new Table1
                {
                    PrimaryKey = 2,
                    Column1 = "Value4",
                    Column2 = "Value5",
                    Column3 = "Value6"
                });
            });

            // 主キーで検索する。
            var rowByPrimaryKey = realm.Find<Table1>(1);
            Console.WriteLine("Find: {0}", rowByPrimaryKey);

            // オブジェクトを読み込む。
            // 書き込みは次回実行に引き継がれるので、繰り返し実行すると行がどんどん増えていく。
            foreach (var row in realm.All<Table1>())
            {
                Console.WriteLine("All: {0}", row);
            }

            realm.Write(() =>
            {
                // オブジェクトのプロパティを直接書き換えてデータベースを更新する。
                var row = realm.Find<Table1>(1);
                row.Column2 = "NewValue2";
            });

            // 更新されたことを確認する。
            Console.WriteLine("Updated: {0}", realm.Find<Table1>(1));

            realm.Write(() =>
            {
                // オブジェクトをデータベースから削除する。
                var deleteTarget = realm.Find<Table1>(1);
                realm.Remove(deleteTarget);
            });

            // 削除されたことを確認する。
            Console.WriteLine("Removed: {0}", realm.Find<Table1>(1));
        }
    }
}
