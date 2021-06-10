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
    public partial class UpdateProduct : Form
    {
        public UpdateProduct()
        {
            InitializeComponent();
        }

        private void type_KeyUp(object sender, KeyEventArgs e)
        {
            
            if (type.Text == "")
            {
                MessageBox.Show("Enter Type To Search ");
            }
            else
            {
                try
                {
                    SqlConnection con = new SqlConnection("Data Source=.\\SQLEXPRESS;Initial Catalog=master;Integrated Security=True");
                    con.Open();
                    SqlCommand cmd = new SqlCommand("Select * From Products where type='" + type.Text + "'", con);
                    cmd.CommandType = CommandType.Text;
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        name.Text = dr[1].ToString();
                        weight.Text = dr[2].ToString();
                        purity.Text = dr[3].ToString();
                        price.Text = dr[4].ToString();
                        byte[] MyImg = (byte[])(dr[5]);
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

        private void UpdateProduct_Load(object sender, EventArgs e)
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
            // TODO: This line of code loads data into the 'dataSet1.Products' table. You can move, or remove it, as needed.
            this.productsTableAdapter.Fill(this.dataSet1.Products);

        }

        private void btnupdate_Click(object sender, EventArgs e)
        {
            byte[] images = null;
            FileStream fs = new FileStream(textBox1.Text, FileMode.Open, FileAccess.Read);
            BinaryReader br = new BinaryReader(fs);
            images = br.ReadBytes((int)fs.Length);
            try
            {
                if (type.Text == "")
                {
                    MessageBox.Show("Enter Type of Product To Update");
                }
                else
                {
                    SqlConnection con = new SqlConnection("Data Source=.\\SQLEXPRESS;Initial Catalog=master;Integrated Security=True");
                    string query = "update Products SET name=@name, weight=@weight, purity=@purity, price=@price, image=@images where type= '" + type.Text + "'";
                    SqlCommand cmd =new SqlCommand(query,con);
                    cmd.Parameters.Add(new SqlParameter("@name",name.Text));
                    cmd.Parameters.Add(new SqlParameter("@weight", weight.Text));
                    cmd.Parameters.Add(new SqlParameter("@purity", purity.Text));
                    cmd.Parameters.Add(new SqlParameter("@price", price.Text));
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
        public void display()
        {
            SqlConnection con = new SqlConnection("Data Source=.\\SQLEXPRESS;Initial Catalog=master;Integrated Security=True");
            con.Open();
            SqlCommand cmd = new SqlCommand("select * from Products", con);
            cmd.ExecuteNonQuery();
            dataGridView1.RowTemplate.Height = 55;
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            dataGridView1.DataSource = dt;

            con.Close();
        }
    }
}
