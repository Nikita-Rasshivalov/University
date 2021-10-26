import numpy as np
import copy
def main():
    A = [[1.0, -3.0, -1.0],[2.0,-2.0,3.0],[3.0,-1.0,1.0]]
    b = [-11,-7,-4]
    get_LU(A,3,b)    
   # getX(L,U,b,3)
def getX(L,U,b,n):
    # (5) Perform substitutioan Ly=b
    y = [0 for i in range(n)]
    y[0] = b[0]/L[0][0]
    for i in range(1,n):
        y[i] = b[i]/float(L[i][i])
        for k in range(0,i):
            y[i] -= y[k]*L[i][k]/float(L[i][i])
	# (6) Perform substitution Ux=y
    x = [0 for i in range(n)]
    x[n-1] = y[n-1]
    for i in range(n-2,-1,-1):
        x[i] = y[i]
        for k in range(i + 1, n):
            x[i] -= x[k]*U[i][k]
    print(x)

def get_LU(A,n,b):
    L = [[0.0,0.0, 0.0],[0.0,0.0,0.0],[0.0,0.0,0.0]]
    U = [[1.0,0.0, 0.0],[0.0,1.0,0.0],[0.0,0.0,1.0]]
    for i in range(0,n):
        L[i][0] = A[i][0]
    for j in range(0,n):
        U[0][j] = A[0][j]/L[0][0]
    for i in range(0,n):
        for j in range(0,n):
            if i>=j and j>0 :
                L[i][j] = A[i][j]
                for k in range(0,j):
                    L[i][j] -= L[i][k]*U[k][j]
            elif i>0 and j>i :
                U[i][j] = A[i][j]
                for k in range(0,i):
                    U[i][j] -= L[i][k]*U[k][j]
                U[i][j] /= L[i][i]
    print(L)
    print(U)
    x = getX(L,U,b,3)
main()