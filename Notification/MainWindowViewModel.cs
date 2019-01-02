using System.IO;
using System.Windows.Input;

using Prism.Commands;
using Realms;

using Notification.Model;

namespace Notification
{
    class MainWindowViewModel
    {
        public RealmConfiguration Config { get; }
        public Counter Counter { get; }
        public ICommand CountUpCommand { get; }

        public MainWindowViewModel()
        {
            Config = new RealmConfiguration(Path.Combine(
                Directory.GetCurrentDirectory(), "Notification.realm"));

            var realm = Realm.GetInstance(Config);

            Counter = realm.Find<Counter>(1);
            if (Counter == null)
            {
                Counter = new Counter { CounterKey = 1, Count = 0, };
                realm.Write(() =>
                {
                    realm.Add(Counter);
                });
            }

            CountUpCommand = new DelegateCommand(CountUp);
        }

        public void CountUp()
        {
            var realm = Realm.GetInstance(Config);

            realm.Write(() =>
            {
                var target = realm.Find<Counter>(1);
                target.Count++;
            });
        }
    }
}
