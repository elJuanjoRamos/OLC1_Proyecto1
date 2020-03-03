﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp1.Controller;
using WindowsFormsApp1.Model;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public string charInicial = "";
        string appPath = Application.StartupPath;

        public Form1()
        {
            InitializeComponent();
        }

        private void nuevaPestañaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var tp = new TabPage("Pestaña Nueva ");
            tabControl1.Controls.Add(tp);
            var rtb = new RichTextBox();
            rtb.Width = 500;
            rtb.Height = 465;
            System.Drawing.Font currentFont = richTextBox1.SelectionFont;
            FontStyle newFontStyle = (FontStyle)(currentFont.Style | FontStyle.Regular);
            rtb.SelectionFont = new System.Drawing.Font(currentFont.FontFamily, 11, newFontStyle);
            tp.Controls.Add(rtb);
        }

        private void abrirArchivoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var filePath = string.Empty;
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "er files (*.er)|*.er";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                filePath = ofd.FileName;
                tabControl1.SelectedTab.Text = filePath;
            }

            if (File.Exists(filePath))
            {
                StreamReader sr = new StreamReader(filePath);
                string line;
                foreach (Control c in tabControl1.SelectedTab.Controls)
                {
                    RichTextBox rtb = c as RichTextBox;
                    c.Text = "";
                    try
                    {
                        line = sr.ReadLine();
                        while (line != null)
                        {
                            rtb.AppendText(line + "\n");
                            line = sr.ReadLine();
                        }
                    }
                    catch (Exception)
                    {
                        alertMessage("Ha ocurrido un error D:");
                    }
                    sr.Close();
                }
            }
        }

        private void guardarArchivoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (File.Exists(tabControl1.SelectedTab.Text))
            {
                String dir = tabControl1.SelectedTab.Text;
                StreamWriter sw = new StreamWriter(@dir);
                try
                {
                    foreach (Control c in tabControl1.SelectedTab.Controls)
                    {
                        RichTextBox rtb = c as RichTextBox;
                        try
                        {
                            sw.WriteLine(rtb.Text);
                            sw.WriteLine("\n");
                        }
                        catch (Exception)
                        {
                            alertMessage("Ha ocurrido un error D:");
                        }
                    }
                }
                catch (Exception) { }
                sw.Close();
            }
            else
            {
                SaveFileDialog svd = new SaveFileDialog();
                svd.Title = "Save Er Files";
                svd.DefaultExt = "er";
                svd.Filter = "Er files (*.er)|*.er";
                svd.FilterIndex = 2;
                svd.RestoreDirectory = true;
                svd.FileName = tabControl1.SelectedTab.Text;

                if (svd.ShowDialog() == DialogResult.OK)
                {
                    String dir = svd.FileName;
                    StreamWriter sw = new StreamWriter(@dir);
                    tabControl1.SelectedTab.Text = dir;
                    try
                    {
                        foreach (Control c in tabControl1.SelectedTab.Controls)
                        {
                            RichTextBox rtb = c as RichTextBox;
                            try
                            {
                                sw.WriteLine(rtb.Text);
                                sw.WriteLine("\n");
                            }
                            catch (Exception)
                            {
                                alertMessage("Ha ocurrido un error D:");

                            }
                        }
                    }
                    catch
                    {
                        alertMessage("Ha ocurrido un error D:");
                    }
                    sw.Close();
                }
            }
        }

        private void guardarComoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog svd = new SaveFileDialog();
            svd.Title = "Save Er Files";
            svd.DefaultExt = "er";
            svd.Filter = "Er files (*.er)|*.er";
            svd.FilterIndex = 2;
            svd.RestoreDirectory = true;
            svd.FileName = tabControl1.SelectedTab.Text;

            if (svd.ShowDialog() == DialogResult.OK)
            {
                String dir = svd.FileName;
                StreamWriter sw = new StreamWriter(@dir);
                try
                {
                    foreach (Control c in tabControl1.SelectedTab.Controls)
                    {
                        RichTextBox rtb = c as RichTextBox;
                        try
                        {
                            sw.WriteLine(rtb.Text);
                            sw.WriteLine("\n");
                        }
                        catch (Exception)
                        {
                            alertMessage("Ha ocurrido un error D:");

                        }
                    }
                }
                catch
                {
                    alertMessage("Ha ocurrido un error D:");
                }
                sw.Close();
            }
        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public void alertMessage(String mensaje)
        {
            MessageBox.Show(mensaje, "Error",
            MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            foreach (Control c in tabControl1.SelectedTab.Controls)
            {
                RichTextBox rtb = c as RichTextBox;
                if (rtb.Text != "")
                {
                    LexicoController.Instance.Analizer(rtb.Text);
                    foreach(Token t in TokenController.Instance.getArrayListTokens())
                    {
                        Console.WriteLine("ID: " + t.Description + " - " + t.Lexema);
                    }
                }
                else
                {
                    alertMessage("No existe texto para analizar D:");
                }
            }
        }

        private void reporteDeTokensToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TokenController.Instance.reportToken();
        }

        private void reporteDeErrorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TokenController.Instance.reportError();
        }
    }
}