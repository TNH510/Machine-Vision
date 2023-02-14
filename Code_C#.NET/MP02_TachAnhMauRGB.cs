using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MP02_TachAnhMauRGB
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            //Tao bien chua duong dan luu hinh cua minh
            string filehinh = @"H:\Code\Photo\lena30.jpg";

            //Tao bien chua bitmap
            Bitmap hinhgoc = new Bitmap(filehinh);

            //Hien thi hinh goc trong picBox_Hinhgoc da tao
            picBox_Hinhgoc.Image = hinhgoc;

            //Khai bao 3 hinh bitmap de chua 3 hinh kenh R, G, B
            Bitmap red = new Bitmap(hinhgoc.Width, hinhgoc.Height);
            Bitmap green = new Bitmap(hinhgoc.Width, hinhgoc.Height);
            Bitmap blue = new Bitmap(hinhgoc.Width, hinhgoc.Height);

            //Moi hinh la mot ma tran 2 chieu nen se dung 2 vong lap for
            //de doc het cac diem anh co trong hinh
            for (int x = 0; x<hinhgoc.Width; x++)
                for (int y = 0; y<hinhgoc.Height; y++)
                {
                    //doc gia tri pixel tai diem anh co vi tri (x,y)
                    Color pixel = hinhgoc.GetPixel(x, y);

                    //Moi pixel chua 4 thong tin gom gia tri mau R,G,B va do trong suot A
                    byte R = pixel.R;
                    byte G = pixel.G;
                    byte B = pixel.B;
                    byte A = pixel.A;

                    //set gia tri pixel doc duoc cho cac hinh chua
                    //cac kenh mau tuong ung
                    red.SetPixel(x, y, Color.FromArgb(A, R, 0, 0));
                    green.SetPixel(x, y, Color.FromArgb(A, 0, G, 0));
                    blue.SetPixel(x, y, Color.FromArgb(A, 0, 0, B));

                    //Hien thi kenh mau RGB tai cac picBox da tao
                    picBox_RED.Image = red;
                    picBox_GREEN.Image = green;
                    picBox_BLUE.Image = blue;

                }

        }
    }
}
