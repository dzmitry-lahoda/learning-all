classdef ScatterVisualizator < Visualizator
    methods (Static=true)
        function [figureHandle,plotHandle] = Visualize(points,colors,texts,labelCells)
            figureHandle = figure;
            l = length(points);
            pc1 = 1;
            pc2 = 2;
            if size(points,2)==1
                pc2 = 1;
            end
            plotHandle = scatter(points(:,pc1),points(:,pc2),77,colors,'filled');
            if ~isempty(texts)
                for i=1:l
                    text(points(i,pc1),points(i,pc2),texts{i});
                end
            end
            if length(labelCells) == 2
                xlabel(labelCells{1})
                ylabel(labelCells{2})
            end
        end
    end
end