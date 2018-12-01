function [sm,sc] = scalecMinMax(m)
%
sm = zeros(size(m));
mx = max(m);
mn = min(m);
for i=1:size(m,2)
    sm(:,i) = m(:,i)-mn(i);
    d = mx(i) - mn(i);
    sm(:,i)= sm(:,i)/d;
end
end
