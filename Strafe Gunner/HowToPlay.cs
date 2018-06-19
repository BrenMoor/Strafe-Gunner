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
    public partial class HowToPlay : UserControl
    {
        public HowToPlay()
        {
            InitializeComponent();
            Focus();

        }




        private void HowToPlay_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
        
            switch (e.KeyCode)
            {

                case Keys.Space:
                    Form htp = this.FindForm();
                    htp.Controls.Remove(this);
                    GameScreen gs = new GameScreen();
                    htp.Controls.Add(gs);
                    gs.Focus();
                    gs.Location = new Point((Screen.PrimaryScreen.Bounds.Width - gs.Width) / 2, (Screen.PrimaryScreen.Bounds.Height - gs.Height) / 2);
                    break;
          

                case Keys.Escape:
                    Application.Exit();
                    break;
                    
            }
        }

      
        
    }
}
