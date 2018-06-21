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
    public partial class EndScreen2 : UserControl
    {
        public EndScreen2()
        {
            InitializeComponent();
        }

        private void EndScreen2_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Escape:
                    Form f = this.FindForm();
                    f.Controls.Remove(this);

                    HomePage hp = new HomePage();
                    f.Controls.Add(hp);
                    hp.Focus();

                    break;
            }
        }
    }
}
