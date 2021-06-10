using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Jewellery_Managment
{
    public partial class Home : Form
    {
        private int ts;
        public Home()
        {
            InitializeComponent();
            timer1.Start();

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            ts++;
            if (ts == 5)
            {
                UserHome uh = new UserHome();
                this.Hide();
                uh.ShowDialog();
                timer1.Stop();
            }
        }
    }
}
