(defun remove_last (lst)
(cond
((null lst) nil)
((null (cdr lst)) nil)
(t (cons (car lst) (remove_last (cdr lst))))
)
)

(defun remove_first_and_last (lst)
(cdr (remove_last lst))
)

(defun replace_with_zeroes (lst)
(cond
((null lst) nil)
((minusp (car lst)) (cons 0 (replace_with_zeroes (cdr lst))))
(t (cons (car lst) (replace_with_zeroes (cdr lst))))
)
)

(defun remove_zeroes (lst)
(cond
((null lst) nil)
((zerop (car lst)) (remove_zeroes (cdr lst)))
(t (cons (car lst) (remove_zeroes (cdr lst))))
)
)

(defun double_x (lst x)
(cond
(
(null lst) 
nil)
(
(eql (car lst) x)
(cons (car lst) (cons (car lst) (double_x (cdr lst) x)))
)
(
t 
(cons (car lst) (double_x (cdr lst) x))
)
)
)

(defun count_x (lst x)
(cond
((null (cdr lst)) 0)
((eq (car lst) x) (+ 1 (count_x (cdr lst) x)))
(t (+ 0 (count_x (cdr lst) x)))
)
)