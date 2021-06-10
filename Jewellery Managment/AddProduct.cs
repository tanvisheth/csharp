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
    public partial class AddProduct : Form
    {
        public AddProduct()
        {
            InitializeComponent();
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
        
        private void btnadd_Click(object sender, EventArgs e)
        {
            byte[] images = null;
            FileStream fs = new FileStream(textBox1.Text, FileMode.Open, FileAccess.Read);
            BinaryReader br = new BinaryReader(fs);
            images = br.ReadBytes((int)fs.Length);
              
            if ((type.Text == "") || (name.Text == "") || (weight.Text == "") || (purity.Text == "") || (price.Text == ""))
            {
                MessageBox.Show(" Please Provide ALL Details ");
            }
            SqlConnection con = new SqlConnection("Data Source=.\\SQLEXPRESS;Initial Catalog=master;Integrated Security=True");
            con.Open();

            string query = "insert into Products(type,name,weight,purity,price,image)values('" + Convert.ToString(type.Text) + "','" + Convert.ToString(name.Text) + "'," + Convert.ToString(weight.Text) + "," + Convert.ToString(purity.Text) + "," + Convert.ToString(price.Text) + ",@images)";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.Add(new SqlParameter("@images", images));
            int result = cmd.ExecuteNonQuery();

            if (result > 0)
                MessageBox.Show(" Data inserted successfully ");
            else
                MessageBox.Show(" Data is not inserting in database ");
            display();
            name.Text = "";
            type.Text = "";
            purity.Text = "";
            price.Text = "";
            weight.Text = "";
            type.Focus();
        }

        private void btnback_Click(object sender, EventArgs e)
        {
            this.Hide();
            AdminHome adhome = new AdminHome();
            adhome.ShowDialog();
        }

        private void AddProduct_Load(object sender, EventArgs e)
        {

            dataGridView1.EnableHeadersVisualStyles = false;
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.HotPink;
            dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Candara Light", 15, FontStyle.Bold);
            dataGridView1.Height=250;
            dataGridView1.RowsDefaultCellStyle.BackColor = Color.LightGoldenrodYellow;
            dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.Turquoise;
            dataGridView1.CellBorderStyle = DataGridViewCellBorderStyle.None;

            dataGridView1.DefaultCellStyle.SelectionBackColor = Color.Red;
            dataGridView1.DefaultCellStyle.SelectionForeColor = Color.Yellow;

            dataGridView1.DefaultCellStyle.WrapMode = DataGridViewTriState.True;


            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.AllowUserToResizeColumns = false;
            // TODO: This line of code loads data into the 'dataSet1.Products' table. You can move, or remove it, as needed.
            this.productsTableAdapter.Fill(this.dataSet1.Products);

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
