classdef OverallExtractor < Extractor
    %Lillie 2008
    properties
        Descriptions='number_of_segments song_duration segment_duration';
    end

    methods
        function [featuresArray] = Extract(obj, cAudio)
            featuresArray = [];
            data = cAudio.Data;
            sf = cAudio.SamplingFrequency;
            %audio = miraudio(data,sf);
            %segments = mirgetdata(mirsegment(audio));            
            
            %numberOfSegments = length(segments);
            %featuresArray =[featuresArray numberOfSegments];
            
            sd = cAudio.Duration;
            featuresArray =[featuresArray sd];

           % segmentDuration = sd/numberOfSegments;
            %featuresArray =[featuresArray segmentDuration];
        end
    end
end