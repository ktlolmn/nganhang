using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace NGANHANG
{
    public partial class frmMain : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public frmMain()
        {
            InitializeComponent();
            if (!mvvmContext1.IsDesignMode)
                InitializeBindings();
        }

        void InitializeBindings()
        {
            var fluent = mvvmContext1.OfType<MainViewModel>();
        }

        private Form CheckExists(Type TypeOfForm)
        {
            foreach(Form f in this.MdiChildren)
            {
                if (f.GetType() == TypeOfForm)
                {
                    return f;
                }
            }
            return null;
        }

        private void btnLogin_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Form form = this.CheckExists(typeof(frmLogin));
            if (form != null)
            {
                form.Activate();
            }
            else
            {
                frmLogin frmLogin = new frmLogin();
                frmLogin.MdiParent = this;
                frmLogin.Show();
                lblId.Text = "Mã Nhân Viên: " + Program.username + "";
                lblName.Text = "Họ tên: " + Program.mHoten + "";
                lblGroup.Text = "Vai trò: " + Program.mGroup + "";
            }
        }

        public void showInfo()
        {
            lblId.Text = "MÃ NHÂN VIÊN: " + Program.username;
            lblName.Text = "HỌ TÊN: " + Program.mHoten;
            lblGroup.Text = "VAI TRÒ: " + Program.mGroup;
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            
        }
    }
}
