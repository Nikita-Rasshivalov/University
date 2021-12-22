# Подключение библиотек
import numpy as np
import matplotlib.pyplot as plt

import NeuralNetwork as nn


def deserialize(filename):
    with open(filename, "r") as file: 
        rows = file.read().split()
        data = []
        for i, k in enumerate(rows[:-1]):
            data.append(float(k))
    return data

class RBFNet:

    def __init__(self, hidden_number, sigma=1.0):
        self.hidden_number = hidden_number
        self.sigma = sigma
        self.centers = 0
        self.weights = 0

    def rbf(self, point, center):
        return np.exp(np.linalg.norm((point - center) ** 2 / (2 * self.sigma ** 2)))

    def calculate_interpolation_matrix(self, x):

        g = np.zeros((len(x), self.hidden_number))
        for i, point in enumerate(x):
            for j, center in enumerate(self.centers):
                g[i, j] = self.rbf(point, center)
        return g

    def fit(self, x, y):
        self.centers = x[np.random.choice(len(x), self.hidden_number)]
        g = self.calculate_interpolation_matrix(x)
        inv_g = np.linalg.pinv(g)
        self.weights = inv_g @ y

    def predict(self, x):
        g = self.calculate_interpolation_matrix(x)
        return g @ self.weights


class RNN:
    def __init__(self, input_number, hidden_number, output_number, lr=0.01):
        self.lr = lr
        self.w_ih = np.random.uniform(-np.sqrt(1 / input_number), np.sqrt(1 / input_number), 
            size=[input_number, hidden_number])
        self.b_ih = np.random.uniform(size=[1, hidden_number])

        self.w_hh = np.random.uniform(-np.sqrt(1 / hidden_number), np.sqrt(1 / hidden_number), 
            size=[hidden_number, hidden_number])
        self.b_hh = np.random.uniform(size=[1, hidden_number])

        self.h = np.zeros(shape=[1, hidden_number])
        self.h_t_1 = np.zeros(shape=[1, hidden_number])

        self.w = np.random.uniform(-np.sqrt(1 / hidden_number), np.sqrt(1 / hidden_number), 
            size=[hidden_number, output_number])
        
    def forward_prop(self, x):
        self.x = x
        self.h_t_1 = self.h
        self.h = self.x @ self.w_ih + self.b_ih + self.h_t_1 @ self.w_hh + self.b_hh
        self.h = np.tanh(self.h)
        self.out = self.h @ self.w
        return self.out
    
    def __call__(self, *args):
        return self.forward_prop(*args)
    
    def backward_prop(self, y):
        dloss = self.out - y
        self.dw = self.h.T @ dloss
        dh = dloss @ self.w.T
        grad = (1 - np.tanh(self.h) ** 2) * dh
        self.dw_ih = self.x.T @ grad
        self.db_ih = 1 * grad
        self.dw_hh = self.h_t_1.T @ grad
        self.db_hh = 1 * grad
        
    def update_weights(self):
        self.w -= self.lr * self.dw
        self.w_ih -= self.lr * self.dw_ih
        self.b_ih -= self.lr * self.db_ih
        self.w_hh -= self.lr * self.dw_hh
        self.b_hh -= self.lr * self.db_hh


def draw_menu():
    print("Меню")
    print("1. Задание 1. Обучение сетей")
    print("2. Задание 1. График аппроксимации")
    print("3. Задание 1. Сравнение сетей с разным кол-вом нейронов на скрытом слое")
    print("4. Задание 2. Обучение сетей")
    print("5. Задание 2. График курса евро к доллару")
    print("6. Задание 2. График предсказаний на январь")
    print("7. Выйти")


