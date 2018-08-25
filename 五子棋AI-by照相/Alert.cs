using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace 五子棋AI_by照相
{
    public partial class Alert : Form
    {
        public Alert()
        {
            InitializeComponent();
        }
        public Label whoFirst;
        private void Alert_Load(object sender, EventArgs e)
        {
            panel1.Location = new Point((this.Width - panel1.Width) / 2, panel1.Location.Y);
            label1.Location = new Point((this.Width - label1.Width) / 2, label1.Location.Y);
            this.Height = panel1.Location.Y + panel1.Height + (this.Height - ClientRectangle.Height)+10;
            this.MaximizeBox = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.whoFirst.Text = "B";
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.whoFirst.Text = "W";
            this.Close();
        }
    }
}
