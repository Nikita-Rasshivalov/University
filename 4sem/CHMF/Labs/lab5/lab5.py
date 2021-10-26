import math
import matplotlib.pyplot as plt
def main():
    n = 150
    xE,yE = findEler(0,0,0.01,n)
    xM,yM = modifiedEler(0,0,0.01,n)
    xR,yR = rungeMethod(0,0,0.01,n)
    ix,iy = initial(xE,n)
    #showGraphic(ix, iy, "red", xE, yE, "blue","Эйлер")
    #showGraphic(ix, iy, "red", xM, yM, "purple","Модифицированный Эйлер")
    #showGraphic(ix, iy, "red", xR, yR, "black","Рунге-Кутты 4-ого порядка")
    plt.grid()
    plt.plot(ix, iy, "green","Исходная")
    plt.plot(xE, yE, "blue","Эйлер")
    plt.plot(xM, yM, "red","Модифицированный Эйлер")
    #plt.plot(ix,findEps(iy, yE),color="red")
    #plt.plot(ix,findEps(iy, yM),color="green")
    #plt.plot(ix,findEps(iy, yR),color="blue")
    #plt.legend(['Eler','Modified','Runge'])
    plt.show()
    #rungeRule(0,0,0.01,150)

def rungeRule(x0,y0,h,n):
    xE,yE = findEler(x0,y0,h,n)
    xE2,yE2 = findEler(x0,y0,h/2,n)
    xM,yM = modifiedEler(x0,y0,h,n)
    xM2,yM2 = modifiedEler(x0,y0,h/2,n)
    xR,yR = rungeMethod(x0,y0,h,n)
    xR2,yR2 = rungeMethod(x0,y0,h/2,n)
    ix,iy = initial(xE,n)
    y1 = dif(yE,yE2,1)
    y2 = dif(yM,yM2,2)
    y3 = dif(yR,yR2,4)
    plt.grid()
    plt.plot(ix,y1,color="red")
    plt.plot(ix,y2,color="green")
    plt.plot(ix,y3,color="blue")
    plt.legend(['Eler','Modified','Runge'])
    plt.show()

def dif(y1,y2,p):
    y = []
    for i in range(0,len(y1)):
        y.append((abs(y1[i]-y2[i]))/(pow(2,p)-1))
    return y

def findEps(iy,my):
    eps = []
    for i in range(0,len(iy)):
        eps.append(abs(iy[i]-my[i]))
    return eps

def findEler(x0,y0,h,n):
    resX = []
    resY = []
    x = x0
    y = y0
    for i in range(0,n):
        y += h*getDxy(x,y)
        x += h
        resX.append(x)
        resY.append(y)
    return resX,resY



def modifiedEler(x0,y0,h,n):
    xE,yE = findEler(x0,y0,h,n)
    resX = []
    resY = []
    x = x0
    y = y0
    for i in range(0,n):
        y += (1/2)*h*(getDxy(x,y)+getDxy(x+h,yE[i]))
        x += h
        resX.append(x)
        resY.append(y)
    return resX,resY

def rungeMethod(x0,y0,h,n):
    resX = []
    resY = []
    x = x0
    y = y0
    for i in range(0,n):
        k1 = h * getDxy(x,y)
        k2 = h * getDxy(x+h/2,y+k1/2)
        k3 = h * getDxy(x+h/2,y+k2/2)
        k4 = h * getDxy(x+h,y+k3)
        y += 1/6*(k1+2*k2+2*k3+k4)
        x += h
        resX.append(x)
        resY.append(y)
    return resX,resY

def initial(xList,n):
    y = []
    for x in xList:
        y.append(x**2/2)
    return xList,y
    
def getDxy(x,y):
    return x

def showGraphic(ix,iy,selectedColor,x,y,selectedColor2,label):
    plt.grid()
    plt.plot(ix,iy,color=selectedColor)
    plt.plot(x,y,color=selectedColor2)
    plt.legend(['Исходная функция',label])
    plt.show()
main()