% b is true when a is false
a(0) .
% \+ is NOT
b(X) :- \+ a(X).

% ?- b(0) false
% ?- b(1) true