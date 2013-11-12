using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
//Speech namespaces
using System.Speech;
using System.Speech.Recognition;
using System.Speech.Recognition.SrgsGrammar;
//XML namespaces
using System.Xml;
using System.Xml.XPath;
//Thread namespaces
using System.Threading;

namespace Managed_SpeechRecognition
{
    public partial class Inproc : Form
    {
        #region objects_decleration
        //Objects needed for different functions
        //protected
        private SpeechRecognitionEngine InprocRecognizer;
        private Thread alternatives_thread;
        private Thread dialog_thread;
        private Thread status_thread;
        private Thread spelled_txtbox_thread;
        //protected
        private confirmation_msgbox obj_msgbox;
        //Strings needed for different functions
        public string place_path;
        public string srgs_path = "srgs.xml";
        private string diff_index = null;
        private string search_engine = null;
        private string value = null;
        private string candidate_list = null;
        private string status_txt;
        public string[] tokens = null;
        public string confirmation_word;
        //domain parts for each domain..protected
        private string googlesearch_part1 = "http://www.google.com/#hl=en&source=hp&q=";
        private string googlesearch_part2 = "&aq=f&aqi=g10&aql=&oq=&gs_rfai=&fp=464fcfd9ec5e5f53";
        private string yahoosearch_part1 = "http://search.yahoo.com/search;_ylt=AjjHxSfBhvNtNhoiaWMiGOGbvZx4?p=";
        private string yahoosearch_part2 = "&toggle=1&cop=mss&ei=UTF-8&fr=yfp-t-701";
        private string wikipedia_domain = "http://en.wikipedia.org/wiki/";
        private string piratebay_part1 = "http://thepiratebay.org/search/";
        private string piratebay_part2 = "/0/99/0";
        private string bing_part1 = "http://www.bing.com/search?q=";
        private string bing_part2 = "&go=&form=QBLH&filt=all";
        #endregion

        public Inproc()
        {
            InitializeComponent();
            progressBar.Minimum = 0;
            progressBar.Maximum = 100;
            progressBar.Step = 25;
        }

