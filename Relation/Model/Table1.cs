using System.Collections.Generic;

using Realms;

namespace Relation.Model
{
    class Table1 : RealmObject
    {
        public string Name { get; set; }

        // 一対一は単にプロパティを作る。
        public Table2 Object1 { get; set; }
        // 一対多は IList のプロパティを作る。
        public IList<Table2> ObjectN { get; }

        public override string ToString()
        {
            return string.Format(
                "Name: {0}, Object1: [{1}], ObjectN: [{2}]",
                Name ?? "",
                Object1?.ToString() ?? "",
                string.Join(",", ObjectN));
        }
    }
}
