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
            imageBox_LamNet.Image = ColorImageSharpening(hinhgoc);
            

        }
        public Image<Bgr, byte> ColorImageSharpening(Image<Bgr, byte> hinhgoc)
        {
            //Tao mot hinh chua anh sau khi da xu li
            Image<Bgr, byte> ImageSharpened = new Image<Bgr, byte>(hinhgoc.Width, hinhgoc.Height);

            //Tien hanh quet cac diem anh

            //v^2(x,y) = f(x - 1, y) + f(x + 1, y) + f(x, y - 1) + f(x,y + 1) - 4*f(x,y)
            //g(x,y) = f(x,y) + c*v^2(x,y)
            //Su dung nhan tich chap ma tran diem anh de tinh gia tri tai diem anh can lam net
            //SharpeningMatrix = [0 -1 0 ; -1 4 -1; 0 -1 0]

            //Lay gia tri tung diem anh trong buc anh
            byte[,] f = new byte[3, 5];
            //Quet diem anh
            for (int x = 1; x < hinhgoc.Width - 1; x++)
                for (int y = 1; y < hinhgoc.Height - 1; y++)
                {
                    //Tao 5 bien gan gia tri cua diem anh hien tai va cua 4 diem xung quanh
                    Bgr Value_0 = hinhgoc[x, y];
                    Bgr Value_X1 = hinhgoc[x - 1, y];
                    Bgr Value_X2 = hinhgoc[x + 1, y];
                    Bgr Value_Y1 = hinhgoc[x, y - 1];
                    Bgr Value_Y2 = hinhgoc[x, y + 1];

                    //Gan cac gia tri vao cac mang R,G,B rieng biet
                    f[0, 0] = (byte)Value_0.Red;
                    f[0, 1] = (byte)Value_X1.Red;
                    f[0, 2] = (byte)Value_X2.Red;
                    f[0, 3] = (byte)Value_Y1.Red;
                    f[0, 4] = (byte)Value_Y2.Red;

                    f[1, 0] = (byte)Value_0.Green;
                    f[1, 1] = (byte)Value_X1.Green;
                    f[1, 2] = (byte)Value_X2.Green;
                    f[1, 3] = (byte)Value_Y1.Green;
                    f[1, 4] = (byte)Value_Y2.Green;

                    f[2, 0] = (byte)Value_0.Blue;
                    f[2, 1] = (byte)Value_X1.Blue;
                    f[2, 2] = (byte)Value_X2.Blue;
                    f[2, 3] = (byte)Value_Y1.Blue;
                    f[2, 4] = (byte)Value_Y2.Blue;
                    
                    
  
                    int[] g = new int[3];
                    for (int k = 0; k < 3; k++)
                    {
                        //g(x,y) = f(x,y) + c*v^2(x,y), chọn c = -1
                        //v^2(x,y) = f(x - 1, y) + f(x + 1, y) + f(x, y - 1) + f(x,y + 1) - 4*f(x,y)
                        g[k] = f[k, 0] + 4 * f[k, 0] - f[k,1] - f[k, 2] - f[k, 3] - f[k, 4] ;
                        if (g[k] < 0) { g[k] = 0; }
                        if (g[k] > 255) { g[k] = 255; }
                    }

                    //Gan cac gia tri R,G,B vao tam hinh da tao
                    ImageSharpened.Data[x, y, 2] = (byte)g[0];
                    ImageSharpened.Data[x, y, 1] = (byte)g[1];
                    ImageSharpened.Data[x, y, 0] = (byte)g[2];

                }

                    return ImageSharpened;
        }
    }
}
