classdef Visualizator
    methods (Static=true, Abstract =true)
        [figureHandle,plotHandle] = Visualize(points,colors,texts,labelCells)
    end
end