import numpy as np
import cv2 
import os
from tkinter import *
from tkinter import filedialog
import matplotlib.pyplot as plt
import matplotlib.lines as mlines
from matplotlib.colors import ListedColormap

def main():
    openFile(True)
def openFile(isDefault):
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
                if files[0][i].__contains__("go"):
                    train_y_trues.append(1)
                else:
                    train_y_trues.append(0)
        train_y_trues = np.array(train_y_trues)
        print(train_y_trues)
        trainData = np.array(images)
        test_y_trues = []
        images = []
        for i in range(len(files[1])):
            if files[1][i].__contains__(".png"):
                images.append(binarize(cv2.imread(files[1][i],cv2.IMREAD_GRAYSCALE)))
                if files[1][i].__contains__("go"):
                    test_y_trues.append(1)
                else:
                    test_y_trues.append(0)
        test_y_trues = np.array(test_y_trues)
        testData = np.array(images)
        linearKernelNetwork = SvmNeuralNetwork(kernel='linear', C=1000, max_iter=60, degree=3)
        linearKernelNetwork.train(trainData, train_y_trues)
        polyKernelNetwork = SvmNeuralNetwork(kernel='poly', C=1000, max_iter=60, degree=3)
        polyKernelNetwork.train(trainData, train_y_trues)
        rbfKernelNetwork = SvmNeuralNetwork(kernel='rbf', C=1000, max_iter=60, degree=3)
        rbfKernelNetwork.train(trainData, train_y_trues)
        y_pred = polyKernelNetwork.predict(testData)
        x_start = 8
        x_end = 12
        sizeX = 5
        sizeY = 5
        print("poly results:")
        print(test_y_trues)
        print(y_pred)
        poly_pred = y_pred
        losses = []
        loss = 0
        s = 0
        for i in range(len(y_pred)):
            loss += abs(y_pred[i] - test_y_trues[i])
            if y_pred[i] > 0.5 and test_y_trues[i] == 1:
                s += 1
            if y_pred[i] <= 0.5 and test_y_trues[i] == 0:
                s += 1 
        losses.append(s/len(y_pred))
        print("poly loss:" + str(loss))
        X = []
        for i in range(len(y_pred)):
            X.append([sum(testData[i]), y_pred[i]])
        X = np.array(X)
        fig, axs = plt.subplots(nrows=1)
        fig.set_size_inches(sizeX, sizeY)
        plt.xlim([x_start, x_end])
        test_plot(X, test_y_trues, SvmNeuralNetwork(kernel='poly', C=1000, max_iter=60, degree=3), axs, 'poly')
        y_pred = linearKernelNetwork.predict(testData)
        linear_pred = y_pred
        print("Linear results:")
        print(test_y_trues)
        print(y_pred)
        loss = 0
        s = 0
        for i in range(len(y_pred)):
            loss += abs(y_pred[i] - test_y_trues[i])
            if y_pred[i] > 0.5 and test_y_trues[i] == 1:
                s += 1
            if y_pred[i] <= 0.5 and test_y_trues[i] == 0:
                s += 1 
        losses.append(s/len(y_pred))
        print("linear loss:" + str(loss))
        X = []
        for i in range(len(y_pred)):
            X.append([sum(testData[i]), y_pred[i]])
        X = np.array(X)
        fig, axs = plt.subplots(nrows=1)
        fig.set_size_inches(sizeX, sizeY)
        plt.xlim([x_start, x_end])
        test_plot(X, test_y_trues, SvmNeuralNetwork(kernel='linear', C=1000, max_iter=60, degree=3), axs, 'linear')
        y_pred = rbfKernelNetwork.predict(testData)
        print("Rbf results:")
        print(test_y_trues)
        print(y_pred)
        rbf_pred = y_pred
        loss = 0
        s = 0
        for i in range(len(y_pred)):
            loss += abs(y_pred[i] - test_y_trues[i])
            if y_pred[i] > 0.5 and test_y_trues[i] == 1:
                s += 1
            if y_pred[i] <= 0.5 and test_y_trues[i] == 0:
                s += 1 
        losses.append(s/len(y_pred))
        print("rbf loss:" + str(loss))
        X = []
        for i in range(len(y_pred)):
            X.append([sum(testData[i]), y_pred[i]])
        X = np.array(X)
        fig, axs = plt.subplots(nrows=1)
        fig.set_size_inches(sizeX, sizeY)
        plt.xlim([x_start, x_end])
        test_plot(X, test_y_trues, SvmNeuralNetwork(kernel='rbf', C=1000, max_iter=60, degree=3), axs, 'rbf')
        print(files[2][0])
        f = open(files[2][0], 'w')
        resultStr = "true    poly    linear    rbf\n"
        for i in range(len(test_y_trues)):
            if test_y_trues[i] > 0.5:
                resultStr += '%-8s' % "go"
            else:
                resultStr += '%-8s' % "stop "
            if poly_pred[i] > 0.5:
                resultStr += '%-8s' % "go"
            else:
                resultStr += '%-8s' % "stop"
            if linear_pred[i] > 0.5:
                resultStr += '%-8s' % "go"
            else:
                resultStr += '%-8s' % "stop"
            if rbf_pred[i] > 0.5:
                resultStr += '%-8s' % "go"
            else:
                resultStr += '%-8s' % "stop"
            resultStr += "\n"
        f.write(resultStr)
        f.close()
        fig, axs = plt.subplots(nrows=1)
        fig.set_size_inches(sizeX, sizeY)
        axs.set_title('% of right answers ')

        axs.bar([1,2,3], losses,color = "b")
        axs.set_xticklabels(['','','poly','','linear','','rbf'])
        plt.show()
