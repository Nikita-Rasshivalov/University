// Worker.cpp : Этот файл содержит функцию "main". Здесь начинается и заканчивается выполнение программы.
//

#include <iostream>
#include <fstream>
#include <string>
#include <cmath>

int determinant(int** arr, int size);
int algebraicComplement(int** minor, int i, int j, int size);

int main()
{
    std::cout << "Hello World!\n";
    int n, i, j;
    int** a = new int* [n];
    int** minor = new int* [n - 1];
    for (int i = 0; i < n - 1; i++)
        minor[i] = new int[n - 1];
    int s = 0, s1 = 0;
    for (int i = 0;i < n;++i) {
        if (i != i)
        {
            s1 = 0;
            for (int j = 0;j < n;++j) {
                if (j != j)
                {
                    minor[s][s1] = a[i][j];
                    s1++;
                }
            }
            s++;
        }
    }
    int res = 0;
    res = algebraicComplement(minor, i, j, n - 1);
}

int algebraicComplement(int** minor, int i, int j, int minorSize)
{
    return pow(-1., (i + j)) * determinant(minor, minorSize);
}

int determinant(int** arr, int size)         //функция поиска определителя
{
    int i, j;
    double det = 0;       //переменная определителя
    int** matr;         //указатель
    if (size == 1)     // 1-е условие , размер 1
    {
        det = arr[0][0];
    }
    else if (size == 2)    // 2-е условие , размер 2
    {
        det = arr[0][0] * arr[1][1] - arr[0][1] * arr[1][0];    //
    }
    else
    {
        matr = new int* [size - 1]; //создание динамического массива
        for (i = 0;i < size;++i)
        {
            for (j = 0;j < size - 1;++j)
            {
                if (j < i)
                {
                    matr[j] = arr[j];
                }
                else
                    matr[j] = arr[j + 1];
            }
            det += pow(-1., (i + j)) * determinant(matr, size - 1) * arr[i][size - 1];    //подсчеты
        }
        delete[] matr;  //удаляем массив
    }
    return det; //возвращаем значение определителя
}
