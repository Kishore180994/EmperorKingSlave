using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Game
{
    /// <summary>
    /// Interaction logic for welcomeScreen.xaml
    /// </summary>
    public partial class welcomeScreen : Window
    {
        public int card;
        int x;
        public welcomeScreen()
        {
            InitializeComponent();
        }

        private void OnImageButtonClick(object sender, RoutedEventArgs e)
        {
            var click = e.Source as FrameworkElement;
            Debug.WriteLine(""+(click.Name));
            if (click.Name == "KingButton") x=1;
            else if (click.Name == "SlaveButton") x=0;
            KingSlave ks = new KingSlave(x);
            ks.Show();
            this.Close();
        }
    }
}
