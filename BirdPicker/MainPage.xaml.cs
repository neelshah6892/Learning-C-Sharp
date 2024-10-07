namespace BirdPicker
{
    public partial class MainPage : ContentPage
    {
        
        public MainPage()
        {
            InitializeComponent();

            BirdPicker.ItemsSource = new string[] {
                "Duck",
                "Pigeon",
                "Penguin",
                "Ostrich",
                "Owl"
            };
        }

        private void AddBird_Clicked(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(Birds.Text))
            {
                Birds.Text = Birds.Text + Environment.NewLine;
            }
            Birds.Text += BirdPicker.SelectedItem;
        }
    }

}
