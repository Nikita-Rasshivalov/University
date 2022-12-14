#include <iostream>
#include <fstream>
#include <string>
#include <cmath>
using namespace std;

void inputMatrix(int** arr, int size);
void outputMatrix(int** arr, int size);
int algebraicComplement(int** arr, int i, int j, int size);

struct Message {
    unsigned int packet_type;

    void serialize(char* data) {
        memcpy(data, this, sizeof(Message));
    }

    void deserialize(char* data) {
        memcpy(this, data, sizeof(Message));
    }
};

int main()
{
    int size;
    std::cout << "Input matrix size" << endl;
    std:: cin >> size;

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

int algebraicComplement(int** arr, int i, int j, int size) 
{
    return 0;//to do impelemt client-server logic
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
