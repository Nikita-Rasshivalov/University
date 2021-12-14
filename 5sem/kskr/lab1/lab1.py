from tkinter import *
import math
import matplotlib.pyplot as plt 
import numpy as np
import xlsxwriter
def main():
    mainWindow = Tk() 
    mainWindow.resizable(width=False, height=False) 
    mainWindow.title("Лабораторная работа 1")
    screenWidth = mainWindow.winfo_screenwidth()
    screenHeight = mainWindow.winfo_screenheight()
    Lmin = StringVar(mainWindow) #минимальная длина части стержня
    LminBox = Entry(mainWindow,textvariable=Lmin)
    Lminlabel = Label(mainWindow, text="Минимальная длина части стержня:" , bg="#3db3b9")
    LminBox.insert(END, "0.1")
    Tmax = StringVar(mainWindow) #плотность теплового потока
    TmaxBox = Entry(mainWindow,textvariable=Tmax)
    Tmaxlabel = Label(mainWindow, text="Максимальная температура на левом конце:" , bg="#3db3b9")
    TmaxBox.insert(END, "100")
    Te = StringVar(mainWindow) #температура внешней среды
    TeBox = Entry(mainWindow,textvariable=Te)
    Telabel = Label(mainWindow, text="Температура внешней среды:" , bg="#3db3b9")
    TeBox.insert(END, "10")
    T0 = StringVar(mainWindow) #начальная температура
    T0Box = Entry(mainWindow,textvariable=T0)
    T0Label = Label(mainWindow, text="Начальная температура:" , bg="#3db3b9")
    T0Box.insert(END, "20")
    calcButton = Button(mainWindow,text="Вычислить", command=lambda:calc(int(N),float(tEnd),float(L),float(q),float(a),float(Te.get()),float(T0.get()),lLambda,mLambda,lRo,mRo,lC,mC,float(Lmin.get()),float(Tmax.get())),bg="red",fg="white",width="12", height="2",font=("MS Sans Serif", 10))
    LminBox.place(x=260, y=90)
    Lminlabel.place(x=50, y=90)
    TmaxBox.place(x=260, y=120)
    Tmaxlabel.place(x=0, y=120)
    TeBox.place(x=260, y=140)
    Telabel.place(x=90, y=140)
    T0Box.place(x=260, y=170)
    T0Label.place(x=105, y=170)
    calcButton.place(x=270, y=200)
    N = 5 #количество разбиений одной части
    tEnd = 30 #окончание по времени
    q = -100000 #плотность теплового потока
    a = 5000 #коэффицент теплообмена
    L = 0.3 #начальный размер стержня
    lLambda = 236 #коэффицент теплопроводности алюминия
    mLambda = 380 #коэффицент теплопроводности меди
    lRo = 2700 #плотность алюминия
    mRo = 8930 #плотность меди
    lC = 904 #теплоемкость алюминия
    mC = 400 #теплоемкость меди
    x = (screenWidth -600)/ 2
    y = (screenHeight -500)/ 2
    mainWindow.geometry('%dx%d+%d+%d' % (600, 300, x, y))  
    mainWindow.configure(background="#3db3b9")
    mainWindow.mainloop()
    
def calc(N,tEnd,L,q,a,Te,T0,lLambda,mLambda,lRo,mRo,lC,mC,Lmin,Tmax):
    isSuit = False
    while isSuit == False:
        h = (Lmin*3)/(N*3)
        t = tEnd/600.0
        T = []
        lA = lLambda/(lRo*lC)
        mA = mLambda/(mRo*mC)
        amount = N*3
        T.append([T0]*amount) #начальный слой
        T[0][0] = (a*Te+mLambda/h*T[0][1])/(a+mLambda/h) #граничное условие 3 рода
        T[0][amount-1]=T[0][amount-2]-q*h/mLambda #граничное условие 2 рода
        Yl = lA*t/h**2
        Ym = mA*t/h**2
        currentTime = t
        while currentTime < tEnd:
            T.append([T0]*amount)
            n = len(T)-1
            for i in range(1,N):
                T[n][i] = Ym * T[n-1][i-1] + (1 - 2*Ym)* T[n-1][i] + Ym * T[n-1][i+1] + t
            for i in range(N,N*2):
                T[n][i] = Yl * T[n-1][i-1] + (1 - 2*Yl)* T[n-1][i] + Yl * T[n-1][i+1] + t
            for i in range(N*2,amount-1):
                T[n][i] = Ym * T[n-1][i-1] + (1 - 2*Ym)* T[n-1][i] + Ym * T[n-1][i+1] + t
            T[n][0] = (a*Te+mLambda/h*T[n][1])/(a+mLambda/h)
            T[n][amount-1] = T[n][amount-2]-q*h/mLambda
            
           
            if n > 100 and math.fabs(T[n][4] - T[n][5]) < 0.0001  and math.fabs(T[n][9] - T[n][10]) < 0.0001:
                print("fabs "+str(T[n][4] - T[n][5])+" fabs2 "+str(T[n][9] - T[n][10]))
                isSuit = True
                break
            currentTime += t
        for i in range(len(T)):
            for j in range(len(T[0])):
                T[i][j] = round(T[i][j],3)
        if isSuit:
            print("Подходящие размеры частей:"+str(Lmin))
            f = open("results.txt", 'w')
            resultStr = "Подходящие размеры частей:"+str(round(Lmin,2))+"\n"
            resultStr += "Матрица температур:\n"
            workbook = xlsxwriter.Workbook('results.xlsx')
            worksheet = workbook.add_worksheet()
            worksheet.write(0, 1,"Подходящие размеры частей:"+str(round(Lmin,2)))
            for i in range(len(T)):
                for j in range(len(T[0])):
                    resultStr += '%-8s' % str(T[i][j])
                    worksheet.write(i+1, j,T[i][j])
                resultStr += "\n"
            f.write(resultStr)
            f.close()
            workbook.close()
            create2DGraphic(T,N*3,h)
            create3DGraphic(T)
            isSuit = True
        else:
            Lmin += 0.01


            
def create2DGraphic(T, N, h):
    lengths = []
    for i in range(N):
        lengths.append(h*i)
    fig, ax = plt.subplots()
    for i in range(len(T)):
        ax.plot(lengths,T[i],'r')
    ax.set_xlabel('длина')
    ax.set_ylabel('температура')
    plt.title('2D-график временных слоев')
    plt.show()
def create3DGraphic(T):
    fig, ax = plt.subplots()
    plt.imshow(T, cmap='viridis')
    plt.colorbar()
    plt.title('3D-график температур')
    ax.set_xlabel('номер разбиения')
    ax.set_ylabel('номер слоя времени')
    plt.show()
main()