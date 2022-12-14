// Window.cpp : Определяет точку входа для приложения.
//

#include "framework.h"
#include "Window.h"
#include <iostream> 
#include <cstdio>
#include <cstring> 
#include <stack> 
#include <map>
#include <chrono>
#define MAX_LOADSTRING 100
constexpr auto CURRENT_GRAPH = 3;
LPCWSTR* titles = new LPCWSTR[4]
{
    L"График функции",
    L"График зависимости времени решения от точности для многопточного",
    L"График зависимости времени решения от точности для однопоточного",
    L"График зависимости времени решения от количества потоков"
};
void DrawAxis(HDC hdc, RECT rectClient);
void DrawGraph(HDC hdc, RECT rectClient);
void DrawGraphTimeFromEpsilon(HDC hdc, RECT rectClient, bool useThreads);
void DrawGraphTimeFromThread(HDC hdc, RECT rectClient);
void OnPaint(HWND hwnd);
HANDLE hSemaphore;
LONG cMax = 1;
using namespace std;
constexpr auto THREAD_COUNT = 4;
void plot(float from, float to);
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

double getResult(map<int, double> results);
DWORD __stdcall process(LPVOID arg);
double calculateIntegralWithThreads(double start, double end, double step, int n, int threadCount);
double calculateIntegralSingle(double start, double end, int n);

HINSTANCE hInst;                                // текущий экземпляр
WCHAR szTitle[MAX_LOADSTRING];                  // Текст строки заголовка
WCHAR szWindowClass[MAX_LOADSTRING];            // имя класса главного окна

// Отправить объявления функций, включенных в этот модуль кода:
ATOM                MyRegisterClass(HINSTANCE hInstance);
BOOL                InitInstance(HINSTANCE, int);
LRESULT CALLBACK    WndProc(HWND, UINT, WPARAM, LPARAM);
INT_PTR CALLBACK    About(HWND, UINT, WPARAM, LPARAM);

int APIENTRY wWinMain(_In_ HINSTANCE hInstance,
                     _In_opt_ HINSTANCE hPrevInstance,
                     _In_ LPWSTR    lpCmdLine,
                     _In_ int       nCmdShow)
{
    UNREFERENCED_PARAMETER(hPrevInstance);
    UNREFERENCED_PARAMETER(lpCmdLine);

    // TODO: Разместите код здесь.

    // Инициализация глобальных строк
    LoadStringW(hInstance, IDS_APP_TITLE, szTitle, MAX_LOADSTRING);
    LoadStringW(hInstance, IDC_WINDOW, szWindowClass, MAX_LOADSTRING);
    MyRegisterClass(hInstance);

    // Выполнить инициализацию приложения:
    if (!InitInstance (hInstance, nCmdShow))
    {
        return FALSE;
    }

    HACCEL hAccelTable = LoadAccelerators(hInstance, MAKEINTRESOURCE(IDC_WINDOW));

    MSG msg;

    // Цикл основного сообщения:
    while (GetMessage(&msg, nullptr, 0, 0))
    {
        if (!TranslateAccelerator(msg.hwnd, hAccelTable, &msg))
        {
            TranslateMessage(&msg);
            DispatchMessage(&msg);
        }
    }

    return (int) msg.wParam;
}



//
//  ФУНКЦИЯ: MyRegisterClass()
//
//  ЦЕЛЬ: Регистрирует класс окна.
//
ATOM MyRegisterClass(HINSTANCE hInstance)
{
    WNDCLASSEXW wcex;

    wcex.cbSize = sizeof(WNDCLASSEX);

    wcex.style          = CS_HREDRAW | CS_VREDRAW;
    wcex.lpfnWndProc    = WndProc;
    wcex.cbClsExtra     = 0;
    wcex.cbWndExtra     = 0;
    wcex.hInstance      = hInstance;
    wcex.hIcon          = LoadIcon(hInstance, MAKEINTRESOURCE(IDI_WINDOW));
    wcex.hCursor        = LoadCursor(nullptr, IDC_ARROW);
    wcex.hbrBackground  = (HBRUSH)(COLOR_WINDOW+1);
    wcex.lpszMenuName   = MAKEINTRESOURCEW(IDC_WINDOW);
    wcex.lpszClassName  = szWindowClass;
    wcex.hIconSm        = LoadIcon(wcex.hInstance, MAKEINTRESOURCE(IDI_SMALL));

    return RegisterClassExW(&wcex);
}

