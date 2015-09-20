% likes_any_mix(water,juice).
% true
% likes_any_mix(water, vodka).
% true
% likes_any_mix(oil, vodka).
% false
 
likes(water).
likes(juice).

likes_any_mix(X,Y) :- likes(X) ; likes(Y). 

