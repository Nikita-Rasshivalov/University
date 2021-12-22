import numpy as np
 
def sigmoid(x):
    # Функция активации sigmoid:: f(x) = 1 / (1 + e^(-x))
    return 1 / (1 + np.exp(-x))
 
def deriv_sigmoid(x):
    # Производная от sigmoid: f'(x) = f(x) * (1 - f(x))
    fx = sigmoid(x)
    return fx * (1 - fx)

def mse_loss(y_true, y_pred):
    # y_true и y_pred являются массивами numpy с одинаковой длиной
    return ((y_true - y_pred) ** 2).mean()
 
class OurNeuralNetwork:
    def __init__(self):
        # Вес
        #self.w1 = np.random.normal()
        #self.w2 = np.random.normal()
        #self.w3 = np.random.normal()
        #self.w4 = np.random.normal()
        #self.w5 = np.random.normal()
        #self.w6 = np.random.normal()
        #self.w7 = np.random.normal()
        #self.w8 = np.random.normal()
        #self.w9 = np.random.normal()


        self.w1 = 1
        self.w2 = 2
        self.w3 = 3
        self.w4 = -1
        self.w5 = -2
        self.w6 = -3
        self.w7 = 4
        self.w8 = -2
        self.w9 = 1
        # Смещения
        self.b = 1

    def feedforward(self, x):
        # x является массивом numpy с двумя элементами
        h1 = sigmoid(self.w1 * x[0] + self.w4 * x[1] )
        h2 = sigmoid(self.w2 * x[0] + self.w5 * x[1] )
        h3 = sigmoid(self.w3 * x[0] + self.w6 * x[1] )
        o1 = sigmoid(self.w7 * h1 + self.w8 * h2 + self.w9 * h3 + self.b )
        return o1
 
    def train(self, data, all_y_trues):
        learn_rate = 0.1
        epochs = 3000 # количество циклов во всём наборе данных
 
        for epoch in range(epochs):
            for x, y_true in zip(data, all_y_trues):
                # --- Выполняем обратную связь (нам понадобятся эти значения в дальнейшем)
                sum_h1 = self.w1 * x[0] + self.w4 * x[1] 
                h1 = sigmoid(sum_h1)
 
                sum_h2 = self.w2 * x[0] + self.w5 * x[1] 
                h2 = sigmoid(sum_h2)

                sum_h3 = self.w3 * x[0] + self.w6 * x[1] 
                h3 = sigmoid(sum_h3)
            
                sum_o1 = self.w7 * h1 + self.w8 * h2 + self.w9 * h3 + self.b
                o1 = sigmoid(sum_o1)
                y_pred = o1
 
                # --- Подсчет частных производных
                # --- Наименование: d_L_d_w1 представляет "частично L / частично w1"
                d_L_d_ypred = -2 * (y_true - y_pred)
 
                # Нейрон o1
                d_ypred_d_w7 = h1 * deriv_sigmoid(sum_o1)
                d_ypred_d_w8 = h2 * deriv_sigmoid(sum_o1)
                d_ypred_d_w9 = h3 * deriv_sigmoid(sum_o1)
                d_ypred_d_b = deriv_sigmoid(sum_o1)
 
                d_ypred_d_h1 = self.w7 * deriv_sigmoid(sum_o1)
                d_ypred_d_h2 = self.w8 * deriv_sigmoid(sum_o1)
                d_ypred_d_h3 = self.w9 * deriv_sigmoid(sum_o1)
                
                # Нейрон h1
                d_h1_d_w1 = x[0] * deriv_sigmoid(sum_h1)
                d_h1_d_w4 = x[1] * deriv_sigmoid(sum_h1)

                # Нейрон h2
                d_h2_d_w2 = x[0] * deriv_sigmoid(sum_h2)
                d_h2_d_w5 = x[1] * deriv_sigmoid(sum_h2)

                # Нейрон h3
                d_h3_d_w3 = x[0] * deriv_sigmoid(sum_h3)
                d_h3_d_w6 = x[1] * deriv_sigmoid(sum_h3)

                # --- Обновляем вес и смещения
                # Нейрон h1
                self.w1 -= learn_rate * d_L_d_ypred * d_ypred_d_h1 * d_h1_d_w1
                self.w4 -= learn_rate * d_L_d_ypred * d_ypred_d_h1 * d_h1_d_w4
 
                # Нейрон h2
                self.w2 -= learn_rate * d_L_d_ypred * d_ypred_d_h2 * d_h2_d_w2
                self.w5 -= learn_rate * d_L_d_ypred * d_ypred_d_h2 * d_h2_d_w5

                # Нейрон h3
                self.w3 -= learn_rate * d_L_d_ypred * d_ypred_d_h3 * d_h3_d_w3
                self.w6 -= learn_rate * d_L_d_ypred * d_ypred_d_h3 * d_h3_d_w6
                
 
                # Нейрон o1
                self.w7 -= learn_rate * d_L_d_ypred * d_ypred_d_w7
                self.w8 -= learn_rate * d_L_d_ypred * d_ypred_d_w8
                self.w9 -= learn_rate * d_L_d_ypred * d_ypred_d_w9
                self.b -= learn_rate * d_L_d_ypred * d_ypred_d_b
                
 
            # --- Подсчитываем общую потерю в конце каждой фазы
            if epoch % 10 == 0:
                y_preds = np.apply_along_axis(self.feedforward, 1, data)
                loss = mse_loss(all_y_trues, y_preds)
                print("Epoch %d loss: %.3f" % (epoch, loss))
        print(self.w1)
        print(self.w2)
        print(self.w3)
        print(self.w4)
        print(self.w5)
        print(self.w6)
        print(self.w7)
        print(self.w8)
        print(self.w9)
        print(self.b)
        f = open("resultGrad.txt", 'w')
        string=str(self.w1).replace('.',',')+'\n'+str(self.w2).replace('.',',')+'\n'+str(self.w3).replace('.',',')+'\n'+str(self.w4).replace('.',',')+'\n'+str(self.w5).replace('.',',')+'\n'+str(self.w6).replace('.',',')+'\n'+str(self.w7).replace('.',',')+'\n'+str(self.w8).replace('.',',')+'\n'+str(self.w9).replace('.',',')+'\n'+str(self.b).replace('.',',')+'\n'
        f.write(string)
        f.close()
 
 
data = np.array([
    [0, 1],    
    [1, 0],     
    [0, 0],     
    [1, 1], 
])
 
all_y_trues = np.array([
    0, 
    1, 
    0, 
    0, 
])
 
network = OurNeuralNetwork()
network.train(data, all_y_trues)


