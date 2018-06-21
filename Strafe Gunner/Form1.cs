using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;
using GameSystemServices;

namespace Strafe_Gunner
{
    public partial class Form1 : Form
    {
        const string gameToken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJnYW1lSWQiOiI1YjJiZDhmNDUwZTRiODIxZDYxNzg1MjgiLCJjYXJkSWQiOiIxIiwiaWF0IjoxNTI5NjAwMzI1fQ.mVdQG0Ay6ykv6FJtxdcKFSEluDnaUQLZ2k5ou1kFQ08";
        public static Service service = new Service(Environment.GetCommandLineArgs(), gameToken);
        SoundPlayer music = new SoundPlayer(Properties.Resources.BACKGROUNDMUSIC);

        public Form1()
        {
            InitializeComponent();
            //Load into HomePage.
            HomePage hp = new HomePage();
            this.Controls.Add(hp);
            hp.Location = new Point((Screen.PrimaryScreen.Bounds.Width - hp.Width) / 2,(Screen.PrimaryScreen.Bounds.Height - hp.Height) / 2);


            music.PlayLooping();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Normal;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Bounds = Screen.PrimaryScreen.Bounds;
            Cursor.Hide();
           
        }
    }
}
