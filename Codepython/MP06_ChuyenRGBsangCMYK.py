import cv2 #thu vien xu li anh
import numpy as np  #thu vien toan hoc

#Đọc ảnh màu dùng thư viện OpenCV
img = cv2.imread('girl.jpg', cv2.IMREAD_COLOR)

#Lay kich thuoc cua anh
height, width, channel = img.shape

#Khai bao 3 bien de chua gia tri 4 kenh C,M,Y,K
cyan = np.zeros(( height, width,3), np.uint8)
magenta = np.zeros(( height, width, 3), np.uint8)
yellow = np.zeros(( height, width, 3), np.uint8)
black = np.zeros(( height, width, 3), np.uint8)

#Ban dau set zero cho tat ca cac diem anh co trong 3 kenh
cyan[:] = [0,0,0]
magenta[:] = [0,0,0]
yellow[:] = [0,0,0]
black[:] = [0,0,0]

#Dung vong for de doc het cac diem anh
for x in range(width):
    for y in range(height):

        #Lay gia tri R,G,B cua tung pixel va gan vao cac bien R,G,B
        R = img[y,x,2]
        G = img[y,x,1]
        B = img[y,x,0]
        #Gan gia tri min cua R,G,B vao bien K
        K = min(R,G,B)

        #Cyan la ket hop cua Green va Blue
        cyan[y,x,1] = G
        cyan[y,x,0] = B

        #Magenta la ket hop cua Red va Blue
        magenta[y,x,2] = R
        magenta[y,x,0] = B

        #Yellow la ket hop cua Red va Green
        yellow[y,x,2] = R
        yellow[y,x,1] = G

        #Black tao nen khi gan min(R,G,B) vao ca 3 kenh R,G,B
        black[y,x,2] = K
        black[y,x,1] = K
        black[y,x,0] = K

#Hien thi 4 tam hinh tren cung mot cua so
vert=np.concatenate((cyan,magenta),axis=0)
vert1=np.concatenate((yellow,black),axis=0)
vert2=np.concatenate((vert,vert1),axis=1)

#Dieu chinh kich thuoc cua so hien thi
CMYK_program = cv2.resize(vert2,(700,700))

#Hiển thị hình dùng thư viện OpenCV
cv2.imshow('Anh goc', img)
cv2.imshow('Chuyen RGB sang CMYK', CMYK_program)

#Bấm phím bất kì để đóng cửa sổ hiển thị hình
cv2.waitKey(0)

#Giai phong bo nho
cv2.destroyAllWindows()