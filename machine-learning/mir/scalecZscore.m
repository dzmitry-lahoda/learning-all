function [sm,sc] = scalecZscore(m)
%
sm = zeros(size(m));
shift = mean(m);
for i=1:size(m,2)
    sm(:,i) = m(:,i)-shift(i);
    d = std(sm(:,i));
    sm(:,i)= sm(:,i)/d;
end
end
