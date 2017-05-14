namespace Bomberman_client
{
    partial class FormConnect
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
            this.TextBoxServerName = new System.Windows.Forms.TextBox();
            this.LabelServerName = new System.Windows.Forms.Label();
            this.ButtonConnect = new System.Windows.Forms.Button();
            this.TextBoxNickname = new System.Windows.Forms.TextBox();
            this.LabelNickname = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // TextBoxServerName
            // 
            this.TextBoxServerName.Location = new System.Drawing.Point(88, 9);
            this.TextBoxServerName.Name = "TextBoxServerName";
            this.TextBoxServerName.Size = new System.Drawing.Size(225, 20);
            this.TextBoxServerName.TabIndex = 0;
            // 
            // LabelServerName
            // 
            this.LabelServerName.AutoSize = true;
            this.LabelServerName.Location = new System.Drawing.Point(12, 9);
            this.LabelServerName.Name = "LabelServerName";
            this.LabelServerName.Size = new System.Drawing.Size(70, 13);
            this.LabelServerName.TabIndex = 1;
            this.LabelServerName.Text = "Server name:";
            // 
            // ButtonConnect
            // 
            this.ButtonConnect.Location = new System.Drawing.Point(107, 73);
            this.ButtonConnect.Name = "ButtonConnect";
            this.ButtonConnect.Size = new System.Drawing.Size(104, 29);
            this.ButtonConnect.TabIndex = 2;
            this.ButtonConnect.Text = "Connect";
            this.ButtonConnect.UseVisualStyleBackColor = true;
            this.ButtonConnect.Click += new System.EventHandler(this.ButtonConnect_Click);
            // 
            // TextBoxNickname
            // 
            this.TextBoxNickname.Location = new System.Drawing.Point(88, 35);
            this.TextBoxNickname.Name = "TextBoxNickname";
            this.TextBoxNickname.Size = new System.Drawing.Size(225, 20);
            this.TextBoxNickname.TabIndex = 3;
            // 
            // LabelNickname
            // 
            this.LabelNickname.AutoSize = true;
            this.LabelNickname.Location = new System.Drawing.Point(24, 35);
            this.LabelNickname.Name = "LabelNickname";
            this.LabelNickname.Size = new System.Drawing.Size(58, 13);
            this.LabelNickname.TabIndex = 4;
            this.LabelNickname.Text = "Nickname:";
            // 
            // FormConnect
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(325, 114);
            this.Controls.Add(this.LabelNickname);
            this.Controls.Add(this.TextBoxNickname);
            this.Controls.Add(this.ButtonConnect);
            this.Controls.Add(this.LabelServerName);
            this.Controls.Add(this.TextBoxServerName);
            this.Name = "FormConnect";
            this.Text = "Bomberman";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox TextBoxServerName;
        private System.Windows.Forms.Label LabelServerName;
        private System.Windows.Forms.Button ButtonConnect;
        private System.Windows.Forms.TextBox TextBoxNickname;
        private System.Windows.Forms.Label LabelNickname;
    }
}

