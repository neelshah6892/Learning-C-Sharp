namespace SampleMAUI
{
    public partial class MainPage : ContentPage
    {
        int count = 0;
        int labelCount = 0;

        public MainPage()
        {
            InitializeComponent();
        }

        private void OnCounterClicked(object sender, EventArgs e)
        {
            count++;

            if (count == 1)
                CounterBtn.Text = $"Clicked {count} time";
            else
                CounterBtn.Text = $"Clicked {count} times";

            SemanticScreenReader.Announce(CounterBtn.Text);

            labelCount++;
            var newLabel = new Label()
            {
                Text = $"Label{labelCount}",
                FontSize = 18,
                HorizontalOptions = LayoutOptions.Center,
            };

            MainStackLayout.Children.Add(newLabel);
        }
    }

}
