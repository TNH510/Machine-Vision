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

            //Hien thi hinh goc trong picBox_Hinhgoc da tao
            imageBox_Hinhgoc.Image = hinhgoc;
            Image<Bgr, byte> X = new Image<Bgr, byte>(hinhgoc.Width, hinhgoc.Height);
            Image<Bgr, byte> Y = new Image<Bgr, byte>(hinhgoc.Width, hinhgoc.Height);
            Image<Bgr, byte> Z = new Image<Bgr, byte>(hinhgoc.Width, hinhgoc.Height);
            Image<Bgr, byte> XYZ = new Image<Bgr, byte>(hinhgoc.Width, hinhgoc.Height);

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
                    //Dunng kieu du lieu double vi trong qua trinh tinh toan cac gia tri H,S,I
                    //co tra ve gia tri so thuc
                    double R = pixelValue.Red;
                    double G = pixelValue.Green;
                    double B = pixelValue.Blue;

                    //Tinh goc theta dua tren cong thuc
                    double X_Value = 0.4124564 * R + 0.3575761 * G + 0.1804375 * B;
                    double Y_Value = 0.2126729 * R + 0.7151522 * G + 0.0721750 * B;
                    double Z_Value = 0.0193339 * R + 0.1191920 * G + 0.9503041 * B;

                    //Gxan cac gia tri vao

                    X.Data[y, x, 2] = (byte)X_Value;
                    X.Data[y, x, 1] = (byte)X_Value;
                    X.Data[y, x, 0] = (byte)X_Value;

                    //Magenta la ket hop cua R va B
                    Y.Data[y, x, 2] = (byte)Y_Value;
                    Y.Data[y, x, 1] = (byte)Y_Value;
                    Y.Data[y, x, 0] = (byte)Y_Value;

                    //Yellow la ket hop cua R va G
                    Z.Data[y, x, 2] = (byte)Z_Value;
                    Z.Data[y, x, 1] = (byte)Z_Value;
                    Z.Data[y, x, 0] = (byte)Z_Value;

                    //
                    XYZ.Data[y, x, 2] = (byte)X_Value;
                    XYZ.Data[y, x, 1] = (byte)Y_Value;
                    XYZ.Data[y, x, 0] = (byte)Z_Value;


                }
            }
            //Hien thi cac kenh mau len imageBox
            imageBox_X.Image = X;
            imageBox_Y.Image = Y;
            imageBox_Z.Image = Z;
            imageBox_XYZ.Image = XYZ;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void imageBox_Hinhgoc_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
