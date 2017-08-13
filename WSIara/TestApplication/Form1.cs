using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestApplication
{
    public partial class frmTests : Form
    {
        IaraModels.User user = null;
        public frmTests()
        {
            InitializeComponent();
        }

        private IaraModels.User CreateUser()
        {
            return new IaraModels.User()
            {
                email = "teste@teste.com",
                password = "teste",
                synchronizedInToMobile = true,
                synchronizedInToServer = true,
                updtDTime = DateTime.Now,
                userName = "teste",
                deleted = false
            };
        }

        private void btnSaveUser_Click(object sender, EventArgs e)
        {
            IaraWrapper.IaraWrapper iw = new IaraWrapper.IaraWrapper(String.Empty, String.Empty);
            lblAddUser.Text = iw.SaveUser(CreateUser()).ToString();
        }

        private void btnGetUser_Click(object sender, EventArgs e)
        {
            IaraWrapper.IaraWrapper iw = new IaraWrapper.IaraWrapper(txtEmail.Text, txtPass.Text);
            lblGetUserRet.Text = iw.GetUser(txtEmail.Text).userName;
        }

        private void btnDelUser_Click(object sender, EventArgs e)
        {
            IaraWrapper.IaraWrapper iw = new IaraWrapper.IaraWrapper(txtEmailDel.Text, txtPassDel.Text);
            lblDelUserRet.Text = iw.DeleteUser(txtEmailDel.Text).ToString();
        }

        private void btnGetUserToUpdt_Click(object sender, EventArgs e)
        {
            IaraWrapper.IaraWrapper iw = new IaraWrapper.IaraWrapper(txtEmailUpdt.Text, txtPassUpdt.Text);
            user = iw.GetUser(txtEmailUpdt.Text);

            lblEmailDesc.Text = user.email;
            lblUserNameDesc.Text = user.userName;
        }

        private void btnUpdtUser_Click(object sender, EventArgs e)
        {
            if (user != null && txtUserName.Text != String.Empty)
            {
                IaraWrapper.IaraWrapper iw = new IaraWrapper.IaraWrapper(txtEmailUpdt.Text, txtPassUpdt.Text);
                user.userName = txtUserName.Text;
                lblUpdtUserRet.Text = iw.UpdateUser(user).ToString();
            }
        }
    }
}