def test_plot(X, y, svm_model, axes, title):
  plt.axes(axes)
  xlim = [np.min(X[:, 0]), np.max(X[:, 0])]
  ylim = [np.min(X[:, 1]), np.max(X[:, 1])]
  xx, yy = np.meshgrid(np.linspace(*xlim, num=700), np.linspace(*ylim, num=700))
  rgb=np.array([[210, 123, 100], [159, 200, 150]])/255.0
  
  svm_model.train(X, y)
  z_model = svm_model.decision_function(np.c_[xx.ravel(), yy.ravel()]).reshape(xx.shape)
  
  plt.scatter(X[:, 0], X[:, 1], c=y, s=50, cmap='cividis')
  plt.contour(xx, yy, z_model, colors='r', levels=[-1, 0, 1], alpha=0.5, linestyles=['--', '-', '--'])
  plt.contourf(xx, yy, np.sign(z_model.reshape(xx.shape)), alpha=0.3, levels=2, cmap=ListedColormap(rgb), zorder=1)
  plt.title(title)

def nonlin(x,deriv=False):
    if(deriv==True):
        return np.exp(-x)/((1 + np.exp(-x))*(1 + np.exp(-x)))
    return 1/(1+np.exp(-x))         
class SvmNeuralNetwork:
  def __init__(self, kernel='linear', C=10, max_iter=100000, degree=3, gamma=1):
    self.kernel = {'poly'  : lambda x,y: np.dot(x, y.T)**degree,
         'rbf': lambda x,y: np.exp(-gamma*np.sum((y-x[:,np.newaxis])**2,axis=-1)),
         'linear': lambda x,y: np.dot(x, y.T)}[kernel]
    self.C = C
    self.max_iter = max_iter

  def restrict_to_square(self, t, v0, u): 
    t = (np.clip(v0 + t*u, 0, self.C) - v0)[1]/u[1]
    return (np.clip(v0 + t*u, 0, self.C) - v0)[0]/u[0]

  def train(self, X, y):
    self.X = X.copy()
    self.y = y * 2 - 1 
    self.lambdas = np.zeros_like(self.y, dtype=float)
    self.K = self.kernel(self.X, self.X) * self.y[:,np.newaxis] * self.y
    
    for _ in range(self.max_iter):
      for idxM in range(len(self.lambdas)):                                    
        idxL = 1                        
        Q = self.K[[[idxM, idxM], [idxL, idxL]], [[idxM, idxL], [idxM, idxL]]] 
        v0 = self.lambdas[[idxM, idxL]]                                        
        k0 = 1 - np.sum(self.lambdas * self.K[[idxM, idxL]], axis=1)           
        u = np.array([-self.y[idxL], self.y[idxM]])                            
        t_max = np.dot(k0, u) / (np.dot(np.dot(Q, u), u) + 1E-15) 
        self.lambdas[[idxM, idxL]] = v0 + u * self.restrict_to_square(t_max, v0, u)
    
    # найти индексы опорных векторов
    idx, = np.nonzero(self.lambdas > 1E-15) 
    # формула (1)
    self.b = np.mean((1.0-np.sum(self.K[idx]*self.lambdas, axis=1))*self.y[idx]) 
  
  def decision_function(self, X):
    return np.sum(self.kernel(X, self.X) * self.y * self.lambdas, axis=1) + self.b

  def predict(self, X): 
    # преобразование классов -1,+1 в 0,1; для лучшей совместимости с sklearn
   # return (np.sign(self.decision_function(X)) + 1) // 2
   return nonlin(self.decision_function(X)) 

def binarize(img):
    vectorImg = []
    for i in range(len(img)):
        for j in range(len(img[0])):
            if img[i][j] > 0:
                vectorImg.append(0)
            else:
                vectorImg.append(1)
    return vectorImg
main()