//
//   ФУНКЦИЯ: InitInstance(HINSTANCE, int)
//
//   ЦЕЛЬ: Сохраняет маркер экземпляра и создает главное окно
//
//   КОММЕНТАРИИ:
//
//        В этой функции маркер экземпляра сохраняется в глобальной переменной, а также
//        создается и выводится главное окно программы.
//
BOOL InitInstance(HINSTANCE hInstance, int nCmdShow)
{
   hInst = hInstance; // Сохранить маркер экземпляра в глобальной переменной

   HWND hWnd = CreateWindowW(szWindowClass, titles[CURRENT_GRAPH], WS_OVERLAPPEDWINDOW,
      CW_USEDEFAULT, 0, CW_USEDEFAULT, 0, nullptr, nullptr, hInstance, nullptr);

   if (!hWnd)
   {
      return FALSE;
   }

   ShowWindow(hWnd, nCmdShow);
   UpdateWindow(hWnd);

   return TRUE;
}

//
//  ФУНКЦИЯ: WndProc(HWND, UINT, WPARAM, LPARAM)
//
//  ЦЕЛЬ: Обрабатывает сообщения в главном окне.
//
//  WM_COMMAND  - обработать меню приложения
//  WM_PAINT    - Отрисовка главного окна
//  WM_DESTROY  - отправить сообщение о выходе и вернуться
//
//
LRESULT CALLBACK WndProc(HWND hWnd, UINT message, WPARAM wParam, LPARAM lParam)
{
    switch (message)
    {
    case WM_COMMAND:
        {
            int wmId = LOWORD(wParam);
            // Разобрать выбор в меню:
            switch (wmId)
            {
            case IDM_ABOUT:
                DialogBox(hInst, MAKEINTRESOURCE(IDD_ABOUTBOX), hWnd, About);
                break;
            case IDM_EXIT:
                DestroyWindow(hWnd);
                break;
            default:
                return DefWindowProc(hWnd, message, wParam, lParam);
            }
        }
        break;
    case WM_PAINT:
        {
            PAINTSTRUCT ps;
            HDC hdc = BeginPaint(hWnd, &ps);
            // TODO: Добавьте сюда любой код прорисовки, использующий HDC...
            RECT rectClient;
            GetClientRect(hWnd, &rectClient);
            DrawAxis(hdc, rectClient);
            switch (CURRENT_GRAPH) {
                // График функции
            case 0: DrawGraph(hdc, rectClient);
                break;
                // График зависимости времени решения от точности для многопточного
            case 1: DrawGraphTimeFromEpsilon(hdc, rectClient, true);
                break;
                // График зависимости времени решения от точности для однопоточного
            case 2: DrawGraphTimeFromEpsilon(hdc, rectClient, false);
                break;
                // График зависимости времени решения от количества потоков
            case 3: DrawGraphTimeFromThread(hdc, rectClient);
                break;
            default: break;
            }
            ValidateRect(hWnd, NULL);

            EndPaint(hWnd, &ps);
        }
        break;
    case WM_DESTROY:
        PostQuitMessage(0);
        break;
    default:
        return DefWindowProc(hWnd, message, wParam, lParam);
    }
    return 0;
}

// Обработчик сообщений для окна "О программе".
INT_PTR CALLBACK About(HWND hDlg, UINT message, WPARAM wParam, LPARAM lParam)
{
    UNREFERENCED_PARAMETER(lParam);
    switch (message)
    {
    case WM_INITDIALOG:
        return (INT_PTR)TRUE;

    case WM_COMMAND:
        if (LOWORD(wParam) == IDOK || LOWORD(wParam) == IDCANCEL)
        {
            EndDialog(hDlg, LOWORD(wParam));
            return (INT_PTR)TRUE;
        }
        break;
    }
    return (INT_PTR)FALSE;
}




