import cv2 #thu vien xu li anh
import numpy as np  #thu vien toan hoc

#Đọc ảnh màu dùng thư viện OpenCV
img = cv2.imread('girl.jpg', cv2.IMREAD_COLOR)

#Lay kich thuoc cua anh
height, width, channel = img.shape

#Khai bao 3 bien de chua gia tri 3 kenh R,G,B
red = np.zeros(( height, width,3), np.uint8)
green = np.zeros(( height, width, 3), np.uint8)
blue = np.zeros(( height, width, 3), np.uint8)

#Ban dau set zero cho tat ca cac diem anh co trong 3 kenh
red[:] = [0,0,0]
green[:] = [0,0,0]
blue[:] = [0,0,0]

#Dung vong for de doc het cac diem anh
for x in range(width):
    for y in range(height):

        R = img[y,x,2]
        G = img[y,x,1]
        B = img[y,x,0]

        red[y,x,2] = R
        green[y,x,1] = G
        blue[y,x,0] = B

#Hien thi 4 tam hinh tren cung mot cua so
vert=np.concatenate((img,red),axis=0)
vert1=np.concatenate((blue,green),axis=0)
vert2=np.concatenate((vert,vert1),axis=1)

#Dieu chinh kich thuoc cua so hien thi
RGB_program = cv2.resize(vert2,(700,700))

#Hiển thị hình dùng thư viện OpenCV
cv2.imshow('Tach mau RGB', RGB_program)

#Bấm phím bất kì để đóng cửa sổ hiển thị hình
cv2.waitKey(0)

#Giai phong bo nho
cv2.destroyAllWindows()