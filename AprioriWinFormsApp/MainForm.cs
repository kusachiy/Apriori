using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using AprioriClassLib;
using System.Diagnostics;

namespace AprioriWinFormsApp
{
    public partial class MainForm : Form
    {
        List<string> filePaths = new List<string>();
        int countOfItems;
        static List<HashSet<int>> list;

        public MainForm()
        {
            filePaths.Add(@"db25000.txt");
            filePaths.Add(@"db50000.txt");
            filePaths.Add(@"db100000.txt");
            filePaths.Add(@"db200000.txt");

            InitializeComponent();

            list = new List<HashSet<int>>();
            comboBox.Items.Add("25000 rows");
            comboBox.Items.Add("50000 rows");
            comboBox.Items.Add("100000 rows");
            comboBox.Items.Add("200000 rows");
            comboBox.Text = comboBox.Items[0].ToString();          
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int currentItem = comboBox.SelectedIndex;
            GetData(currentItem);
        }
        void GetData(int fileNumber)
        {
            StreamReader sr = new StreamReader(filePaths[fileNumber]);
            countOfItems = int.Parse(sr.ReadLine());
            while (!sr.EndOfStream)
            {
                List<int> set = new List<int>();
                string[] stra = sr.ReadLine().Split();
                foreach (var item in stra)
                {
                    set.Add(int.Parse(item));
                }
                list.Add(new HashSet<int>(set));
            }
            sr.Close();


            MaximumSize = new Size(438, 196);
            Size = MaximumSize;
            comboBox.Enabled = false;
            panel1.Visible = true;
            button1.Enabled = false;
            textBox1.Text = "File is found, the data is loaded.";
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            MaximumSize = new Size(246, 196);
            Size = MaximumSize;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Apriori apriori = new Apriori(list,countOfItems);
            Stopwatch stw = new Stopwatch();
            stw.Start();
            apriori.Build();
            stw.Stop();
            runTimeTextBox.Text = stw.Elapsed.TotalMilliseconds.ToString();
            Dictionary<int, List<Rule>> rules = apriori.GetRules;
            ResultForm rf = new ResultForm();
            rf.SetData(rules);
            rf.ShowDialog();
        }
    }
}
