import numpy as np
import cv2 
import os
from tkinter import *
from tkinter import filedialog
import matplotlib.pyplot as plt 
from threading import Thread
import random


def main():
    mainWindow = Tk() 
    mainWindow.resizable(width=False, height=False) 
    mainWindow.title("Лабораторная работа 2")
    screenWidth = mainWindow.winfo_screenwidth()
    screenHeight = mainWindow.winfo_screenheight()
    x = (screenWidth -300)/ 2
    y = (screenHeight -200)/ 2
    mainWindow.geometry('%dx%d+%d+%d' % (300, 200, x, y))  
    mainWindow.configure(background="#9580a8")
    #Создание меню
    xCoordinate = 160/2-100/2
    buttonColor="#3db3b9"
    buttonWidth="21"
    thread1 = Thread(target=createGraphic)
    thread2 = Thread(target=createGraphic)
    thread1.daemon = True
    thread2.daemon = True
    # button1 = Button(text="Выбрать файл и вычислить", command=lambda:openFile(False),bg=buttonColor,fg="white",width=buttonWidth, height="1",font=("MS Sans Serif", 14),relief="groove",activebackground="#dc3f3f")
    # button1.place(x=xCoordinate, y=90)
    button2 = Button(text="График скорости обучения", command=lambda:thread1.start(),bg=buttonColor,fg="white",width=buttonWidth, height="1",font=("MS Sans Serif", 14),relief="groove",activebackground="#edeef0")
    button2.place(x=xCoordinate, y=50)
    
    button3 = Button(text="График функции потерь", command=lambda:thread2.start(),bg=buttonColor,fg="white",width=buttonWidth, height="1",font=("MS Sans Serif", 14),relief="groove",activebackground="#edeef0")
    button3.place(x=xCoordinate, y=110)
    openFile(True)
    mainWindow.mainloop() 




def createGraphic():
    global isNew
    global hebbInfo
    global gradInfo
    global trainData
    global train_y_trues
    if isNew:
        network = GradientNeuralNetwork()
        gradInfo = []
        network.train(trainData, train_y_trues, gradInfo, 500)
        network = HebbNeuralNetwork()
        hebbInfo = []
        network.train(trainData, train_y_trues, hebbInfo, 500)
        isNew = False
    fig, ax = plt.subplots()
    ax.plot(gradInfo[0],gradInfo[1],'r', label = 'градиент')
    ax.plot(hebbInfo[0],hebbInfo[1],'b', label = 'хебб')
    ax.legend()
    plt.title('Сравнение скорости обучения методами Хебба и градиента')
    plt.show()

def createLossGraphic():
    global gradLossInfo
    global hebbLossInfo
    global isNew
    global hebbInfo
    global gradInfo
    global trainData
    global train_y_trues
    if isNew:
        network = GradientNeuralNetwork()
        gradInfo = []
        network.train(trainData, train_y_trues, gradInfo, 500)
        network = HebbNeuralNetwork()
        hebbInfo = []
        network.train(trainData, train_y_trues, hebbInfo, 500)
        isNew = False
    fig, ax = plt.subplots()
    ax.plot(gradLossInfo[0],gradLossInfo[1],'r', label = 'градиент')
    ax.plot(hebbLossInfo[0],hebbLossInfo[1],'b', label = 'хебб')
    cross = [[],[]]
    for i in range(len(gradLossInfo[0])):
        print(abs(gradLossInfo[1][i]-hebbLossInfo[1][i]))
        if abs(gradLossInfo[1][i]-hebbLossInfo[1][i]) < 0.001:
            cross[0].append(gradLossInfo[0][i])
            cross[1].append(gradLossInfo[1][i])
    plt.plot(cross[0][len(cross[0])-1], cross[1][len(cross[1])-1], 'mo',label = 'момент начала превосходства градиента')
    ax.legend()
    plt.title('Сравнение качества обучения методами Хебба и градиента')
    plt.show()


def openFile(isDefault):
    global isNew
    global trainData
    global train_y_trues
    if isDefault==True:
        path=os.path.dirname(os.path.abspath(__file__))+'\\config.txt'
    else:
        path = filedialog.askopenfilename(filetypes=(("Текстовый файл", "*.txt"),("Текстовый файл", "*.txt")))
    if path!='':
        images = []
        lines = open(path).readlines()
        files = []
        j = 0
        for i in range(3):
            files.append([])
            while  j < len(lines) and lines[j].__contains__("-----") == False:
                files[i].append(lines[j].replace("\n",""))
                j += 1
            j += 1
        train_y_trues = []
        for i in range(len(files[0])):
            if files[0][i].__contains__(".png"):
                images.append(binarize(cv2.imread(files[0][i],cv2.IMREAD_GRAYSCALE)))
                if files[0][i].__contains__("nine"):
                    train_y_trues.append(1)
                else:
                    train_y_trues.append(0)
        print(train_y_trues)            
        trainData = np.array(images)
        test_y_trues = []
        images = []
        for i in range(len(files[1])):
            if files[1][i].__contains__(".png"):
                images.append(binarize(cv2.imread(files[1][i],cv2.IMREAD_GRAYSCALE)))
                if files[1][i].__contains__("nine"):
                    test_y_trues.append(1)
                else:
                    test_y_trues.append(0)
        testData = np.array(images)
        gradNetwork = GradientNeuralNetwork()
        gradNetwork.train(trainData, train_y_trues, None, 2000)
        hebbNetwork = HebbNeuralNetwork()
        hebbNetwork.train(trainData, train_y_trues, None, 2000)
        test(gradNetwork,hebbNetwork, testData, test_y_trues, files[2][0])
        isNew = True
            