def menu_item_1(fx, rbf, perceptron_1, perceptron_2, perceptron_1x2, perceptron_1x3):
    x = np.random.uniform(-4.5, 4.5, size=[75000, 1])
    y = fx(x)
    rbf.sigma = np.std(y)
    rbf.fit(x, y)
    for i in range(75000):
        xi = x[i].reshape(1, 1)
        yi = y[i].reshape(1, 1)
        perceptron_1.forward_prop(xi)
        perceptron_1.backward_prop(yi)
        perceptron_1.update_weights()
        perceptron_2.forward_prop(xi)
        perceptron_2.backward_prop(yi)
        perceptron_2.update_weights()
        perceptron_1x2.forward_prop(xi)
        perceptron_1x2.backward_prop(yi)
        perceptron_1x2.update_weights()
        perceptron_1x3.forward_prop(xi)
        perceptron_1x3.backward_prop(yi)
        perceptron_1x3.update_weights()
    return True

def menu_item_2(fx, rbf, perceptron_1, perceptron_2):
    points = np.linspace(-4.5, 4.5, 400)
    p1 = []
    p2 = []
    for i in points:
        p1.append(perceptron_1(i.reshape(1, 1)).reshape(-1))
        p2.append(perceptron_2(i.reshape(1, 1)).reshape(-1))
    plt.plot(points, rbf.predict(points), "g", label="Радиально-базисная сеть")
    plt.plot(points, p1, "r", label="Персептрон с 1 скрытым слоем")
    plt.plot(points, p2, "b", label="Персептрон с 2 скрытыми слоями")
    plt.plot(points, fx(points), "y", label="График функции")
    plt.xlabel("x")
    plt.ylabel("y")
    plt.title(r"Аппроксимация функции $f(x) = {2^{x√e^{(\sin(x)\cos(x))}}}$")
    plt.legend()
    plt.grid()
    plt.show()

def menu_item_3(fx, perceptron_1, perceptron_1x2, perceptron_1x3):
    points = np.linspace(-4.5, 4.5, 400)
    p1 = []
    p1_x2 = []
    p1_x3 = []
    for i in points:
        p1.append(perceptron_1(i.reshape(1, 1)).reshape(-1))
        p1_x2.append(perceptron_1x2(i.reshape(1, 1)).reshape(-1))
        p1_x3.append(perceptron_1x3(i.reshape(1, 1)).reshape(-1))
    plt.plot(points, p1, "g", label="Персептрон с 5 нейронами на скрытом слое")
    plt.plot(points, p1_x2, "r", label="Персептрон с 10 нейронами на скрытом слое")
    plt.plot(points, p1_x3, "b", label="Персептрон с 20 нейронами на скрытом слое")
    plt.plot(points, fx(points), "y", label="График функции")
    plt.xlabel("x")
    plt.ylabel("y")
    plt.title(r"Аппроксимация функции $f(x) = {2^{x√e^{(\sin(x)\cos(x))}}}$")
    plt.legend()
    plt.grid()
    plt.show()

def menu_item_4(seq_length, train, rnn, perceptron_3, mean, std):
    for epoch in range(500):
        q = np.random.randint(0, seq_length)
        for i in range(q, len(train) - seq_length, seq_length):
            x = (np.array(train[i            : i+seq_length]).reshape(1, seq_length) - mean) / std
            y = (np.array(train[i+seq_length : i+seq_length+1]).reshape(1, 1) - mean) / std
            rnn(x)
            rnn.backward_prop(y)
            rnn.update_weights()
            perceptron_3.forward_prop(x)
            perceptron_3.backward_prop(y)
            perceptron_3.update_weights()
    return True

def menu_item_5(train):
    plt.plot(train)
    plt.xlabel("День (начиная с 1 января 2016 года)")
    plt.title("Курс евро к доллару")
    plt.grid()
    plt.show()

