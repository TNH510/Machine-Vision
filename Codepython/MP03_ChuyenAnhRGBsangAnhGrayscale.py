import cv2 #Xu li anh
from PIL import Image #Thu vien xu li anh ho tro nhieu loai anh
import numpy as np

#Khai bao duong dan file hinh
filehinh = r'lena30.jpg'

#Doc anh mau dung thu vien OpenCV
img = cv2.imread(filehinh, cv2.IMREAD_COLOR)

#Doc anh mau bang thu vien PIL
#Thuc hien cac tac vu xu li tren PIL
imgPIL = Image.open(filehinh)

#Tao mot anh co cung kich thuoc voi anh imgPIL
#Anh nay dung de chua ket qua chuyen doi RGB sang Grayscale
average = Image.new(imgPIL.mode, imgPIL.size)
lightness = Image.new(imgPIL.mode, imgPIL.size)
luminance = Image.new(imgPIL.mode, imgPIL.size)

#Lay kich thuoc cua anh tu imgPIL
width = average.size[0]
height = average.size[1]

#Moi anh la mot ma tran 2 chieu nen dung 2 vong for de doc
#het cac diem anh
for x in range(width):
    for y in range(height):
        #Lay gia tri cac diem anh tai vi tri (x,y)
        R, G, B = imgPIL.getpixel((x,y))

        #Cong thuc chuyen doi diem anh mau RGB thanh diem anh muc xam
        #dung phuong phap average
        gray_average = np.uint8((R + G + B)/3)

        #dung phuong phap lightness
        MIN = min(R,G,B)
        MAX = max(R,G,B)
        gray_lightness = np.uint8((MIN + MAX)/2)

        #dung phuong phap luminance
        gray_luminance = np.uint8(0.2126*R + 0.7152*G + 0.0722*B)

        #Gan gia tri muc xam vua tinh cho anh xam
        average.putpixel((x,y), (gray_average, gray_average, gray_average))
        lightness.putpixel((x,y), (gray_lightness, gray_lightness, gray_lightness))
        luminance.putpixel((x,y), (gray_luminance, gray_luminance, gray_luminance))

#Chuyen anh tu PIL sang OpenCV de hien thi
anhmucxam_average = np.array(average)
anhmucxam_lightness = np.array(lightness)
anhmucxam_luminance = np.array(luminance)

#Hien thi 4 tam hinh tren cung mot cua so
vert=np.concatenate((img,anhmucxam_average),axis=0)
vert1=np.concatenate((anhmucxam_lightness,anhmucxam_luminance),axis=0)
vert2=np.concatenate((vert,vert1),axis=1)

#Dieu chinh kich thuoc cua so hien thi
Grayscale_convert = cv2.resize(vert2,(800,800))

#Hien thi anh dung thu vien OpenCV
cv2.imshow('Chuyen RGB thanh Grayscale', Grayscale_convert)

#Bam phim bat ki de dong cua so
cv2.waitKey(0)

#Giai phong bo nho
cv2.destroyAllWindows()
