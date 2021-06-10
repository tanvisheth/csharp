using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.Sql;
using System.Data.SqlClient;

namespace Jewellery_Managment
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void btnlogin_Click(object sender, EventArgs e)
        {

            try
            {
                if (username.Text == string.Empty)
                {
                    MessageBox.Show(" Please Enter Username ");
                }
                if (password.Text == string.Empty)
                {
                    MessageBox.Show(" Please Enter Password ");
                }
                if (username.Text.Equals("admin") && password.Text.Equals("admin123"))
                {
                    this.Hide();
                    AdminHome adhome = new AdminHome();
                    adhome.ShowDialog();
                }
                else
                {
                    if (!(username.Text == string.Empty) && !(password.Text == string.Empty))
                    {
                        
                        String str = "Data Source=.\\SQLEXPRESS;Initial Catalog=master;Integrated Security=True";
                        SqlConnection con = new SqlConnection(str);
                        con.Open();
                        String query = "select * from Users where Username ='" + username.Text + "' and Password ='" + password.Text + "'";
                        SqlCommand cmd = new SqlCommand(query, con);
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            MessageBox.Show(" Login Successfullyy !!! ");
                            this.Hide();
                            Home home = new Home();
                            home.ShowDialog();
                        }

                        else
                        {
                            MessageBox.Show(" You are not Registered User. Register yourself First!!! ");
                        }
                    }
                }
            }
            catch (Exception em)
            {
                MessageBox.Show(em.Message);
            }
        }

        private void btnsignup_Click(object sender, EventArgs e)
        {
            this.Hide();
            SignUp sign = new SignUp();
            sign.ShowDialog();
        }
    }
}
