using DevExpress.XtraEditors;
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

namespace NGANHANG
{
    public partial class frmLogin : DevExpress.XtraEditors.XtraForm
    {
        private SqlConnection connPublisher = new SqlConnection();
        public frmLogin()
        {
            InitializeComponent();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void gridControl1_Click(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'nGANHANGDataSet1.V_DS_PHANMANH' table. You can move, or remove it, as needed.
            this.v_DS_PHANMANHTableAdapter.Fill(this.nGANHANGDataSet1.V_DS_PHANMANH);
            if (KetNoiDatabaseGoc() == 0)
            {
                return;
            }
            else
            {
                layDanhSachPhanManh("select top 2 * from dbo.V_DS_PHANMANH");
            }
        }

        private int KetNoiDatabaseGoc()
        {
            if (connPublisher != null && connPublisher.State == ConnectionState.Open)
                connPublisher.Close();
            try
            {
                connPublisher.ConnectionString = Program.connstrPublisher;
                connPublisher.Open();

                return 1;
            }

            catch (Exception e)
            {
                MessageBox.Show("Lỗi kết nối cơ sở dữ liệu.\nBạn xem lại user name và password.\n " + e.Message, "", MessageBoxButtons.OK);
                return 0;
            }
        }

        private void layDanhSachPhanManh(String cmd)
        {
            if (connPublisher.State == ConnectionState.Closed)
            {
                connPublisher.Open();
            }
            DataTable dt = new DataTable();
            // adapter dùng để đưa dữ liệu từ view sang database
            SqlDataAdapter da = new SqlDataAdapter(cmd, connPublisher);
            // dùng adapter thì mới đổ vào data table được
            da.Fill(dt);
            connPublisher.Close();
            Program.bindingSource.DataSource = dt;
            cbBranch.DataSource = Program.bindingSource;
            cbBranch.DisplayMember = "TENCN";
            cbBranch.ValueMember = "TENSERVER";
        }
        private DataTable layThongTinNV()
        {
            DataTable dataTable = new DataTable();
            try
            {
                using (SqlCommand command = new SqlCommand("SELECT * FROM NHANVIEN", Program.conn))
                {
                    Program.conn.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        dataTable.Load(reader);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi lấy dữ liệu từ cơ sở dữ liệu: {ex.Message}");
            }
            finally
            {
                Program.conn.Close();
            }
            return dataTable;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {

            String loginName = txtUsername.Text.Trim();
            if (string.IsNullOrEmpty(loginName))
            {
                MessageBox.Show("Mã nhân viên không được trống");
                return;
            }

            string pass = txtPassword.Text.Trim();
            if (string.IsNullOrEmpty(pass))
            {
                MessageBox.Show("Mật khẩu không được trống");
                return;
            }

            String servername = cbBranch.SelectedValue.ToString();
            Program.servername = servername;
            Program.mlogin = txtUsername.Text.ToString();
            Program.password = txtPassword.Text.ToString();
            Program.KetNoi();
            String cmd = "exec SP_LayThongTinNhanVien '" + Program.mlogin + "'";
            Program.myReader = Program.ExecSqlDataReader(cmd);
            if (Program.myReader == null)
            {
                return;
            }
            Program.myReader.Read();
            Program.username = Program.myReader.GetString(0);
            if (Convert.IsDBNull(Program.username))
            {
                MessageBox.Show("Login nhập vào không có quyền truy cập!");
                return;
            }
            Program.mHoten = Program.myReader.GetString(1);
            Program.mGroup = Program.myReader.GetString(2);
            Program.myReader.Close();
            Program.conn.Close();
            Program.frmMain.showInfo();
        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void fillByToolStripButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.v_DS_PHANMANHTableAdapter.FillBy(this.nGANHANGDataSet1.V_DS_PHANMANH);
            }
            catch (System.Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }

        }

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {

        }
    }
}