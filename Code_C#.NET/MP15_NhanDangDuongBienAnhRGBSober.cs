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
        //Tao bien chua duong dan luu hinh cua minh
        Image<Bgr, byte> hinhgoc = new Image<Bgr, byte>(@"H:\Code\Photo\lena_color.jpg");
        int nguong;
        public Form1()
        {
            InitializeComponent();
            //Hien thi hinh goc trong picBox_Hinhgoc da tao
            imageBox_Hinhgoc.Image = hinhgoc;

        }
        public Image<Bgr, byte> PhanDoanAnhRGB(Image<Bgr, byte> hinhgoc, int nguong)
        {
            //Tao mot anh chua hinh da xu li
            Image<Bgr, byte> AnhRGBDaPhanDoan = new Image<Bgr, byte>(hinhgoc.Width, hinhgoc.Height);

            //Quet diem anh de tinh toan tren tung gia tri diem anh
            for (int x = 1; x < hinhgoc.Width - 1; x++)
                for (int y = 1; y < hinhgoc.Height - 1; y++)
                {

                    //tính[gx(R) gy(R)], [gx(G) gy(G)] và[gx(B) gy(B)] bằng cách nhân ma trận mặt nạ
                    //3x3 các điểm ảnh hàng xóm của điểm đang xét tại vị trí(x, y) với ma trận Sobel
                    //theo phương x(để tính gx) và theo phương y(để tính gy).
                    //Tao cac ma tran Sober
                    double[,] Sober_X = { { -1, -2, -1 }, { 0, 0, 0 }, { 1, 2, 1 } };
                    double[,] Sober_Y = { { -1, 0, 1 }, { -2, 0, 2 }, { -1, 0, 1 } };
                    double[,] Gradient = { { 0, 0 }, { 0, 0 }, { 0, 0 } };
                    double Gxx, Gxy, Gyy;
                    double theta_xy;
                    //Dung for de quet cac diem anh xung quanh diem f(x,y)    
                    for (int i = -1; i <= 1; i++)
                        for (int j = -1; j <= 1; j++)
                        {
                            Bgr pixelValue = hinhgoc[y + j, x + i];

                            //Tinh gradient
                            //red
                            Gradient[0, 0] = Gradient[0, 0] + pixelValue.Red * Sober_X[i + 1, j + 1];
                            Gradient[0, 1] = Gradient[0, 1] + pixelValue.Red * Sober_Y[i + 1, j + 1];

                            //green
                            Gradient[1, 0] = Gradient[1, 0] + pixelValue.Blue * Sober_X[i + 1, j + 1];
                            Gradient[1, 1] = Gradient[1, 1] + pixelValue.Blue * Sober_Y[i + 1, j + 1];

                            //blue
                            Gradient[2, 0] = Gradient[2, 0] + pixelValue.Green * Sober_X[i + 1, j + 1];
                            Gradient[2, 1] = Gradient[2, 1] + pixelValue.Green * Sober_Y[i + 1, j + 1];

                            //tính các giá trị gxx, gyy và gxy
                            Gxx = Gradient[0, 0] * Gradient[0, 0] + Gradient[1, 0] * Gradient[1, 0] + Gradient[2, 0] * Gradient[2, 0];
                            Gyy = Gradient[0, 1] * Gradient[0, 1] + Gradient[1, 1] * Gradient[1, 1] + Gradient[2, 1] * Gradient[2, 1];
                            Gxy = Gradient[0, 0]*Gradient[0, 1] + Gradient[1, 0]*Gradient[1, 1] + Gradient[2, 0]* Gradient[2, 1];

                            //Tinh goc theta
                            theta_xy = Math.Atan2((2 * Gxy), (Gxx - Gyy)) / 2;

                            //tính F0
                            double F0 = Math.Sqrt((Gxx + Gxy + (Gxx - Gyy)*Math.Cos(2*theta_xy) + 2*Gxy*Math.Sin(2*theta_xy))/2);

                            //Nếu M <= D0 thì f(x, y) là thuộc background, ngược lại thì f(x, y) là thuộc edge
                            if (F0 <= nguong)
                            {
                                AnhRGBDaPhanDoan.Data[y, x, 2] = 0;
                                AnhRGBDaPhanDoan.Data[y, x, 1] = 0;
                                AnhRGBDaPhanDoan.Data[y, x, 0] = 0;
                            }
                            else
                            {
                                AnhRGBDaPhanDoan.Data[y, x, 2] = 255;
                                AnhRGBDaPhanDoan.Data[y, x, 1] = 255;
                                AnhRGBDaPhanDoan.Data[y, x, 0] = 255;
                            }
                        }


                }
            return AnhRGBDaPhanDoan;
        }

        private void bt_Convert_Click(object sender, EventArgs e)
        {

            nguong = Int32.Parse(tBox_nguong.Text);
            imageBox_DuongBien.Image = PhanDoanAnhRGB(hinhgoc,nguong);
        }

    }
}
