classdef CompositeExtractor < Extractor
    %
    properties
        Descriptions='all';
    end
    properties (SetAccess=private,GetAccess=private)
        Extractors=[];
    end
    methods
        function obj = CompositeExtractor(extractorCellArray)
            obj.Extractors = extractorCellArray;
        end
    end
    methods (Static=true)
        function extractor = GetDefault()
            extractor = CompositeExtractor({
                %OverallExtractor ...
                TempoExtractor ...
                %RhythmExtractor ... %TODO:Use MIRToolbox.
                StatsExtractor ...
                %MFCCExtractor ...
                %PitchExtractor ...
                %TonalityExtractor...
                %TimbreExtractor...%TODO: Memory managment.
                });
        end
    end
    methods
        function [featuresArray] = Extract(obj, audio)
            featuresArray = [];
            for i=1:length(obj.Extractors)
                newFeaturesArray = obj.Extractors{i}.Extract(audio);
                featuresArray = [featuresArray newFeaturesArray];
            end
        end
    end
end