x=1:0.5:2
y2 = x .*(sqrt(1+x.^2)).*sin(x);
y=(2*x^3)-(3*x^2)
plot(x,y,'-sc')
plot(x,y2,'--vg')
xgrid ()
xtitle("График","Ось Оx","Ось Oy")
legend("График Y(X)","График Y2(X)")
