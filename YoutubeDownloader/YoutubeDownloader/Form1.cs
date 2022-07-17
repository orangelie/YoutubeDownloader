using MediaToolkit;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VideoLibrary;

namespace YoutubeDownloader
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog fdb = new FolderBrowserDialog() { Description = "Select your path" })
            {
                if (fdb.ShowDialog() == DialogResult.OK)
                {
                    textBox2.Text = fdb.SelectedPath;
                }
            }
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            progressBar1.Maximum = 1;
            progressBar1.Minimum = 0;
            progressBar1.Value = 0;

            WebRequest GetTitle = HttpWebRequest.Create(textBox1.Text);

            var youtube = YouTube.Default;
            var video = await youtube.GetVideoAsync(textBox1.Text);

            File.WriteAllBytes(textBox2.Text + @"\" + video.FullName, await video.GetBytesAsync());
            if (radioButton1.Checked == Enabled)
            {
                var inputFile = new MediaToolkit.Model.MediaFile { Filename = textBox2.Text + @"\" + video.FullName };
                var outputFile = new MediaToolkit.Model.MediaFile { Filename = $"{textBox2.Text + @"\" + video.FullName}.mp3" };
                using (var engine = new Engine())
                {
                    engine.GetMetadata(inputFile);
                    engine.Convert(inputFile, outputFile);
                }
                if (radioButton2.Checked == true)
                { 
                    File.Delete($"{textBox2.Text + @"\" + video.FullName}.mp3");            
                }
                else if (radioButton1.Checked == true)
                {
                    File.Delete(textBox2.Text + @"\" + video.FullName);
                }
            }

            progressBar1.Value = 1;
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/orangelie");
        }
    }
}
