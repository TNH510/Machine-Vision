import cv2 #Xu li anh
from PIL import Image #Thu vien xu li anh ho tro nhieu loai anh
import numpy as np
import matplotlib.pyplot as plt #Ve bieu do histogram
import math
#Tao duong dan
filehinh = r'lena_color.jpg'
#Đọc ảnh màu dùng thư viện OpenCV
img = cv2.imread(filehinh, cv2.IMREAD_COLOR)
#Đọc ảnh màu dùng thư viện PIL

imgPIL = Image.open(filehinh)
#Lay kich thuoc cua anh
height, width, channel = img.shape

#Khai bao 3 bien de chua gia tri 4 kenh C,M,Y,K
X = Image.new(imgPIL.mode, imgPIL.size)
Y = Image.new(imgPIL.mode, imgPIL.size)
Z = Image.new(imgPIL.mode, imgPIL.size)
XYZ = Image.new(imgPIL.mode, imgPIL.size)

#Dung vong for de doc het cac diem anh
for x in range(width):
    for y in range(height):

        #Lay gia tri R,G,B cua tung pixel va gan vao cac bien R,G,B
        R, G, B = imgPIL.getpixel((x,y))      

        X_Value = np.uint8(0.4124564 * R + 0.3575761 * G + 0.1804375 * B)
        Y_Value = np.uint8(0.2126729 * R + 0.7151522 * G + 0.0721750 * B)
        Z_Value = np.uint8(0.0193339 * R + 0.1191920 * G + 0.9503041 * B)
        
        X.putpixel((x,y),(X_Value,X_Value,X_Value))

        Y.putpixel((x,y),(Y_Value,Y_Value,Y_Value))

        Z.putpixel((x,y),(Z_Value,Z_Value,Z_Value)) 
    
        #Black tao nen khi gan min(R,G,B) vao ca 3 kenh R,G,B
        XYZ.putpixel((x,y),(Z_Value,Y_Value,X_Value)) 

X_CV = np.array(X)
Y_CV = np.array(Y)
Z_CV = np.array(Z)
XYZ_CV = np.array(XYZ)

#Hien thi 4 tam hinh tren cung mot cua so
vert=np.concatenate((X_CV,Y_CV),axis=0)
vert1=np.concatenate((Z_CV,XYZ_CV),axis=0)
vert2=np.concatenate((vert,vert1),axis=1)

#Dieu chinh kich thuoc cua so hien thi
XYZ_program = cv2.resize(vert2,(700,700))

#Hiển thị hình dùng thư viện OpenCV
cv2.imshow('Anh goc', img)
cv2.imshow('XYZ',XYZ_program)

#Bấm phím bất kì để đóng cửa sổ hiển thị hình
cv2.waitKey(0)

#Giai phong bo nho
cv2.destroyAllWindows()