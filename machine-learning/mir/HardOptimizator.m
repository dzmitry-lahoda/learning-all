classdef  HardOptimizator < Optimizator
    %
    methods
        function [downsampledData,newSf] = Downsample(obj,data,sf)
            downsampledData = data;
            newSf = sf;
        end
        function [reapedData,sf] = Reap(obj,data,sf)
            l = size(data,1);
            d = l/sf;
            
            if d>45
                step = floor(l/3);
                reapedData = [];
                pos = 1;
                for i=1:3
                    addData = data(pos:pos+15*sf,1);
                    reapedData = [reapedData ; addData];
                    pos = pos + step;
                end
            else
               reapedData = data;
            end
        end
    end
end