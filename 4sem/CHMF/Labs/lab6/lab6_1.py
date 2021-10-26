import numpy as np
import matplotlib.pyplot as plt
import matplotlib.animation as animation
from matplotlib.animation import FuncAnimation

xLength = 60 #длина по x
yLength = 70 #длина по y
iterationsNumber = 400 #количество итераций

alpha = 2 #коэффицент теплопроводности материала
delta_x = 1 #шаг по x
delta_y = 1 #шаг по н


delta_t = 0.125 #шаг по времени

# Инициализация трехмерного массива
u = np.empty((iterationsNumber, xLength, yLength))

# Начальное условие
u_initial = 89

# Граничное условие
u_top = 6.0
u_left = 8.0
u_bottom = 5.0
u_right = 1.0

#Установка начальных услдвия
u.fill(u_initial)

# Установка граничных условий
u[:, (xLength-1):, :] = u_top
u[:, :, :1] = u_left
u[:, :1, 1:] = u_bottom
u[:, :, (yLength-1):] = u_right

def calculate(u): #функция вычисляющая значения температуры 
    for k in range(0, iterationsNumber-1, 1):
        for i in range(1, xLength-1, delta_x):
            for j in range(1, yLength-1, delta_x):
                u[k + 1][i][j] = u[k][i][j] + delta_t * alpha * ((u[k][i+1][j]-2*u[k][i][j]+u[k][i-1][j])/delta_x**2+(u[k][i][j+1]-2*u[k][i][j]+u[k][i][j-1])/delta_y**2)
    return u

def plotheatmap(u_k, k): #создает график распределения температуры
    # Clear the current plot figure
    plt.clf()

    plt.title(f"Температура при t = {k*delta_t:.3f}")
    plt.xlabel("x")
    plt.ylabel("y")

    # This is to plot u_k (u at time-step k)
    plt.pcolormesh(u_k, cmap=plt.cm.jet, vmin=0, vmax=100)
    plt.colorbar()

    return plt

# Do the calculation here
u = calculate(u)

def animate(k): #задает k и передает значения
    plotheatmap(u[k], k)

anim = animation.FuncAnimation(plt.figure(), animate, interval=1, frames=iterationsNumber, repeat=False) #создает анимацию
anim.save('D:\\animation.gif', writer='PillowWriter', fps=20) #сохраняет анимацию в виде gif

print("Выполнено")