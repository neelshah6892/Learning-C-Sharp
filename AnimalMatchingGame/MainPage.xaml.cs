namespace AnimalMatchingGame
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void PlayAgainButton_Clicked(object sender, EventArgs e)
        {
            AnimalButtons.IsVisible = true;
            PlayAgainButton.IsVisible = false;

            List<string> animalEmoji = [
                "😂", "😂",
                "😝", "😝",
                "🤒", "🤒",
                "🥶", "🥶",
                "👹", "👹",
                "😍", "😍",
                "🐶", "🐶",
                "🐰", "🐰",
            ];

            foreach (var button in AnimalButtons.Children.OfType<Button>())
            {
                int index = Random.Shared.Next(animalEmoji.Count);
                string nextEmoji = animalEmoji[index];
                button.Text = nextEmoji;
                animalEmoji.RemoveAt(index);
            }
        }

        private void Button_Clicked(object sender, EventArgs e)
        {

        }
    }

}
