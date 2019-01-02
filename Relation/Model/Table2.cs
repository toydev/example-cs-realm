using Realms;

namespace Relation.Model
{
    class Table2 : RealmObject
    {
        public string Name { get; set; }

        public override string ToString()
        {
            return string.Format("Name: {0}", Name);
        }
    }
}
