namespace FSRCompiler_2._0
{
    public partial class Form1 : Form
    {
        public string? vanillaItem;
        public Form1()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e) // Vanilla item
        {
            TextBox objTextBox = (TextBox)sender;
            vanillaItem = objTextBox.Text;
        }
        private void textBox2_TextChanged(object sender, EventArgs e) // Skyblock ID
        {

        }
        private void button1_Click(object sender, EventArgs e) // Create properties file
        {
           File.CreateText(string.Concat("item=", textBox1.Text));
        }
        private void CreatePropertiesFile(string vanillaItem, string skyblockID,)
        {

        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}