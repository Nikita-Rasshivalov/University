[Context control constructs]
QUOTE	- return an expression unevaluated=(quote <expr>)
LAMBDA - make a function closure=(lambda <args>  <expr>...)
LET	- create local bindings=(let (<binding>...)  <expr>...)

[Control constructs]
CASE	- select by case=(case <expr>  (<value> <expr>...)  ...  [(T <expr>)])
COND	- evaluate conditionally=(cond  (<pred> <expr>...)  ...)
IF	- evaluate expressions conditionally=(if <texpr>  <expr1>  [<expr2>])
UNLESS	- evaluate only when a condition is false=(unless <texpr>  <expr>...)
WHEN	- evaluate only when a condition is true=(when <texpr>  <expr>...)

[Looping constructs]
DO	- general looping form=(do (<binding>...)  (<texpr> <rexpr>...)  <expr>...)
DOLIST	- loop through a list=(dolist (<sym> <expr> [<rexpr>])  <expr>...)
DOTIMES - loop from zero to n-1=(dotimes (<sym> <expr> [<rexpr>])  <expr>...)
LOOP	- basic looping form=(loop  <expr>...)

[Program feature constructs]
PROG	- the program feature=(prog (<binding>...)  <expr>...  (go <sym>)  ...  (return [<expr>])  ...)
PROG1	- execute expressions sequentially=(prog1  <expr1>  <expr>...)
PROG2	- execute expressions sequentially=(prog2  <expr1>  <expr2>  <expr>...)
PROGN	- execute expressions sequentially=(progn  <expr>...)
