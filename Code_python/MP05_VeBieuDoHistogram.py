import cv2 #Xu li anh
from PIL import Image #Thu vien xu li anh ho tro nhieu loai anh
import numpy as np
import matplotlib.pyplot as plt #Ve bieu do histogram

def ChuyenAnhRGBsangAnhGrayscale(imgPIL):
    
    #Tao mot anh co cung kich thuoc voi anh imgPIL
    #Anh nay dung de chua ket qua chuyen doi RGB sang Grayscale
    luminance = Image.new(imgPIL.mode, imgPIL.size)

    #Lay kich thuoc cua anh tu imgPIL
    width = luminance.size[0]
    height = luminance.size[1]

    #Moi anh la mot ma tran 2 chieu nen dung 2 vong for de doc
    #het cac diem anh
    for x in range(width):
        for y in range(height):
            #Lay gia tri cac diem anh tai vi tri (x,y)
            R, G, B = imgPIL.getpixel((x,y))

            #dung phuong phap luminance
            gray_luminance = np.uint8(0.2126*R + 0.7152*G + 0.0722*B)

            #Gan gia tri muc xam vua tinh cho anh xam
            luminance.putpixel((x,y), (gray_luminance, gray_luminance, gray_luminance))

    return luminance

#Tinh histogram cua anh xam
def TinhHistogram(HinhXamPIL):
    #Moi pixel co gia tri tu 0 den 255 nen tao ra mang 256 gia tri
    his = np.zeros(256)

    #Kich thuoc anh
    w = HinhXamPIL.size[0]
    h = HinhXamPIL.size[1]

    for x in range(w):
        for y in range(h):
            #Lay gia tri xam tai diem (x,y)
            gR, gG, gB = HinhXamPIL.getpixel((x,y))

            #Gia tri gray tinh ra cung chinh la phan tu thu gray
            #trong mang da khai bao o tren, se tang so dem
            #cua phan tu thu gray len 1
            his[gR] += 1 #Trong hinh xam thi R = G = B
    return his

#Ve bieu do histogram dung matplotlib
def VeBieuDoHistogram(his):
    w = 5
    h = 4
    plt.figure('Biểu đồ histogram ảnh xám', figsize= (((w, h))), dpi = 100) 
    trucX = np.zeros(256)#Tao ra mot mang chua 256 
    trucX = np.linspace(0,256,256)#min,max,khoảng cách giữa 2 điểm
    plt.plot(trucX, his, color = 'orange')
    plt.title('Biểu đồ histogram ảnh xám')
    plt.show()

#Chuong trinh chinh
#Khai bao duong dan file hinh
filehinh = r'lena30.jpg'

#Doc anh mau bang thu vien PIL
#Thuc hien cac tac vu xu li tren PIL
imgPIL = Image.open(filehinh)

#Chuyen thanh anh muc xam
HinhXamPIL = ChuyenAnhRGBsangAnhGrayscale(imgPIL)

#Tinh histogram
his = TinhHistogram(HinhXamPIL)

#Chuyen anh PIL sang OpenCV de hien thi
HinhXamCV = np.array(HinhXamPIL)
cv2.imshow('Anh muc xam', HinhXamCV)

#Hien thi bieu do Histogram
VeBieuDoHistogram(his) 

#Bam phim bat ki de dong cua so
cv2.waitKey(0)

#Giai phong bo nho
cv2.destroyAllWindows()
