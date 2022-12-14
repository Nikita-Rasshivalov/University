#include <windows.h>
#include <iostream> 
#include <cstdio>
#include <cstring> 
#include <stack> 
#include <chrono>

HANDLE hSemaphore;
LONG cMax = 1;
using namespace std;
constexpr auto THREAD_COUNT = 4;
struct integralData {
    int id;
    double start;
    double end;
    int n;
};
struct ResultData {
    int elapsed;
    double result;
    double accuracy;
};
typedef double(*pointFunc)(double);
double f(double x) {
    return exp(cos(x)) * sin(x);
}

double integral(pointFunc f, double start, double end, int n) {
    double x, step;
    double sum = 0.0;
    double fx;
    step = (end - start) / n;

    for (int i = 1; i <= n; i++) {
        x = start + i * step;
        fx = f(x);
        sum += fx;
    }
    return (sum * step);
}

ResultData Calc(double end, double start, int n, double eps, int threadCount, bool isLog);

class ThreadSafeResult {
public:
    ThreadSafeResult() {
        sum = 0.0;
    }

    double getAndclear() {
        DWORD dwWaitResult = 0;
        while (dwWaitResult != WAIT_OBJECT_0)
        {
            dwWaitResult = WaitForSingleObject(
                hSemaphore,
                1
            );
        }

        double result = sum;
        sum = 0.0;

        ReleaseSemaphore(
            hSemaphore,
            1,
            NULL);

        return result;
    }

    void push(double value) {
        DWORD dwWaitResult = 0;
        while (dwWaitResult != WAIT_OBJECT_0)
        {
            dwWaitResult = WaitForSingleObject(
                hSemaphore,
                1
            );
        }

        sum += value;

        ReleaseSemaphore(
            hSemaphore,
            1,
            NULL);
    }

private:
    double sum;
};

ThreadSafeResult safe_result;

DWORD __stdcall process(LPVOID arg);
double calculateIntegralWithThreads(double start, double end, double step, int n, int threadCount);
double calculateIntegralSingle(double start, double end, int n);

int main() {
    hSemaphore = CreateSemaphore(
        NULL,	
        cMax,	
        cMax,	
        NULL	
    );
    double start;
    double end;
    int n;
    double eps;
    start = 0;
    end = 100;
    n = 1;
    eps = 0.000001;
    std::cout << "Using threading" << endl;
    ResultData res = Calc(end, start, n, eps, THREAD_COUNT, true);
    std::cout << "Total elapsed time:" << res.elapsed << " ms" << endl;
    std::cout << "Square:" << fabs(res.result) << endl;
    std::cout << "Not using threading" << endl;
    ResultData res2 = Calc(end, start, n, eps, THREAD_COUNT, false);
    std::cout << "Total elapsed time:" << res2.elapsed << " ms" << endl;
    std::cout << "Square:" << fabs(res2.result) << endl;
    cout << "press any key to exit" << endl;
    int nothing;
    cin >> nothing;
    return 0;
}

double calculateIntegralSingle(double start, double end, int n) {
    return integral(f, start, end, n);
}

double calculateIntegralWithThreads(double start, double end, double step, int n, int threadCount) {
    auto start_time = std::chrono::steady_clock::now();
    HANDLE* threads = new HANDLE[threadCount];
    LPVOID* threadData = new LPVOID[threadCount];
    double threadStart;
    double threadEnd;
    for (int i = 0; i < threadCount; i++) {
        threadStart = start + step * i;
        threadEnd = start + step * (i + 1);
        threadData[i] = (LPVOID)new integralData{ i, threadStart, threadEnd, n };

        threads[i] = CreateThread(
            nullptr,
            65536,
            process,
            threadData[i],
            0,
            nullptr
        );
    }

    WaitForMultipleObjects(threadCount, threads, TRUE, INFINITE);

    for (int i = 0; i < threadCount; i++) {
        CloseHandle(threads[i]);
    }
    auto end_time = std::chrono::steady_clock::now();
    auto elapsed_ms = std::chrono::duration_cast<std::chrono::milliseconds>(end_time - start_time);

    return safe_result.getAndclear();
}


DWORD __stdcall process(LPVOID arg)
{
    integralData data = *(integralData*)arg;
    safe_result.push(integral(f, data.start, data.end, data.n));
    return 0;
}

ResultData Calc(double end, double start, int n, double eps, int threadCount, bool useThreads) {
    double step = (end - start) / threadCount;
    auto start_time = std::chrono::steady_clock::now();
    double previousResult, currentResult;
    currentResult = useThreads ? calculateIntegralWithThreads(start, end, step, n, threadCount) : calculateIntegralSingle(start, end, n);
    double loss;
    do {
        previousResult = currentResult;
        n = 2 * n;
        currentResult = useThreads ? calculateIntegralWithThreads(start, end, step, n, threadCount) : calculateIntegralSingle(start, end, n);
        loss = fabs(previousResult - currentResult);
    } while (loss > eps);

    auto end_time = std::chrono::steady_clock::now();
    auto elapsed_ms = std::chrono::duration_cast<std::chrono::milliseconds>(end_time - start_time);
    int ms = elapsed_ms.count();
    return ResultData{ ms, currentResult };
}