def menu_item_6(seq_length, train, test, rnn, perceptron_3, mean, std):
    rnn_y = []
    perceptron_y = []
    for i in train[-seq_length:]:
        rnn_y.append((i - mean) / std)
        perceptron_y.append((i - mean) / std)
    for i in range(len(test)):
        out_rnn = rnn(np.array(rnn_y[i:i+seq_length]).reshape(1, seq_length)).reshape(-1)
        out_perceptron_3 = perceptron_3(np.array(perceptron_y[i:i+seq_length]).reshape(1, seq_length)).reshape(-1)
        rnn_y.append(out_rnn[0])
        perceptron_y.append(out_perceptron_3[0])
    plt.plot(np.array(rnn_y[seq_length:]) * std + mean, label="Сеть Элмана")
    plt.plot(np.array(perceptron_y[seq_length:]) * std + mean, label="Персептрон")
    plt.plot(test, label="Правильный курс")
    plt.legend()
    plt.grid()
    plt.show()


def create_nets_for_task_1():

    rbf = RBFNet(7)

    perceptron_1 = nn.NN(0.0001)
    perceptron_1.add_layer(1, 5, "tanh", need_bias=True)
    perceptron_1.add_layer(5, 1)

    perceptron_2 = nn.NN(0.0001)
    perceptron_2.add_layer(1, 5, "tanh", need_bias=True)
    perceptron_2.add_layer(5, 5, "tanh", need_bias=True)
    perceptron_2.add_layer(5, 1)

    perceptron_1x2 = nn.NN(0.0001)
    perceptron_1x2.add_layer(1, 10, "tanh", need_bias=True)
    perceptron_1x2.add_layer(10, 1)

    perceptron_1x3 = nn.NN(0.0001)
    perceptron_1x3.add_layer(1, 20, "tanh", need_bias=True)
    perceptron_1x3.add_layer(20, 1)

    return rbf, perceptron_1, perceptron_2, perceptron_1x2, perceptron_1x3

def create_nets_for_task_2(seq_length):
    rnn = RNN(seq_length, 50, 1, 0.003)
    perceptron_3 = nn.NN(0.003)
    perceptron_3.add_layer(seq_length, 50, "tanh")
    perceptron_3.add_layer(50, 1)

    return rnn, perceptron_3


if __name__ == "__main__":
    train = deserialize(r"C:/Users/nikit/Desktop/Neuron/Lab5/train.txt")
    train.reverse()  
    test = deserialize(r"C:/Users/nikit/Desktop/Neuron/Lab5/test.txt")  
    test.reverse()  
    mean = np.mean(train)
    std = np.std(train)
    # Задание 1
    training_complete = False
    fx = lambda x: 2**(x*np.sqrt(np.exp(np.sin(x)*np.cos(x))))
    rbf, perceptron_1, perceptron_2, perceptron_1x2, perceptron_1x3 = create_nets_for_task_1()

    # Задание 2
    training_complete_2 = False
    seq_length = 80
    rnn, perceptron_3 = create_nets_for_task_2(seq_length)
    
    flag = False
    k = 0
    while not flag:
        draw_menu()
        try:
            k = int(input("Введите номер пункта меню: "))
        except ValueError:
            pass
        print()
        
        if k == 1:
            training_complete = menu_item_1(fx, rbf, perceptron_1, perceptron_2, perceptron_1x2, perceptron_1x3)
            print("\nОбучение завершено\n")
        elif k == 2:
            if training_complete:
                menu_item_2(fx, rbf, perceptron_1, perceptron_2)
            else:
                print("Сначала проведите обучение нейронных сетей!\n")
        elif k == 3:
            if training_complete:
                menu_item_3(fx, perceptron_1, perceptron_1x2, perceptron_1x3)
            else:
                print("Сначала проведите обучение нейронных сетей!\n")
        elif k == 4:
            training_complete_2 = menu_item_4(seq_length, train, rnn, perceptron_3, mean, std)
            print("\nОбучение завершено\n")
        elif k == 5:
            menu_item_5(train)
        elif k == 6:
            if training_complete_2:
                menu_item_6(seq_length, train, test, rnn, perceptron_3, mean, std)
            else:
                print("Сначала проведите обучение нейронных сетей!\n")
        elif k == 7:
            flag = True