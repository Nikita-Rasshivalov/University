import numpy as np
import copy


def main():
    A = [[1.0, -3.0, -1.0], [2.0, -2.0, 3.0], [3.0, -1.0, 1.0]]
    b = [-11, -7, -4]
    get_LU(A, 3, b)

def getX2(L,U,b,n):
    # получение y из формулы Ly=b
    y = [0 for i in range(n)]
    y[0] = b[0]
    for i in range(1,n):
        y[i] = b[i]
        for k in range(0,i):
            y[i] -= y[k]*L[i][k]
	# получение x из формулы Ux=y
    x = [0 for i in range(n)]
    x[n-1] = y[n-1]/float(U[n-1][n-1])
    for i in range(n-2,-1,-1):
        x[i] = y[i]
        for k in range(i+1, n):
            x[i] -= x[k]*U[i][k]
        x[i]/=U[i][i]      
    print(x)
    return x

def get_LU(A, n, b):
    L = [[1.0, 0.0, 0.0], [0.0, 1.0, 0.0], [0.0, 0.0, 1.0]]
    U = [[0.0, 0.0, 0.0], [0.0, 0.0, 0.0], [0.0, 0.0, 0.0]]
    for j in range(0, n):
        U[0][j] = A[0][j]
    for i in range(1, n):
        L[i][0] = A[i][0]/U[0][0]
    for i in range(0, n):
        for j in range(0, n):
            if i>j:
                L[i][j] = A[i][j]
                for k in range(0, j):
                    L[i][j] -= L[i][k]*U[k][j]
                L[i][j] /= U[j][j]
            elif i <= j:
                U[i][j] = A[i][j]
                for k in range(0, i):
                    U[i][j] -= L[i][k]*U[k][j]
    print(L)
    print(U)
    getX2(L, U, b, 3)
main()
