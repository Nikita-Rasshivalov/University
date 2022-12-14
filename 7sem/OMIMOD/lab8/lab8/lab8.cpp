#include <mpi.h>
#include <stdio.h>
#include <iostream>
#include <fstream>
#include <ctime>
#include <string>
#include <iomanip>

const int Dimension = 100;
const char* MatrixAFilename = "A.txt";
const char* VectorBFilename = "B.txt";
const char* VectorXFilename = "X.txt";
const int MainRank = 0;

void GenerateMatrixAFile(const char *filename, int dimension, int min, int max);
void GenerateVectorBFile(const char *filename, int dimension, int min, int max);
float* ReadMatrixAFile(const char *filename, int dimension);
float* ReadVectorBFile(const char *filename, int dimension);
void WriteVectorXFile(const char *filename, float *vectorX, int dimension);
void LUDecomposition(float *matrixA, float *matrixL, float *matrixU, int dimension, int worldRank, int worldSize);
void ComputeVectorY(float *matrixL, float *vectorB, float *vectorY, int dimension, int worldRank, int worldSize);
void ComputeVectorX(float *matrixU, float *vectorY, float *vectorX, int dimension, int worldRank, int worldSize);
void ShowMatrix(float *matrix, int size);
void ShowVector(float *vector, int size);
void SumMatrix(float *mainMatrix, float *subMatrix, int size);
void SumVector(float *mainVector, float *subVector, int size);

int main(int argc, char** argv)
{
    MPI_Init(NULL, NULL);

    // Find rank
    int worldRank;
    MPI_Comm_rank(MPI_COMM_WORLD, &worldRank);

    // Find rank count
    int worldSize;
    MPI_Comm_size(MPI_COMM_WORLD, &worldSize);

    GenerateMatrixAFile(MatrixAFilename, Dimension, -20, 100);
    GenerateVectorBFile(VectorBFilename, Dimension, 40, 80);

    float *matrixA = ReadMatrixAFile(MatrixAFilename, Dimension);
    float *matrixL = new float[Dimension * Dimension];
    float *matrixU = new float[Dimension * Dimension];

    LUDecomposition(matrixA, matrixL, matrixU, Dimension, worldRank, worldSize);

    if (worldRank == MainRank)
    {
        float* vectorB = ReadVectorBFile(VectorBFilename, Dimension);
        float* vectorY = new float[Dimension];
        float* vectorX = new float[Dimension];

        ComputeVectorY(matrixL, vectorB, vectorY, Dimension, worldRank, worldSize);
        ComputeVectorX(matrixU, vectorY, vectorX, Dimension, worldRank, worldSize);

        WriteVectorXFile(VectorXFilename, vectorX, Dimension);

        delete[] vectorB;
        delete[] vectorY;
        delete[] vectorX;
    }

    delete[] matrixA;
    delete[] matrixL;
    delete[] matrixU;

    MPI_Finalize();

    return 0;
}

void GenerateMatrixAFile(const char *filename, int dimension, int min, int max)
{
    srand(1);

    std::ofstream output(filename, std::ofstream::trunc);

    for (int i = 0; i < dimension; ++i)
    {
        for (int j = 0; j < dimension; ++j)
        {
            output << rand() % (max - min) + min << (j + 1 == dimension ? '\n' : ' ');
        }
    }

    output.flush();
    output.close();
}

void GenerateVectorBFile(const char* filename, int dimension, int min, int max)
{
    srand(1);

    std::ofstream output(filename, std::ofstream::trunc);

    for (int i = 0; i < dimension; ++i)
    {
        output << rand() % (max - min) + min << std::endl;
    }

    output.flush();
    output.close();
}

float* ReadMatrixAFile(const char *filename, int dimension)
{
    float *matrixA = new float[dimension * dimension];

    std::ifstream input(filename);

    std::string text;

    for (int i = 0; i < dimension; ++i)
    {
        std::getline(input, text);

        size_t pos = 0;

        for (int j = 0; j < dimension; ++j)
        {
            if (j + 1 < dimension)
            {
                pos = text.find(" ");

                matrixA[i * dimension + j] = std::stof(text.substr(0, pos));

                text.erase(0, pos + 1);
            }
            else
            {
                matrixA[i * dimension + j] = std::stof(text);
            }
        }

        text.clear();
    }

    input.close();

    return matrixA;
}

float* ReadVectorBFile(const char *filename, int dimension)
{
    float *vectorB = new float[dimension];

    std::ifstream input(filename);

    std::string text;

    for (int i = 0; i < dimension; ++i)
    {
        std::getline(input, text);

        vectorB[i] = std::stof(text);

        text.clear();
    }

    input.close();

    return vectorB;
}

