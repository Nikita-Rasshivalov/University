(defun prog1(list1 list2)
(cond
((null list1) nil)
((null list2) nil)
(T (cons (car list1) (cons (car list2) (prog1 (cdr list1) (cdr list2)))))
)
)



(defun task (lst m n)
  (cond
    ((null lst) nil)
    ((or (<= m)) (task (cdr lst) (1- m) (1- n)))
    (T (cons (car lst) (task (cdr lst) (1- m) (1- n) )))))





(defun z3 (lst)
  (cond ((null lst) nil)
          ((symbolp (car lst)) (z3 (cdr lst)))
          (t (cons (car lst) (z3 (cdr lst))))))

(defun replace(list)
(cond
((null list) nil)
((listp (car list)) (replace list))
((> (car list) 0) (cons "Positive" (replace (cdr list))))
((< (car list) 0) (cons "Negative" (replace (cdr list))))
(T (cons "Zero" (replace (cdr list))))
)
)
(defun removeInNumbers (_list n m &optional(i 0))
(cond
((null _list)nil)
((= i n) (removeInNumbers(cdr _list) n m (+ i 1)))
((= i m) (removeInNumbers(cdr _list) n m (+ i 1)))
(t (cons ( car _list) (removeInNumbers(cdr _list) n m (+ i 1))))
)
)
(defun z21 (lst)
    (listfun (next lst))
)

(defun next (lst)
    (cond
        ((listp (car lst))
            (cons (next (car lst)) (cdr lst))
        )
        (
            (cdr lst)
        )
    )
)


(defun listfun (lst)
    (cond
        ((Null (cdr lst))
            (cond
                ((listp (car lst))
                    (list (listfun (car lst)))
                )
                (
                    Nil
                )
            )
        )
        (
            (cons (car lst) (listfun (cdr lst)))
        )
    )
)
(defun lsplit (list)
  (lsplit2 nil nil list)
)

(defun lsplit2 (list1 list2 list)
  (cond
    ((null list) (values list1 list2))        
    (T (lsplit2 list2 (append list1 (list (car list))) (cdr list)) )
  ) )
 
(defun deln (list)
  (cond
    ((null list) nil)
    ((listp (car list)) (cons (deln (car list)) (deln (cdr list))))
    ((>= (car list) 0) (cons (car list) (deln (cdr list))))
    (T (deln (cdr list)))
  )
)
(defun rlist (l)
  (cond 
    ((null l) nil)
    ((listp (car l)) (append (rlist (cdr l)) (list (rlist (car l)))))
    (T (append (rlist (cdr l)) (list (car l))))
  )
)

(defun d(x)
    (cond
        ((Null x)
            Nil
        )
        ((symbolp (car x))
            (cond
                ((and (string< (car x) 'k) (string< 'd (car x)))
                    (d(cdr x))
                )
                (
                    (cons (car x) (d (cdr x)))
                )
            )
        )
        ((listp (car x))
            (cons (d (car x)) (d (cdr x)))
        )
        (
            (cons (car x) (d (cdr x)))
        )
    )
)


