import numpy as np
matrix = []
n = 0
Y = []
E = 0.0
def vvodSort():
    global n
    global Y
    global matrix #расширенная матрица
    global E
    E = float(input("Точность ")) # ввод точности
    n = 3 # ввод количества решений
    matrix =[[2, -1, 3, -4],[ 1, 3, -1, 11],[1, -2, 2, -7]]
    i = 0
    max = 0
    Y = list(range(n))
    while i<n:
        j = 0
        while j<n:
            if abs(matrix[j][i])>abs(matrix[i][i]):
                max = j
            j += 1
        k = 0
        while k<n:
            Y[k] = matrix[i][k]
            k += 1
        temp=matrix[i][n]
        matrix[i][n]=matrix[max][n]
        matrix[max][n]=temp
        k = 0
        while k < n:
            matrix[i][k]=matrix[max][k]
            matrix[max][k]=Y[k]
            k += 1
        i += 1
def check(): # функция проверки подходит ли метод
    global n
    global matrix
    z = 0
    i = 0
    while i<n:
        sum = 0 
        j = 0
        while j<n:
            sum=sum+abs(matrix[i][j])
            j += 1
        if abs(matrix[i][i])<abs(sum-abs(matrix[i][i])):
            z = 1
            break
        i += 1
    if z == 0:
        z = 1
    return z
def zegeli(): #функция вычисления корней
    global matrix
    global n
    global E
    global Y
    C = []
    for i in range(0,n): #заполнение расширенной матрицы
        C.append([])
        for j in range(0,n+1):
            C[i].append(0)
    X = list(range(n)) # корни
    D = list(range(n)) # свободные члены деленные на диоганальные коэффиценты
    i = 0
    while i<n:
        Y[i]=X[i]=matrix[i][n]/matrix[i][i]
        D[i]=matrix[i][n]/matrix[i][i]
        i += 1
    i = 0
    while i<n:
        j = 0
        while j<n:
            if i==j:
                C[i][j]=0
            else:
                C[i][j]=matrix[i][j]/(-matrix[i][i])
            j += 1
        i += 1
    z = 0 #количество точных корней
    while z!=n:
        i = 0
        while i<n:#сохранение предыдущей итерации
            Y[i]=X[i]
            i += 1
        t=0
        i = 0
        while i<n:
            j = 0
            while j<n:
                t=t+C[i][j]*X[j]
                X[i]=t+D[i]
                j += 1
            t=0
            i += 1
        z = 0
        i = 0
        while i<n: #проверка на точность вычисленных корней
            if X[i]-Y[i]<E:
                z += 1
            i += 1
    print(matrix)
    print(X)
vvodSort()
if check():
    zegeli()
else:
    print("Не подходит")