using Realms;

namespace Transaction.Model
{
    class Table1 : RealmObject
    {
        [PrimaryKey]
        public int PrimaryKey { get; set; }
        public string Column1 { get; set; }
        public string Column2 { get; set; }
        [Indexed]
        public string Column3 { get; set; }

        public override string ToString()
        {
            return string.Format(
                "PrimaryKey: {0}, Column1: {1}, Column2: {2}, Column3: {3}",
                PrimaryKey, Column1, Column2, Column3);
        }
    }
}
