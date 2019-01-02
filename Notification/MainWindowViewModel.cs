using System.IO;
using System.Windows.Input;

using Prism.Commands;
using Realms;

using Notification.Model;

namespace Notification
{
    class MainWindowViewModel
    {
        public const int COUNTER_KEY = 1;

        public RealmConfiguration Config { get; }
        public Counter Counter { get; }
        public ICommand CountUpCommand { get; }

        public MainWindowViewModel()
        {
            Config = new RealmConfiguration(Path.Combine(
                Directory.GetCurrentDirectory(), "Notification.realm"));

            var realm = Realm.GetInstance(Config);

            Counter = realm.Find<Counter>(COUNTER_KEY);
            if (Counter == null)
            {
                Counter = new Counter { CounterKey = COUNTER_KEY, Count = 0, };
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
                // Realm.Find で取得しなおしたオブジェクト経由で変更しても変更通知は伝わる。
                var target = realm.Find<Counter>(COUNTER_KEY);

                // INotifyPropertyChanged による変更通知で画面の表示が自動で変わる。
                target.Count++;
            });
        }
    }
}
