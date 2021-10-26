import math
n=3 # ввод колчиества решений
m = n+1
matA = [] #матрица
matB = [] #свободные коэффиценты
matA = [[5, -11, 0],[2,-2,3],[0,8,4]]
matB = [-40,-7,29]
y = matA[0][0] #элемент из главной диоганали
a = [] #значения получаемые из формулы -с/y где c элемент побочной диоганали
B = [] #значения получаемы из формулы d/y где d элемент свободный коэффицент
matRes = [] # массив результатов
for i in range(0,n):
    a.append(0)
    B.append(0)
    matRes.append(0)
N1 = n - 1  #n-1, т.к. по-особому обрабатывается последняя строка матрицы
a[0] = -matA[0][1] / y
B[0] = matB[0] / y
i = 1
while i < N1:  
    y = matA[i][i] + matA[i][i - 1] * a[i - 1]
    a[i] = -matA[i][i + 1] / y
    B[i] = (matB[i] - matA[i][i - 1] * B[i - 1]) / y
    i += 1
matRes[N1] = (matB[N1] - matA[N1][N1 - 1] * B[N1 - 1]) / (matA[N1][N1] + matA[N1][N1 - 1] * a[N1 - 1])
i = N1 -1
while i >= 0:
    matRes[i] = a[i] * matRes[i + 1] + B[i]
    i -= 1
print(matA)
print(matB)
print((matRes))