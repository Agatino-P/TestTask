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
            get => (ObservableCollection<string>)GetValue(StringsProperty);
            set => SetValue(StringsProperty, value);
        }
        public static readonly DependencyProperty StringsProperty =
            DependencyProperty.Register("Strings", typeof(ObservableCollection<string>), typeof(MainWindow), new PropertyMetadata(null));

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            Strings.Clear();

            try
            {
                var newStrings = await getStringsAsync();
                foreach (var ns in newStrings)
                {
                    Strings.Add(ns);
                }

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }

        }

        private async Task<ObservableCollection<string>> getStringsAsync()
        {
            return await Task.Factory.StartNew(() => initStrings());
        }

        private ObservableCollection<string> initStrings()
        {
            var collection = new ObservableCollection<string>();
            Random rand = new Random();
            int numStrings = rand.Next(5, 11);
            if (numStrings > 8)
                throw (new Exception());
            for (int strCount = 0; strCount < numStrings; strCount++)
            {
                collection.Add(rand.Next(1, 101).ToString());
                Thread.Sleep(1000);
            }
            return collection;
        }
    }
}
