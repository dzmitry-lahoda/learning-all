classdef DimensionReductor
    %
    properties
        Description='all';
    end
    methods (Static=true, Abstract = true)
        componentScores = Reduct(featureMatix)
    end
end