        //Event: Start System
        private void sys_start_btn_Click(object sender, EventArgs e)
        {
            //progressBar.Maximum = 100;
            //string selected_item = SR_comboBox.SelectedItem.ToString();
            timer1.Enabled = true;
            if (SR_comboBox.SelectedItem == "Go to")
            {
                //progressBar.Value = 100;
                goto_grammar_loader();
                kill_all_btn.Enabled = true;
                mute_box.Enabled = true;
                //progressBar.Value = 0;
            }
            if (SR_comboBox.SelectedItem == "Searcher")
            {
                grammar_loader();
                kill_all_btn.Enabled = true;
                mute_box.Enabled = true;
            }
            if (SR_comboBox.SelectedItem == null)
            {
                MessageBox.Show("You need to choose an SR!", "No SR detected", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            //timer1.Enabled = false;
        }
        //fucntion: loads srgs (go to) grammar into SR
        private void goto_grammar_loader()
        {
            InprocRecognizer = new SpeechRecognitionEngine();
            InprocRecognizer.SetInputToDefaultAudioDevice();
            Grammar goto_objgrammar = new Grammar(srgs_path, "goto");
            InprocRecognizer.LoadGrammar(goto_objgrammar);
            InprocRecognizer.RecognizeAsync(RecognizeMode.Multiple);
            goto_objgrammar.SpeechRecognized += new EventHandler<SpeechRecognizedEventArgs>(goto_objgrammar_SpeechRecognized);
            status_txt = "Status: Go to | Active";
            status_thread = new Thread(status_lbl_setter);
            status_thread.Start();
        }

        void goto_objgrammar_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            string word = e.Result.Text;
            string recognized_folder = domain_analyzer(word);

            status_txt = "Status: Folder: | " + recognized_folder;
            status_thread = new Thread(status_lbl_setter);
            status_thread.Start();

            folder_chooser(recognized_folder);
        }
        //function: loads srgs (search) grammar into SR
        private void grammar_loader()
        {
            //Create SR
            InprocRecognizer = new SpeechRecognitionEngine();
            InprocRecognizer.SetInputToDefaultAudioDevice();
            //Create new grammar from srgs.xml, rule initiliazer
            Grammar srgs_objgrammar = new Grammar(srgs_path, "initializer");
            //load SR with that grammar
            InprocRecognizer.LoadGrammar(srgs_objgrammar);
            InprocRecognizer.RecognizeAsync(RecognizeMode.Multiple);
            //Create event for when grammar recognizes a phrase
            srgs_objgrammar.SpeechRecognized += new EventHandler<SpeechRecognizedEventArgs>(srgs_objgrammar_SpeechRecognized);
            status_txt = "Status: Search | Active";
            status_thread = new Thread(status_lbl_setter);
            status_thread.Start();
        }
        //Event method for when grammar recognizes a pharse
        void srgs_objgrammar_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            string word = e.Result.Text;
            search_engine = domain_analyzer(word);
            dictation_srgs_grammar_loader();

            status_txt = "Status: Dictation | " + search_engine;
            status_thread = new Thread(status_lbl_setter);
            status_thread.Start();
        }
        //function: analyzes domnain from the recognized phrase
        private string domain_analyzer(string phrase)
        {
            string domain = null;
            string[] token = phrase.Split(new char[] { ' ' });
            domain = token[2];

            //MessageBox.Show("Search Engine : " + domain, "Domain Analyzer", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return domain;
        }
        //fucntion: loads dictation grammar from SRGS
        private void dictation_srgs_grammar_loader()
        {
            status_txt = "Status: Dictation | waiting";
            status_thread = new Thread(status_lbl_setter);
            status_thread.Start();

            InprocRecognizer.UnloadAllGrammars();
            Grammar dictation_srgs_objgrammar = new Grammar(srgs_path, "dictionary");
            InprocRecognizer.LoadGrammar(dictation_srgs_objgrammar);
            dictation_srgs_objgrammar.SpeechRecognized += new EventHandler<SpeechRecognizedEventArgs>(dictation_srgs_objgrammar_SpeechRecognized);
        }
        //function: changes status label based on recogniton state
        private delegate void status_lbl_setterDelegate();
        private void status_lbl_setter()
        {
            if (this.InvokeRequired)
            {
                status_lbl_setterDelegate del = new status_lbl_setterDelegate(status_lbl_setter);
                this.Invoke(del);
            }
            else
            {
                this.status_lbl.Text = status_txt;
                status_txt = null;
            }
        }
        //Event: for when dictation from srgs detects something.
        void dictation_srgs_objgrammar_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            string word = e.Result.Text;
            if (word == "spelling")
            {
                InprocRecognizer.UnloadAllGrammars();
                spelling_grammar_loader();
            }
            if (word == "cancel")
            {
                InprocRecognizer.UnloadAllGrammars();
                grammar_loader();
            }
            if (word != "cancel" && word != "spelling")
            {
                confirmation_word = word;
                value = word;
                read_item(word);
                //dialog_thread = new Thread(confirmation_dialog_loader);
                //dialog_thread.Start();
                status_txt = "Status: Confrimation | " + confirmation_word + "?";
                status_thread = new Thread(status_lbl_setter);
                status_thread.Start();
                confirmation_grammar_loader();
            }
        }
        //Function: loads confirmaiton dialog
        private void confirmation_dialog_loader()
        {
            int rVAL = confirmation_msgbox.showbox(confirmation_word);
            action_from_confirmation(rVAL, confirmation_word);
        }
        //Function | takes apropriate action based on confirmation
        public void action_from_confirmation(int rVAL, string word)
        {

            obj_msgbox = new confirmation_msgbox();

            if (rVAL == 1)//yes
            {
                value = word;
                string link = link_assembler(search_engine, value);
                app_executor(link);
                SR_initializer();
                value = null;
                //NOT WORKING !Need to dispose the confirmation box after each recognition, NOT WORKING
                obj_msgbox.Close();
            }
            if (rVAL == 2)//no...spell it
            {
                InprocRecognizer.UnloadAllGrammars();
                spelling_grammar_loader();
                //NOT WORKING !Need to dispose the confirmation box after each recognition, NOT WORKING
                obj_msgbox.Close();
                
            }
            if (rVAL == 3)//no...list
            {
                MessageBox.Show("Select an item from candidate list", "Exit", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                InprocRecognizer.UnloadAllGrammars();
                grammar_loader();
                //NOT WORKING !Need to dispose the confirmation box after each recognition, NOT WORKING
                obj_msgbox.Close();
            }
            if (rVAL == 0)//cancel
            {
                //MessageBox.Show("Canceling...", "Exit", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                InprocRecognizer.UnloadAllGrammars();
                grammar_loader();
                //NOT WORKING !Need to dispose the confirmation box after each recognition, NOT WORKING
                obj_msgbox.Close();
            }
            confirmation_word = null;
        }
        //Event: for selecting from candidate list
        private void select_fromlistbox_btn_Click(object sender, EventArgs e)
        {
            value = candidates_listbox.SelectedItem.ToString();
            string link = link_assembler(search_engine, value);
            app_executor(link);

        }
        //function: reads item from SRGS based on percentage
        private void read_item(string reco)
        {
            XmlDocument xmldoc = new XmlDocument();
            xmldoc.Load(srgs_path);
            string ns = "http://www.w3.org/2001/06/grammar";
            XmlNamespaceManager nsMgr = new XmlNamespaceManager(xmldoc.NameTable);
            nsMgr.AddNamespace("g", ns);
            System.Diagnostics.Debug.WriteLine(nsMgr.DefaultNamespace);
            XmlNode foundNode = xmldoc.SelectSingleNode("//g:rule[@id='" + "dictionary" + "']/g:one-of", nsMgr);

            if (foundNode != null)
            {

                int lenght = 0;
                foreach (System.Xml.XmlNode n in foundNode)
                {
                    string text = n.InnerText;
                    int per_int = percentage_assembler(reco, text);
                    if (per_int >= 50)
                    {
                        lenght++;
                    }
                }
                foreach (System.Xml.XmlNode n in foundNode)
                {
                    int per_int = percentage_assembler(reco, n.InnerText);
                    if (per_int >= 50)
                    {
                        candidate_list = candidate_list + n.InnerText + "|";
                    }
                }
                tokens = tokenizer_candidate_list(candidate_list);
                thread_initializer();
                tokens = null;
                candidate_list = null;
            }
        }
        //function: initializes thread to add read items into list box
        private void thread_initializer()
        {
            ParameterizedThreadStart thread_starter = new ParameterizedThreadStart(thread_helper);
            alternatives_thread = new Thread(thread_starter);
            string[] list = tokens;
            alternatives_thread.Start(list);
        }
        //function: loads the function to be threaded
        private void thread_helper(object list)
        {
            addtolistbox((string[])list);
        }
        //function: adds items to listbox
        private delegate void addtolistboxDelegate(string[] list);
        private void addtolistbox(string[] list)
        {
            if (this.InvokeRequired)
            {
                addtolistboxDelegate del = new addtolistboxDelegate(addtolistbox);
                object[] parameters = { list };
                this.Invoke(del, parameters);
            }
            else
            {
                candidates_listbox.Items.Clear();
                candidates_listbox.Items.AddRange(list);
            }
        }
        //function: loads confirmation grammar from SRGS
        private void confirmation_grammar_loader()
        {


            InprocRecognizer.UnloadAllGrammars();
            Grammar confirmation_srgs_objgrammar = new Grammar(srgs_path, "confirmation");
            InprocRecognizer.LoadGrammar(confirmation_srgs_objgrammar);
            confirmation_srgs_objgrammar.SpeechRecognized += new EventHandler<SpeechRecognizedEventArgs>(confirmation_srgs_objgrammar_SpeechRecognized);
        }
        //Event: sends confirmation phrase for decision
        void confirmation_srgs_objgrammar_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            string word = e.Result.Text;

            SemanticValue semv_val = new SemanticValue(e.Result.Semantics.Value);
            string sem_val_string = semv_val.Value.ToString();
            //Here add if to check if the answer is yes, no or cancel.
            //if yes -> app_exo, if no -> dictation_grammar_loader() and load candidate list, if cancel -> unloadallgrammar()
            
            if (sem_val_string == "0")
            {
                SR_initializer();
            }
            if (sem_val_string == "1")
            {
                //value = word;
                string link = link_assembler(search_engine, value);
                app_executor(link);
                SR_initializer();
                value = null;
            }
            if (sem_val_string == "2")
            {
                InprocRecognizer.UnloadAllGrammars();
                spelling_grammar_loader();
            }
            if (sem_val_string == "3")
            {
                InprocRecognizer.UnloadAllGrammars();
                dictation_srgs_grammar_loader();
            }
            

            //aborts the dialog thread
            //dialog_thread.Abort();
            //dialog_thread.Join();
            //takes action from recognized phrase
            //int rVAL = Convert.ToInt32(sem_val_string);
            //word is yes, no, cancel, it should be value

            //action_from_confirmation(rVAL, confirmation_word);


        }
        //function: loads spelling grammar from SRGS
        private void spelling_grammar_loader()
        {
            status_txt = "Status: Spelling";
            status_thread = new Thread(status_lbl_setter);
            status_thread.Start();

            Grammar spelling_srgs_objgrammar = new Grammar(srgs_path, "spelling");
            InprocRecognizer.LoadGrammar(spelling_srgs_objgrammar);
            //Create event for when spelling gramamr recognizes a letter
            spelling_srgs_objgrammar.SpeechRecognized += new EventHandler<SpeechRecognizedEventArgs>(spelling_srgs_objgrammar_SpeechRecognized);
            //MessageBox.Show("waiting for you spelling", "Spelling", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        //Event: for when a letter is recogized by spelling gramamr
        void spelling_srgs_objgrammar_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            //the following code assembles the value (temp) -- should be in function
            string word = e.Result.Text;
            if (word == "cancel")
            {
                InprocRecognizer.UnloadAllGrammars();
                grammar_loader();
            }
            if (word != "cancel")
            {
                SemanticValue semv_val = new SemanticValue(e.Result.Semantics.Value);
                string sem_val_string = semv_val.Value.ToString();

                string letter = sem_val_string.ToLower();
                value_assembler(letter);
            }
           
        }
        
