using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Game
{
    /// <summary>
    /// Interaction logic for splashScreen.xaml
    /// </summary>
    public partial class splashScreen : Window
    {
        public splashScreen()
        {
            InitializeComponent();
            loadImage();
        }

        private void loadImage()
        {
            WaitforLoadAsync();
        }

        private async void WaitforLoadAsync()
        {
            await Task.Delay(4000);
            welcomeScreen ws = new welcomeScreen();
            ws.Show();
            this.Close();
        }
    }
}
