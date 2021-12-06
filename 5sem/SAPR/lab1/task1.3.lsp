(defun get_first_digit (N)
(cond
((< N 10) N)
(t (get_first_digit (floor (/ N 10))))
)
)

(defun get_all_divisors (N i)
(cond 
((zerop i) nil)
((zerop (rem N i)) (cons i (get_all_divisors N (- i 1))))
(t (get_all_divisors N (- i 1)))
)
)

(defun count_digits (N)
(cond
((null N) nil)
((< N 10) 1)
(t (+ 1 (count_digits (floor (/ N 10)))))
)
)

(defun sum_digits (N)
(cond
((null N) nil)
((< N 10) N)
(t (+ (rem N 10) (sum_digits (floor (/ N 10)))))
)
)

(defun sum_and_count_digits (N)
(cons (sum_digits N) (count_digits N))
)

(defun get_all_common_divisors (N M i)
(cond 
((zerop i) nil)
((and (zerop (rem M i)) (zerop (rem N i))) (cons i (get_all_common_divisors N M (- i 1))))
(t (get_all_common_divisors N M (- i 1)))
)
)

(defun gcd_of_list (mylst)
    (cond ((null mylst) nil)
          ((null (cdr mylst)) (car mylst))
          ((gcd (car mylst) (gcd_of_list (cdr mylst))))))
