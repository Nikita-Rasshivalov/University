import numpy as np
 
 
def sigmoid(x):
    y = 1 / (1 + np.exp(-x))
    if y > 0.5:
        return 1
    else:
        return 0

def findDeltaW(x, y):
        if x*y == 1:
            return 1
        if x==0:
            return 0
        return -1

class OurNeuralNetwork:
    def __init__(self):
        # Вес
        self.w1 = -1
        self.w2 = -1
        self.w4 = -1
        self.w5 = -2
        self.w6 = -2
        self.w3 = -2
        self.w7 = 0
        self.w8 = 0
        self.w9 = 0
        self.b =  0

    def train(self, data, all_y_trues):
        epochs = 3000 # количество циклов во всём наборе данных
 
        for epoch in range(epochs):
            for x, y_true in zip(data, all_y_trues):
                sum_h1 = self.w1 * x[0] + self.w4 * x[1] 
                h1 = sigmoid(sum_h1)
                sum_h2 = self.w2 * x[0] + self.w5 * x[1] 
                h2 = sigmoid(sum_h2)
                sum_h3 = self.w3 * x[0] + self.w6 * x[1] 
                h3 = sigmoid(sum_h3)
                sum_o1 = self.w7 * h1 + self.w8 * h2 + self.w9 * h3 + self.b
                o1 = sigmoid(sum_o1)
        
                if o1 != y_true:
                    
                    self.w1 = self.w1 + findDeltaW(x[0], y_true)
                    self.w2 = self.w2 + findDeltaW(x[0], y_true)
        
                    self.w3 = self.w3 + findDeltaW(x[0], y_true)
                    self.w4 = self.w4 + findDeltaW(x[1], y_true)

                    self.w5 = self.w5 + findDeltaW(x[1], y_true)
                    self.w6 = self.w6 + findDeltaW(x[1], y_true)

                    self.w7 = self.w7 + findDeltaW(h1, y_true)
                    self.w8 = self.w8 + findDeltaW(h2, y_true)
                    self.w9 = self.w9 + findDeltaW(h3, y_true)
                    
                    self.b = self.b + findDeltaW(h3, y_true)
            y_pred = []
            for x, y_true in zip(data, all_y_trues):
                sum_h1 = self.w1 * x[0] + self.w4 * x[1] 
                h1 = sigmoid(sum_h1)
                sum_h2 = self.w2 * x[0] + self.w5 * x[1] 
                h2 = sigmoid(sum_h2)
                sum_h3 = self.w3 * x[0] + self.w6 * x[1] 
                h3 = sigmoid(sum_h3)
                sum_o1 = self.w7 * h1 + self.w8 * h2 + self.w9 * h3 + self.b
                o1 = sigmoid(sum_o1)               
                y_pred.append(o1)
            
            
            if y_pred[0] == all_y_trues[0] and y_pred[1] == all_y_trues[1] and y_pred[2] == all_y_trues[2] and y_pred[3] == all_y_trues[3]:
                print("w1 "+str(self.w1))
                print("w2 "+str(self.w2))
                print("w3 "+str(self.w3))
                print("w4 "+str(self.w4))
                print("w5 "+str(self.w5))
                print("w6 "+str(self.w6))
                print("w7 "+str(self.w7))
                print("w8 "+str(self.w8))
                print("w9 "+str(self.w9))
                print("b "+str(self.b))
                break

# Определение набора данных
data = np.array([
    [0, 1],    
    [1, 1],     
    [1, 0],     
    [0, 0]
])
 
all_y_trues = np.array([
    1, 
    1, 
    1, 
    0
])
 
# Тренируем нашу нейронную сеть!
network = OurNeuralNetwork()
network.train(data, all_y_trues)