#include <iostream>
#include <fstream>
#include <string>
#include <cmath>
using namespace std;

int algebraicComplement(int** arr, int i, int j, int size);
int determinant(int** Arr, int size);
void inputMatrix(int** arr, int size);
void outputMatrix(int** arr, int size);

int main()
{
    int size;
    std::cout << "Input matrix size" << endl;
    std::cin >> size;

    int** arr = new int* [size];
    int** algebraicComplements = new int* [size];
    for (int i = 0; i < size; i++)
        arr[i] = new int[size];
    inputMatrix(arr, size);
    outputMatrix(arr, size);
    for (int aci = 0; aci < size; aci++)
    {
        algebraicComplements[aci] = new int[size];
        for (int acj = 0; acj < size; acj++)
        {
            algebraicComplements[aci][acj] = algebraicComplement(arr, aci, acj, size);
        }
    }

    std::cout << "Algebraic complements" << endl;
    outputMatrix(algebraicComplements, size);
    system("pause");
}

int algebraicComplement(int** arr, int aci, int acj, int size)
{
    int** temp = new int* [size - 1];
    for (int i = 0; i < size - 1; i++)
        temp[i] = new int[size - 1];
    int s = 0, s1 = 0;
    for (int i = 0;i < size;++i) 
    {
        if (i != aci)
        {
            s1 = 0;
            for (int j = 0;j < size;++j)
                if (j != acj)
                {
                    temp[s][s1] = arr[i][j];
                    s1++;
                }
            s++;
        }
    }
    return pow(-1., aci + acj) * determinant(temp, size - 1);
}


void inputMatrix(int** arr, int size)
{
    int i, j;

    for (i = 0; i < size; i++)
    {
        for (j = 0; j < size; j++)
        {
            std::cout << "input [" << i << "," << j << "]" << endl;
            std::cin >> arr[i][j];
        }
    }
}

void outputMatrix(int** arr, int size)
{
    int i, j;
    for (i = 0;i < size;i++)
    {
        for (j = 0;j < size;j++)
        {
            std::cout << arr[i][j] << " ";
        }
        std::cout << endl;
    }
    std::cout << endl;
}

int determinant(int** Arr, int size)         //функция поиска определителя
{
    int i, j;
    double det = 0;       //переменная определителя
    int** matr;         //указатель
    if (size == 1)     // 1-е условие , размер 1
    {
        det = Arr[0][0];
    }
    else if (size == 2)    // 2-е условие , размер 2
    {
        det = Arr[0][0] * Arr[1][1] - Arr[0][1] * Arr[1][0];    //
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
                    matr[j] = Arr[j];
                }
                else
                    matr[j] = Arr[j + 1];
            }
            det += pow(-1., (i + j)) * determinant(matr, size - 1) * Arr[i][size - 1];    //подсчеты
        }
        delete[] matr;  //удаляем массив
    }
    return det; //возвращаем значение определителя
}
