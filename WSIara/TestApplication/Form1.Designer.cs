namespace TestApplication
{
    partial class frmTests
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnSaveUser = new System.Windows.Forms.Button();
            this.GPBGetUser = new System.Windows.Forms.GroupBox();
            this.lblGetUserRet = new System.Windows.Forms.Label();
            this.txtPass = new System.Windows.Forms.TextBox();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.btnGetUser = new System.Windows.Forms.Button();
            this.lblCapPass = new System.Windows.Forms.Label();
            this.lblCapEmail = new System.Windows.Forms.Label();
            this.grpAddUser = new System.Windows.Forms.GroupBox();
            this.lblAddUser = new System.Windows.Forms.Label();
            this.grpDeleteUser = new System.Windows.Forms.GroupBox();
            this.lblDelUserRet = new System.Windows.Forms.Label();
            this.txtPassDel = new System.Windows.Forms.TextBox();
            this.txtEmailDel = new System.Windows.Forms.TextBox();
            this.btnDelUser = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.grpUpdtUser = new System.Windows.Forms.GroupBox();
            this.lblUpdtUserRet = new System.Windows.Forms.Label();
            this.btnUpdtUser = new System.Windows.Forms.Button();
            this.txtUserName = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtPassUpdt = new System.Windows.Forms.TextBox();
            this.txtEmailUpdt = new System.Windows.Forms.TextBox();
            this.btnGetUserToUpdt = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.grpUser = new System.Windows.Forms.GroupBox();
            this.lblUserNameDesc = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.lblEmailDesc = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.GPBGetUser.SuspendLayout();
            this.grpAddUser.SuspendLayout();
            this.grpDeleteUser.SuspendLayout();
            this.grpUpdtUser.SuspendLayout();
            this.grpUser.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnSaveUser
            // 
            this.btnSaveUser.Location = new System.Drawing.Point(6, 19);
            this.btnSaveUser.Name = "btnSaveUser";
            this.btnSaveUser.Size = new System.Drawing.Size(75, 23);
            this.btnSaveUser.TabIndex = 0;
            this.btnSaveUser.Text = "Add User";
            this.btnSaveUser.UseVisualStyleBackColor = true;
            this.btnSaveUser.Click += new System.EventHandler(this.btnSaveUser_Click);
            // 
            // GPBGetUser
            // 
            this.GPBGetUser.Controls.Add(this.lblGetUserRet);
            this.GPBGetUser.Controls.Add(this.txtPass);
            this.GPBGetUser.Controls.Add(this.txtEmail);
            this.GPBGetUser.Controls.Add(this.btnGetUser);
            this.GPBGetUser.Controls.Add(this.lblCapPass);
            this.GPBGetUser.Controls.Add(this.lblCapEmail);
            this.GPBGetUser.Location = new System.Drawing.Point(12, 103);
            this.GPBGetUser.Name = "GPBGetUser";
            this.GPBGetUser.Size = new System.Drawing.Size(242, 105);
            this.GPBGetUser.TabIndex = 1;
            this.GPBGetUser.TabStop = false;
            this.GPBGetUser.Text = "GetUser";
            // 
            // lblGetUserRet
            // 
            this.lblGetUserRet.AutoSize = true;
            this.lblGetUserRet.Location = new System.Drawing.Point(87, 81);
            this.lblGetUserRet.Name = "lblGetUserRet";
            this.lblGetUserRet.Size = new System.Drawing.Size(35, 13);
            this.lblGetUserRet.TabIndex = 3;
            this.lblGetUserRet.Text = "label1";
            // 
            // txtPass
            // 
            this.txtPass.Location = new System.Drawing.Point(87, 57);
            this.txtPass.Name = "txtPass";
            this.txtPass.Size = new System.Drawing.Size(149, 20);
            this.txtPass.TabIndex = 4;
            // 
            // txtEmail
            // 
            this.txtEmail.Location = new System.Drawing.Point(87, 26);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(149, 20);
            this.txtEmail.TabIndex = 3;
            // 
            // btnGetUser
            // 
            this.btnGetUser.Location = new System.Drawing.Point(6, 76);
            this.btnGetUser.Name = "btnGetUser";
            this.btnGetUser.Size = new System.Drawing.Size(75, 23);
            this.btnGetUser.TabIndex = 2;
            this.btnGetUser.Text = "GetUser";
            this.btnGetUser.UseVisualStyleBackColor = true;
            this.btnGetUser.Click += new System.EventHandler(this.btnGetUser_Click);
            // 
            // lblCapPass
            // 
            this.lblCapPass.AutoSize = true;
            this.lblCapPass.Location = new System.Drawing.Point(6, 60);
            this.lblCapPass.Name = "lblCapPass";
            this.lblCapPass.Size = new System.Drawing.Size(41, 13);
            this.lblCapPass.TabIndex = 2;
            this.lblCapPass.Text = "Senha:";
            // 
            // lblCapEmail
            // 
            this.lblCapEmail.AutoSize = true;
            this.lblCapEmail.Location = new System.Drawing.Point(6, 29);
            this.lblCapEmail.Name = "lblCapEmail";
            this.lblCapEmail.Size = new System.Drawing.Size(35, 13);
            this.lblCapEmail.TabIndex = 2;
            this.lblCapEmail.Text = "Email:";
            // 
            // grpAddUser
            // 
            this.grpAddUser.Controls.Add(this.lblAddUser);
            this.grpAddUser.Controls.Add(this.btnSaveUser);
            this.grpAddUser.Location = new System.Drawing.Point(12, 12);
            this.grpAddUser.Name = "grpAddUser";
            this.grpAddUser.Size = new System.Drawing.Size(242, 85);
            this.grpAddUser.TabIndex = 2;
            this.grpAddUser.TabStop = false;
            this.grpAddUser.Text = "AddUser";
            // 
            // lblAddUser
            // 
            this.lblAddUser.AutoSize = true;
            this.lblAddUser.Location = new System.Drawing.Point(6, 45);
            this.lblAddUser.Name = "lblAddUser";
            this.lblAddUser.Size = new System.Drawing.Size(35, 13);
            this.lblAddUser.TabIndex = 3;
            this.lblAddUser.Text = "label1";
            // 
            // grpDeleteUser
            // 
            this.grpDeleteUser.Controls.Add(this.lblDelUserRet);
            this.grpDeleteUser.Controls.Add(this.txtPassDel);
            this.grpDeleteUser.Controls.Add(this.txtEmailDel);
            this.grpDeleteUser.Controls.Add(this.btnDelUser);
            this.grpDeleteUser.Controls.Add(this.label2);
            this.grpDeleteUser.Controls.Add(this.label3);
            this.grpDeleteUser.Location = new System.Drawing.Point(12, 412);
            this.grpDeleteUser.Name = "grpDeleteUser";
            this.grpDeleteUser.Size = new System.Drawing.Size(242, 105);
            this.grpDeleteUser.TabIndex = 5;
            this.grpDeleteUser.TabStop = false;
            this.grpDeleteUser.Text = "DeleteUser";
            // 
            // lblDelUserRet
            // 
            this.lblDelUserRet.AutoSize = true;
            this.lblDelUserRet.Location = new System.Drawing.Point(87, 81);
            this.lblDelUserRet.Name = "lblDelUserRet";
            this.lblDelUserRet.Size = new System.Drawing.Size(35, 13);
            this.lblDelUserRet.TabIndex = 3;
            this.lblDelUserRet.Text = "label1";
            // 
            // txtPassDel
            // 
            this.txtPassDel.Location = new System.Drawing.Point(87, 57);
            this.txtPassDel.Name = "txtPassDel";
            this.txtPassDel.Size = new System.Drawing.Size(149, 20);
            this.txtPassDel.TabIndex = 4;
            // 
            // txtEmailDel
            // 
            this.txtEmailDel.Location = new System.Drawing.Point(87, 26);
            this.txtEmailDel.Name = "txtEmailDel";
            this.txtEmailDel.Size = new System.Drawing.Size(149, 20);
            this.txtEmailDel.TabIndex = 3;
            // 
            // btnDelUser
            // 
            this.btnDelUser.Location = new System.Drawing.Point(6, 76);
            this.btnDelUser.Name = "btnDelUser";
            this.btnDelUser.Size = new System.Drawing.Size(75, 23);
            this.btnDelUser.TabIndex = 2;
            this.btnDelUser.Text = "DelUser";
            this.btnDelUser.UseVisualStyleBackColor = true;
            this.btnDelUser.Click += new System.EventHandler(this.btnDelUser_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 60);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Senha:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 29);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Email:";
            // 
            // grpUpdtUser
            // 
            this.grpUpdtUser.Controls.Add(this.lblUpdtUserRet);
            this.grpUpdtUser.Controls.Add(this.btnUpdtUser);
            this.grpUpdtUser.Controls.Add(this.txtUserName);
            this.grpUpdtUser.Controls.Add(this.label8);
            this.grpUpdtUser.Controls.Add(this.txtPassUpdt);
            this.grpUpdtUser.Controls.Add(this.txtEmailUpdt);
            this.grpUpdtUser.Controls.Add(this.btnGetUserToUpdt);
            this.grpUpdtUser.Controls.Add(this.label4);
            this.grpUpdtUser.Controls.Add(this.label5);
            this.grpUpdtUser.Location = new System.Drawing.Point(12, 214);
            this.grpUpdtUser.Name = "grpUpdtUser";
            this.grpUpdtUser.Size = new System.Drawing.Size(242, 153);
            this.grpUpdtUser.TabIndex = 5;
            this.grpUpdtUser.TabStop = false;
            this.grpUpdtUser.Text = "UpdtUser";
            // 
            // lblUpdtUserRet
            // 
            this.lblUpdtUserRet.AutoSize = true;
            this.lblUpdtUserRet.Location = new System.Drawing.Point(84, 129);
            this.lblUpdtUserRet.Name = "lblUpdtUserRet";
            this.lblUpdtUserRet.Size = new System.Drawing.Size(35, 13);
            this.lblUpdtUserRet.TabIndex = 8;
            this.lblUpdtUserRet.Text = "label1";
            // 
            // btnUpdtUser
            // 
            this.btnUpdtUser.Location = new System.Drawing.Point(6, 124);
            this.btnUpdtUser.Name = "btnUpdtUser";
            this.btnUpdtUser.Size = new System.Drawing.Size(75, 23);
            this.btnUpdtUser.TabIndex = 7;
            this.btnUpdtUser.Text = "UpdtUser";
            this.btnUpdtUser.UseVisualStyleBackColor = true;
            this.btnUpdtUser.Click += new System.EventHandler(this.btnUpdtUser_Click);
            // 
            // txtUserName
            // 
            this.txtUserName.Location = new System.Drawing.Point(87, 105);
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.Size = new System.Drawing.Size(149, 20);
            this.txtUserName.TabIndex = 6;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 108);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(46, 13);
            this.label8.TabIndex = 5;
            this.label8.Text = "Usuário:";
            // 
            // txtPassUpdt
            // 
            this.txtPassUpdt.Location = new System.Drawing.Point(87, 57);
            this.txtPassUpdt.Name = "txtPassUpdt";
            this.txtPassUpdt.Size = new System.Drawing.Size(149, 20);
            this.txtPassUpdt.TabIndex = 4;
            // 
            // txtEmailUpdt
            // 
            this.txtEmailUpdt.Location = new System.Drawing.Point(87, 26);
            this.txtEmailUpdt.Name = "txtEmailUpdt";
            this.txtEmailUpdt.Size = new System.Drawing.Size(149, 20);
            this.txtEmailUpdt.TabIndex = 3;
            // 
            // btnGetUserToUpdt
            // 
            this.btnGetUserToUpdt.Location = new System.Drawing.Point(6, 76);
            this.btnGetUserToUpdt.Name = "btnGetUserToUpdt";
            this.btnGetUserToUpdt.Size = new System.Drawing.Size(75, 23);
            this.btnGetUserToUpdt.TabIndex = 2;
            this.btnGetUserToUpdt.Text = "GetUser";
            this.btnGetUserToUpdt.UseVisualStyleBackColor = true;
            this.btnGetUserToUpdt.Click += new System.EventHandler(this.btnGetUserToUpdt_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 60);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 13);
            this.label4.TabIndex = 2;
            this.label4.Text = "Senha:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 29);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(35, 13);
            this.label5.TabIndex = 2;
            this.label5.Text = "Email:";
            // 
            // grpUser
            // 
            this.grpUser.Controls.Add(this.lblUserNameDesc);
            this.grpUser.Controls.Add(this.label7);
            this.grpUser.Controls.Add(this.lblEmailDesc);
            this.grpUser.Controls.Add(this.label6);
            this.grpUser.Location = new System.Drawing.Point(260, 12);
            this.grpUser.Name = "grpUser";
            this.grpUser.Size = new System.Drawing.Size(200, 85);
            this.grpUser.TabIndex = 6;
            this.grpUser.TabStop = false;
            this.grpUser.Text = "Dados do Usuário";
            // 
            // lblUserNameDesc
            // 
            this.lblUserNameDesc.AutoSize = true;
            this.lblUserNameDesc.Location = new System.Drawing.Point(58, 45);
            this.lblUserNameDesc.Name = "lblUserNameDesc";
            this.lblUserNameDesc.Size = new System.Drawing.Size(35, 13);
            this.lblUserNameDesc.TabIndex = 3;
            this.lblUserNameDesc.Text = "label8";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 45);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(46, 13);
            this.label7.TabIndex = 2;
            this.label7.Text = "Usuário:";
            // 
            // lblEmailDesc
            // 
            this.lblEmailDesc.AutoSize = true;
            this.lblEmailDesc.Location = new System.Drawing.Point(58, 24);
            this.lblEmailDesc.Name = "lblEmailDesc";
            this.lblEmailDesc.Size = new System.Drawing.Size(35, 13);
            this.lblEmailDesc.TabIndex = 1;
            this.lblEmailDesc.Text = "label7";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 24);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(35, 13);
            this.label6.TabIndex = 0;
            this.label6.Text = "Email:";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(278, 214);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 7;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // frmTests
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(466, 527);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.grpUser);
            this.Controls.Add(this.grpUpdtUser);
            this.Controls.Add(this.grpDeleteUser);
            this.Controls.Add(this.grpAddUser);
            this.Controls.Add(this.GPBGetUser);
            this.Name = "frmTests";
            this.Text = "Testes";
            this.GPBGetUser.ResumeLayout(false);
            this.GPBGetUser.PerformLayout();
            this.grpAddUser.ResumeLayout(false);
            this.grpAddUser.PerformLayout();
            this.grpDeleteUser.ResumeLayout(false);
            this.grpDeleteUser.PerformLayout();
            this.grpUpdtUser.ResumeLayout(false);
            this.grpUpdtUser.PerformLayout();
            this.grpUser.ResumeLayout(false);
            this.grpUser.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnSaveUser;
        private System.Windows.Forms.GroupBox GPBGetUser;
        private System.Windows.Forms.Button btnGetUser;
        private System.Windows.Forms.Label lblCapPass;
        private System.Windows.Forms.Label lblCapEmail;
        private System.Windows.Forms.GroupBox grpAddUser;
        private System.Windows.Forms.Label lblAddUser;
        private System.Windows.Forms.Label lblGetUserRet;
        private System.Windows.Forms.TextBox txtPass;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.GroupBox grpDeleteUser;
        private System.Windows.Forms.Label lblDelUserRet;
        private System.Windows.Forms.TextBox txtPassDel;
        private System.Windows.Forms.TextBox txtEmailDel;
        private System.Windows.Forms.Button btnDelUser;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox grpUpdtUser;
        private System.Windows.Forms.TextBox txtPassUpdt;
        private System.Windows.Forms.TextBox txtEmailUpdt;
        private System.Windows.Forms.Button btnGetUserToUpdt;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox grpUser;
        private System.Windows.Forms.Label lblUserNameDesc;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lblEmailDesc;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtUserName;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button btnUpdtUser;
        private System.Windows.Forms.Label lblUpdtUserRet;
        private System.Windows.Forms.Button button1;
    }
}

