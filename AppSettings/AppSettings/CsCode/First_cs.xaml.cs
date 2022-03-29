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

namespace AppSettings.CsCode
{
    /// <summary>
    /// Interaction logic for First_cs.xaml
    /// </summary>
    public partial class First_cs : Window
    {
        public First_cs()
        {
            InitializeComponent();
        }

        private void First_button_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("First Button Clicked");
            MessageBox.Show("Next Msg");
        }

        private void Second_button_Click(object sender, RoutedEventArgs e)
        {

            MessageBox.Show("Second Buttoon Clicked", "Hello");
        }

        private void Change_title_button_Click(object sender, RoutedEventArgs e)
        {
            this.Title = "Changed Title";
        }

        private void Change_title_Click(object sender, RoutedEventArgs e)
        {
            Change_title.Content = "New Button Title";
            Label.Content = "Surprise";
            Textbox.Text = "New Text";
        }
    }
}
