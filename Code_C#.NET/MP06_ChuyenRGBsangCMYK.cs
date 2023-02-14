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
            Image<Bgr, byte> cyan = new Image<Bgr, byte>(hinhgoc.Width, hinhgoc.Height);
            Image<Bgr, byte> magenta = new Image<Bgr, byte>(hinhgoc.Width, hinhgoc.Height);
            Image<Bgr, byte> yellow = new Image<Bgr, byte>(hinhgoc.Width, hinhgoc.Height);
            Image<Bgr, byte> black = new Image<Bgr, byte>(hinhgoc.Width, hinhgoc.Height);

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
                    byte R = (byte)pixelValue.Red;
                    byte G = (byte)pixelValue.Green;
                    byte B = (byte)pixelValue.Blue;
                    byte K = Math.Min(R, Math.Min(G, B));

                    //set gia tri pixel doc duoc cho cac hinh chua
                    //cac kenh mau tuong ung
                    //Cyan la ket hop cua G va B
                    cyan.Data[y, x, 2] = 0;
                    cyan.Data[y, x, 1] = (Byte)G;
                    cyan.Data[y, x, 0] = (Byte)B;

                    //Magenta la ket hop cua R va B
                    magenta.Data[y, x, 2] = (Byte)R;
                    magenta.Data[y, x, 1] = 0;
                    magenta.Data[y, x, 0] = (Byte)B;

                    //Yellow la ket hop cua R va G
                    yellow.Data[y, x, 2] = (Byte)R;
                    yellow.Data[y, x, 1] = (Byte)G;
                    yellow.Data[y, x, 0] = 0;

                    //Black tao ra khi gan gia tri min(R,G,B) vao ca 3 kenh R,G,B
                    black.Data[y, x, 2] = (Byte)K;
                    black.Data[y, x, 1] = (Byte)K;
                    black.Data[y, x, 0] = (Byte)K;



                }
            }
            //Hien thi cac kenh mau len imageBox
            imageBox_C.Image = cyan;
            imageBox_M.Image = magenta;
            imageBox_Y.Image = yellow;
            imageBox_K.Image = black;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void imageBox_Hinhgoc_Click(object sender, EventArgs e)
        {

        }
    }
}
