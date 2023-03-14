import cv2 #Xu li anh
from PIL import Image #Thu vien xu li anh ho tro nhieu loai anh
import numpy as np
import matplotlib.pyplot as plt #Ve bieu do histogram

def  ColorImageSegmentation(imgPIL, x1, x2, y1, y2, nguong):
    ImageSegmentation = Image.new(imgPIL.mode, imgPIL.size)
    width = ImageSegmentation.size[0] 
    height = ImageSegmentation.size[1]

    averageRGB = np.zeros(3, float)
    for i in range(x1 ,x2 + 1):
        for j in range(y1 ,y2 + 1):
            #Lay gia tri cac diem anh tai vi tri (x,y)
            R, G, B = imgPIL.getpixel((i,j))

            averageRGB[0] += R
            averageRGB[1] += G
            averageRGB[2] += B

    NumberPoints = np.abs(x2 - x1)*np.abs(y2 - y1)
    averageRGB[0] = averageRGB[0]/NumberPoints
    averageRGB[1] = averageRGB[1]/NumberPoints
    averageRGB[2] = averageRGB[2]/NumberPoints

    for x in range(width):
        for y in range(height):
            zR,zG,zB = imgPIL.getpixel((x,y))

            D_za = np.sqrt((zR - averageRGB[0])*(zR - averageRGB[0]) + (zG - averageRGB[1])*(zG - averageRGB[1]) + (zB - averageRGB[2])*(zB - averageRGB[2]))

            if (D_za < nguong):
                zR = 255
                zB = 255
                zG = 255
            
            ImageSegmentation.putpixel((x,y), (np.uint8(zB), np.uint8(zG), np.uint8(zR)))
    ImageSegmentation_CV = np.array(ImageSegmentation)
    return ImageSegmentation_CV

def NhapThongSo():
    
    ThongSo = np.zeros(5, int)
    ThongSo[0] = input("Nhập x1:") #x1
    ThongSo[1] = input("Nhập x2:") #x2
    ThongSo[2] = input("Nhập y1:") #y1
    ThongSo[3] = input("Nhập y2:") #y2
    ThongSo[4] = input("Nhập nguõng:") #ngưỡng

    return ThongSo
    
ThongSo = np.zeros(5, int)
ThongSo = NhapThongSo()

#Tao duong dan
filehinh = r'lena30.jpg'
#Đọc ảnh màu dùng thư viện OpenCV
img = cv2.imread(filehinh, cv2.IMREAD_COLOR)
#Đọc ảnh màu dùng thư viện PIL
imgPIL = Image.open(filehinh)

img_Segmentation = ColorImageSegmentation(imgPIL,ThongSo[0], ThongSo[1],ThongSo[2],ThongSo[3], ThongSo[4])
cv2.imshow('Segmentation Image', img_Segmentation)
cv2.imshow('Original Image', img)

#Bam phim bat ki de dong cua so
cv2.waitKey(0)

#Giai phong bo nho
cv2.destroyAllWindows()