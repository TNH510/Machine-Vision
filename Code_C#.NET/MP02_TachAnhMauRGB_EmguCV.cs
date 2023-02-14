using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.Util;

namespace MP02_TachAnhMauRGB_EmguCV
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            //Tao bien chua duong dan luu hinh cua minh
            Image<Bgr, byte> hinhgoc = new Image<Bgr, byte>(@"H:\Code\Photo\lena30.jpg");

            //Hien thi hinh goc trong picBox_Hinhgoc da tao
            imageBox_Hinhgoc.Image = hinhgoc;
            Image<Bgr, byte> red = new Image<Bgr, byte>(hinhgoc.Width, hinhgoc.Height);
            Image<Bgr, byte> green = new Image<Bgr, byte>(hinhgoc.Width, hinhgoc.Height);
            Image<Bgr, byte> blue = new Image<Bgr, byte>(hinhgoc.Width, hinhgoc.Height);

            //Moi hinh la mot ma tran 2 chieu nen se dung 2 vong lap for
            //de doc het cac diem anh co trong hinh
            for (int x = 0; x < hinhgoc.Width; x++)
            {
                for (int y = 0; y < hinhgoc.Height; y++)
                {
                    //doc gia tri pixel tai diem anh co vi tri (x,y)
                    Bgr pixelValue = hinhgoc[x, y];

                    //Moi pixel chua 4 thong tin gom gia tri mau R,G,B va do trong suot A
                    byte R = (byte)pixelValue.Red;
                    byte G = (byte)pixelValue.Green;
                    byte B = (byte)pixelValue.Blue;

                    //set gia tri pixel doc duoc cho cac hinh chua
                    //cac kenh mau tuong ung
                    red.Data[x, y, 2] = (Byte)R;
                    red.Data[x, y, 1] = 0;
                    red.Data[x, y, 0] = 0;

                    green.Data[x, y, 2] = 0;
                    green.Data[x, y, 1] = (Byte)G;
                    green.Data[x, y, 0] = 0;

                    blue.Data[x, y, 2] = 0;
                    blue.Data[x, y, 1] = 0;
                    blue.Data[x, y, 0] = (Byte)B;

                    
                }
            }
            imageBox_Red.Image = red;
            imageBox_Green.Image = green;
            imageBox_Blue.Image = blue;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void imageBox_Hinhgoc_Click(object sender, EventArgs e)
        {

        }
    }
}
