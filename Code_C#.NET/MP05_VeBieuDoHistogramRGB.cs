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

            //Tinh histogram
            double[,] histogram = TinhHistogram(hinhgoc);

            //Chuyen doi kieu du lieu
            List<PointPairList> points = ChuyendoiHistogram(histogram);

            //Ve bieu do histogram va cho hien thi
            zedGraphRGB.GraphPane = BieudoHistogram(points);
            zedGraphRGB.Refresh();




        }               

        //Tinh histogram cua anh RGB
        public double[,] TinhHistogram(Image <Bgr, byte> HinhRGB)
        {
            //Su dung mang 2 chieu de chua thong tin histogram
            //cho cac kenh R,G,B
            //3: la so kenh mau
            //256 la can 256 vi tri tuong ung gia tri mau tu 0 den 255
            double[,] histogram = new double[3, 256];

            for (int x = 0; x < HinhRGB.Width; x++)
                for (int y = 0; y < HinhRGB.Height; y++)
                {       
                    //Doc gia tri R,G,B cua tung pixel va gan gia tri cua chung vao cac bien R,G,B
                    Bgr pixelValue = HinhRGB[y, x];
                    byte R = (byte)pixelValue.Red;
                    byte G = (byte)pixelValue.Green;
                    byte B = (byte)pixelValue.Blue;

                    //Gia tri R hoặc G hoặc B cung la phan tu thu R hoặc G hoặc B trong mang histogram
                    //da khai bao. Se tang so dem cua phan tu gray len 1
                    histogram[0, R]++; //histogram kenh R
                    histogram[1, G]++; //histogram kenh G
                    histogram[2, B]++; //histogram kenh B

                }
            return histogram; //Tra mang 2 chieu chua thong tin histogram cua 3 kenh mau


        }

        List<PointPairList> ChuyendoiHistogram(double[,] histogram)
        {
            //PointPairList la kieu du lieu cua Zedgraph de ve bieu do
            List<PointPairList> points = new List<PointPairList>();
            PointPairList redPoints = new PointPairList();  //Chua histogram kenh R
            PointPairList greenPoints = new PointPairList();//Chua histogram kenh G
            PointPairList bluePoints = new PointPairList(); //Chua histogram kenh B
            for (int i = 0; i < 256; i++)
            {
                //i tuong ung truc nam ngang tu 0 - 255
                //Histogram tuong ung truc dung, la so pixel
                greenPoints.Add(i, histogram[1, i]);    //Chuyen doi cho kenh G
                redPoints.Add(i, histogram[0, i]);      //Chuyen doi cho kenh R                   
                bluePoints.Add(i, histogram[2, i]);     //Chuyen doi cho kenh B

            }
            points.Add(redPoints);
            points.Add(greenPoints);
            points.Add(bluePoints);

            return points;

        }

        //Thiet lap mot bieu do trong Zedgraph
        public GraphPane BieudoHistogram(List<PointPairList> histogram)
        {
            //GraphPane la doi tuong trong Zedgraph
            GraphPane gp = new GraphPane();

            gp.Title.Text = @"Biểu đồ Histogram";
            gp.Rect = new Rectangle(0, 0, 700, 500);//Khung chua bieu do

            //Thiet lap truc ngang
            gp.XAxis.Title.Text = @"Giá trị mau của các điểm ảnh";
            gp.XAxis.Scale.Min = 0; // nho nhat la 0
            gp.XAxis.Scale.Max = 255; // lon nhat la 255
            gp.XAxis.Scale.MajorStep = 5;// Moi buoc chinh la 5
            gp.XAxis.Scale.MinorStep = 1; //Moi buoc trong mot buoc chinh la 1

            //Tuong tu thiet lap cho truc dung
            gp.YAxis.Title.Text = @"Số điểm ảnh có cùng giá trị màu";
            gp.YAxis.Scale.Min = 0; // nho nhat la 0
            gp.YAxis.Scale.Max = 15000; // So nay phai lon hon kich thuoc anh
            gp.YAxis.Scale.MajorStep = 5;// Moi buoc chinh la 5
            gp.YAxis.Scale.MinorStep = 1; //Moi buoc trong mot buoc chinh la 1

            //Dung bieu do dang bar de bieu dien histgram          
            gp.AddBar("Histogram's Red", histogram[0], Color.Red);
            gp.AddBar("Histogram's Green", histogram[1], Color.Green);
            gp.AddBar("Histogram's Blue", histogram[2], Color.Blue);

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
