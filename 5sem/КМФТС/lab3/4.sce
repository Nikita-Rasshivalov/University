figure(4)
X=-110.1:110, i=1
while i<=length(X)
if X(i)>3 then y=sin(X(i))
elseif X(i) <= 0 then y=cos(X(i))
else y=X(i)+2
end
Y(i)=y
i=i+1
end
plot(X,Y,'LineWidth' ,5) 
xtitle("График функци","ось Х","ось У")
legend("график функции Y(X)")
xgrid (1,1,0)
