import cv2 #Xu li anh
from PIL import Image #Thu vien xu li anh ho tro nhieu loai anh
import numpy as np
import matplotlib.pyplot as plt #Ve bieu do histogram


#Tinh histogram cua anh RGB
def TinhHistogram(HinhPIL):
    #Moi pixel co gia tri tu 0 den 255 nen tao ra mang 256 gia tri
    his = np.zeros(( 3,256), np.uint8)
    hisR = np.zeros(256)
    hisG = np.zeros(256)
    hisB = np.zeros(256)

    #Kich thuoc anh
    w = HinhPIL.size[0]
    h = HinhPIL.size[1]

    for x in range(w):
        for y in range(h):
            #Lay gia tri xam tai diem (x,y)
            R, G, B = HinhPIL.getpixel((x,y))

            #Gia tri gray tinh ra cung chinh la phan tu thu gray
            #trong mang da khai bao o tren, se tang so dem
            #cua phan tu thu gray len 1
            hisR[R] += 1 #Trong hinh xam thi R
            hisG[G] += 1 #Trong hinh xam thi G
            hisB[B] += 1 #Trong hinh xam thi B
    his = [hisR, hisG, hisB]    

    return his

#Ve bieu do histogram dung matplotlib
def VeBieuDoHistogram(his):
    w = 5
    h = 4
    plt.figure('Biểu đồ histogram ảnh màu RGB', figsize= (((w, h))), dpi = 100) 
    trucX = np.zeros(256)
    trucX = np.linspace(0,256,256)
    plt.plot(trucX, his[0], color = 'red')
    plt.plot(trucX, his[1], color = 'green')
    plt.plot(trucX, his[2], color = 'blue')
    plt.title('Biểu đồ histogram ảnh màu RGB')
    plt.show()

#Chuong trinh chinh
#Khai bao duong dan file hinh
filehinh = r'bird_small.jpg'

#Doc anh mau bang thu vien PIL
#Thuc hien cac tac vu xu li tren PIL
imgPIL = Image.open(filehinh)

#Tinh histogram
his = TinhHistogram(imgPIL)

#Chuyen anh PIL sang OpenCV de hien thi
imgCV = cv2.imread(filehinh, cv2.IMREAD_COLOR)
cv2.imshow('Anh goc', imgCV)

#Hien thi bieu do Histogram
VeBieuDoHistogram(his) 

#Bam phim bat ki de dong cua so
cv2.waitKey(0)

#Giai phong bo nho
cv2.destroyAllWindows()
