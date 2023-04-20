using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using CmlLib.Core;
using CmlLib.Core.Auth;
using CmlLib.Core.Version;

namespace Atarikafa_Launcher
{
    public partial class mainform : Form
    {
        public mainform()
        {
            InitializeComponent();
            Control.CheckForIllegalCrossThreadCalls = false;
        }
        private void path()
        {
            var path = new MinecraftPath();
            var launcher = new CMLauncher(path);

            launcher.FileChanged += (e) =>
            {
                listBox1.Items.Add(string.Format("[{0}] {1} - {2}/{3}", e.FileKind.ToString(), e.FileName, e.ProgressedFileCount, e.TotalFileCount));
            };
            launcher.ProgressChanged += (s, e) =>
            {
                //bar2.Value = e.ProgressPercentage;
            };
            vercombo.Items.Clear();
            foreach (var item in launcher.GetAllVersions())
            {
                if(checkBox1.Checked == true)
                {
                    if (item.MType != MVersionType.Snapshot && item.MType != MVersionType.OldBeta && item.MType != MVersionType.OldAlpha && item.MType != MVersionType.Custom)
                    {
                        
                        vercombo.Items.Add(item.Name);
                        vercombo.StartIndex = 0;
                    }
                }
                else
                {

                    vercombo.Items.Add(item.Name);
                    vercombo.StartIndex = 0;
                }
            }

        }

        private void Launch()
        {
            var path = new MinecraftPath();
            var launcher = new CMLauncher(path);
            launcher.FileChanged += (e) =>
            {
                listBox1.Items.Add(string.Format("[{0}] {1} - {2}/{3}", e.FileKind.ToString(), e.FileName, e.ProgressedFileCount, e.TotalFileCount));
            };
            var launchOption = new MLaunchOption
            {
                MaximumRamMb = Convert.ToInt32(ram.Text),
                Session = MSession.GetOfflineSession(Form1.username),
                ServerIp = ip.Text,


            };
            string versiyon;
            versiyon = vercombo.SelectedItem.ToString();
            var process = launcher.CreateProcess(versiyon, launchOption);

            process.Start();
            Hide();
        }
        private void guna2ControlBox1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void mainform_Load(object sender, EventArgs e)
        {
            ram.Text = local.Default.ram;
            if (local.Default.ip != "")
            {
                ip.Text = local.Default.ip;
            }
            var request = WebRequest.Create("https://minotar.net/helm/" + Form1.username + "/100.png");
            using (var response = request.GetResponse())
            using (var stream = response.GetResponseStream())
            {
                guna2CirclePictureBox1.Image = Bitmap.FromStream(stream);
            }
            uname.Text = Form1.username;
            path();
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            listBox1.Show();
            guna2Button1.Enabled = false;
            Thread thread = new Thread(() => Launch());
            thread.IsBackground = true;
            thread.Start();
        }

        private void guna2PictureBox3_Click(object sender, EventArgs e)
        {
            if (settings.Visible == true)
            {
                settings.Hide();
            }
            else
            {
                settings.Show();
            }
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            local.Default.ram = ram.Text;
            local.Default.ip = ip.Text;
            local.Default.Save();
            path();
        }

        private void guna2PictureBox4_Click(object sender, EventArgs e)
        {
            logout();
        }
        public void logout()
        {
            this.Hide();
            local.Default.uname = "";
            local.Default.Save();
            Form1 form = new Form1();

           form.Show();
        }
    }
}
