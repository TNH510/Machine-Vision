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
Hue = Image.new(imgPIL.mode, imgPIL.size)
Saturation = Image.new(imgPIL.mode, imgPIL.size)
Value = Image.new(imgPIL.mode, imgPIL.size)
HSV = Image.new(imgPIL.mode, imgPIL.size)

#Dung vong for de doc het cac diem anh
for x in range(width):
    for y in range(height):

        #Lay gia tri R,G,B cua tung pixel va gan vao cac bien R,G,B
        R, G, B = imgPIL.getpixel((x,y))      

        t1 = ((R - G) + (R - B)) / 2
        t2 = math.sqrt((R - G) * (R - G) + (R - B)*(G - B))
        theta = math.acos(t1 / t2)

        #Tinh gia tri H
        H = 0
        if (B<= G):
            H = theta
        else :
            H = math.pi*2 - theta
        H = np.uint8(H*180/math.pi)

        #Tinh gia tri S
        Min = min(R,G,B)
        S = np.uint8((1 - 3*Min/(R+G+B))*255)

        #Tinh gia tri I
        V = np.uint8(max(R,G,B))
        
        Hue.putpixel((x,y),(H,H,H))

        Saturation.putpixel((x,y),(S,S,S))

        Value.putpixel((x,y),(V,V,V)) 
    
        #Black tao nen khi gan min(R,G,B) vao ca 3 kenh R,G,B
        HSV.putpixel((x,y),(V,S,H)) 

Hue_CV = np.array(Hue)
Intensity_CV = np.array(Value)
Saturation_CV = np.array(Saturation)
HSV_CV = np.array(HSV)

#Hien thi 4 tam hinh tren cung mot cua so
vert=np.concatenate((Hue_CV,Intensity_CV),axis=0)
vert1=np.concatenate((Saturation_CV,HSV_CV),axis=0)
vert2=np.concatenate((vert,vert1),axis=1)

#Dieu chinh kich thuoc cua so hien thi
HSV_program = cv2.resize(vert2,(700,700))

#Hiển thị hình dùng thư viện OpenCV
cv2.imshow('Anh goc', img)
cv2.imshow('HSI',HSV_program)

#Bấm phím bất kì để đóng cửa sổ hiển thị hình
cv2.waitKey(0)

#Giai phong bo nho
cv2.destroyAllWindows()