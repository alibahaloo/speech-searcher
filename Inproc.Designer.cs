namespace Managed_SpeechRecognition
{
    partial class Inproc
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
            this.components = new System.ComponentModel.Container();
            this.kill_all_btn = new System.Windows.Forms.Button();
            this.sys_start_btn = new System.Windows.Forms.Button();
            this.candidates_listbox = new System.Windows.Forms.ListBox();
            this.select_fromlistbox_btn = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.status_lbl = new System.Windows.Forms.Label();
            this.mute_box = new System.Windows.Forms.CheckBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.add_word_button = new System.Windows.Forms.Button();
            this.new_word_textBox = new System.Windows.Forms.TextBox();
            this.add_word_checkBox = new System.Windows.Forms.CheckBox();
            this.SR_comboBox = new System.Windows.Forms.ComboBox();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // kill_all_btn
            // 
            this.kill_all_btn.Enabled = false;
            this.kill_all_btn.Location = new System.Drawing.Point(12, 177);
            this.kill_all_btn.Name = "kill_all_btn";
            this.kill_all_btn.Size = new System.Drawing.Size(75, 23);
            this.kill_all_btn.TabIndex = 3;
            this.kill_all_btn.Text = "Stop System";
            this.kill_all_btn.UseVisualStyleBackColor = true;
            this.kill_all_btn.Click += new System.EventHandler(this.kill_all_btn_Click);
            // 
            // sys_start_btn
            // 
            this.sys_start_btn.Location = new System.Drawing.Point(12, 34);
            this.sys_start_btn.Name = "sys_start_btn";
            this.sys_start_btn.Size = new System.Drawing.Size(75, 23);
            this.sys_start_btn.TabIndex = 6;
            this.sys_start_btn.Text = "Start System";
            this.sys_start_btn.UseVisualStyleBackColor = true;
            this.sys_start_btn.Click += new System.EventHandler(this.sys_start_btn_Click);
            // 
            // candidates_listbox
            // 
            this.candidates_listbox.FormattingEnabled = true;
            this.candidates_listbox.HorizontalScrollbar = true;
            this.candidates_listbox.Location = new System.Drawing.Point(12, 76);
            this.candidates_listbox.Name = "candidates_listbox";
            this.candidates_listbox.Size = new System.Drawing.Size(293, 95);
            this.candidates_listbox.TabIndex = 8;
            // 
            // select_fromlistbox_btn
            // 
            this.select_fromlistbox_btn.Location = new System.Drawing.Point(250, 177);
            this.select_fromlistbox_btn.Name = "select_fromlistbox_btn";
            this.select_fromlistbox_btn.Size = new System.Drawing.Size(55, 23);
            this.select_fromlistbox_btn.TabIndex = 10;
            this.select_fromlistbox_btn.Text = "Select";
            this.select_fromlistbox_btn.UseVisualStyleBackColor = true;
            this.select_fromlistbox_btn.Click += new System.EventHandler(this.select_fromlistbox_btn_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 60);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 13);
            this.label1.TabIndex = 11;
            this.label1.Text = "Candidates List:";
            // 
            // status_lbl
            // 
            this.status_lbl.AutoSize = true;
            this.status_lbl.Dock = System.Windows.Forms.DockStyle.Right;
            this.status_lbl.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.status_lbl.Location = new System.Drawing.Point(236, 0);
            this.status_lbl.Name = "status_lbl";
            this.status_lbl.Size = new System.Drawing.Size(81, 13);
            this.status_lbl.TabIndex = 12;
            this.status_lbl.Text = "Status: Inactive";
            this.status_lbl.TextChanged += new System.EventHandler(this.status_lbl_TextChanged);
            // 
            // mute_box
            // 
            this.mute_box.AutoSize = true;
            this.mute_box.Enabled = false;
            this.mute_box.Location = new System.Drawing.Point(267, 16);
            this.mute_box.Name = "mute_box";
            this.mute_box.Size = new System.Drawing.Size(50, 17);
            this.mute_box.TabIndex = 13;
            this.mute_box.Text = "Mute";
            this.mute_box.UseVisualStyleBackColor = true;
            this.mute_box.CheckedChanged += new System.EventHandler(this.mute_box_CheckedChanged);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.add_word_button);
            this.panel1.Controls.Add(this.new_word_textBox);
            this.panel1.Enabled = false;
            this.panel1.Location = new System.Drawing.Point(15, 230);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(290, 29);
            this.panel1.TabIndex = 14;
            // 
            // add_word_button
            // 
            this.add_word_button.Location = new System.Drawing.Point(235, 1);
            this.add_word_button.Name = "add_word_button";
            this.add_word_button.Size = new System.Drawing.Size(52, 23);
            this.add_word_button.TabIndex = 2;
            this.add_word_button.Text = "Add";
            this.add_word_button.UseVisualStyleBackColor = true;
            this.add_word_button.Click += new System.EventHandler(this.add_word_button_Click);
            // 
            // new_word_textBox
            // 
            this.new_word_textBox.Location = new System.Drawing.Point(3, 3);
            this.new_word_textBox.Name = "new_word_textBox";
            this.new_word_textBox.Size = new System.Drawing.Size(226, 20);
            this.new_word_textBox.TabIndex = 1;
            // 
            // add_word_checkBox
            // 
            this.add_word_checkBox.AutoSize = true;
            this.add_word_checkBox.Location = new System.Drawing.Point(15, 207);
            this.add_word_checkBox.Name = "add_word_checkBox";
            this.add_word_checkBox.Size = new System.Drawing.Size(134, 17);
            this.add_word_checkBox.TabIndex = 15;
            this.add_word_checkBox.Text = "Add word to dictionary:";
            this.add_word_checkBox.UseVisualStyleBackColor = true;
            this.add_word_checkBox.CheckedChanged += new System.EventHandler(this.add_word_checkBox_CheckedChanged);
            // 
            // SR_comboBox
            // 
            this.SR_comboBox.FormattingEnabled = true;
            this.SR_comboBox.Items.AddRange(new object[] {
            "Searcher",
            "Go to"});
            this.SR_comboBox.Location = new System.Drawing.Point(12, 7);
            this.SR_comboBox.Name = "SR_comboBox";
            this.SR_comboBox.Size = new System.Drawing.Size(121, 21);
            this.SR_comboBox.TabIndex = 17;
            this.SR_comboBox.Text = "Choose SR";
            this.SR_comboBox.SelectedIndexChanged += new System.EventHandler(this.SR_comboBox_SelectedIndexChanged);
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(15, 266);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(287, 10);
            this.progressBar.TabIndex = 18;
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // Inproc
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(317, 284);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.SR_comboBox);
            this.Controls.Add(this.add_word_checkBox);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.mute_box);
            this.Controls.Add(this.status_lbl);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.select_fromlistbox_btn);
            this.Controls.Add(this.candidates_listbox);
            this.Controls.Add(this.sys_start_btn);
            this.Controls.Add(this.kill_all_btn);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Inproc";
            this.Text = "Speech Searcher";
            this.TopMost = true;
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button kill_all_btn;
        private System.Windows.Forms.Button sys_start_btn;
        private System.Windows.Forms.ListBox candidates_listbox;
        private System.Windows.Forms.Button select_fromlistbox_btn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label status_lbl;
        private System.Windows.Forms.CheckBox mute_box;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button add_word_button;
        private System.Windows.Forms.TextBox new_word_textBox;
        private System.Windows.Forms.CheckBox add_word_checkBox;
        private System.Windows.Forms.ComboBox SR_comboBox;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Timer timer1;
    }
}

