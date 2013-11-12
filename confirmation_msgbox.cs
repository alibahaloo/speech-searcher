using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Managed_SpeechRecognition
{

    public partial class confirmation_msgbox : Form
    {
        public string reco_phrase;
        static confirmation_msgbox obj_msgbox;
        public static int return_value = 0;
        //return_value = 0 | Cancel
        //return_value = 1 | Yes
        //return_value = 2 | No... Spelling
        //return_value = 3 | No... Candidate List
        public confirmation_msgbox()
        {
            InitializeComponent();
        }

        //function: opens this form
        public static int showbox(string word)
        {
            obj_msgbox = new confirmation_msgbox();
            obj_msgbox.reco_phrase = word;
            obj_msgbox.ShowDialog();
            return return_value;
        }
        //Event: yes button
        private void yes_btn_Click(object sender, EventArgs e)
        {
            return_value = 1;
            obj_msgbox.Dispose();
        }
        //Event: no...spell
        private void no_spell_btn_Click(object sender, EventArgs e)
        {
            return_value = 2;
            obj_msgbox.Dispose();
        }
        //Event: no...Candidate list
        private void no_list_btn_Click(object sender, EventArgs e)
        {
            return_value = 3;
            obj_msgbox.Dispose();
        }
        //Event: cancel
        private void cancel_btn_Click(object sender, EventArgs e)
        {
            return_value = 0;
            obj_msgbox.Dispose();
            //components.Dispose();
        }
        //Event: loading elements of this form
        private void confirmation_msgbox_Load(object sender, EventArgs e)
        {
            string reco = reco_phrase;
            reco_lbl.Text = "Did you say: " + '"' + reco + '"' + " ?";
        }
    }
}