        //function: assembles value from recognized letter by spelling grammar
        private void value_assembler(string letter)
        {
            if (letter == "back" && letter != "finish")
            {
                //MessageBox.Show("Going Back");
                //delete that last index of value...
                value = value.Remove(value.Length - 1);
                status_thread = new Thread(status_lbl_setter);
                status_txt = "Spelling: " + value;
                status_thread.Start();
          
            }
            if (letter != "finish" && letter != "back")
            {
                //MessageBox.Show("you spelled : " + letter, "Spelling", MessageBoxButtons.OK, MessageBoxIcon.Information);
                value = value + letter;
                status_thread = new Thread(status_lbl_setter);
                status_txt = "Spelling: " + value;
                status_thread.Start();
            }
            if (letter == "finish")
            {
                //MessageBox.Show("Spelled Word : " + value, "Spelling", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //add value to xml file (dictionary)
                AddItemToRule(srgs_path, srgs_path, "dictionary", value);
                string link = link_assembler(search_engine, value);
                app_executor(link);
                InprocRecognizer.UnloadAllGrammars();
                grammar_loader();
            }

        }
        //function: assembles link based on lvl2_choice (domain) - domains should be added here.
        //domains need to be read from somewhere elese, somewhere editable so that users can add new domains.
        private string link_assembler(string lvl2_choice, string value)
        {
            string link = null;
            if (lvl2_choice == "google")
            {
                link = googlesearch_part1 + value + googlesearch_part2;
                search_engine = null;
            }
            if (lvl2_choice == "yahoo")
            {
                link = yahoosearch_part1 + value + yahoosearch_part2;
                search_engine = null;
            }
            if (lvl2_choice == "wikipedia")
            {
                link = wikipedia_domain + value;
                search_engine = null;
            }
            if (lvl2_choice == "piratebay")
            {
                link = piratebay_part1 + value + piratebay_part2;
                search_engine = null;
            }
            if (lvl2_choice == "bing")
            {
                link = bing_part1 + value + bing_part2;
                search_engine = null;
            }
            return link;
        }
        //function: executes applications for desired link
        private void app_executor(string link)
        {
            System.Diagnostics.Process.Start(link);
        }
        //function: unloads everything from SR and brings it back to it's first state (xml,initializer)
        private void SR_initializer()
        {
            InprocRecognizer.UnloadAllGrammars();
            grammar_loader();
        }
        //function: assembles percentage value from compraing to items from SRGS
        private int percentage_assembler(string reco, string list)
        {
            int ir = reco.Length;
            int il = list.Length;
            string percentage_string = null;
            int percentage_int = 0;
            int percentage = 0;
            int index;
            int diff;
            int i;
            //if lenght of recognized phares is larger than the lenght of phares in the list
            if (ir > il)
            {
                //minus 1 because index starts from 0
                diff = ir - il - 1;
                for (i = 0; i <= diff; i++)
                {
                    diff_index = diff_index + "0";
                }
                //percentage = "|" + diff.ToString() + "|";
                for (index = 0; index < il; index++)
                {
                    if (reco[index] == list[index])
                    {
                        percentage_string = percentage_string + "1";
                    }
                    else
                    {
                        percentage_string = percentage_string + "0";
                    }
                }
            }
            //if lenght of recognized phares is smaller than the lenght of phares in the list
            if (ir < il)
            {
                //minus 1 because index starts from 0
                diff = il - ir - 1;
                for (i = 0; i <= diff; i++)
                {
                    diff_index = diff_index + "0";
                }
                //percentage = "|" + diff.ToString() + "|";
                for (index = 0; index < ir; index++)
                {
                    if (reco[index] == list[index])
                    {
                        percentage_string = percentage_string + "1";
                    }
                    else
                    {
                        percentage_string = percentage_string + "0";
                    }
                }
            }
            //if lenght of recognized phares is equal than the lenght of phares in the list
            if (ir == il)
            {
                for (index = 0; index < ir; index++)
                {
                    if (reco[index] == list[index])
                    {
                        percentage_string = percentage_string + "1";
                    }
                    else
                    {
                        percentage_string = percentage_string + "0";
                    }
                }
            }
            percentage_string = percentage_string + diff_index;
            int percentage_lenght = percentage_string.Length;
            for (int per_len_indexer = 0; per_len_indexer <= percentage_lenght - 1; per_len_indexer++)
            {
                if (percentage_string[per_len_indexer] == '1')
                {
                    percentage_int++;
                }
            }
            //defines percentage on the given formula x = ( y * 100 ) / n
            percentage = percentage_int * 100;
            percentage = percentage / percentage_lenght;
            diff_index = null;
            percentage_int = 0;
            return percentage;
        }
        //function : adds and item to the grammar file ( srgs.xml)
        static bool AddItemToRule(XmlDocument xmlDoc, string ruleId, string item)
        {
            string ns = "http://www.w3.org/2001/06/grammar";
            XmlNamespaceManager nsMgr = new XmlNamespaceManager(xmlDoc.NameTable);
            nsMgr.AddNamespace("g", ns);
            System.Diagnostics.Debug.WriteLine(nsMgr.DefaultNamespace);
            XmlNode foundNode = xmlDoc.SelectSingleNode("//g:rule[@id='" + ruleId + "']/g:one-of", nsMgr);
            if (foundNode != null)
            {
                XmlElement eleItem = xmlDoc.CreateElement("item", ns);
                eleItem.InnerText = item;
                foundNode.AppendChild(eleItem);
                return true;
            }
            return false;
        }
        //function: adds item to element in xml
        static void AddItemToRule(string inFile, string outFile, string ruleId, string item)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(inFile);
            if (AddItemToRule(xmlDoc, ruleId, item))
            {
                xmlDoc.Save(outFile);
            }
        }
        //function: tokenizes the candidate list and returns a list of candidates
        private string[] tokenizer_candidate_list(string input)
        {
            string sep = "|";
            string[] stringlist = input.Split(sep.ToCharArray());
            string[] can_list = new string[stringlist.Length];
            for (int i = 0; i < stringlist.Length; i++)
            {
                can_list[i] = stringlist[i].ToString();
            }
            return can_list;
        }
        //Event: unloads all grammar
        private void kill_all_btn_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Please wait while the grammer is being unloaded.", "Unloading Grammar", MessageBoxButtons.OK, MessageBoxIcon.Information);
            InprocRecognizer.UnloadAllGrammars();
            status_lbl.Text = "Status: Inactive";
            kill_all_btn.Enabled = false;
            mute_box.Enabled = false;
            progressBar.Value = 0;
        }
        //Event: mutes microphone
        private void mute_box_CheckedChanged(object sender, EventArgs e)
        {
            if (mute_box.Checked == true)
            {
                InprocRecognizer.RecognizeAsyncCancel();
            }
            if (mute_box.Checked == false)
            {
                InprocRecognizer.RecognizeAsync();
            }
        }

        private void status_lbl_TextChanged(object sender, EventArgs e)
        {
            if (status_lbl.Text == "Status: Inactive")
            {
                status_lbl.ForeColor = Color.Red;
            }
            if (status_lbl.Text == "Status: Search | Active")
            {
                status_lbl.ForeColor = Color.Green;
            }
            if (status_lbl.Text == "Status: Go to | Active")
            {
                status_lbl.ForeColor = Color.Green;
            }
            if (status_lbl.Text.Contains("Spelling: "))
            {
                status_lbl.ForeColor = Color.Black;
            }
        }

        private void add_word_button_Click(object sender, EventArgs e)
        {
            string new_word = new_word_textBox.Text;
            AddItemToRule(srgs_path, srgs_path, "dictionary", new_word);
            MessageBox.Show("Word added to grammar, is now detectable during dictation.", "Adding Done.", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void add_word_checkBox_CheckedChanged(object sender, EventArgs e)
        {
            if (add_word_checkBox.Checked == true)
            {
                panel1.Enabled = true;
            }
            if (add_word_checkBox.Checked == false)
            {
                panel1.Enabled = false;
            }
        }

        //Function: Chooses the path from recognition
        private void folder_chooser(string place)
        {
            if (place == "documents")
            {
                place_path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            }
            if (place == "music")
            {
                place_path = Environment.GetFolderPath(Environment.SpecialFolder.MyMusic);
            }
            if (place == "computer")
            {
                place_path = Environment.GetFolderPath(Environment.SpecialFolder.MyComputer);
            }
            if (place == "videos")
            {
                place_path = Environment.GetFolderPath(Environment.SpecialFolder.MyVideos);
            }
            if (place == "tools")
            {
                place_path = Environment.GetFolderPath(Environment.SpecialFolder.CommonAdminTools);
            }
            if (place == "pictures")
            {
                place_path = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
            }
            if (place == "desktop")
            {
                place_path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            }
            if (place == "programs")
            {
                place_path = Environment.GetFolderPath(Environment.SpecialFolder.CommonPrograms);
            }
            string windir = Environment.GetEnvironmentVariable("WINDIR");
            System.Diagnostics.Process prc = new System.Diagnostics.Process();
            prc.StartInfo.FileName = windir + @"\explorer.exe";
            prc.StartInfo.Arguments = place_path;
            prc.Start();
        }

        private void SR_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (InprocRecognizer != null)
            {
                if (SR_comboBox.SelectedItem == "Searcher")
                {
                    InprocRecognizer.UnloadAllGrammars();
                    grammar_loader();
                }
                if (SR_comboBox.SelectedItem == "Go to")
                {
                    InprocRecognizer.UnloadAllGrammars();
                    goto_grammar_loader();
                }
            }
            //Check if SR is not empty, then unload ... but how?

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (progressBar.Value == progressBar.Maximum)
            {
                timer1.Enabled = false;
                timer1.Dispose();
            }
            else
            {
                progressBar.PerformStep();
            }
        }
        private void sentence_dic_loader()
        {
            InprocRecognizer = new SpeechRecognitionEngine();
            InprocRecognizer.SetInputToDefaultAudioDevice();
            //Create new grammar from srgs.xml, rule initiliazer
            Grammar srgs_objgrammar = new Grammar(srgs_path, "initializer");
            Grammar dic_objgrammar = new DictationGrammar("grammar:dictation");
            //load SR with that grammar
            InprocRecognizer.LoadGrammar(dic_objgrammar);
            InprocRecognizer.RecognizeAsync(RecognizeMode.Multiple);
            //Create event for when grammar recognizes a phrase
            dic_objgrammar.SpeechRecognized += new EventHandler<SpeechRecognizedEventArgs>(dic_objgrammar_SpeechRecognized);
            status_txt = "Status: Search | Sentence Dic";
            status_thread = new Thread(status_lbl_setter);
            status_thread.Start();
        }
        void dic_objgrammar_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            string sentence = e.Result.Text;
            MessageBox.Show(sentence);
        }
    }
}
