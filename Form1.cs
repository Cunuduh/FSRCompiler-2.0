using System.Text.RegularExpressions;
namespace FSRCompiler_2._0
{
    public partial class Form1 : Form
    {
        public enum ItemType
        {
            Default,
            ArmourModel,
            ArmourIcon,
            Bow,
            Skull,
            FishingRod
        }
        public Form1()
        {
            InitializeComponent();
        }
        private void button1_Click(object sender, EventArgs e) // Create properties file
        {
            SaveFileDialog sfd = new SaveFileDialog()
            {
                InitialDirectory = Application.StartupPath + "\\Scripts\\",
                Title = "Save CIT File",
                CheckPathExists = true,
                DefaultExt = "properties",
                FileName = textBox3.Text,
                Filter = ".properties files (*.properties)|*.properties|All files (*.*)|*.*",
                FilterIndex = 1,
                RestoreDirectory = true
            };
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                File.WriteAllText(sfd.FileName, CreatePropertiesText());
            }
        }
        private string CreatePropertiesText()
        {
            string file;
            switch (comboBox1.SelectedIndex)
            {
                case (int)ItemType.Bow:
                    file = string.Concat("items=minecraft:bow",
                                         "\ntexture.bow_standby=", textBox3.Text,
                                         "\ntexture.bow_pulling_0=", textBox3.Text, "_pulling_0",
                                         "\ntexture.bow_pulling_1=", textBox3.Text, "_pulling_1",
                                         "\ntexture.bow_pulling_2=", textBox3.Text, "_pulling_2");
                    break;
                case (int)ItemType.ArmourIcon:
                    file = string.Concat("items=minecraft:", textBox1.Text,
                                         "\ntexture.", textBox1.Text, "=", textBox3.Text,
                                         textBox1.Text.Contains("leather") ? string.Concat("\ntexture.", textBox1.Text, "_overlay=", textBox8.Text) : null);
                    break;
                case (int)ItemType.ArmourModel:
                    file = string.Concat("type=armor\nitems=minecraft:", textBox1.Text,
                                         "\ntexture.", textBox9.Text, "_layer_1=", textBox8.Text,
                                         textBox9.Text == "leather" ? string.Concat("\ntexture.", textBox9.Text, "layer_1_overlay=", textBox12.Text) : null,
                                         "\ntexture.", textBox9.Text, "_layer_2=", textBox10.Text,
                                         textBox9.Text == "leather" ? string.Concat("\ntexture.", textBox9.Text, "layer_2_overlay=", textBox11.Text) : null);
                                        
                    if (Regex.IsMatch(textBox13.Text, "[a-zA-Z]"))
                    {
                        file = string.Concat(file, "\ntexture.", textBox13.Text, "_layer_1=", textBox8.Text,
                                             textBox13.Text == "leather" ? string.Concat("\ntexture.", textBox13.Text, "layer_1_overlay=", textBox12.Text) : null,
                                             "\ntexture.", textBox13.Text, "_layer_2=", textBox10.Text,
                                             textBox13.Text == "leather" ? string.Concat("\ntexture.", textBox13.Text, "layer_2_overlay=", textBox11.Text) : null);
                    }
                    break;
                case (int)ItemType.Skull:
                    file = string.Concat("nbt.SkullOwner.Properties.textures.0.Value=", textBox9);
                    break;
                default:
                    file = string.Concat("items=minecraft:", textBox1.Text);
                    break;
            }
            return string.Concat(file,
                                 checkBox6.Checked ? string.Concat("\nmodel=", textBox4.Text) : null,
                                 checkBox5.Checked ? string.Concat("\ntexture=", textBox3.Text) : null,
                                 checkBox1.Checked ? string.Concat("\nnbt.display.Name=ipattern:", textBox2.Text) : string.Concat("\nnbt.ExtraAttributes.id=", textBox2.Text),
                                 checkBox3.Checked ? string.Concat("\n", textBox5.Text) : null,
                                 checkBox2.Checked ? string.Concat("\nnbt.display.Lore.*=ipattern:", textBox6.Text) : null);
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == (int)ItemType.ArmourIcon)
            {
                label9.Text = "Skull";
                label10.Text = "Leather Overlay Icon";
                label10.Enabled = true;
                textBox8.Enabled = true;
                label9.Enabled = false;
                textBox9.Enabled = false;
                label11.Enabled = false;
                textBox10.Enabled = false;
                label14.Enabled = false;
                textBox13.Enabled = false;
                return;
            }
            if (comboBox1.SelectedIndex == (int)ItemType.ArmourModel)
            {
                label9.Text = "Armour Material";
                label10.Text = "Armour Layer 1";
                label9.Enabled = true;
                textBox9.Enabled = true;
                label10.Enabled = true;
                textBox8.Enabled = true;
                label11.Enabled = true;
                textBox10.Enabled = true;
                label14.Enabled = true;
                textBox13.Enabled = true;
                return;
            }
            label9.Text = "Skull";
            label10.Text = "Armour Layer 1";
            label10.Enabled = false;
            textBox8.Enabled = false;
            label9.Enabled = false;
            textBox9.Enabled = false;
            label11.Enabled = false;
            textBox10.Enabled = false;
            label14.Enabled = false;
            textBox13.Enabled = false;
        }
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                label3.Text = "Item Name";
                return;
            }
            label3.Text = "Skyblock ID";
        }
        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
            {
                label7.Enabled = true;
                textBox6.Enabled = true;
                return;
            }
            label7.Enabled = false;
            textBox6.Enabled = false;
        }
        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox3.Checked)
            {
                label6.Enabled = true;
                textBox5.Enabled = true;
                return;
            }
            label6.Enabled = false;
            textBox5.Enabled = false;
        }

        private void checkBox5_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox5.Checked)
            {
                label8.Enabled = true;
                textBox7.Enabled = true;
                return;
            }
            label8.Enabled = false;
            textBox7.Enabled = false;
        }

        private void checkBox6_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox6.Checked)
            {
                label5.Enabled = true;
                textBox4.Enabled = true;
                return;
            }
            label5.Enabled = false;
            textBox4.Enabled = false;
        }
    }
} // if ur reading this i <3 u