using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;

namespace Jewellery_Managment
{
    public partial class AddEmployee : Form
    {
        public AddEmployee()
        {
            InitializeComponent();
        }

        private void AddEmployee_Load(object sender, EventArgs e)
        {
            dataGridView1.EnableHeadersVisualStyles = false;
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.HotPink;
            dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Candara Light", 15, FontStyle.Bold);
            dataGridView1.RowsDefaultCellStyle.BackColor = Color.LightGoldenrodYellow;
            dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.Turquoise;
            dataGridView1.CellBorderStyle = DataGridViewCellBorderStyle.None;
            dataGridView1.Height = 250;
            dataGridView1.DefaultCellStyle.SelectionBackColor = Color.Red;
            dataGridView1.DefaultCellStyle.SelectionForeColor = Color.Yellow;

            dataGridView1.DefaultCellStyle.WrapMode = DataGridViewTriState.True;


            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.AllowUserToResizeColumns = false;
            // TODO: This line of code loads data into the 'dataSet1.Employees' table. You can move, or remove it, as needed.
            this.employeesTableAdapter.Fill(this.dataSet1.Employees);
        }

        private void btnback_Click(object sender, EventArgs e)
        {
            this.Hide();
            AdminHome adhome = new AdminHome();
            adhome.ShowDialog();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp)|*.jpg; *.jpeg; *.gif; *.bmp";
            if (open.ShowDialog() == DialogResult.OK)
            {
                string picpath = open.FileName.ToString();
                textBox1.Text = picpath;
                pictureBox1.ImageLocation = picpath;
            } 
        }
        public void display()
        {
            SqlConnection con = new SqlConnection("Data Source=.\\SQLEXPRESS;Initial Catalog=master;Integrated Security=True");
            con.Open();
            SqlCommand cmd = new SqlCommand("select * from Employees", con);
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            dataGridView1.DataSource = dt;

            con.Close();
        }

        private void add_Click(object sender, EventArgs e)
        {
            byte[] images = null;
            FileStream fs = new FileStream(textBox1.Text, FileMode.Open, FileAccess.Read);
            BinaryReader br = new BinaryReader(fs);
            images = br.ReadBytes((int)fs.Length);

            if ((id.Text == "") || (name.Text == "") || (designation.Text == "") || (salary.Text == ""))
            {
                MessageBox.Show(" Please Provide ALL Details ");
            }
            SqlConnection con = new SqlConnection("Data Source=.\\SQLEXPRESS;Initial Catalog=master;Integrated Security=True");
            con.Open();
            string query = "insert into Employees(ID,Name,Designation,Salary,Emppic)values('" + Convert.ToString(id.Text) + "','" + Convert.ToString(name.Text) + "','" + Convert.ToString(designation.Text) + "'," + Convert.ToString(salary.Text)+ ",@images)";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.Add(new SqlParameter("@images", images));
            int result = cmd.ExecuteNonQuery();

            if (result > 0)
                MessageBox.Show(" Data inserted successfully ");
            else
                MessageBox.Show(" Data is not inserting in database ");
            display();
            name.Text = "";
            id.Text = "";
            designation.Text = "";
            salary.Text = "";
            id.Focus();
        }

        private void update_Click(object sender, EventArgs e)
        {
            byte[] images = null;
            FileStream fs = new FileStream(textBox1.Text, FileMode.Open, FileAccess.Read);
            BinaryReader br = new BinaryReader(fs);
            images = br.ReadBytes((int)fs.Length);
            try
            {
                if (id.Text == "")
                {
                    MessageBox.Show("Enter Employee ID To Update");
                }
                else
                {
                    SqlConnection con = new SqlConnection("Data Source=.\\SQLEXPRESS;Initial Catalog=master;Integrated Security=True");
                    string query = "update Employee SET Name=@name, Designation=@designation, Salary=@salary, Emppic=@images where ID= '" + id.Text + "'";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.Add(new SqlParameter("@name", name.Text));
                    cmd.Parameters.Add(new SqlParameter("@designation", designation.Text));
                    cmd.Parameters.Add(new SqlParameter("@salary", salary.Text));
                    cmd.Parameters.Add(new SqlParameter("@images", images));
                    con.Open();
                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                    MessageBox.Show(" Data updated successfully ");
                    con.Close();
                    display();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void delete_Click(object sender, EventArgs e)
        {
            try
            {
                if (id.Text == "")
                {
                    MessageBox.Show("Enter Employee ID To Delete");
                }
                else
                {
                    SqlConnection con = new SqlConnection("Data Source=.\\SQLEXPRESS;Initial Catalog=master;Integrated Security=True");
                    SqlCommand cmd = new SqlCommand("Delete Employees where type='" + id.Text + "'", con);
                    con.Open();
                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                    MessageBox.Show(" Data deleted successfully ");
                    display();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void id_KeyUp(object sender, KeyEventArgs e)
        {
            if (id.Text == "")
            {
                MessageBox.Show("Enter ID To Search ");
            }
            else
            {
                try
                {
                    SqlConnection con = new SqlConnection("Data Source=.\\SQLEXPRESS;Initial Catalog=master;Integrated Security=True");
                    con.Open();
                    SqlCommand cmd = new SqlCommand("Select * From Employees where ID='" + id.Text + "'", con);
                    cmd.CommandType = CommandType.Text;
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        name.Text = dr[1].ToString();
                        designation.Text = dr[2].ToString();
                        salary.Text = dr[3].ToString();
                        byte[] MyImg = (byte[])(dr[4]);
                        if (MyImg == null)
                        {
                            pictureBox1.Image = null;
                        }
                        else
                        {

                            MemoryStream ms = new MemoryStream(MyImg);
                            Image img = Image.FromStream(ms);
                            pictureBox1.Image = img;
                        }
                    }
                    dr.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
    }
}
