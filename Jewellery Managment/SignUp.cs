using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Jewellery_Managment
{
    public partial class SignUp : Form
    {
        public SignUp()
        {
            InitializeComponent();
        }

        private void btnlogin_Click(object sender, EventArgs e)
        {
            this.Hide();
            Login login = new Login();
            login.ShowDialog();
        }

        private void btnsignup_Click(object sender, EventArgs e)
        {
            if ((name.Text == "") || (username.Text == "") || (password.Text== "") || (phone.Text == "") || (address.Text == "") || (confirmpass.Text == ""))
            {
                MessageBox.Show(" Please Provide ALL Details ");
            }
            
            if (phone.Text.Length == 10)
            { }
            else
            {
                MessageBox.Show(" Please Enter only 10 digit phone number ");
            }

            if (!(password.Text == string.Empty) && !(confirmpass.Text == string.Empty))
            {
                if (password.Text.Trim() == confirmpass.Text.Trim())
                {
                    SqlConnection con = new SqlConnection("Data Source=.\\SQLEXPRESS;Initial Catalog=master;Integrated Security=True");
                    con.Open();
                    SqlCommand cmd = new SqlCommand("insert into Users values('" + Convert.ToString(name.Text) + "','" + Convert.ToString(username.Text) + "','" + Convert.ToString(phone.Text) + "','" + Convert.ToString(address.Text) + "','" + Convert.ToString(password.Text) + "')", con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show(" Registration Successfullyy!!! ");
                    name.Text = "";
                    username.Text = "";
                    password.Text = "";
                    address.Text = "";
                    phone.Text = "";
                    confirmpass.Text = "";
                    name.Focus();
                }
                else
                {
                    MessageBox.Show(" Confirm Password is not matched with Password.... ");
                }
            }
            else
            {
                MessageBox.Show(" Password or Confirm Password value is missing ");
            }
        }
    }
}
