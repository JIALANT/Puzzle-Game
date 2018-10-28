using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace 拼图游戏
{
    public partial class Form_Main : Form
    {
        PictureBox[] picturelist = null;
        SortedDictionary<string, Bitmap> pictureLocationDict = new SortedDictionary<string, Bitmap>();
        Point []pointlist=null;
        SortedDictionary<string, PictureBox > pictureBoxLocationDict = new SortedDictionary<string, PictureBox>();
        
        PictureBox currentpicturebox = null;
        PictureBox havetopicturebox = null;
        Point oldlocation = Point.Empty;
        Point newlocation = Point.Empty;
        Point mouseDownPoint = Point.Empty;
        Rectangle rect = Rectangle.Empty;
        bool isDrag = false;
        public string originalpicpath;
        private int Imgnubers
        {
            get
            {
                return (int)this.numericUpDown1.Value;
            }
        }
        private int sidelength
        {
            get { return 600 / this.Imgnubers; }
        }
        public void InitRandomPictureBox()
        {
            pnl_Picture.Controls.Clear();
            picturelist = new PictureBox[Imgnubers * Imgnubers];
            pointlist = new Point [Imgnubers * Imgnubers];
           
            for (int i = 0; i < this.Imgnubers; i++)
            {
                for (int j = 0; j < this.Imgnubers; j++)
                {
                    PictureBox pic = new PictureBox();
                    pic.Name = "picturebox" + (j + i * Imgnubers + 1).ToString();
                    pic.Location = new Point(j * sidelength, i * sidelength);
                    pic.Size = new Size(sidelength, sidelength);
                    pic.Visible = true;
                    pic.BorderStyle = BorderStyle.FixedSingle;
                    pic.MouseDown += new MouseEventHandler(picture_MouseDown);
                    pic.MouseMove += new MouseEventHandler(picture_MouseMove);
                    pic.MouseUp += new MouseEventHandler(picture_MouseUp);
                    pnl_Picture.Controls.Add(pic);
                    picturelist[j + i * Imgnubers] = pic;
                    pointlist[j + i * Imgnubers] = new Point(j * sidelength, i * sidelength);
                }
            }
        }
        public void Flow(string path, bool disorder)
        {
            InitRandomPictureBox();
            Image bm = CutPicture.Resize(path, 600, 600);
            CutPicture.BitMapList = new List<Bitmap>();
            for(int y=0;y<600;y+=sidelength )
            {
                for (int x = 0; x < 600; x += sidelength)
                {
                    Bitmap temp = CutPicture.Cut(bm, x, y, sidelength, sidelength);
                    CutPicture.BitMapList.Add(temp);
                }
            }
                ImporBitMap(disorder );
        }
        public void ImporBitMap(bool disorder)
        {
            try
            {
                int i=0;
                foreach (PictureBox item in picturelist )
                {
                    Bitmap temp = CutPicture.BitMapList[i];
                    item.Image = temp;
                    i++;
                }
                if(disorder )ResetPictureLoaction();
            }
            catch (Exception exp)
            {
                Console .WriteLine (exp.Message );
            }
        }
        public void ResetPictureLoaction()
        {
            Point[] temp = DisOrderLocation();
            int i = 0;
            foreach (PictureBox item in picturelist)
            {
                item.Location = temp[i];
                i++;
            }
        }
        public Point[] DisOrderLocation()
        {
            Point[] tempArray = (Point[])pointlist.Clone();
            for (int i = tempArray.Length - 1; i > 0; i--)
            {
                Random rand = new Random();
                int p = rand.Next(i);
                Point temp = tempArray[p];
                tempArray[p] = tempArray[i];
                tempArray[i] = temp;
            }
            return tempArray;
        }  
        private void Form_Main_Load(object sender, EventArgs e)
        {
        
        }
        public void initgame()
        { 
           /* picturelist = new PictureBox[9] { pictureBox1, pictureBox2, pictureBox3, pictureBox4, pictureBox5, pictureBox6, pictureBox7, pictureBox8, pictureBox9 };  
            pointlist = new Point[9] { new Point(0, 0), new Point(100, 0), new Point(200, 0), new Point(0, 100), new Point(100, 100), new Point(200, 100), new Point(0, 200), new Point(100, 200), new Point(200, 200) };
            */if (!Directory.Exists(Application.StartupPath.ToString() + "//Resources"))
            {
                Directory.CreateDirectory(Application.StartupPath.ToString() + "//Resources");
                Properties.Resources._0.Save(Application.StartupPath.ToString() + "//Resources//0.jpg");
                Properties.Resources._1.Save(Application.StartupPath.ToString() + "//Resources//1.jpg");
                Properties.Resources._2.Save(Application.StartupPath.ToString() + "//Resources//2.jpg");
                Properties.Resources._3.Save(Application.StartupPath.ToString() + "//Resources//3.jpg");
                Properties.Resources._4.Save(Application.StartupPath.ToString() + "//Resources//4.jpg");
            }
            Random r=new Random ();
            int i=r.Next (5);
            originalpicpath = Application.StartupPath.ToString() + "//Resources//" + i.ToString() + ".jpg";
            Flow(originalpicpath ,true );
        }
        public PictureBox GetPictureBoxByLocation(int x, int y)
        {
            PictureBox pic = null;
            foreach (PictureBox item in picturelist)
            {
                if (x > item.Location.X && y > item.Location.Y && item.Location.X + sidelength > x && item.Location.Y + sidelength > y)
                { pic = item; }
            }
            return pic;
        }
        public PictureBox GetPictureBoxByHashCode(string hascode)
        {
            PictureBox pic = null;
            foreach (PictureBox item in picturelist)
            {
                if (hascode == item.GetHashCode().ToString())
                {
                    pic = item;
                }
            }
            return pic;
        }
        private void picture_MouseDown(object sender, MouseEventArgs e)
        {
            oldlocation = new Point(e.X, e.Y);
            currentpicturebox = GetPictureBoxByHashCode(sender.GetHashCode().ToString());
            MoseDown(currentpicturebox, sender, e);
        }
        public void MoseDown(PictureBox pic, object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                oldlocation = e.Location;
                rect = pic.Bounds;
            }
        }
        
        private void picture_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isDrag = true;
                rect.Location = getPointToForm(new Point(e.Location.X - oldlocation.X, e.Location.Y - oldlocation.Y));
                this.Refresh();

            }
        }
        private Point getPointToForm(Point p)
        {
            return this.PointToClient(pictureBox1 .PointToScreen (p));
        }
        private void reset()
        {
            mouseDownPoint = Point.Empty;
            rect = Rectangle.Empty;
            isDrag = false;
        }

        private void picture_MouseUp(object sender, MouseEventArgs e)
        {
            oldlocation = new Point(currentpicturebox .Location .X ,currentpicturebox .Location .Y );
            if (oldlocation.X + e.X > 600 || oldlocation.Y + e.Y > 600 || oldlocation.X + e.X < 0 || oldlocation.Y + e.Y < 0)
            {
                return;
            }
            havetopicturebox  = GetPictureBoxByLocation(oldlocation.X + e.X, oldlocation.Y + e.Y);
            newlocation = new Point(havetopicturebox.Location.X, havetopicturebox.Location.Y);
            havetopicturebox.Location = oldlocation;
            PictureMouseUp(currentpicturebox, sender, e);
            if (Judge())
            {
                MessageBox.Show("恭喜拼图成功");  
            }
        }
        public void PictureMouseUp(PictureBox pic, object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (isDrag)
                {
                    isDrag = false;
                    pic.Location = newlocation;
                    this.Refresh();
                }
                reset();
            }
        }
        public bool Judge()//判断是否成功
        {
            bool result=true;
            int i=0;
            foreach (PictureBox item in picturelist)
            {
                if (item.Location != pointlist[i])
                { result = false; }
                i++;
            }
            return result;
        }
        public void ExchangePictureBox(MouseEventArgs e)
        { }
        public PictureBox[] DisOrderArray(PictureBox[] pictureArray)
        {
            PictureBox[] tempArray = pictureArray;
            for (int i = tempArray.Length - 1; i > 0; i--)
            {
                Random rand = new Random();
                int p = rand.Next(i);
                PictureBox temp = tempArray[p];
                tempArray[p] = tempArray[i];
                tempArray[i] = temp;
            }
            return tempArray;
        }  
        public Form_Main()
        {
            InitializeComponent();
            initgame();
        }

        private void pnl_Picture_Paint(object sender, PaintEventArgs e)
        {

        }

        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btn_import_Click(object sender, EventArgs e)
        {
            if (new_picture.ShowDialog() == DialogResult.OK)
            {
                originalpicpath = new_picture.FileName;
                CutPicture.picturePath = new_picture.FileName;
                Flow(CutPicture.picturePath, true);
            }
            
        }
 
        private void btn_changepic_Click(object sender, EventArgs e)
        {
            Random r = new Random();
            int i = r.Next(5);
            originalpicpath = Application.StartupPath.ToString() + "//Resources//" + i.ToString() + ".jpg";
            Flow(originalpicpath, true);
        }

        private void btn_Reset_Click(object sender, EventArgs e)
        {
            Flow(originalpicpath, true);
        }

        private void btn_originalpic_Click(object sender, EventArgs e)
        {
            Form_Original original = new Form_Original();
            original.picpath = originalpicpath;
            original.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            timer1.Start();
            timer1.Enabled = true;
            timer1.Interval = 10000;
           
            


        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (Judge())
            { MessageBox.Show("挑战成功"); timer1.Stop(); }
            else { MessageBox.Show("挑战失败"); timer1.Stop(); }
        }
    }
}
