import matplotlib.pyplot as plt
import numpy as np
def main():
    x = [320, 360, 410, 460, 510]
    y = [280, 310, 330, 360, 380]
    x2,y2 = methodCanonic(x,y)
    print("Canonic"+str(y2)+"\n")
    showGraphic(x,y,"red",x2,y2,"purple",'Интерполяция каноническим полиномом')
    x2,y2 = methodLagrange(x,y)
    print("Langarnge"+str(y2)+"\n")
    showGraphic(x,y,"red",x2,y2,"blue",'Интерполяция полиномом Лагранжа')
    x2,y2 = methodNewton(x,y)
    print("Newton"+str(y2)+"\n")
    showGraphic(x,y,"red",x2,y2,"green",'Интерполяция полиномом Ньютона')
    x2,y2 = methodSmallestQuadrates(x,y)
    showGraphic(x,y,"red",x2,y2,"grey",'МНК')
#Находит массив значений в точке t от x0 до xn
def methodLagrange(x,y):
    t = x[0]
    xres = [] #все значения t
    yres = [] #все найденные f(t)
    while t < x[len(x)-1]:
        yres.append(lagrange(t,x,y))
        xres.append(t)
        t += 10
    return xres,yres
#метод полинома Лагранжа
def lagrange(t,x,y):
    n = len(x) #получение количества исходных значений
    sum = 0 #значение функции в точке t
    for i in range(0,n):
        l = 1 # функция L(t)
        for j in range(0,n):
            if j != i:
                l = l * (t - x[j])/(x[i] - x[j]) #нахождение значения L(t)
        sum += y[i]*l
    return sum

#Находит массив значений в точке t от x0 до xn
def methodCanonic(x,y):
    t = x[0]
    xres = []
    yres = []
    while t < x[len(x)-1]:
        yres.append(canonic(t,x,y))
        xres.append(t)
        t += 10
    return xres,yres
#метод канонического полинома
def canonic(t,x,y):
    n = len(x) #получение количества исходных значений
    m = n+1 #размер расширенной матрицы
    matrix = []
    for i in range(0,n): #заполнение матрицы
        matrix.append([])
        for j in range(0,n):
            matrix[i].append(pow(x[i],j))
    for i in range(0,n): #заполнение свободных членов матрицы
        matrix[i].append(y[i])
    A = gausseSLAU(matrix) #решение СЛАУ
    print("Polinom: "+ str(A[0]) + "x +" + str(A[1]) + "x^2 " + str(A[2]) + "x^3 +" + str(A[3]) + "x^4 " + str(A[4]) + "x^5 " )
  
    sum = 0 #значение функции в точке t
    for i in range(0,n): #получение значения sum
        sum += A[i] * pow(t,i)
    return sum
#решает слау методом Гаусса
def gausseSLAU(matrix):
    n = len(matrix)
    x = list(range(n))
    i = 0
    while i<n: #прямой ход (приведение матрицы к треугольному виду)
        tmp = matrix[i][i]
        j = n
        while j >= i:
            matrix[i][j] /= tmp #расчет коэффицента для k-ой строки
            j -= 1
        j = i+1
        while j<n:
            tmp = matrix[j][i]
            k = n
            while k >= i:
                matrix[j][k] -= tmp*matrix[i][k]
                k -= 1
            j += 1
        i += 1
    x[n - 1] = matrix[n - 1][n] #последний корень уже есть,с помощью его по очереди находятся остальные
    i = n-2 #начало с предпоследней строки
    while i >= 0: #обратный ход
        x[i] = matrix[i][n]
        j = i+1
        while j<n:
            x[i] -= matrix[i][j] * x[j]
            j += 1
        i -= 1
    return x
def methodSmallestQuadrates(x,y):
    n = len(x)
    xsum = 0 #сумма значений x
    ysum = 0 #сумма значений y
    for i in range(0,len(x)):
        xsum += x[i]
        ysum += y[i]
    x2sum = 0 #сумма квадратов x
    xysum = 0 #сумма произведений x,y
    for i in range(0,len(x)):
        x2sum += x[i]*x[i]
        xysum += x[i]*y[i]
    a1 = (n*xysum - xsum*ysum)/(n*x2sum-xsum*xsum) #коэффициент a1 функции y = a0 +a1*x
    a0 = ysum/n - a1*xsum/n #коэффициент a0 функции y = a0 +a1*x
    ost = 0.0
    for i in range(0,n):
        ost+= (y[i]-a1*x[i]-a0)**2
    print("Ost:" + str(ost))    
    print("a0:"+str(a0))
    print("a1:"+str(a1))
    xres = []
    yres = []
    t = x[0]
    while t < x[len(x)-1]: #получение значений с помощью полученных коэффициентов
        yres.append(a1*t + a0)
        xres.append(t)
        t += 0.1
    return xres,yres
#Находит массив значений в точке t от x0 до xn
def methodNewton(x,y):
    t = x[0]
    xres = []
    yres = []
    while t < x[len(x)-1]:
        yres.append(newton(t,x,y))
        xres.append(t)
        t += 10
    return xres,yres
def newton(t,x,y):
    n = len(x)
    C = [] #создание матрицы
    for i in range(0,n):
        C.append([])
        C[i].append(y[i]) #заполнение первого столбца значениями y
        for j in range(0,n):
            C[i].append(0)
    for j in range(1,n): #получение значений матрицы
        for i in range(1,n):
            if i<j:
                C[i][j] = 0
            else:
                C[i][j] = (C[i][j-1]-C[j-1][j-1])/(x[i]-x[j-1])
    A = [] #диоганальные коэффициенты полученной матрицы
    for i in range(0,n):
        A.append(C[i][i])
    sum = A[n-1] #значение функции в точке t
    for i in range(n-2,-1,-1):
        sum = sum*(t-x[i]) + A[i]
    return sum
#Строит график исходной и найденной функции
def showGraphic(x,y,selectedColor,x2,y2,selectedColor2,label):
    plt.grid()
    plt.plot(x,y,color=selectedColor)
    plt.plot(x2,y2,color=selectedColor2)
    plt.legend(['Исходная функция',label])
    plt.show()
main()