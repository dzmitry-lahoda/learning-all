
#lang racket
(define (fib n)
  (if (or (= n 0) (= n 1)) n
          (+ (fib(- n 1)) (fib (- n 2)))
  )
)


(fib 4)

(define (flatten seq)
  (cond ((null? seq) '())
        ((list? (car seq))
          (append (flatten (car seq)) 
                  (flatten (cdr seq))))
        (else (cons (car seq) (flatten (cdr seq)))) 
        )
)

(define (flatten2 seq)
  (cond ((null? seq) '())
        ((list? (car seq))
          (append (flatten (car seq)) 
                  (flatten (cdr seq))))
        (else (cons (car seq) (flatten (cdr seq)))) 
        )
)

(define (comp-num num-list)
  (<= num-list)
  )

(define (loop2 list operation newlist)
  (if (= (length list) 0) 
      newlist
      (loop2 (cdr list) operation  (append newlist (cons (operation (car list) ) `() )) )
   )
)

(define (loop1 list operation)
  
   (loop2 list double `())
    )

(define (mum fn seq)
  (if (null? seq) `()
      (cons (fn (car seq)) (mum fn (cdr seq)))
  )
)


(define (double num) (* 2 num))

(mum double `(1 2 3))
(loop1 `(1 2 3) double)
  
  


