using System;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows.Forms;

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
            FishingRod,
            Block
        }
        public Form1()
        {
            InitializeComponent();
        }
        private void button1_Click(object sender, EventArgs e) // Create properties file
        {
            SaveFileDialog sfd = new()
            {
                InitialDirectory = Application.StartupPath + "\\Scripts\\",
                Title = "Save CIT File",
                CheckPathExists = true,
                DefaultExt = "properties",
                FileName = textBox3.Text,
                Filter = ".PROPERTIES files (*.properties)|*.properties|All files (*.*)|*.*",
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
                                         textBox9.Text == "leather" ? string.Concat("\ntexture.", textBox9.Text, "_layer_1_overlay=", !textBox12.Text.Any(p => char.IsLetterOrDigit(p)) ? textBox8.Text : textBox12.Text) : null,
                                         "\ntexture.", textBox9.Text, "_layer_2=", textBox10.Text,
                                         textBox9.Text == "leather" ? string.Concat("\ntexture.", textBox9.Text, "_layer_2_overlay=", !textBox11.Text.Any(p => char.IsLetterOrDigit(p)) ? textBox10.Text : textBox11.Text) : null);
                    if (textBox13.Text.Any(p => char.IsLetterOrDigit(p)))
                    {
                        file = string.Concat(file, "\ntexture.", textBox13.Text, "_layer_1=", textBox8.Text,
                                             textBox13.Text == "leather" ? string.Concat("\ntexture.", textBox13.Text, "_layer_1_overlay=",!textBox12.Text.Any(p => char.IsLetterOrDigit(p)) ? textBox8.Text : textBox12.Text) : null,
                                             "\ntexture.", textBox13.Text, "_layer_2=", textBox10.Text,
                                             textBox13.Text == "leather" ? string.Concat("\ntexture.", textBox13.Text, "_layer_2_overlay=", !textBox11.Text.Any(p => char.IsLetterOrDigit(p)) ? textBox10.Text : textBox11.Text) : null);
                    }
                    break;
                case (int)ItemType.Skull:
                    file = string.Concat("nbt.SkullOwner.Properties.textures.0.Value=", textBox9);
                    break;
                case (int)ItemType.FishingRod:
                    file = string.Concat("items=minecraft:fishing_rod",
                                         "\ntexture.fishing_rod_cast=", textBox3.Text, "_cast");
                    break;
                case (int)ItemType.Block:
                    file = string.Concat("matchBlocks=minecraft:", textBox1.Text,
                                         "\ntiles=", textBox3.Text,
                                         "\nmethod=", textBox13.Text,
                                         "\nbiome=", textBox9.Text);
                    return file;
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
                EnableStuff(false);
                label13.Enabled = false;
                textBox12.Enabled = false;
                label12.Enabled = false;
                textBox11.Enabled = false;
                checkBox4.Enabled = true;
                if (!checkBox4.Checked)
                {
                    label10.Enabled = false;
                    textBox8.Enabled = false;
                }
                return;
            }
            if (comboBox1.SelectedIndex == (int)ItemType.ArmourModel)
            {
                label9.Text = "Armour Material";
                label10.Text = "Armour Layer 1";
                EnableStuff(true);
                checkBox4.Enabled = true;
                label10.Enabled = true;
                textBox8.Enabled = true;
                if (checkBox4.Checked)
                { 
                    label13.Enabled = true;
                    textBox12.Enabled = true;
                    label12.Enabled = true;
                    textBox11.Enabled = true;
                }
                return;
            }
            if (comboBox1.SelectedIndex == (int)ItemType.Skull || comboBox1.SelectedIndex == (int)ItemType.Block)
            {
                label9.Text = "Skull";
                label9.Enabled = true;
                textBox9.Enabled = true;
                if (comboBox1.SelectedIndex == (int)ItemType.Block)
                {
                    label14.Enabled = true;
                    textBox13.Enabled = true;
                    label9.Text = "Biome";
                    label14.Text = "Method";
                }
                else
                {
                    label14.Enabled = false;
                    textBox13.Enabled = false;
                }
            }
            else
            {
                label9.Enabled = false;
                textBox9.Enabled = false;
            }
            label10.Text = "Armour Layer 1";
            label10.Enabled = false;
            textBox8.Enabled = false;
            label11.Enabled = false;
            textBox10.Enabled = false;
            checkBox4.Enabled = false;
            checkBox4.Checked = false;
        }
        private void EnableStuff(bool a)
        {
            label9.Enabled = a;
            textBox9.Enabled = a;
            label11.Enabled = a;
            textBox10.Enabled = a;
            label14.Enabled = a;
            textBox13.Enabled = a;
        }
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                label3.Text = "Item Name";
                textBox2.AutoCompleteMode = AutoCompleteMode.None;
                return;
            }
            label3.Text = "Skyblock ID";
            textBox2.AutoCompleteMode = AutoCompleteMode.Suggest;
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
        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox4.Checked)
            {
                if (comboBox1.SelectedIndex == (int)ItemType.ArmourModel)
                {
                    label13.Enabled = true;
                    textBox12.Enabled = true;
                    label12.Enabled = true;
                    textBox11.Enabled = true;
                }
                label10.Enabled = true;
                textBox8.Enabled = true;
                return;
            }
            if (comboBox1.SelectedIndex != (int)ItemType.ArmourModel)
            {
                label10.Enabled = false;
                textBox8.Enabled = false;
            }
            label13.Enabled = false;
            textBox12.Enabled = false;
            label12.Enabled = false;
            textBox11.Enabled = false;
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

        private void button2_Click(object sender, EventArgs e) // clear all textBoxes
        {
            var allTextBoxes = Controls.OfType<TextBox>();
            var allCheckBoxes = Controls.OfType<CheckBox>();
            foreach (TextBox textBox in allTextBoxes)
            {
                textBox.Text = null;
            }
            foreach (CheckBox checkBox in allCheckBoxes)
            {
                checkBox.Checked = false;
            }
            comboBox1.Text = null;
        }
    }
} // if ur reading this i <3 u
