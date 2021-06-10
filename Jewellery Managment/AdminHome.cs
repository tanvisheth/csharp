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
    public partial class AdminHome : Form
    {
        public AdminHome()
        {
            InitializeComponent();
        }

        private void AddProduct_Click(object sender, EventArgs e)
        {
            this.Hide();
            AddProduct ap = new AddProduct();
            ap.ShowDialog();
        }

        private void DeleteProduct_Click(object sender, EventArgs e)
        {
            this.Hide();
            DeleteProduct dp = new DeleteProduct();
            dp.ShowDialog();
        }

        private void UpdateProduct_Click(object sender, EventArgs e)
        {
            this.Hide();
            UpdateProduct upd = new UpdateProduct();
            upd.ShowDialog();
        }

        private void UserRecord_Click(object sender, EventArgs e)
        {
            this.Hide();
            UserRecord ur = new UserRecord();
            ur.ShowDialog();
        }

        private void Logout_Click(object sender, EventArgs e)
        {
            this.Hide();
            Login login = new Login();
            login.ShowDialog();
        }

        private void AddEmployee_Click(object sender, EventArgs e)
        {
            this.Hide();
            AddEmployee ae = new AddEmployee();
            ae.ShowDialog();
        }
    }
}
