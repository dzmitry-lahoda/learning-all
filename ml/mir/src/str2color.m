function c = str2color(str)
%TODO: Alternate - use all string to make much variance
%or to use predefined color map.
js = java.lang.String(str);
h = js.hashCode;
h = h/2+java.lang.Integer.MAX_VALUE/2;
d = 256;
c.arr =zeros(3,1);
for i=1:3
n =floor(h/d);
rem = h - n*d;
h = h - rem;
c.arr(i) = floor(rem);
h = floor(h/d);
end
c.r = c.arr(1);
c.g = c.arr(2);
c.b = c.arr(3);
end
