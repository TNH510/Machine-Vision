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
            Image<Bgr, byte> Hue = new Image<Bgr, byte>(hinhgoc.Width, hinhgoc.Height);
            Image<Bgr, byte> Saturation = new Image<Bgr, byte>(hinhgoc.Width, hinhgoc.Height);
            Image<Bgr, byte> Value = new Image<Bgr, byte>(hinhgoc.Width, hinhgoc.Height);
            Image<Bgr, byte> HSV = new Image<Bgr, byte>(hinhgoc.Width, hinhgoc.Height);

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
                    double t1 = ((R - G) + (R - B)) / 2;
                    double t2 = Math.Sqrt((R - G) * (R - G) + (R - B)*(G - B));
                    double theta = Math.Acos(t1 / t2);

                    //Tinh gia tri H
                    double H = 0;
                    if (B <= G)
                        H = theta;
                    else
                        H = 2*Math.PI - theta; //Gia tri goc hien tai dang la Radian

                    H = H * 180 / Math.PI; //Chuyen ve gia tri Do

                    //Tinh gia tri S
                    double S = 1 - 3 * Math.Min(R, Math.Min(G, B)) / (R + G + B);

                    //Tinh gia tri V
                    double V = Math.Max(R, Math.Max(G,B));

                    //Gan cac gia tri vao

                    Hue.Data[y, x, 2] = (byte)H;
                    Hue.Data[y, x, 1] = (byte)H;
                    Hue.Data[y, x, 0] = (byte)H;

                    //Magenta la ket hop cua R va B
                    Saturation.Data[y, x, 2] = (byte)(S*255);
                    Saturation.Data[y, x, 1] = (byte)(S*255);
                    Saturation.Data[y, x, 0] = (byte)(S*255);

                    //Yellow la ket hop cua R va G
                    Value.Data[y, x, 2] = (byte)V;
                    Value.Data[y, x, 1] = (byte)V;
                    Value.Data[y, x, 0] = (byte)V;

                    //
                    HSV.Data[y, x, 2] = (byte)H;
                    HSV.Data[y, x, 1] = (byte)(S*255);
                    HSV.Data[y, x, 0] = (byte)V;


                }
            }
            //Hien thi cac kenh mau len imageBox
            imageBox_H.Image = Hue;
            imageBox_S.Image = Saturation;
            imageBox_V.Image = Value;
            imageBox_HSV.Image = HSV;
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
