//code
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

namespace MP03_ChuyenAnhMauRGBsangGrayscale
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Image<Bgr, byte> hinhgoc = new Image<Bgr, byte>(@"H:\Code\Photo\lena30.jpg");
            imageBox_Hinhgoc.Image = hinhgoc;

            //Hien thi hinh goc trong picBox_Hinhgoc da tao
            imageBox_Hinhgoc.Image = hinhgoc;

            //Hien thi hinh muc xam Lightness
            imageBox_Lightness.Image = HinhxamLightness(hinhgoc);

            //Hien thi anh muc xam Avarage
            imageBox_Average.Image = HinhxamAverage(hinhgoc);

            //Hien thi anh muc xam Luminance
            imageBox_Luminance.Image = HinhxamLuminance(hinhgoc);



        }

        //Khai bao ham tinh toan muc xam theo phuong phap Average
        public Image<Bgr, byte> HinhxamAverage(Image<Bgr, byte> hinhgoc)
        {
            Image<Bgr, byte> Hinhmucxam = new Image<Bgr, byte>(hinhgoc.Width, hinhgoc.Height);

            for (int x = 0; x < hinhgoc.Width; x++)
            {
                for (int y = 0; y < hinhgoc.Height; y++)
                {
                    //Lay diem anh
                    Bgr pixelValue = hinhgoc[x, y];
                    byte R = (byte)pixelValue.Red;
                    byte G = (byte)pixelValue.Green;
                    byte B = (byte)pixelValue.Blue;

                    //Tinh gia tri muc xam
                    byte gray = (byte)((R + G + B) / 3);

                    //Gan gia tri muc xam vua tinh duoc vao hinh muc xam
                    Hinhmucxam.Data[x, y, 2] = gray;
                    Hinhmucxam.Data[x, y, 1] = gray;
                    Hinhmucxam.Data[x, y, 0] = gray;


                }
            }
            return Hinhmucxam;


        }

        //Khai bao ham tinh toan muc xam theo phuong phap lightness
        public Image<Bgr, byte> HinhxamLightness(Image<Bgr, byte> hinhgoc)
        {
            Image<Bgr, byte> Hinhmucxam = new Image<Bgr, byte>(hinhgoc.Width, hinhgoc.Height);

            for (int x = 0; x < hinhgoc.Width; x++)
            {
                for (int y = 0; y < hinhgoc.Height; y++)
                {
                    //Lay diem anh
                    Bgr pixelValue = hinhgoc[x, y];
                    byte R = (byte)pixelValue.Red;
                    byte G = (byte)pixelValue.Green;
                    byte B = (byte)pixelValue.Blue;

                    //Tinh gia tri muc xam
                    byte max = Math.Max(R,Math.Max(G,B));
                    byte min = Math.Min(R,Math.Min(G,B));
                    byte gray = (byte)((max + min) / 2);

                    //Gan gia tri muc xam vua tinh duoc vao hinh muc xam
                    Hinhmucxam.Data[x, y, 2] = gray;
                    Hinhmucxam.Data[x, y, 1] = gray;
                    Hinhmucxam.Data[x, y, 0] = gray;

                    

                }
            }
            return Hinhmucxam;

        }

        //Khai bao ham tinh toan muc xam bang phuong phap Luminance (do sang tuyen tinh)
        public Image<Bgr, byte> HinhxamLuminance(Image<Bgr, byte> hinhgoc)
        {
            Image<Bgr, byte> Hinhmucxam = new Image<Bgr, byte>(hinhgoc.Width, hinhgoc.Height);

            for (int x = 0; x < hinhgoc.Width; x++)
            {
                for (int y = 0; y < hinhgoc.Height; y++)
                {
                    //Lay diem anh
                    Bgr pixelValue = hinhgoc[x, y];
                    byte R = (byte)pixelValue.Red;
                    byte G = (byte)pixelValue.Green;
                    byte B = (byte)pixelValue.Blue;

                    //Tinh gia tri muc xam

                    byte gray = (byte)((0.2126*R + 0.7152*G + 0.0722*B));

                    //Gan gia tri muc xam vua tinh duoc vao hinh muc xam
                    Hinhmucxam.Data[x, y, 2] = gray;
                    Hinhmucxam.Data[x, y, 1] = gray;
                    Hinhmucxam.Data[x, y, 0] = gray;



                }
            }
            return Hinhmucxam;

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}
