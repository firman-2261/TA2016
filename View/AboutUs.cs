using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace View
{
    public partial class AboutUs : Form
    {
        public AboutUs()
        {
            InitializeComponent();
            tabControl2.DrawItem += new DrawItemEventHandler(Gambar_Tab_Form);
        }

        private void AboutUs_FormClosed(object sender, FormClosedEventArgs e)
        {
            Close();
        }

        private void Gambar_Tab_Form(object sender, DrawItemEventArgs e)
        {
            Graphics g = e.Graphics;
            Brush _textBrush;
            TabPage _tabPage = tabControl2.TabPages[e.Index];
            Rectangle _tabBounds = tabControl2.GetTabRect(e.Index);

            Font _tabfont = new Font("Segoe UI", (float)12.0, GraphicsUnit.Pixel);
            StringFormat _StringFlags = new StringFormat();

            _StringFlags.Alignment = StringAlignment.Center;
            _StringFlags.LineAlignment = StringAlignment.Center;


            if (e.State == DrawItemState.Selected)
            {
                _textBrush = new SolidBrush(Color.White);
                g.FillRectangle(Brushes.Gray, e.Bounds);
                _tabPage.BackColor = Color.Black;


                _tabfont = new Font("Calisto MT", (float)12.0, FontStyle.Bold, GraphicsUnit.Pixel);

            }
            else
            {
                _textBrush = new SolidBrush(e.ForeColor);
                e.DrawBackground();
                _tabfont = new Font("Calisto MT", (float)12.0, GraphicsUnit.Pixel);

            }
            g.DrawString(_tabPage.Text, _tabfont, _textBrush, _tabBounds, new StringFormat(_StringFlags));

        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
