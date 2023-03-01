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

def ColorImageSharpening(imgPIL):
    ImageSharpened = Image.new(imgPIL.mode, imgPIL.size)
    width = ImageSharpened.size[0] 
    height = ImageSharpened.size[1]

    f = np.zeros((3,5), float)
    for x in range(1, width - 1):
        for y in range(1, height - 1):

            #R, G, B = HinhPIL.getpixel((x,y))
            f[0,0], f[1,0], f[2,0] = imgPIL.getpixel((x,y))
            f[0,1], f[1,1], f[2,1] = imgPIL.getpixel((x - 1,y))
            f[0,2], f[1,2], f[2,2] = imgPIL.getpixel((x + 1,y))
            f[0,3], f[1,3], f[2,3] = imgPIL.getpixel((x,y - 1))
            f[0,4], f[1,4], f[2,4] = imgPIL.getpixel((x,y + 1))

            g = np.zeros(3,float)
            for k in range(3):
                g[k] = f[k, 0] + 4 * f[k, 0] - f[k,1] - f[k, 2] - f[k, 3] - f[k, 4]
                if (g[k] < 0):
                    g[k] = 0
                if (g[k] > 255):
                    g[k] = 255
        
            ImageSharpened.putpixel((x,y),(np.uint8(g[2]), np.uint8(g[1]), np.uint8(g[0])))
    ImageSharpened_CV = np.array(ImageSharpened)
    return ImageSharpened_CV

img_Sharpened = ColorImageSharpening(imgPIL)
cv2.imshow('Sharpening Image', img_Sharpened)
cv2.imshow('Original Image', img)

#Bam phim bat ki de dong cua so
cv2.waitKey(0)

#Giai phong bo nho
cv2.destroyAllWindows()