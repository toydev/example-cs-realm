using Realms;

namespace Simple.Model
{
    // RealmObject を継承したクラスがテーブルになる。
    class Table1 : RealmObject
    {
        // プロパティがカラムになる。

        // 主キーには PrimaryKey アノテーションを付ける。
        [PrimaryKey]
        public int PrimaryKey { get; set; }
        public string Column1 { get; set; }
        public string Column2 { get; set; }
        // インデックスを作る項目には Indexed アノテーションを付ける。
        [Indexed]
        public string Column3 { get; set; }

        // 確認表示用に ToString を実装する。
        public override string ToString()
        {
            return string.Format(
                "PrimaryKey: {0}, Column1: {1}, Column2: {2}, Column3: {3}",
                PrimaryKey, Column1, Column2, Column3);
        }
    }
}
