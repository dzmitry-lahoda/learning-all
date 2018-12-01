classdef RhythmExtractor < Extractor
    %TZANETAKIS 2002
    properties
        Descriptions='rhytm_histo::20 rhytm_tzan::6';
    end

    methods
        function [featuresArray] = Extract(obj, cAudio)
            featuresArray = [];
            data = cAudio.Data;
            sf = cAudio.SamplingFrequency;
            audio = miraudio(data,sf);
            [featureStruct] = rp_extract('func','process_audiofile',cAudio.URL);
            rhythmHistogram = featureStruct.rh;

            s = sum(rhythmHistogram);

            [a0, i0] = max(rhythmHistogram);
            rhythmHistogram(i0)=0;
            [a1, i1] = max(rhythmHistogram);

            rhythmHistogram(i0) = a0;

            ra = a1/a0;

            a0 = a0/s;
            a1 = a1/s;

            

            rhythmHistogram = reshape(rhythmHistogram, 3, 20);
            rhythmHistogram = sum(rhythmHistogram);
            %featuresArray = [featuresArray rhythmHistogram];
            
            %featuresArray = [featuresArray a0 a1 ra i0 i1 s ];
            featuresArray = i0;

        end
    end
end