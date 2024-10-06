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
            Birds.Text = Birds.Text + Environment.NewLine + BirdPicker.SelectedItem;
        }
    }

}
