import cv2 #Xu li anh
from PIL import Image #Thu vien xu li anh ho tro nhieu loai anh
import numpy as np
import math

def NhanDangDuongBien(anhRGB, nguong):
    #Tao tam anh luu ket qua
    AnhDaNhanDangDuongBien = Image.new(anhRGB.mode, anhRGB.size)

    #Lay kich thuoc anh goc
    width = anhRGB.size[0]
    height = anhRGB.size[1]

    Sober_X = np.array([[-1, -2, -1], [ 0, 0, 0 ],[ 1, 2, 1 ]])
    Sober_Y = np.array([[-1, 0, 1 ], [ -2, 0, 2 ],[ -1, 0, 1 ]])
    
    #Quet diem anh
    for y in range(1, height - 1):
        for x in range(1, width - 1):

            Gradient = np.array([[0 , 0],[0 , 0],[0 , 0]])
            for j in range(-1, 2):
                for i in range(-1, 2):
                    #Lay gia tri cac diem anh tai vi tri (x,y)
                    r,g,b = imgPIL.getpixel((x + i,y + j))
                     
                    #Tinh gradient
                    Gradient[0, 0] += r * Sober_X[i + 1, j + 1]
                    Gradient[0, 1] += r * Sober_Y[i + 1, j + 1]

                    Gradient[1, 0] += g * Sober_X[i + 1, j + 1]
                    Gradient[1, 1] += g * Sober_Y[i + 1, j + 1]

                    Gradient[2, 0] += b * Sober_X[i + 1, j + 1]
                    Gradient[2, 1] += b * Sober_Y[i + 1, j + 1]

                    #Tinh gxx, gxy, gyy
                    Gxx = Gradient[0, 0]*Gradient[0, 0] + Gradient[1, 0]*Gradient[1, 0] + Gradient[2, 0]*Gradient[2, 0]
                    Gyy = Gradient[0, 1]*Gradient[0, 1] + Gradient[1, 1]*Gradient[1, 1] + Gradient[2, 1]*Gradient[2, 1]
                    Gxy = Gradient[0, 0]*Gradient[0, 1] + Gradient[1, 0]*Gradient[1, 1] + Gradient[2, 0]*Gradient[2, 1]
                    #Tinh goc theta
                    theta_xy = math.atan2((2 * Gxy), (Gxx - Gyy)) / 2

                    #Tinh F0
                    F0 = math.sqrt((Gxx + Gxy + (Gxx - Gyy)*math.cos(2*theta_xy) + 2*Gxy*math.sin(2*theta_xy))/2)

                    if (F0 <= nguong):
                        AnhDaNhanDangDuongBien.putpixel((x, y), (0, 0, 0))
                    else:
                        AnhDaNhanDangDuongBien.putpixel((x, y), (255, 255, 255))
    AnhDaNhanDangDuongBien_CV = np.array(AnhDaNhanDangDuongBien)
    return AnhDaNhanDangDuongBien_CV

def NhapThongSo():
    
    ThongSo = int(input("Nhập ngưỡng:")) #ngưỡng
    return ThongSo
    
ThongSo = NhapThongSo()

#Tao duong dans
filehinh = r'lena_color.jpg'
#Đọc ảnh màu dùng thư viện OpenCV
img = cv2.imread(filehinh, cv2.IMREAD_COLOR)
#Đọc ảnh màu dùng thư viện PIL
imgPIL = Image.open(filehinh)

img_NhanDangDuongBien = NhanDangDuongBien(imgPIL,ThongSo)
cv2.imshow('Anh nhan dang duong bien', img_NhanDangDuongBien)
cv2.imshow('Original Image', img)

#Bam phim bat ki de dong cua so
cv2.waitKey(0)

#Giai phong bo nho
cv2.destroyAllWindows()