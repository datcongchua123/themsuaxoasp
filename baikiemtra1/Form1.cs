using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace baikiemtra1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        SqlConnection con;
        SqlCommand cmd;
        private void concection()
        {
            String strcon = "server=LAPTOP-CREE9M5V\\SQLEXPRESS;database=QLSP;integrated security=true";
            con = new SqlConnection(strcon);

        }
        private void Form1_Load(object sender, EventArgs e)
        {
            loaddata();
        }
        private void cleardata()
        {
            txttensp.Clear();
            txtgia.Clear();
            txtsoluong.Clear();
            txttimkiem.Clear();
        }
        private void loaddata()
        {
            concection();
            SqlDataAdapter da = new SqlDataAdapter("select * from QLSP", con);
            DataTable dt = new DataTable();
            con.Open();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            con.Close();

        }
        private void btnsua_Click(object sender, EventArgs e)
        {
            concection();
            cmd = new SqlCommand("update QLSP set tensp='" + txttensp.Text + "',gia='" + Convert.ToDouble(txtgia.Text) + "',soluongton='" + Convert.ToInt16(txtsoluong.Text) + "'where tensp='" + txttensp.Text + "'", con);
            con.Open();
            int ret = cmd.ExecuteNonQuery();
            if (ret == 1)
                MessageBox.Show("Cap nhat xong roi may!");
            con.Close();
            loaddata();
            cleardata();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            txttensp.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            txtgia.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            txtsoluong.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();

        }
        private void btntimkiem_Click(object sender, EventArgs e)
        {
            concection();
            SqlDataAdapter da = new SqlDataAdapter("select * from QLSP where tensp LIKE '%" + txttimkiem.Text + "%' and soluongton <100 and gia > 1000", con);
            DataTable dt = new DataTable();
            con.Open();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            con.Close();
            cleardata();
            
        }

        private void btnxoa_Click(object sender, EventArgs e)
        {
            concection();
            cmd = new SqlCommand("delete from QLSP where tensp= '" + txttensp.Text + "'", con);
            con.Open();
            int ret = cmd.ExecuteNonQuery();
            if (ret == 1)
                MessageBox.Show("Khong thich xoa day^^");
            con.Close();
            loaddata();

        }

        private void btnthem_Click(object sender, EventArgs e)
        {
            concection();
            cmd = new SqlCommand("insert into qlsp values('" + txttensp.Text + "','" + Convert.ToInt16(txtgia.Text) + "','" + Convert.ToInt16(txtsoluong.Text) + "')", con);
            con.Open();
            int ret = cmd.ExecuteNonQuery();
            if (ret == 1)
            {
                MessageBox.Show("Them Thanh Cong!");
            }
            con.Close();
            loaddata();
            cleardata() ;
        }
    }
}