def sigmoid(x):
    return 1 / (1 + np.exp(-x))
 
 
def derivSigmoid(x):
    fx = sigmoid(x)
    return fx * (1 - fx)
 
def test(grad,hebb, images,y_true,resultPath):
    grad_y_pred = []
    for i in range(len(images)):
        hiddenNeurons = []
        for k in range(6):
            hiddenSum = 0
            for j in range(k*6,(k+1)*6):
                hiddenSum += grad.firstWeights[j] * images[i][j]
            hiddenNeurons.append(sigmoid(hiddenSum))
        sumOutput = 0
        for z in range(6):
            sumOutput += grad.secondWeights[z] * hiddenNeurons[z]
        output = sigmoid(sumOutput)
        grad_y_pred.append(output)
    hebb_y_pred = []
    for i in range(len(images)):
        hiddenNeurons = []
        for k in range(6):
            hiddenSum = 0
            for j in range(k*6,(k+1)*6):
                hiddenSum += hebb.firstWeights[j] * images[i][j]
            hiddenNeurons.append(hebbSigmoid(hiddenSum))
        sumOutput = 0
        for z in range(6):
            sumOutput += hebb.secondWeights[z] * hiddenNeurons[z]
        output = hebbSigmoid(sumOutput)
        hebb_y_pred.append(output)
    f = open(resultPath, 'w')
    resultStr = "true    grad    hebb\n"
    for i in range(len(y_true)):
        if y_true[i] == 1:
            resultStr += '%-8s' % "nine"
        else:
            resultStr += '%-8s' % "zero "
        if round(grad_y_pred[i]) == 1:
            resultStr += '%-8s' % "nine"
        else:
            resultStr += '%-8s' % "zero"
        if hebb_y_pred[i] == 1:
            resultStr += '%-8s' % "nine"
        else:
            resultStr += '%-8s' % "zero"
        resultStr += "\n"
    f.write(resultStr)
    f.close()

def mseLoss(y_true, y_pred):
    sum = 0.0
    for i in range(len(y_true)):
        sum += (y_true[i] - y_pred[i]) ** 2
    return sum/len(y_true)

class GradientNeuralNetwork:
    def __init__(self):
        self.firstWeights = [np.random.normal()]*36
        self.secondWeights = [np.random.normal()]*6

    def feedforward(self, x):
        hiddenNeurons = []
        for i in range(6):
            hiddenSum = 0
            for j in range(i*6,(i+1)*6):
                hiddenSum += self.firstWeights[j] * x[j]
            hiddenNeurons.append(sigmoid(hiddenSum))
        sumOutput = 0
        for i in range(6):
            sumOutput += self.secondWeights[i] * hiddenNeurons[i]
        output = sigmoid(sumOutput)
        return output
 
    def train(self, data, all_y_trues, infoData, epochs):
        global gradLossInfo
        gradLossInfo = []
        gradLossInfo.append([])
        gradLossInfo.append([])
        learn_rate = 0.1
        if infoData != None:
            infoData.append([])
            infoData.append([])
        for epoch in range(epochs):
            for x, y_true in zip(data, all_y_trues):
                sumHiddens = []
                sumSumHiddens = []
                for i in range(6):
                    sumH = 0
                    for j in range(i*6,(i+1)*6):
                        sumH += self.firstWeights[j]*x[j]
                    sumSumHiddens.append(sumH)
                    sumHiddens.append(sigmoid(sumH))
                sumO = 0
                for i in range(6):
                    sumO += self.secondWeights[i]*sumHiddens[i]
                o1 = sigmoid(sumO)
                y_pred = o1
                d_L_d_ypred = -2 * (y_true - y_pred)
                d_ypred_d_wH = []
                for i in range(6):
                    d_ypred_d_wH.append(sumHiddens[i]*derivSigmoid(sumO))
                d_ypred_d_hH = []
                for i in range(6):
                    d_ypred_d_hH.append(self.secondWeights[i]*derivSigmoid(sumO))

                d_h1_d_w = []
                currentH = 0
                for i in range(36):
                    d_h1_d_w.append(x[i]*derivSigmoid(sumSumHiddens[currentH]))
                    if i !=0 and i%6==0:
                        currentH += 1
                # Обновление весов
                currentH = 0
                for i in range(36):
                    self.firstWeights[i] -= learn_rate * d_L_d_ypred * d_h1_d_w[i] * d_ypred_d_hH[currentH]
                    if i !=0 and i%6==0:
                        currentH += 1
                for i in range(6):
                    self.secondWeights[i] -= learn_rate * d_L_d_ypred * d_ypred_d_wH[i]
            y_pred = []
            for x, y_true in zip(data, all_y_trues):
                y_pred.append(round(self.feedforward(x)))
            correctCount = 0
            for i in range(len(y_pred)):
                if y_pred[i] == all_y_trues[i]:
                    correctCount+=1
            if infoData != None:
                infoData[0].append(epoch)
                infoData[1].append(correctCount/len(y_pred))
            
            y_preds = np.apply_along_axis(self.feedforward, 1, data)
            loss = mseLoss(all_y_trues, y_preds)
            gradLossInfo[0].append(epoch)
            gradLossInfo[1].append(loss)
            print("Epoch %d loss: %.3f" % (epoch, loss))

