using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GraphVS
{
    public partial class Form1 : Form
    {
        private List<Panel> panels;

        public Form1()
        {
            InitializeComponent();
            panels = new List<Panel>();
        }

        private void btnAddPanel_Click(object sender, EventArgs e)
        {
            int count = panels.Count;
            Panel newPanel = new Panel();
            newPanel.Location = new Point(count * 20, count * 20);
            newPanel.Name = count + "pnl";
            newPanel.BackColor = Color.FromArgb(count * 5, count * 5, count * 5);
            newPanel.Size = new Size(15, 15);
            newPanel.TabIndex = count + 2;
            panels.Add(newPanel);
            Controls.Add(panels[count]);
        }

        private void btnPnlRmv_Click(object sender, EventArgs e)
        {
            panels.Remove(panels[1]);
            Controls.Remove(panels[1]);
        }
    }
}
