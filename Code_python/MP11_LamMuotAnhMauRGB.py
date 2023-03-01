import cv2 #Xu li anh
from PIL import Image #Thu vien xu li anh ho tro nhieu loai anh
import numpy as np
import matplotlib.pyplot as plt #Ve bieu do histogram


#Tao duong dan
filehinh = r'bird_small.jpg'
#Đọc ảnh màu dùng thư viện OpenCV
img = cv2.imread(filehinh, cv2.IMREAD_COLOR)
#Đọc ảnh màu dùng thư viện PIL
imgPIL = Image.open(filehinh)

def ImageSmoothing3x3(imgPIL):
    SmoothedImage3x3 = Image.new(imgPIL.mode, imgPIL.size)
    width = SmoothedImage3x3.size[0] 
    height = SmoothedImage3x3.size[1]
    for x in range(1, width - 1):
        for y in range(1, height - 1):
            #Lay gia tri cac diem anh tai vi tri (x,y)
            Rs = 0
            Gs = 0
            Bs = 0

            for i in range(x - 1,x + 2):
                for j in range(y - 1,y + 2):
                    #Lay gia tri cac diem anh tai vi tri (x,y)
                    R, G, B = imgPIL.getpixel((i,j))

                    Rs += R
                    Gs += G
                    Bs += B

            K = 3*3
            Rs = np.uint8(Rs/K)   
            Bs = np.uint8(Bs/K)
            Gs = np.uint8(Gs/K)

            SmoothedImage3x3.putpixel((x,y),(Bs, Gs, Rs))
    SmoothedImage3x3_CV = np.array(SmoothedImage3x3)
    return SmoothedImage3x3_CV

def ImageSmoothing5x5(imgPIL):
    SmoothedImage5x5 = Image.new(imgPIL.mode, imgPIL.size)
    width = SmoothedImage5x5.size[0] 
    height = SmoothedImage5x5.size[1]
    for x in range(2, width - 2):
        for y in range(2, height - 2):
            #Lay gia tri cac diem anh tai vi tri (x,y)
            Rs = 0
            Gs = 0
            Bs = 0

            for i in range(x - 2,x + 3):
                for j in range(y - 2,y + 3):
                    #Lay gia tri cac diem anh tai vi tri (x,y)
                    R, G, B = imgPIL.getpixel((i,j))

                    Rs += R
                    Gs += G
                    Bs += B

            K = 5*5
            Rs = np.uint8(Rs/K)   
            Bs = np.uint8(Bs/K)
            Gs = np.uint8(Gs/K)

            SmoothedImage5x5.putpixel((x,y),(Bs, Gs, Rs))
    SmoothedImage5x5_CV = np.array(SmoothedImage5x5)
    return SmoothedImage5x5_CV

def ImageSmoothing7x7(imgPIL):
    SmoothedImage7x7 = Image.new(imgPIL.mode, imgPIL.size)
    width = SmoothedImage7x7.size[0] 
    height = SmoothedImage7x7.size[1]
    for x in range(3, width - 3):
        for y in range(3, height - 3):
            #Lay gia tri cac diem anh tai vi tri (x,y)
            Rs = 0
            Gs = 0
            Bs = 0

            for i in range(x - 3,x + 4):
                for j in range(y - 3,y + 4):
                    #Lay gia tri cac diem anh tai vi tri (x,y)
                    R, G, B = imgPIL.getpixel((i,j))

                    Rs += R
                    Gs += G
                    Bs += B

            K = 7*7
            Rs = np.uint8(Rs/K)   
            Bs = np.uint8(Bs/K)
            Gs = np.uint8(Gs/K)

            SmoothedImage7x7.putpixel((x,y),(Bs, Gs, Rs))
    SmoothedImage7x7_CV = np.array(SmoothedImage7x7)
    return SmoothedImage7x7_CV

def ImageSmoothing9x9(imgPIL):
    SmoothedImage9x9 = Image.new(imgPIL.mode, imgPIL.size)
    width = SmoothedImage9x9.size[0] 
    height = SmoothedImage9x9.size[1]
    for x in range(4, width - 4):
        for y in range(4, height - 4):
            #Lay gia tri cac diem anh tai vi tri (x,y)
            Rs = 0
            Gs = 0
            Bs = 0

            for i in range(x - 4,x + 5):
                for j in range(y - 4,y + 5):
                    #Lay gia tri cac diem anh tai vi tri (x,y)
                    R, G, B = imgPIL.getpixel((i,j))

                    Rs += R
                    Gs += G
                    Bs += B

            K = 9*9
            Rs = np.uint8(Rs/K)   
            Bs = np.uint8(Bs/K)
            Gs = np.uint8(Gs/K)

            SmoothedImage9x9.putpixel((x,y),(Bs, Gs, Rs))
    SmoothedImage9x9_CV = np.array(SmoothedImage9x9)
    return SmoothedImage9x9_CV

img_3x3 = ImageSmoothing3x3(imgPIL)
img_5x5 = ImageSmoothing5x5(imgPIL)
img_7x7 = ImageSmoothing7x7(imgPIL)
img_9x9 = ImageSmoothing9x9(imgPIL)

#Hien thi 4 tam hinh tren cung mot cua so
vert=np.concatenate((img_3x3,img_5x5),axis=0)
vert1=np.concatenate((img_7x7,img_9x9),axis=0)
vert2=np.concatenate((vert,vert1),axis=1)

#Dieu chinh kich thuoc cua so hien thi
Smoothing_convert = cv2.resize(vert2,(800,800))

cv2.imshow('Original Image', img)
cv2.imshow('Smooth Image', Smoothing_convert)

#Bam phim bat ki de dong cua so
cv2.waitKey(0)

#Giai phong bo nho
cv2.destroyAllWindows()