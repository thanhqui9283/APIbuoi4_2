using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NguyenThanhQui_5951071086
{
    public partial class Form1 : Form
    {
        SqlConnection con;
        private int StudentID;

        public Form1()
        {
            InitializeComponent();
         
           
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            GetStudentsRecord();
        }

        private void GetStudentsRecord()
        {//kết nối
            SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-4935HAB\SQLEXPRESS;Initial Catalog=Demo;Integrated Security=True ");
            // truy vấn
            SqlCommand com = new SqlCommand("select * from StudentTb", con);
            DataTable dt = new DataTable();
            con.Open();
            SqlDataReader sdr = com.ExecuteReader();
            dt.Load(sdr);
            con.Close();
            dataGridView1.DataSource = dt;
        }


        private bool IsValidData()
        {
            if (TxtHName.Text == String.Empty || TxtName.Text == String.Empty || TxtAddress.Text == String.Empty || string.IsNullOrEmpty(TxtPhone.Text) || string.IsNullOrEmpty(TxtRoll.Text))
            {
                MessageBox.Show("Có chỗ chưa nhập dữ liệu!!!", "Lỗi dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        private void ResetData()
        {
            TxtHName.Text= "";
            TxtName.Text = "";
            TxtAddress.Text= "";
            TxtRoll.Text = "";
           TxtPhone.Text = "";
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (IsValidData())
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("insert into StudentTb values (@Name, @FatherName, @RollNumber, @Address, @Mobile)", con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@Name", TxtHName.Text);
                cmd.Parameters.AddWithValue("@FatherName", TxtName.Text);
                cmd.Parameters.AddWithValue("@RollNumber", TxtRoll.Text);
                cmd.Parameters.AddWithValue("@Address", TxtAddress.Text);
                cmd.Parameters.AddWithValue("@Mobile", TxtPhone.Text);


                cmd.ExecuteNonQuery();
                con.Close();
                GetStudentsRecord();
            }
        }

        private void StudentRecordData_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int n = dataGridView1.CurrentRow.Index;
            StudentID = Convert.ToInt32(dataGridView1.Rows[n].Cells[0].Value);
            TxtHName.Text = dataGridView1.Rows[n].Cells[1].Value.ToString();
            TxtName.Text = dataGridView1.Rows[n].Cells[2].Value.ToString();
            TxtRoll.Text = dataGridView1.Rows[n].Cells[3].Value.ToString();
            TxtAddress.Text = dataGridView1.Rows[n].Cells[4].Value.ToString();
            TxtPhone.Text = dataGridView1.Rows[n].Cells[5].Value.ToString();
        }

        private void btnCapNhat_Click(object sender, EventArgs e)
        {
            if (StudentID > 0)
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("update StudentTb set Name = @Name, FatherName = @FatherName, RollNumber = @RollNumber, Address = @Address, Mobile = @Mobile where StudentID = @ID", con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@Name", TxtHName.Text);
                cmd.Parameters.AddWithValue("@FatherName", TxtName.Text);
                cmd.Parameters.AddWithValue("@RollNumber", TxtRoll.Text);
                cmd.Parameters.AddWithValue("@Address", TxtAddress.Text);
                cmd.Parameters.AddWithValue("@Mobile", TxtPhone.Text);
                cmd.Parameters.AddWithValue("ID", this.StudentID);
                cmd.ExecuteNonQuery();
                con.Close();
                GetStudentsRecord();
                ResetData();
            }
            else
            {
                MessageBox.Show("Cập nhật bị lỗi!!", "Lỗi !", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (StudentID > 0)
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("delete from StudentTb where StudentID = @ID", con);
                cmd.CommandType = CommandType.Text;

                cmd.Parameters.AddWithValue("ID", this.StudentID);
                cmd.ExecuteNonQuery();
                con.Close();
                GetStudentsRecord();
                ResetData();
            }
            else
            {
                MessageBox.Show("Cập nhật bị lỗi!!", "Lỗi !", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }
    }
}