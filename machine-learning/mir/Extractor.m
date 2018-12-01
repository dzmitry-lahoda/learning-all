classdef Extractor
    %
    properties (Constant = true)
        %If audio is more then this value in secondes it considered to big
        %to be processed by the way like other sound pieces.
        Delimeter2 = 21600;
        %If audio is more then this value in seconds it considered big (piece of music) and
        %worth of periodic features extraction.
        Delimeter1 = 10;
        AW = 0.023; %Analysis window in seconds
        TW = 1;%Texture window in seconds
    end
    properties
        Reader = WavReader;
        Optimizator = HardOptimizator;
    end
    properties(Abstract=true)
        Descriptions
    end
    methods (Abstract)
        [featuresArray] = Extract(obj,cAudio)
    end
    methods
        function [featuresMatrix] = BatchExtract(obj,fileArray)
            featuresMatrix = [];
            l = length(fileArray);
            tic
            for i=1:l
                tic
                disp(['Currently extracting song(' int2str(i) '/' int2str(l) '): ' fileArray(i).url])
                disp(obj)
                url = fileArray(i).url;
                audio = obj.Reader.Read(url);
                [newData,sf] = obj.Optimizator.Reap(audio.Data, audio.SamplingFrequency);
                newAudio = Audio(audio.URL,newData,sf)    ;
                featuresArray = obj.Extract(newAudio);
                featuresMatrix = [featuresMatrix ; featuresArray ];
                toc
            end
            toc
        end
    end
end