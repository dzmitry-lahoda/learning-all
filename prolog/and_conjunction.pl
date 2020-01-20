% likes_both_mix(water,juice).
% true
% likes_both_mix(water, vodka).
% false 


likes(water).
likes(juice).

likes_both_mix(X,Y) :- likes(X) , likes(Y). 



