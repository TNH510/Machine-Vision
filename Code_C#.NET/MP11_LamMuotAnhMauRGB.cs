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

            //Su dung ham lam muot va hien thi len ImageBox
            imageBox_3x3.Image = ColorImageSmoothing3x3(hinhgoc);
            imageBox_5x5.Image = ColorImageSmoothing5x5(hinhgoc);
            imageBox_7x7.Image = ColorImageSmoothing7x7(hinhgoc);
            imageBox_9x9.Image = ColorImageSmoothing9x9(hinhgoc);

        }
        public Image<Bgr, byte> ColorImageSmoothing3x3(Image<Bgr, byte> hinhgoc)
        {
            //Tao mot hinh chua anh sau khi da xu li
            Image <Bgr, byte> SmoothedImage3x3 = new Image<Bgr, byte>(hinhgoc.Width, hinhgoc.Height);

            //Tien hanh quet cac diem anh
            //Bo qua vien anh ngoai doi voi mat na 3x3
            for (int x = 1; x < hinhgoc.Width - 1; x++)
                for (int y = 1; y < hinhgoc.Height - 1; y++)
                {
                    //Cac bien de chua gia tri cong don cac diem anh
                    int Rs = 0, Gs = 0, Bs = 0;

                    //Quet cac diem anh trong mat na
                    for (int i = x - 1; i <= x + 1; i++)
                        for (int j = y - 1; j <= y + 1; j++)
                        {
                            //Lay thong tin mau RGB tai diem anh trong mat na
                            //tai vi tri (i;j)
                            Bgr pixelValue = hinhgoc[i, j];
                            byte R = (byte)pixelValue.Red;
                            byte G = (byte)pixelValue.Green;
                            byte B = (byte)pixelValue.Blue;

                            //Cong don tat ca cac diem ah do cho moi kenh
                            Rs += R;
                            Gs += G;
                            Bs += B;


                        }
                    //Ket thuc quet va cong don diem anh trong mat na
                    //Bat dau tinh trung binh cong cho moi kenh theo cong thuc 6.6-1 trong sach
                    //cho tung kenh mau R,G,B
                    byte K = 3 * 3;
                    Rs = (int)(Rs / K);
                    Gs = (int)(Gs / K);
                    Bs = (int)(Bs / K);

                    //Gan gia tri diem anh da lam muot
                    SmoothedImage3x3.Data[x, y, 2] = (byte)Rs;
                    SmoothedImage3x3.Data[x, y, 1] = (byte)Gs;
                    SmoothedImage3x3.Data[x, y, 0] = (byte)Bs;


                }

            //Tra anh
            return SmoothedImage3x3;
        }

        public Image<Bgr, byte> ColorImageSmoothing5x5(Image<Bgr, byte> hinhgoc)
        {
            //Tao mot hinh chua anh sau khi da xu li
            Image<Bgr, byte> SmoothedImage5x5 = new Image<Bgr, byte>(hinhgoc.Width, hinhgoc.Height);

            //Tien hanh quet cac diem anh
            //Bo qua vien anh ngoai doi voi mat na 5x5
            for (int x = 2; x < hinhgoc.Width - 2; x++)
                for (int y = 2; y < hinhgoc.Height -2; y++)
                {
                    //Cac bien de chua gia tri cong don cac diem anh
                    int Rs = 0, Gs = 0, Bs = 0;

                    //Quet cac diem anh trong mat na
                    for (int i = x - 2; i <= x + 2; i++)
                        for (int j = y - 2; j <= y + 2; j++)
                        {
                            //Lay thong tin mau RGB tai diem anh trong mat na
                            //tai vi tri (i;j)
                            Bgr pixelValue = hinhgoc[i, j];
                            byte R = (byte)pixelValue.Red;
                            byte G = (byte)pixelValue.Green;
                            byte B = (byte)pixelValue.Blue;

                            //Cong don tat ca cac diem ah do cho moi kenh
                            Rs += R;
                            Gs += G;
                            Bs += B;


                        }
                    //Ket thuc quet va cong don diem anh trong mat na
                    //Bat dau tinh trung binh cong cho moi kenh theo cong thuc 6.6-1 trong sach
                    //cho tung kenh mau R,G,B
                    byte K = 5 * 5;
                    Rs = (int)(Rs / K);
                    Gs = (int)(Gs / K);
                    Bs = (int)(Bs / K);

                    //Gan gia tri diem anh da lam muot
                    SmoothedImage5x5.Data[x, y, 2] = (byte)Rs;
                    SmoothedImage5x5.Data[x, y, 1] = (byte)Gs;
                    SmoothedImage5x5.Data[x, y, 0] = (byte)Bs;


                }

            //Tra anh
            return SmoothedImage5x5;
        }

        public Image<Bgr, byte> ColorImageSmoothing7x7(Image<Bgr, byte> hinhgoc)
        {
            //Tao mot hinh chua anh sau khi da xu li
            Image<Bgr, byte> SmoothedImage7x7 = new Image<Bgr, byte>(hinhgoc.Width, hinhgoc.Height);

            //Tien hanh quet cac diem anh
            //Bo qua vien anh ngoai doi voi mat na 7x7
            for (int x = 3; x < hinhgoc.Width - 3; x++)
                for (int y = 3; y < hinhgoc.Height - 3; y++)
                {
                    //Cac bien de chua gia tri cong don cac diem anh
                    int Rs = 0, Gs = 0, Bs = 0;

                    //Quet cac diem anh trong mat na
                    for (int i = x - 3; i <= x + 3; i++)
                        for (int j = y - 3; j <= y + 3; j++)
                        {
                            //Lay thong tin mau RGB tai diem anh trong mat na
                            //tai vi tri (i;j)
                            Bgr pixelValue = hinhgoc[i, j];
                            byte R = (byte)pixelValue.Red;
                            byte G = (byte)pixelValue.Green;
                            byte B = (byte)pixelValue.Blue;

                            //Cong don tat ca cac diem ah do cho moi kenh
                            Rs += R;
                            Gs += G;
                            Bs += B;


                        }
                    //Ket thuc quet va cong don diem anh trong mat na
                    //Bat dau tinh trung binh cong cho moi kenh theo cong thuc 6.6-1 trong sach
                    //cho tung kenh mau R,G,B
                    byte K = 7 * 7;
                    Rs = (int)(Rs / K);
                    Gs = (int)(Gs / K);
                    Bs = (int)(Bs / K);

                    //Gan gia tri diem anh da lam muot
                    SmoothedImage7x7.Data[x, y, 2] = (byte)Rs;
                    SmoothedImage7x7.Data[x, y, 1] = (byte)Gs;
                    SmoothedImage7x7.Data[x, y, 0] = (byte)Bs;


                }

            //Tra anh
            return SmoothedImage7x7;
        }

        public Image<Bgr, byte> ColorImageSmoothing9x9(Image<Bgr, byte> hinhgoc)
        {
            //Tao mot hinh chua anh sau khi da xu li
            Image<Bgr, byte> SmoothedImage9x9 = new Image<Bgr, byte>(hinhgoc.Width, hinhgoc.Height);

            //Tien hanh quet cac diem anh
            //Bo qua vien anh ngoai doi voi mat na 9x9
            for (int x = 4; x < hinhgoc.Width - 4; x++)
                for (int y = 4; y < hinhgoc.Height - 4; y++)
                {
                    //Cac bien de chua gia tri cong don cac diem anh
                    int Rs = 0, Gs = 0, Bs = 0;

                    //Quet cac diem anh trong mat na
                    for (int i = x - 4; i <= x + 4; i++)
                        for (int j = y - 4; j <= y + 4; j++)
                        {
                            //Lay thong tin mau RGB tai diem anh trong mat na
                            //tai vi tri (i;j)
                            Bgr pixelValue = hinhgoc[i, j];
                            byte R = (byte)pixelValue.Red;
                            byte G = (byte)pixelValue.Green;
                            byte B = (byte)pixelValue.Blue;

                            //Cong don tat ca cac diem ah do cho moi kenh
                            Rs += R;
                            Gs += G;
                            Bs += B;


                        }
                    //Ket thuc quet va cong don diem anh trong mat na
                    //Bat dau tinh trung binh cong cho moi kenh theo cong thuc 6.6-1 trong sach
                    //cho tung kenh mau R,G,B
                    byte K = 9 * 9;
                    Rs = (int)(Rs / K);
                    Gs = (int)(Gs / K);
                    Bs = (int)(Bs / K);

                    //Gan gia tri diem anh da lam muot
                    SmoothedImage9x9.Data[x, y, 2] = (byte)Rs;
                    SmoothedImage9x9.Data[x, y, 1] = (byte)Gs;
                    SmoothedImage9x9.Data[x, y, 0] = (byte)Bs;


                }

            //Tra anh
            return SmoothedImage9x9;
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

        private void imageBox_3x3_Click(object sender, EventArgs e)
        {

        }

        private void imageBox_7x7_Click(object sender, EventArgs e)
        {

        }

        private void imageBox_9x9_Click(object sender, EventArgs e)
        {

        }
    }
}
