using System.Linq;

using Realms;

namespace Relation.Model
{
    class Table2 : RealmObject
    {
        public string Name { get; set; }

        // Table1.Object1 の逆参照
        [Backlink(nameof(Table1.Object1))]
        public IQueryable<Table1> Owners1 { get; }

        // Table1.ObjectN の逆参照
        [Backlink(nameof(Table1.ObjectN))]
        public IQueryable<Table1> OwnersN { get; }

        public override string ToString()
        {
            return string.Format(
                "Name: {0}, Owners1: [{1}], OwnersN: [{2}]",
                Name ?? "",
                string.Join(",", Owners1?.ToList().Select(i => i.Name ?? "")),
                string.Join(",", OwnersN?.ToList().Select(i => i.Name ?? "")));
        }
    }
}
