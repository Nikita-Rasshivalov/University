(defun isNumList (llist)
	(cond
		((null llist) T)
		((numberp (car llist)) (isNumList (cdr llist)))
		(t nil)
	)
)

(defun findMinPos (mylist)
	(cond
		((listp (car mylist)) (min (findMinPos (car mylist)) (findMinPos (cdr mylist))))
		((null (cdr mylist)) (car mylist))  
		((plusp (car mylist)) (min (car mylist) (findMinPos (cdr mylist))))
		(t (findMinPos (cdr mylist)))
	)
)
(defun funcMP (llist)
	(cond 
		((null (cadr llist)) T)
		((> (car llist) (cadr llist)) nil)
		(t (funcMP (cdr llist)))
	)
)
(defun a-merge (w v)
  (cond ((null w) v) ((null v) w)
        ((<= (car w) (car v)) (cons (car w) 
                             (a-merge (cdr w) v)))
        ((cons (car v) (a-merge w (cdr v))))))

(defun sumMaxMin (llist) 
(+ (findMax (cadr (separ llist))) (findMin (car (separ llist)))) 
) 
(defun separ (llist &optional listA listB) 
(cond 
((null llist) (list listA listB)) 
((null (cdr llist)) (separ (cdr llist) (cons (car llist) listA) listB)) 
(t (separ (cddr llist) (cons (car llist) listA) (cons (cadr llist) listB))) 
) 
) 
(defun findMin (llist) 
(apply #'min llist ) 
) 
(defun findMax (llist) 
(apply #'max llist ) 
)
(defun funcV (a n)
	(* a (- a n) (- a (* 2 n)) (- a (* n n)))
)

(defun my-sin (x &optional (eps 1E-15) (s x) (n 1) (a x))
(let ((a (- 0.0 (/ (* a x x) (+ n 1) (+ n 2)))))
(if (<= (abs a) eps) s (my-sin x eps (+ s a) (+ n 2) a))))

(defun calcPosl (EPS &optional (i 1))
	(cond 
		((< (/ 1.0 i) EPS) 0)
		(t (cond
				((= 1 (mod i 2)) (+ (/ 1.0 i) (calcPosl EPS (+ 1 i))))
				((= 0 (mod i 2)) (- (/ -1.0 i) (calcPosl EPS (+ 1 i))))
			)
		)
	)
)

(defun mult (list) 
(cond 
((null (cdr list)) (car list)) 
((listp (car list)) (cons (mult (car list)) (mult (cdr list))) ) 
((= (car list) 0) (mult (cdr list))) 
(t (* (car list) (mult (cdr list)))) 
) 
) 


(defun equation (x)
(cond
(
(numberp x) (- (+ x (log (+ x 0.5))) 0.5)
)
(nil)
)
)

(defun dichotomy_roots (left_border right_border &optional (eps 0.0001))
(cond
((< (- right_border left_border) eps) (+ left_border (/ (- right_border left_border) 2)))
(
(minusp (* (equation left_border) (equation (+ left_border (/ ( - right_border left_border) 2))))) (dichotomy_roots left_border (+ left_border (/ (- right_border left_border) 2)) eps)
)
(
(dichotomy_roots (+ left_border (/ (- right_border left_border) 2)) right_border eps)
)
)
)

(defun main (left_border right_border &optional (eps 0.001))
(cond
((not (minusp (* (equation left_border) (equation right_border)))) nil)
((dichotomy_roots left_border right_border))
)
)

(print (main 0 5))

