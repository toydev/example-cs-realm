using System.IO;
using System.Windows.Input;

using Prism.Commands;
using Realms;

using SimpleWpfApp.Model;

namespace SimpleWpfApp
{
    class MainWindowViewModel
    {
        public Counter Counter { get; }
        public ICommand CountUpCommand { get; }

        public MainWindowViewModel()
        {
            var config = new RealmConfiguration(Path.Combine(
                Directory.GetCurrentDirectory(), "SimpleWpfApp.realm"));
            Realm.DeleteRealm(config);

            var realm = Realm.GetInstance(config);
            realm.Write(() =>
            {
                realm.Add(new Counter
                {
                    CounterKey = 1,
                    Count = 0,
                });
            });

            Counter = realm.Find<Counter>(1);
            CountUpCommand = new DelegateCommand(CountUp);
        }

        public void CountUp()
        {
            var config = new RealmConfiguration(Path.Combine(
                Directory.GetCurrentDirectory(), "SimpleWpfApp.realm"));
            var realm = Realm.GetInstance(config);

            realm.Write(() =>
            {
                var target = realm.Find<Counter>(1);

                target.Count++;
            });
        }
    }
}
