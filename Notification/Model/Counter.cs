using Realms;

namespace Notification.Model
{
    class Counter : RealmObject
    {
        [PrimaryKey]
        public int CounterKey { get; set; }
        public int Count { get; set; }
    }
}
