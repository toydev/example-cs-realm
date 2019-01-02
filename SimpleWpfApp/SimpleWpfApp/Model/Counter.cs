using Realms;

namespace SimpleWpfApp.Model
{
    class Counter : RealmObject
    {
        [PrimaryKey]
        public int CounterKey { get; set; }
        public int Count { get; set; }
    }
}
