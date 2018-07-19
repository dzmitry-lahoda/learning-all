classdef  Optimizator
%
    methods (Abstract)
        [data,sf] = Downsample(obj,data,sf)
        [reapedData,sf] = Reap(obj,data,sf)
    end
    methods 
        function [data,sf] = Minimize(obj,data,sf)
            [data,sf] = Reap(data);
            [data,sf] = Downsample(data,sf);
        end
    end
end