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
        Image<Bgr, byte> hinhgoc; //Tao bien hinhgoc la bien toan cuc de su dung cho nhieu ham
        public Form1()
        {
            InitializeComponent();
            //Chuyen bien nay thanh bien toan cuc (global) de su dung cho ham khac
            hinhgoc = new Image<Bgr, byte>(@"H:\Code\Photo\lena30.jpg");
            imageBox_Hinhgoc.Image = hinhgoc;           

            //Hien thi hinh goc trong picBox_Hinhgoc da tao
            imageBox_Hinhgoc.Image = hinhgoc;

            //Hien thi hinh muc xam Lightness
            imageBox_Lightness.Image = HinhxamLightness(hinhgoc);

            //Hien thi anh muc xam Avarage
            imageBox_Average.Image = HinhxamAverage(hinhgoc);

            //Hien thi anh muc xam Luminance
            imageBox_Luminance.Image = HinhxamLuminance(hinhgoc);

            //Hien thi anh nhi phan
            imageBox_Binary.Image = HinhNhiPhan(hinhgoc, 100);

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

        public Image<Bgr, byte> HinhNhiPhan(Image<Bgr, byte> hinhgoc, byte nguong)
        {
            Image<Bgr, byte> Hinhnhiphan = new Image<Bgr, byte>(hinhgoc.Width, hinhgoc.Height);

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

                    byte binary = (byte)((0.2126 * R + 0.7152 * G + 0.0722 * B));

                    //Phan loai diem anh sang nhi phan dua vao gia tri nguong
                    if (binary < nguong)
                        binary = 0;
                    else
                        binary = 255;

                    //Gan gia tri nhi phan vua tinh vao hinh nhi phan
                    Hinhnhiphan.Data[x, y, 2] = binary;
                    Hinhnhiphan.Data[x, y, 1] = binary;
                    Hinhnhiphan.Data[x, y, 0] = binary;


                }
            }
            return Hinhnhiphan;
        }

        private void vScrollBar1_Scroll(object sender, EventArgs e)
        {
            //Lay gia ti nguong tu gia tri thanh cuon
            byte nguong = (byte)vScrollBarHinhnhiphan.Value;

            //Cho hien thi gia tri nguong
            lblNguong.Text = nguong.ToString();

            //Goi ham tinh anh nhi phan va cho hien thi
            imageBox_Binary.Image = HinhNhiPhan(hinhgoc, nguong);
        }


        private void Form1_Load(object sender, EventArgs e)
        {








        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click_1(object sender, EventArgs e)
        {

        }


        private void label6_Click(object sender, EventArgs e)
        {

        }
    }
}