def hebbSigmoid(x):
    y = 1 / (1 + np.exp(-x))
    if y > 0.5:
        return 1
    else:
        return 0

def findDeltaW(x, y):
        if x*y != 0:
            return random.random()
        if x==0:
            return 0
        return -1*random.random()

class HebbNeuralNetwork:
    def __init__(self):
        self.firstWeights = [0.0]*36
        self.secondWeights = [0.0]*6
    
    def feedforward(self, x, isSigmoid):
        hiddenNeurons = []
        for i in range(6):
            hiddenSum = 0
            for j in range(i*6,(i+1)*6):
                hiddenSum += self.firstWeights[j] * x[j]
            hiddenNeurons.append(hebbSigmoid(hiddenSum))
        sumOutput = 0
        for i in range(6):
            sumOutput += self.secondWeights[i] * hiddenNeurons[i]
        if isSigmoid:
            return hebbSigmoid(sumOutput)
        else:
            return 1 / (1 + np.exp(-sumOutput))

    def train(self, data, all_y_trues, infoData, epochs):
        global hebbLossInfo
        hebbLossInfo = []
        hebbLossInfo.append([])
        hebbLossInfo.append([])
        if infoData != None:
            infoData.append([])
            infoData.append([])
        for epoch in range(epochs):
            isEnd = False
            eps = 1
            for x, y_true in zip(data, all_y_trues):
                hiddenNeurons = []
                for i in range(6):
                    hiddenSum = 0
                    for j in range(i*6,(i+1)*6):
                        hiddenSum += self.firstWeights[j] * x[j]
                    hiddenNeurons.append(hebbSigmoid(hiddenSum))
                sumOutput = 0
                for i in range(6):
                    sumOutput += self.secondWeights[i] * hiddenNeurons[i]
                output = hebbSigmoid(sumOutput)
                if output != y_true:
                    for i in range(len(self.firstWeights)):
                        self.firstWeights[i] += findDeltaW(x[i], y_true)
                    for i in range(len(self.secondWeights)):
                        self.secondWeights[i] += findDeltaW(hiddenNeurons[i], y_true)
                y_pred = []
                for x, y_true in zip(data, all_y_trues):
                    y_pred.append(self.feedforward(x, True))
                correctCount = 0
                for i in range(len(y_pred)):
                    if y_pred[i] == all_y_trues[i]:
                        correctCount+=1
                if correctCount/len(y_pred) >= eps and infoData == None:
                    isEnd = True
                    break
                if epoch % 100 == 0 and infoData == None:
                    eps -= 0.01
            y_pred = []
            for x, y_true in zip(data, all_y_trues):
                y_pred.append(self.feedforward(x, True))
            y_loss = []
            for x, y_true in zip(data, all_y_trues):
                y_loss.append(self.feedforward(x, False))
            correctCount = 0
            for i in range(len(y_pred)):
                if y_pred[i] == all_y_trues[i]:
                    correctCount+=1        
            if infoData != None:
                infoData[0].append(epoch)
                infoData[1].append(correctCount/len(y_pred))
                hebbLossInfo[0].append(epoch)
                hebbLossInfo[1].append(mseLoss(all_y_trues, y_loss))
            if epoch % 100 == 0:
                print("ep:"+str(epoch))
            if isEnd == True:
                print("epochs number:"+str(epoch))
                break
            
def binarize(img):
    vectorImg = []
    for i in range(len(img)):
        for j in range(len(img[0])):
            if img[i][j] > 0:
                vectorImg.append(0)
            else:
                vectorImg.append(1)
    return vectorImg


gradLossInfo = []
hebbLossInfo = []
hebbInfo = []
gradInfo = []
isNew = True
trainData = []
train_y_trues = []
main()