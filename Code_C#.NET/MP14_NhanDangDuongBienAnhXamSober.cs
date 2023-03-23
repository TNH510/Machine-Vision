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

        public Image<Bgr, byte> PhanDoanAnhXam(Image<Bgr, byte> hinhgoc, int nguong)
        {
            //Tao mot anh chua hinh da xu li
            Image<Bgr, byte> AnhxamDaPhanDoan = new Image<Bgr, byte>(hinhgoc.Width, hinhgoc.Height);

            //Quet diem anh de tinh toan tren tung gia tri diem anh
            for (int x = 1; x < hinhgoc.Width - 1; x++)
                for (int y = 1; y < hinhgoc.Height - 1; y++)
                {

                    //xét điểm ảnh f(x, y) để biết pixel này là thuộc background(nền) hay edge(biên)
                    //tính #Gradient của f(x, y) theo công thức 10.2-9 trang 706.
                    //Dung phuong phap Sober
                    //Tao cac ma tran Sober
                    double[,] Sober_X = { { -1, -2, -1 }, { 0, 0, 0 }, { 1, 2, 1 } };
                    double[,] Sober_Y = { { -1, 0, 1 }, { -2, 0, 2 }, { -1, 0, 1 } };
                    double[] Gradient = {0 , 0};
                    //Dung for de quet cac diem anh xung quanh diem f(x,y)
                    double[,] DiemHangXom = new double [3, 3];
                    for (int i = -1; i <= 1; i++)
                        for (int j = -1; j <= 1; j++)
                        {
                            Bgr pixelValue = hinhgoc[y + j, x + i];

                            double R = pixelValue.Red;
                            double G = pixelValue.Green;
                            double B = pixelValue.Blue;
                            double gray = 0.2126 * R + 0.7152 * G + 0.0722 * B;
                            
                            //Tinh gradient
                            Gradient[0] += gray * Sober_X[i + 1, j + 1];
                            Gradient[1] += gray * Sober_Y[i + 1, j + 1];

                            //tính #Magnitude (length), hay còn gọi là biên độ, của vector gradient(f)
                            //M(x, y) = |gx| + |gy|
                            double M_xy = Math.Abs(Gradient[0]) + Math.Abs(Gradient[1]);

                            //Nếu M <= D0 thì f(x, y) là thuộc background, ngược lại thì f(x, y) là thuộc edge
                            if (M_xy <= nguong)
                            {
                                AnhxamDaPhanDoan.Data[y, x, 2] = 0;
                                AnhxamDaPhanDoan.Data[y, x, 1] = 0;
                                AnhxamDaPhanDoan.Data[y, x, 0] = 0;
                            }
                            else 
                            {
                                AnhxamDaPhanDoan.Data[y, x, 2] = 255;
                                AnhxamDaPhanDoan.Data[y, x, 1] = 255;
                                AnhxamDaPhanDoan.Data[y, x, 0] = 255;
                            }
                        }


                }
            return AnhxamDaPhanDoan;
        }

        private void bt_Convert_Click(object sender, EventArgs e)
        {

            nguong = Int32.Parse(tBox_nguong.Text);
            imageBox_DuongBien.Image = PhanDoanAnhXam(hinhgoc,nguong);
        }

    }
}