void DrawAxis(HDC hdc, RECT rectClient)
{
    HPEN penGraph = CreatePen(PS_SOLID, 2, RGB(0, 0, 255));
    HGDIOBJ gdiOld = SelectObject(hdc, penGraph);
    MoveToEx(hdc, 0, rectClient.bottom / 2, NULL);
    LineTo(hdc, rectClient.right, rectClient.bottom / 2);
    LineTo(hdc, rectClient.right - 5, rectClient.bottom / 2 + 2);
    MoveToEx(hdc, rectClient.right, rectClient.bottom / 2, NULL);
    LineTo(hdc, rectClient.right - 5, rectClient.bottom / 2 - 2);
    MoveToEx(hdc, rectClient.right / 2, rectClient.bottom, NULL);
    LineTo(hdc, rectClient.right / 2, rectClient.top);
    LineTo(hdc, rectClient.right / 2 - 2, rectClient.top + 5);
    MoveToEx(hdc, rectClient.right / 2, rectClient.top, NULL);
    LineTo(hdc, rectClient.right / 2 + 2, rectClient.top + 5);
    SelectObject(hdc, gdiOld);
}
void DrawGraph(HDC hdc, RECT rectClient)
{
    double x_start = -100;
    double x_end = 100;

    HPEN penGraph = CreatePen(PS_SOLID, 2, RGB(255, 0, 0));
    HGDIOBJ gdiOld = SelectObject(hdc, penGraph);
    double x_current = x_start;
    double step = (x_end - x_start) / rectClient.right;
    double y_start = f(x_start);
    MoveToEx(hdc, 0, int(-y_start / step) + rectClient.bottom / 2, NULL);
    while (x_current < x_end)
    {
        x_current += step;
        double y_next = f(x_current);
        LineTo(hdc, int(x_current / step) + rectClient.right / 2, int(-y_next / step) + rectClient.bottom / 2);
    }
    SelectObject(hdc, gdiOld);
}

void DrawGraphTimeFromEpsilon(HDC hdc, RECT rectClient, bool useThreads)
{
    
    int start = 0;
    int current = start;
    int end = 4;
    double* epsilons = new double[end] { 0.00005, 0.0001, 0.0005, 0.001, 0.01};
    int step = 1;
    double x_start = 0;
    double x_end = 100;

    HPEN penGraph = CreatePen(PS_SOLID, 2, RGB(255, 0, 0));
    HGDIOBJ gdiOld = SelectObject(hdc, penGraph);
    double x_current = x_start;
    ResultData d = Calc(x_end, x_start, 1, epsilons[current], 4, useThreads);
    double y_start = d.elapsed;
    MoveToEx(hdc, rectClient.right / 2, int(-y_start / step) + rectClient.bottom / 2, NULL);
    while (current < end)
    {
        current += step;
        d = Calc(x_end, x_start, 1, epsilons[current], 4, useThreads);
        double y_next = d.elapsed;
        LineTo(hdc, int(current * 50 / step) + rectClient.right / 2, int(-y_next / step) + rectClient.bottom / 2);
    }
    SelectObject(hdc, gdiOld);
}

void DrawGraphTimeFromThread(HDC hdc, RECT rectClient)
{
    double x_start = 0;
    double x_end = 100;
    double epsilon = 0.00001;
    HPEN penGraph = CreatePen(PS_SOLID, 2, RGB(255, 0, 0));
    HGDIOBJ gdiOld = SelectObject(hdc, penGraph);
 
    int threadStart = 1;
    int threadCurrent = threadStart;
    int threadEnd = 8;
    double step = 1.0;
    ResultData d = Calc(x_end, x_start, 1, epsilon, threadCurrent, true);
    double y_start = d.elapsed/10.0;
    MoveToEx(hdc, rectClient.right / 2, int(-y_start / step) + rectClient.bottom / 2, NULL);
    while (threadCurrent < threadEnd)
    {
        threadCurrent += step;
        d = Calc(x_end, x_start, 1, epsilon, threadCurrent, true);
        double y_next = d.elapsed/10.0;
        LineTo(hdc, int(threadCurrent*50 / step) + rectClient.right / 2, int(-y_next / step) + rectClient.bottom / 2);
    }
    SelectObject(hdc, gdiOld);
}

void OnPaint(HWND hwnd)
{
    PAINTSTRUCT ps;
    RECT rectClient;
    HDC hdc = BeginPaint(hwnd, &ps);
    GetClientRect(hwnd, &rectClient);
    DrawAxis(hdc, rectClient);
    DrawGraph(hdc, rectClient);
    ValidateRect(hwnd, NULL);
    EndPaint(hwnd, &ps);
}

double calculateIntegralSingle(double start, double end, int n) {
    return integral(f, start, end, n);
}

double calculateIntegralWithThreads(double start, double end, double step, int n, int threadCount) {
    //cout << "Starting iteration" << endl;
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
    //std::cout << "Iteration elapsed time:" << elapsed_ms.count() << " ms" << endl;

    return getResult(safe_map.getAndclear());
}


DWORD __stdcall process(LPVOID arg)
{
    integralData data = *(integralData*)arg;
    safe_map.push(data.id, integral(f, data.start, data.end, data.n));
    return 0;
}

double getResult(map<int, double> results)
{
    map <int, double> ::iterator it = results.begin();
    double result = 0.0;
    for (int i = 0; it != results.end(); it++, i++)
    {
        result = result + it->second;
    }

    return result;
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