void WriteVectorXFile(const char *filename, float *vectorX, int dimension)
{
    std::ofstream output(filename, std::ofstream::trunc);

    for (int i = 0; i < dimension; ++i)
    {
        output << vectorX[i] << std::endl;
    }

    output.flush();
    output.close();
}

float getSumU(int dimension, int i, int j, float *matrixL, float *matrixU)
{
    float res = 0;

    for (int k = 0; k < i; k++)
    {
        res += matrixL[i * dimension + k] * matrixU[k * dimension + j];
    }

    return res;
}

float getSumL(int dimension, int i, int j, float *matrixL, float *matrixU)
{
    float res = 0;

    for (int k = 0; k < i; k++)
    {
        res += matrixL[j * dimension + k] * matrixU[k * dimension + i];
    }

    return res;
}

void LUDecomposition(float *matrixA, float *matrixL, float *matrixU, int dimension, int worldRank, int worldSize)
{
    memset(matrixL, 0, sizeof(float) * dimension * dimension);
    memset(matrixU, 0, sizeof(float) * dimension * dimension);

    for (int j = worldRank; j < dimension; j += worldSize)
    {
        matrixU[j] = matrixA[j];
        matrixL[j * dimension] = matrixA[j * dimension] / matrixA[0];
    }

    if (worldRank == MainRank)
    {
        for (int rank = 0; rank < worldSize; ++rank)
        {
            if (rank == MainRank)
            {
                continue;
            }

            float *subMatrixL = new float[dimension * dimension];
            float *subMatrixU = new float[dimension * dimension];

            MPI_Recv(subMatrixL, Dimension * Dimension, MPI_FLOAT, rank, 0, MPI_COMM_WORLD, MPI_STATUS_IGNORE);
            MPI_Recv(subMatrixU, Dimension * Dimension, MPI_FLOAT, rank, 0, MPI_COMM_WORLD, MPI_STATUS_IGNORE);

            SumMatrix(matrixL, subMatrixL, dimension);
            SumMatrix(matrixU, subMatrixU, dimension);
        }
    }
    else
    {
        MPI_Send(matrixL, Dimension * Dimension, MPI_FLOAT, MainRank, 0, MPI_COMM_WORLD);
        MPI_Send(matrixU, Dimension * Dimension, MPI_FLOAT, MainRank, 0, MPI_COMM_WORLD);
    }

    for (int i = 1; i < dimension; i++)
    {
        for (int j = i; j < dimension; j++)
        {
            matrixU[i * dimension + j] = matrixA[i * dimension + j] - getSumU(dimension, i, j, matrixL, matrixU);

            matrixL[j * dimension + i] = (matrixA[j * dimension + i] - getSumL(dimension, i, j, matrixL, matrixU)) / matrixU[i * dimension + i];
        }
    }
}

void ShowMatrix(float *matrix, int size)
{
    for (int i = 0; i < size; ++i)
    {
        for (int j = 0; j < size; ++j)
        {
            printf("%-8.2f ", matrix[i * size + j]);
        }

        printf("\n");
    }
}

void ShowVector(float *vector, int size)
{
    for (int i = 0; i < size; ++i)
    {
        printf("%-8.8f\n", vector[i]);
    }
}

void SumMatrix(float *mainMatrix, float *subMatrix, int size)
{
    for (int i = 0; i < size; ++i)
    {
        for (int j = 0; j < size; ++j)
        {
            mainMatrix[i * size + j] += subMatrix[i * size + j];
        }
    }
}

void SumVector(float *mainVector, float *subVector, int size)
{
    for (int i = 0; i < size; ++i)
    {
        mainVector[i] += subVector[i];
    }
}

void ComputeVectorY(float *matrixL, float *vectorB, float *vectorY, int dimension, int worldRank, int worldSize)
{
    memset(vectorY, 0, sizeof(float) * dimension);

    for (int i = 0; i < dimension; ++i)
    {
        vectorY[i] = vectorB[i];

        for (int j = 0; j < i; ++j)
        {
            vectorY[i] -= matrixL[i * dimension + j] * vectorY[j];
        }
    }
}

void ComputeVectorX(float *matrixU, float *vectorY, float *vectorX, int dimension, int worldRank, int worldSize)
{
    memset(vectorX, 0, sizeof(float) * dimension);

    for (int i = dimension - 1; i >= 0; --i)
    {
        vectorX[i] = vectorY[i];

        for (int j = i + 1; j < dimension; ++j)
        {
            vectorX[i] -= matrixU[i * dimension + j] * vectorX[j];
        }

        vectorX[i] /= matrixU[i * dimension + i];
    }
}