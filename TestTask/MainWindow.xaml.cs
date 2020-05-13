using System;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace TestTask
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Strings = new ObservableCollection<string>();
                Strings.Add("s1");
            Strings.Add("s2");
        }

        public ObservableCollection<string> Strings
        {
            get { return (ObservableCollection<string>)GetValue(StringsProperty); }
            set { SetValue(StringsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty StringsProperty =
            DependencyProperty.Register("Strings", typeof(ObservableCollection<string>), typeof(MainWindow), new PropertyMetadata(null));

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            Strings.Clear();
            var newStrings = await getStringsAsync();
            foreach (var ns in newStrings)
            {
                Strings.Add(ns);
            }
        }

        private async Task<ObservableCollection<string>> getStringsAsync()
        {
            Task task = Task.Factory.StartNew(() => initStrings());
            await task;
            return ( task as Task<ObservableCollection<string>>).Result;
            
        }

        private ObservableCollection<string> initStrings()
        {
            var collection = new ObservableCollection<string>();
            collection.Add("a");
            collection.Add("b");
            collection.Add("c");
            Thread.Sleep(1000);
            return collection;
        }
    }
}
