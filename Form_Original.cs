using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 拼图游戏
{
    public partial class Form_Original : Form
    {
        public string picpath;
        public Form_Original()
        {
            InitializeComponent();
        }

        private void pic_Original_Click(object sender, EventArgs e)
        {

        }

        private void Form_Original_Load(object sender, EventArgs e)
        {
            pic_Original.Image = CutPicture.Resize(picpath, 600, 600);
        }
    }
}
