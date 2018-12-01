function tf = contains(vector,value)
%
tf=false;    
    for i=1:length(vector)
        if value == vector(i)
            tf = true;
            return;
        end
    end
end