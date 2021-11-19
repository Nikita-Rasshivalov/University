A = [3 -2 4; 3 4 -2;2 -1 -1]'
B = [21 9 10]'
addA = []
for i = 1:3,
    for j = 1:3,
        temp = A
        temp(i,:)=[]
        temp(:,j)=[]
        addA(i,j)=det(temp)
    end
end
addA(1,2) = addA(1,2) * -1
addA(2,1) = addA(2,1) * -1 
addA(2,3) = addA(2,3) * -1
addA(3,2) = addA(3,2) * -1
BackA = 1/det(A)*addA
disp(BackA*B)

disp(roots([1 -10.2 -6.09 78.31 -66.66]))
