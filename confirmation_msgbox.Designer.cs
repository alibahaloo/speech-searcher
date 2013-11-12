namespace Managed_SpeechRecognition
{
    partial class confirmation_msgbox
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
            this.yes_btn = new System.Windows.Forms.Button();
            this.reco_lbl = new System.Windows.Forms.Label();
            this.no_spell_btn = new System.Windows.Forms.Button();
            this.no_list_btn = new System.Windows.Forms.Button();
            this.cancel_btn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // yes_btn
            // 
            this.yes_btn.Location = new System.Drawing.Point(12, 25);
            this.yes_btn.Name = "yes_btn";
            this.yes_btn.Size = new System.Drawing.Size(75, 23);
            this.yes_btn.TabIndex = 0;
            this.yes_btn.Text = "Yes";
            this.yes_btn.UseVisualStyleBackColor = true;
            this.yes_btn.Click += new System.EventHandler(this.yes_btn_Click);
            // 
            // reco_lbl
            // 
            this.reco_lbl.AutoSize = true;
            this.reco_lbl.Location = new System.Drawing.Point(12, 9);
            this.reco_lbl.Name = "reco_lbl";
            this.reco_lbl.Size = new System.Drawing.Size(0, 13);
            this.reco_lbl.TabIndex = 1;
            // 
            // no_spell_btn
            // 
            this.no_spell_btn.Location = new System.Drawing.Point(93, 25);
            this.no_spell_btn.Name = "no_spell_btn";
            this.no_spell_btn.Size = new System.Drawing.Size(75, 23);
            this.no_spell_btn.TabIndex = 2;
            this.no_spell_btn.Text = "No | Spelling";
            this.no_spell_btn.UseVisualStyleBackColor = true;
            this.no_spell_btn.Click += new System.EventHandler(this.no_spell_btn_Click);
            // 
            // no_list_btn
            // 
            this.no_list_btn.Location = new System.Drawing.Point(174, 25);
            this.no_list_btn.Name = "no_list_btn";
            this.no_list_btn.Size = new System.Drawing.Size(75, 23);
            this.no_list_btn.TabIndex = 3;
            this.no_list_btn.Text = "No | List";
            this.no_list_btn.UseVisualStyleBackColor = true;
            this.no_list_btn.Click += new System.EventHandler(this.no_list_btn_Click);
            // 
            // cancel_btn
            // 
            this.cancel_btn.Location = new System.Drawing.Point(255, 25);
            this.cancel_btn.Name = "cancel_btn";
            this.cancel_btn.Size = new System.Drawing.Size(75, 23);
            this.cancel_btn.TabIndex = 4;
            this.cancel_btn.Text = "Cancel";
            this.cancel_btn.UseVisualStyleBackColor = true;
            this.cancel_btn.Click += new System.EventHandler(this.cancel_btn_Click);
            // 
            // confirmation_msgbox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(342, 57);
            this.Controls.Add(this.cancel_btn);
            this.Controls.Add(this.no_list_btn);
            this.Controls.Add(this.no_spell_btn);
            this.Controls.Add(this.reco_lbl);
            this.Controls.Add(this.yes_btn);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "confirmation_msgbox";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Confirmation";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.confirmation_msgbox_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button yes_btn;
        private System.Windows.Forms.Label reco_lbl;
        private System.Windows.Forms.Button no_spell_btn;
        private System.Windows.Forms.Button no_list_btn;
        private System.Windows.Forms.Button cancel_btn;
    }
}