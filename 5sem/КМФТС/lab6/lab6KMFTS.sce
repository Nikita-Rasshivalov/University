h = 0.01

A = 1344.58
C = 310.23
b = -3.67
t = 0 : 0.025 : 5
function y = fni(x)
    y = C + A * exp(b + b * x)
endfunction
function y = levo(t)
    ly = 30
    q = 1000
    y  = q / ly + 15 * t
endfunction
function y = prav(t)
    ct = 800
    ly = 30
    T0 = 230
    y = (ct / ly) .* (T0 - ((C + A .* exp(b + b * h)) + t * 10))
endfunction

function [T, x, t] = pfun(nx, nt, h, t_k, A)
    dx = h / nx;
    dt = t_k / nt;
    for i = 1 : nx + 1
        x(i) = (i - 1) * dx
        T(i, 1) = fni(x(i))
    end
    for j = 1 : nt + 1
        t(j) = (j - 1) * dt
        T(1, j) = prav(t(j))
        T(nx + 1, j) = levo(t(j))
    end
    bet = A^2 * dt / dx^2
    for j = 1 : nt
        for i = 2 : nx
            T(i, j + 1) = bet * T(i - 1, j) + (1 - 2 * bet) * T(i, j) + bet * T(i + 1, j)
        end
    end
endfunction
[T, x, t] = pfun(50, 200, 5, 3, 0.4)
sumT=sum(T)/length(T)

//////////////////////////////////////////////////////////////

h = 0.01

A = 1344.58
C = 310.23
b = -3.67
t = 0 : 0.025 : 5
function y = fni1(x)
    y = C + A * exp(b + b * x)
endfunction
function y = levo1(t)
    ly = 30
    q1 = 1100
    y  = q1 / ly + 15 * t
endfunction
function y = prav1(t)
    ct = 800
    ly = 30
    T0 = 230
    y = (ct / ly) .* (T0 - ((C + A .* exp(b + b * h)) + t * 10))
endfunction

function [T, x, t] = pfun1(nx, nt, h, t_k, A)
    dx = h / nx;
    dt = t_k / nt;
    for i = 1 : nx + 1
        x(i) = (i - 1) * dx
        T(i, 1) = fni1(x(i))
    end
    for j = 1 : nt + 1
        t(j) = (j - 1) * dt
        T(1, j) = prav1(t(j))
        T(nx + 1, j) = levo1(t(j))
    end
    bet = A^2 * dt / dx^2
    for j = 1 : nt
        for i = 2 : nx
            T(i, j + 1) = bet * T(i - 1, j) + (1 - 2 * bet) * T(i, j) + bet * T(i + 1, j)
        end
    end
endfunction
[T, x, t] = pfun1(50, 200, 5, 3, 0.4)
sumT1=sum(T)/length(T)
disp("T1/T=")
disp(sumT1/sumT)

//////////////////////////////////////////////////////

h = 0.01

A = 1344.58
C = 310.23
b = -3.67
t = 0 : 0.025 : 5
function y = fni2(x)
    y = C + A * exp(b + b * x)
endfunction
function y = levo2(t)
    ly = 30
    q2 = 1200
    y  = q2 / ly + 15 * t
endfunction
function y = prav2(t)
    ct = 800
    ly = 30
    T0 = 230
    y = (ct / ly) .* (T0 - ((C + A .* exp(b + b * h)) + t * 10))
endfunction

function [T, x, t] = pfun2(nx, nt, h, t_k, A)
    dx = h / nx;
    dt = t_k / nt;
    for i = 1 : nx + 1
        x(i) = (i - 1) * dx
        T(i, 1) = fni2(x(i))
    end
    for j = 1 : nt + 1
        t(j) = (j - 1) * dt
        T(1, j) = prav2(t(j))
        T(nx + 1, j) = levo2(t(j))
    end
    bet = A^2 * dt / dx^2
    for j = 1 : nt
        for i = 2 : nx
            T(i, j + 1) = bet * T(i - 1, j) + (1 - 2 * bet) * T(i, j) + bet * T(i + 1, j)
        end
    end
endfunction
[T, x, t] = pfun2(50, 200, 5, 3, 0.4)
sumT2=sum(T)/length(T)
disp("T2/T=")
disp(sumT2/sumT)

///////////////////////////////////////////////////////////////////////////////////////////////////////

h = 0.01

A = 1344.58
C = 310.23
b = -3.67
t = 0 : 0.025 : 5
function y = fni3(x)
    y = C + A * exp(b + b * x)
endfunction
function y = levo3(t)
    ly = 30
    q3 = 1300
    y  = q3 / ly + 15 * t
endfunction
function y = prav3(t)
    ct = 800
    ly = 30
    T0 = 230
    y = (ct / ly) .* (T0 - ((C + A .* exp(b + b * h)) + t * 10))
endfunction

function [T, x, t] = pfun3(nx, nt, h, t_k, A)
    dx = h / nx;
    dt = t_k / nt;
    for i = 1 : nx + 1
        x(i) = (i - 1) * dx
        T(i, 1) = fni3(x(i))
    end
    for j = 1 : nt + 1
        t(j) = (j - 1) * dt
        T(1, j) = prav3(t(j))
        T(nx + 1, j) = levo3(t(j))
    end
    bet = A^2 * dt / dx^2
    for j = 1 : nt
        for i = 2 : nx
            T(i, j + 1) = bet * T(i - 1, j) + (1 - 2 * bet) * T(i, j) + bet * T(i + 1, j)
        end
    end
endfunction
[T, x, t] = pfun3(50, 200, 5, 3, 0.4)
sumT3=sum(T)/length(T)
disp("T3/T=")
disp(sumT3/sumT)

///////////////////////////////////////////////////////////////////////////////////////////////////

h = 0.01

A = 1344.58
C = 310.23
b = -3.67
t = 0 : 0.025 : 5
function y = fni4(x)
    y = C + A * exp(b + b * x)
endfunction
function y = levo4(t)
    ly = 30
    q4 = 900
    y  = q4 / ly + 15 * t
endfunction
function y = prav4(t)
    ct = 800
    ly = 30
    T0 = 230
    y = (ct / ly) .* (T0 - ((C + A .* exp(b + b * h)) + t * 10))
endfunction

function [T, x, t] = pfun4(nx, nt, h, t_k, A)
    dx = h / nx;
    dt = t_k / nt;
    for i = 1 : nx + 1
        x(i) = (i - 1) * dx
        T(i, 1) = fni4(x(i))
    end
    for j = 1 : nt + 1
        t(j) = (j - 1) * dt
        T(1, j) = prav4(t(j))
        T(nx + 1, j) = levo4(t(j))
    end
    bet = A^2 * dt / dx^2
    for j = 1 : nt
        for i = 2 : nx
            T(i, j + 1) = bet * T(i - 1, j) + (1 - 2 * bet) * T(i, j) + bet * T(i + 1, j)
        end
    end
endfunction
[T, x, t] = pfun4(50, 200, 5, 3, 0.4)
sumT4=sum(T)/length(T)
disp("T4/T=")
disp(sumT4/sumT)
disp("T4=")
disp(sumT4)
disp("T3=")
disp(sumT3)
disp("T2=")
disp(sumT2)
disp("T1=")
disp(sumT1)
disp("T=")
disp(sumT)
