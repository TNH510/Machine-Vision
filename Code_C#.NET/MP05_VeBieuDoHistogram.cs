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
using ZedGraph;

namespace MP03_ChuyenAnhMauRGBsangGrayscale
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Image<Bgr, byte> hinhgoc = new Image<Bgr, byte>(@"H:\Code\Photo\girl.jpg");
            imageBox_Hinhgoc.Image = hinhgoc;

            //Hien thi hinh goc trong picBox_Hinhgoc da tao
            imageBox_Hinhgoc.Image = hinhgoc;

            //Hien thi anh muc xam Avarage
            imageBox_Average.Image = HinhxamAverage(hinhgoc);

            //Goi cac ham de ve bieu do histogram
            //=================================================

            Image <Bgr, byte> Hinhmucxam = HinhxamAverage(hinhgoc);
            //Tinh histogram
            double[] histogram = TinhHistogram(Hinhmucxam);

            //Chuyen doi kieu du lieu
            PointPairList points = ChuyendoiHistogram(histogram);

            //Ve bieu do histogram va cho hien thi
            zedGraphHistogram.GraphPane = BieudoHistogram(points);
            zedGraphHistogram.Refresh();



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
                    Bgr pixelValue = hinhgoc[y, x];
                    byte R = (byte)pixelValue.Red;
                    byte G = (byte)pixelValue.Green;
                    byte B = (byte)pixelValue.Blue;

                    //Tinh gia tri muc xam
                    byte gray = (byte)((R + G + B) / 3);

                    //Gan gia tri muc xam vua tinh duoc vao hinh muc xam
                    Hinhmucxam.Data[y, x, 2] = gray;
                    Hinhmucxam.Data[y, x, 1] = gray;
                    Hinhmucxam.Data[y, x, 0] = gray;


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

        //Tinh histogram cua anh xam
        public double[] TinhHistogram(Image <Bgr, byte> Hinhmucxam)
        {
            //Moi pixel muc xam co gia tri tu 0 den 255, do vay ta khai bao mot mang
            //co 256 phan tu dung de chua so dem cua cac pixels co cung muc xam trong anh
            //Dung kieu double vi tong so dem rat lon va phu thuoc vao kich thuoc cua anh
            double[] histogram = new double[256];

            for (int x = 0; x < Hinhmucxam.Width; x++)
                for (int y = 0; y < Hinhmucxam.Height; y++)
                {
                    Bgr pixelValue = Hinhmucxam[y, x];
                    byte gray = (byte)pixelValue.Red;
                    //Lay red vi trong hinh xam red = green = blue

                    //Gia tri gray cung la phan tu thu gray trong mang histogram
                    //da khai bao. Se tang so dem cua phan tu gray len 1
                    histogram[gray]++;
          
                }
            return histogram;//So luong diem anh co cung gia tri muc xam


        }
        //Chuyen du lieu tu dang ma tran sang kieu du lieu cua zedgragp
        PointPairList ChuyendoiHistogram(double[] histogram) 
        {
            //PointPairList la kieu du lieu cua Zedgraph de ve bieu do
            PointPairList points = new PointPairList();

            for (int i = 0; i < histogram.Length; i++)
            {   
                //i tuong ung truc nam ngang tu 0 - 255
                //Histogram tuong ung truc dung, la so pixel cung 
                points.Add(i, histogram[i]);

                
            }
            return points;

        }

        //Thiet lap mot bieu do trong Zedgraph
        public GraphPane BieudoHistogram(PointPairList histogram)
        {
            //GraphPane la doi tuong trong Zedgraph
            GraphPane gp = new GraphPane();

            gp.Title.Text = @"Biểu đồ Histogram";
            gp.Rect = new Rectangle(0, 0, 700, 500);//Khung chua bieu do

            //Thiet lap truc ngang
            gp.XAxis.Title.Text = @"Giá trị mức xám của các điểm ảnh";
            gp.XAxis.Scale.Min = 0; // nho nhat la 0
            gp.XAxis.Scale.Max = 255; // lon nhat la 255
            gp.XAxis.Scale.MajorStep = 5;// Moi buoc chinh la 5
            gp.XAxis.Scale.MinorStep = 1; //Moi buoc trong mot buoc chinh la 1

            //Tuong tu thiet lap cho truc dung
            gp.YAxis.Title.Text = @"Số điểm ảnh có cùng mức xám";
            gp.YAxis.Scale.Min = 0; // nho nhat la 0
            gp.YAxis.Scale.Max = 15000; // So nay phai lon hon kich thuoc anh
            gp.YAxis.Scale.MajorStep = 5;// Moi buoc chinh la 5
            gp.YAxis.Scale.MinorStep = 1; //Moi buoc trong mot buoc chinh la 1

            //Dung bieu do dang bar de bieu dien histgram
            gp.AddBar("Histogram", histogram, Color.OrangeRed);
            return gp;

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void zedGraph_Load(object sender, EventArgs e)
        {

        }
    }
}
