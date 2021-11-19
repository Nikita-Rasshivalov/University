function dx=syst(t,x)
R=1000
R1=20000
C=100/1000000
L=5
dx=zeros(2,1);
dx(1)=-R1/L*x(1)-x(2)/L;
dx(2)=x(1)/C-x(2)/R/C;
endfunction
R=1000
R1=20000
C=100/1000000
L=5
x=[1;10/L];
t0=0;
t=0:0.01:0.1;
y=ode("stiff",x,t0,t,syst);

plot(t,y);
xgrid();
disp(y);
