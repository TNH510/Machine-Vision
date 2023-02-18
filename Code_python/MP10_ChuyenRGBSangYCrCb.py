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
Y = Image.new(imgPIL.mode, imgPIL.size)
Cr = Image.new(imgPIL.mode, imgPIL.size)
Cb = Image.new(imgPIL.mode, imgPIL.size)
YCrCb = Image.new(imgPIL.mode, imgPIL.size)

#Dung vong for de doc het cac diem anh
for x in range(width):
    for y in range(height):

        #Lay gia tri R,G,B cua tung pixel va gan vao cac bien R,G,B
        R, G, B = imgPIL.getpixel((x,y))      

        Y_Value = np.uint8(16 + (65.738 / 256) * R + (129.057 / 256) * G + (25.064 / 256) * B)
        Cr_Value = np.uint8(128 - (37.945 / 256) * R - (74.494 / 256) * G + (112.439 / 256) * B)
        Cb_Value = np.uint8(128 + (112.439 / 256) * R - (94.154 / 256) * G - (18.285 / 256) * B)
        
        Y.putpixel((x,y),(Y_Value,Y_Value,Y_Value))

        Cr.putpixel((x,y),(Cr_Value,Cr_Value,Cr_Value))

        Cb.putpixel((x,y),(Cb_Value,Cb_Value,Cb_Value)) 
    
        #Black tao nen khi gan min(R,G,B) vao ca 3 kenh R,G,B
        YCrCb.putpixel((x,y),(Cb_Value,Cr_Value,Y_Value)) 

Y_CV = np.array(Y)
Cr_CV = np.array(Cr)
Cb_CV = np.array(Cb)
YCrCb_CV = np.array(YCrCb)

#Hien thi 4 tam hinh tren cung mot cua so
vert=np.concatenate((Y_CV,Cr_CV),axis=0)
vert1=np.concatenate((Cb_CV,YCrCb_CV),axis=0)
vert2=np.concatenate((vert,vert1),axis=1)

#Dieu chinh kich thuoc cua so hien thi
YCrCb_program = cv2.resize(vert2,(700,700))

#Hiển thị hình dùng thư viện OpenCV
cv2.imshow('Anh goc', img)
cv2.imshow('HSI',YCrCb_program)

#Bấm phím bất kì để đóng cửa sổ hiển thị hình
cv2.waitKey(0)

#Giai phong bo nho
cv2.destroyAllWindows()