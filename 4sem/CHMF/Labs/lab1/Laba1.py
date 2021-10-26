import math
import matplotlib.pyplot as plt
import numpy as np
import os

#Метод половинного деления (дихoтомии).
def dichotomyMethod():
    a=float(input("Введите a "))
    b=float(input("Введите b "))
    e=float(input("Введите точность "))
    fa=func(a)
    while (b-a)>=2*e:
        c=(a+b)/2
        fc=func(c)
        if fa*fc>0:
            a=c
        else:
            b=c
    c=(a+b)/2
    print("Метод половин: "+str(c))
    y = func(c)
    print(str(y))

#Метод хорд (пропорциональных частей). 
def chordMethod():
    a=float(input("Введите a "))
    b=float(input("Введите b "))
    e=float(input("Введите точность "))
    fa=func(a)
    while (abs(b-a))>e:
        c=a-(b-a)/(func(b)-func(a))*func(a)
        fc=func(c)
        if fa*fc>0:
            a=c
        else:
            b=c
    c=(a+b)/2 
    print("Метод хорд: "+str(c))
    y = func(c)
    print(str(y))

#Метод касательных (метод Ньютона). 
def newtonMethod():
    x=float(input("Введите начальное приближение x "))
    e=float(input("Введите точность "))
    c=x
    while True: 
        x=c-func(c)/proizFunc(c)
        if(abs(x-c))<=e: 
            break
        else:
            c=x      
    print("Метод Ньютона: "+str(c))
    y = func(c)
    print(str(y))

#Метод простой итерации (последовательных приближений). 
def iterationMethod():
    x=float(input("Введите начальное приближение x "))
    e=float(input("Введите точность "))
    c=x
    while True:
        x=secondFunc(c)
        if(math.fabs(x-c))<e:
            break
        c+=0.000001 
    print("Метод итераций: "+str(x))
    y = func(x)
    print(str(y))




 


#Вычисляет значение функции при заданном x
def func(x):
    return 3*pow(x,3)+16*pow(x,2)-17*(math.cos(x)/math.sin(x))-100

#Вычисляет значение преоразованной для метода простых итераций функции при заданном x
def secondFunc(x):
    return 3*pow(x,3)+16*pow(x,2)-17*(math.cos(x)/math.sin(x))-100+x
 
#Вычисляет значение производной функции для метода Ньютона при заданном x
def proizFunc(x):
    return 9*x**2+32*x+(17/math.sin(x)*math.sin(x))

def showGraphic():
    xmin=0.25
    xmax=3
    xlist=np.linspace(xmin,xmax,100)
    ylist=[func(x) for x in xlist]
    plt.grid()
    plt.plot(xlist,ylist)
    plt.show()


def menu():
    print("1.Метод половинного деления (дихoтомии).")
    print("2.Метод хорд (пропорциональных частей).")
    print("3.Метод касательных (метод Ньютона). ")
    print("4.Метод простой итерации (последовательных приближений). ")
    print("5.Построить график")
    print("6.Проверить подсчет")
    print("7.Выход")
    choice = int(input("Выберите: "))
    return choice

def main():
    choice = 0
    while choice != 7:
        choice = menu()
        if choice == 1:
            dichotomyMethod()
        if choice == 2:
            chordMethod()
        if choice == 3:
            newtonMethod()
        if choice == 4:
            iterationMethod()
        if choice == 5:
            showGraphic()
        if choice == 6:
            print("Метод половинного деления (дихoтомии).")
            print("Метод хорд (пропорциональных частей).")
            print("Метод касательных (метод Ньютона). ")
            print("Метод простой итерации (последовательных приближений). ")
            dichotomyMethod()
            chordMethod()
            newtonMethod()
            iterationMethod()
            
main()



        


    
