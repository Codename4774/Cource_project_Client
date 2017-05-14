namespace Bomberman_client
{
    partial class FormGame
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
            this.PanelInfo = new System.Windows.Forms.Panel();
            this.MainField = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.MainField)).BeginInit();
            this.SuspendLayout();
            // 
            // PanelInfo
            // 
            this.PanelInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.PanelInfo.AutoSize = true;
            this.PanelInfo.Location = new System.Drawing.Point(544, 12);
            this.PanelInfo.Name = "PanelInfo";
            this.PanelInfo.Size = new System.Drawing.Size(161, 369);
            this.PanelInfo.TabIndex = 1;
            // 
            // MainField
            // 
            this.MainField.BackColor = System.Drawing.Color.ForestGreen;
            this.MainField.Location = new System.Drawing.Point(13, 12);
            this.MainField.Name = "MainField";
            this.MainField.Size = new System.Drawing.Size(514, 369);
            this.MainField.TabIndex = 2;
            this.MainField.TabStop = false;
            // 
            // FormGame
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(717, 393);
            this.Controls.Add(this.MainField);
            this.Controls.Add(this.PanelInfo);
            this.Name = "FormGame";
            this.Text = "FormGame";
            this.Load += new System.EventHandler(this.FormGame_Load);
            ((System.ComponentModel.ISupportInitialize)(this.MainField)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel PanelInfo;
        private System.Windows.Forms.PictureBox MainField;
    }
}