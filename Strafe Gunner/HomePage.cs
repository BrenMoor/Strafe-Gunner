using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Strafe_Gunner
{
    public partial class HomePage : UserControl
    {
        public HomePage()
        {
            InitializeComponent();
            
        

        }

        private void HomePage_Click(object sender, EventArgs e)
        {
            //Switching to GameScreen on click.
            //Form hp = this.FindForm();
            //hp.Controls.Remove(this);
            //HowToPlay htp = new HowToPlay();
            //hp.Controls.Add(htp);

        }

        private void HomePage_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Space:
                    Form hp = this.FindForm();
                    hp.Controls.Remove(this);
                    HowToPlay htp = new HowToPlay();
                    hp.Controls.Add(htp);
                    htp.Focus();
                    htp.Location = new Point((Screen.PrimaryScreen.Bounds.Width - htp.Width) / 2, (Screen.PrimaryScreen.Bounds.Height - htp.Height) / 2);
                    break;

                case Keys.Escape:
                    Application.Exit();
                    break;
            }

        }
    }
}
