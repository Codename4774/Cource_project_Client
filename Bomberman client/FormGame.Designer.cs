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
            this.PanelInfo.Location = new System.Drawing.Point(458, 8);
            this.PanelInfo.Name = "PanelInfo";
            this.PanelInfo.Size = new System.Drawing.Size(161, 384);
            this.PanelInfo.TabIndex = 1;
            // 
            // MainField
            // 
            this.MainField.BackColor = System.Drawing.Color.ForestGreen;
            this.MainField.Location = new System.Drawing.Point(12, 8);
            this.MainField.Name = "MainField";
            this.MainField.Size = new System.Drawing.Size(432, 384);
            this.MainField.TabIndex = 2;
            this.MainField.TabStop = false;
            // 
            // FormGame
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(631, 404);
            this.Controls.Add(this.MainField);
            this.Controls.Add(this.PanelInfo);
            this.DoubleBuffered = true;
            this.Name = "FormGame";
            this.Text = "Bomberman";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormGame_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormGame_FormClosed);
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