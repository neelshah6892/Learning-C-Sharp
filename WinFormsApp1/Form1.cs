namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void subMenu1ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form Country = new Form();
            Country.ShowDialog();
        }
    }
}