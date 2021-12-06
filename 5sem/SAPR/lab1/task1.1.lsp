(defun fact1 (n)
(cond ((zerop n) 1)
 (t (* n (fact1 (- n 1))))))