classdef PrincipalComponentAnalyser < DimensionReductor
    %
    methods (Static=true)
        function componentScores = Reduct(featuresMatrix,significanceVector)
            if isempty(significanceVector)
                sl = size(featuresMatrix,2);
                significanceVector = ones(1,sl);
            end
            
            wid = size(significanceVector,2);
            if(wid == 1)
                significanceVector = significanceVector';
            end
            
            featuresMatrix(isnan(featuresMatrix))=0;
            featuresMatrix(featuresMatrix==Inf)=0;
            featuresMatrix(featuresMatrix==-Inf)=0;
            normFeatures=scalecMinMax(featuresMatrix);
            [nmusics,ndims] = size(featuresMatrix);
            significanceMatrix = repmat(significanceVector,nmusics,1);
            normFeatures = normFeatures.*significanceMatrix;
            if ndims > 2
            [coeff, componentScores] = princomp(normFeatures);
            else
                componentScores = normFeatures;
            end           
            componentScores = [componentScores(:,1) componentScores(:,2)];
        end
    end
end