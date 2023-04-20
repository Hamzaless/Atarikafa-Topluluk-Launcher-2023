using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Atarikafa_Launcher
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public static string username;
        private void guna2ControlBox1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            if (txt_name.TextLength > 3)
            {

                this.Hide();
                username = txt_name.Text;   
                mainform mf =   new mainform();
                mf.Show();
                local.Default.uname = username;
                local.Default.Save();
            }
            else
            {
                MessageBox.Show("Please enter a length name!","Minecraft Launcher"); 
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
          
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            if (local.Default.uname == "")
            {

            }
            else
            {
                this.Hide();
                txt_name.Text = local.Default.uname;
                username = txt_name.Text;
                mainform mf = new mainform();
                mf.Show();
                local.Default.uname = username;
                local.Default.Save();
            }
        }
    }
}
