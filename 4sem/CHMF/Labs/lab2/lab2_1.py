import math
n=3 # ввод колчиества решений
matrix = [] #расширенная матрица
matrix =[[1, -3, -1,-11],[2,-2,3,-7],[3,-1,1,-4]]
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
print(matrix)
print(x)

