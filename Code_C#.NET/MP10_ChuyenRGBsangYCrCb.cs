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
            Image<Bgr, byte> hinhgoc = new Image<Bgr, byte>(@"H:\Code\Photo\girl.jpg");

            //Hien thi hinh goc trong ImgBox_Hinhgoc da tao
            imageBox_Hinhgoc.Image = hinhgoc;
            Image<Bgr, byte> Y = new Image<Bgr, byte>(hinhgoc.Width, hinhgoc.Height);
            Image<Bgr, byte> Cr = new Image<Bgr, byte>(hinhgoc.Width, hinhgoc.Height);
            Image<Bgr, byte> Cb = new Image<Bgr, byte>(hinhgoc.Width, hinhgoc.Height);
            Image<Bgr, byte> YCrCb = new Image<Bgr, byte>(hinhgoc.Width, hinhgoc.Height);

            //Moi hinh la mot ma tran 2 chieu nen se dung 2 vong lap for
            //de doc het cac diem anh co trong hinh
            for (int x = 0; x < hinhgoc.Width; x++)
            {
                for (int y = 0; y < hinhgoc.Height; y++)
                {
                    //doc gia tri pixel tai diem anh co vi tri (x,y)
                    Bgr pixelValue = hinhgoc[y, x];

                    //Tao ra 3 bien R,G,B de lay gia tri RED, GREEN, BLUE tai tuong pixel
                    //Tao bien K de gan gia tri nho nhat cua R,G,B vao
                    double R = pixelValue.Red;
                    double G = pixelValue.Green;
                    double B = pixelValue.Blue;

                    double Y_Value = 16 + (65.738 / 256) * R + (129.057 / 256) * G + (25.064 / 256) * B;
                    double Cr_Value = 128 - (37.945 / 256) * R - (74.494 / 256) * G + (112.439 / 256) * B;
                    double Cb_Value = 128 + (112.439 / 256) * R - (94.154 / 256) * G - (18.285 / 256) * B;

                    //Gan gia tri
                    Y.Data[y, x, 2] = (byte)Y_Value;
                    Y.Data[y, x, 1] = (byte)Y_Value;
                    Y.Data[y, x, 0] = (byte)Y_Value;

                    
                    Cr.Data[y, x, 2] = (byte)Cr_Value;
                    Cr.Data[y, x, 1] = (byte)Cr_Value;
                    Cr.Data[y, x, 0] = (byte)Cr_Value;

                    
                    Cb.Data[y, x, 2] = (byte)Cb_Value;
                    Cb.Data[y, x, 1] = (byte)Cb_Value;
                    Cb.Data[y, x, 0] = (byte)Cb_Value;

                    
                    YCrCb.Data[y, x, 2] = (byte)Y_Value;
                    YCrCb.Data[y, x, 1] = (byte)Cr_Value;
                    YCrCb.Data[y, x, 0] = (byte)Cb_Value;



                }
            }
            //Hien thi cac kenh mau len imageBox
            imageBox_Y.Image = Y;
            imageBox_Cr.Image = Cr;
            imageBox_Cb.Image = Cb;
            imageBox_YCrCb.Image = YCrCb;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void imageBox_Hinhgoc_Click(object sender, EventArgs e)
        {

        }
    }
}
