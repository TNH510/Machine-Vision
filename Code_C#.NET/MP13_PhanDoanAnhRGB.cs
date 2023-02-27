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
        int x1, x2, y1, y2, nguong;
        public Form1()
        {
            InitializeComponent();
            //Hien thi hinh goc trong picBox_Hinhgoc da tao
            imageBox_Hinhgoc.Image = hinhgoc;
            
        }
        public Image<Bgr, byte> ColorImageSegmentation(Image<Bgr, byte> hinhgoc, int x1, int x2, int y1, int y2, int nguong)
        {
            //Tao mot hinh chua anh sau khi da xu li
            Image<Bgr, byte> ImageSegmentation = new Image<Bgr, byte>(hinhgoc.Width, hinhgoc.Height);

            //a gọi là VECTOR MÀU TRUNG BÌNH (AVERAGE COLOR)
            double[] averageRGB = new double[3];
            //chọn một vùng ảnh từ vị trí (x1, y1) đến vị trí (x2, y2)
            //Vector a = [aR aG aB], chứa ba thành phần trung bình màu cho từng kênh R-G-B. 
            //Tại mỗi kênh màu R-G-B chúng ta tiến hành 
            //tính trung bình cộng của tất cả các điểm ảnh (pixel) thuộc vùng ảnh đã chọn
            for (int x = x1; x <= x2; x++)
            {
                for (int y = y1; y <= y2; y++)
                {
                    Bgr Value = hinhgoc[y, x];

                    //Tinh tong cac gia tri diem anh 
                    //Tai moi kenh R,G,B
                    averageRGB[0] += Value.Red;
                    averageRGB[1] += Value.Green;
                    averageRGB[2] += Value.Blue;
                }
            }
            //Tinh so diem anh co trong vung anh da chon
            int NumberPoints = Math.Abs(x2 - x1) * Math.Abs(y2 - y1);
            averageRGB[0] = (int)(averageRGB[0] / NumberPoints);
            averageRGB[1] = (int)(averageRGB[1] / NumberPoints);
            averageRGB[2] = (int)(averageRGB[2] / NumberPoints);

            //vector z chính là điểm ảnh tại vị trí (x, y) mà chúng ta
            //đang muốn tính xem nó là điểm thuộc 
            //nền (background) hay thuộc đối tượng (object)
            //z(x, y) = [R(x,y) G(x,y) B(x,y)] = [zR zG zB]
            for (int x = 0; x < hinhgoc.Width; x++)
                for (int y = 0; y < hinhgoc.Height; y++)
                {
                    Bgr Value = hinhgoc[y, x];
                    double zR = Value.Red;
                    double zG = Value.Green;
                    double zB = Value.Blue;

                    //tính Euclidean Distance giữa hai vector a và z sẽ như sau:
                    //D(z,a) = SQRT[(zR - aR)^2 + (zG - aG)^2 + (zB - aB)^2]
                    int D_za = (int)Math.Sqrt((zR - averageRGB[0])*(zR - averageRGB[0]) + (zG - averageRGB[1])*(zG - averageRGB[1]) + (zB - averageRGB[2])*(zB - averageRGB[2]));

                    //Sau khi tính được giá trị D(z,a), chúng ta sẽ so sánh với 
                    //giá trị ngưỡng (threshold) D0
                    //Nếu D(z,a) <= D0 thì z(x,y) là background, chúng ta có thể set màu cho điểm này về màu trắng 
                    //(255) hoặc đen (0) hay bất kỳ màu gì.
                    if (D_za <= nguong)
                    {
                        zR = 255;
                        zG = 255;
                        zB = 255;
                    }
                    //Ngược lại thì z(x,y) là object, chúng ta giữ nguyên màu cho điểm này
                    //Gan gia tri cac diem anh da xu li vao tam hinh da tao
                    ImageSegmentation.Data[y, x, 2] = (byte)zR;
                    ImageSegmentation.Data[y, x, 1] = (byte)zG;
                    ImageSegmentation.Data[y, x, 0] = (byte)zB;
                }
           
            return ImageSegmentation;
        }
  


        private void bt_Convert_Click(object sender, EventArgs e)
        {
            
            x1 = Int32.Parse(tBox_x1.Text);
            x2 = Int32.Parse(tBox_x2.Text);
            y1 = Int32.Parse(tBox_y1.Text);
            y2 = Int32.Parse(tBox_y2.Text);
            nguong = Int32.Parse(tBox_nguong.Text);
            imageBox_PhanDoan.Image = ColorImageSegmentation(hinhgoc, x1, x2, y1, y2, nguong);
        }

       

        private void tBox_x1_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void tBox_x2_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void tBox_y1_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void tBox_y2_TextChanged(object sender, EventArgs e)
        {
            
        }
        private void tBox_nguong_TextChanged(object sender, EventArgs e)
        {
            
        }
    }
}
