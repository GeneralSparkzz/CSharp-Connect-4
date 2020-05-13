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

namespace CSharpFinalProject
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
        }

        private void Button_start_Click(object sender, RoutedEventArgs e)
        {
            if (Radio_AI_Player.IsChecked == true)
            {
                MainWindow MW = new MainWindow(true);
                MW.Show();
                this.Close();
            }
            else if(Radio_Player.IsChecked == true)
            {
                MainWindow MW = new MainWindow(false);
                MW.Show();
                this.Close();
            }
        }

        private void Radio_AI_Player_Checked(object sender, RoutedEventArgs e)
        {
        }

        private void Radio_Player_Checked(object sender, RoutedEventArgs e)
        {

        }
    }
}
