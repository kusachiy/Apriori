using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AprioriClassLib;

namespace AprioriWinFormsApp
{
    public partial class ResultForm : Form
    {
        public ResultForm()
        {
            InitializeComponent();
        }

        public void SetData(Dictionary<int, List<Rule>> rules)
        {
            for (int i = 2; i < rules.Keys.Count + 2; i++)
            {
                for (int j = 0; j < rules[i].Count; j++)
                {
                    listBox1.Items.Add(rules[i][j].ToString());                    
                }
            }
        }

        private void ResultForm_Load(object sender, EventArgs e)
        {
            
        }
    }
}
