def main():
    n=3 # ввод количества решений
    eps=float(input("Точность ")) # ввод точности
    matA = [] #матрица
    matB = [] #свободные коэффиценты
    matA = [[7,-5,1],
            [4,-8,1],
            [7,-5,7]]
    matB = [-19,-29,-18]
    if check(matA):
        iteration(matA,matB,eps,n)
    else:
        print("Матрица не удовлетворяет условию сходимости")
def check(matA): #проверка выполняется ли условие сходимости
    isGood = True
    i = 0
    for line in matA:
        j = 0
        for elem in line:
            sum = 0
            if j!= i:
                sum += elem
            j += 1
        if abs(matA[i][i]) < abs(sum):
            isGood = False
        i += 1
    return isGood

def iteration(matA,matB,eps,n):
    res = [] #массив корней(предыдущая итерация)
    i = 0
    while i<n:
        res.append(matB[i]/matA[i][i])
        i += 1
    current = [] #текущая итерация
    for i in range(0,n):
        current.append(0)
    while(True):
        i = 0
        while i < n: #выполнение итерационных формул,для приближения
            current[i] = matB[i] / matA[i][i]            
            j = 0
            while j < n:
                if i != j:
                    current[i] -= matA[i][j] / matA[i][i] * res[j]
                j += 1
            i += 1
        flag = True #флаг,достигнута ли точность
        i = 0
        while i < n-1: #проверка точности
            if abs(current[i] - res[i]) > eps:
                flag = False
                break
            i += 1
        i = 0
        while i < n: #сохранение последней итерации
            res[i] = current[i]
            i += 1
        if flag == True:
            break
    print(res)
main()