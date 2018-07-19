classdef Audio
    properties
        URL;
        Data;%Vector value with file data.
        SamplingFrequency;%Sampling rate
    end
    properties (Dependent)
        Name
        Duration
        SN
    end
    methods
        function name = get.Name(obj)
            [path, name, ext, version] = fileparts(obj.URL);
        end
        function sn = get.SN(obj)
            sn = length(obj.Data);
        end
        function duration = get.Duration(obj)
            duration = obj.SN/obj.SamplingFrequency;
        end
    end
    methods
        function obj = Audio(url,audioMatrix,sf)%
            obj.Data = Audio.CombineChanells(audioMatrix);
            obj.SamplingFrequency = sf;
            obj.URL = url;
        end
    end
    methods (Static=true)
        function audioList = Read(fileUrls)
            audioList = [];
            for i=1:length(fileUrls)
                fileUrl = fileUrls(i);
                audio = Audio(fileUrls);
                audioList = [audioList ; audio ];
            end
        end
    end
    methods (Access =private, Static=true)
        function data = CombineChanells(audioMatrix)%Creates one chanell audio file from several chanells.
            data = sum(audioMatrix,2)./length(audioMatrix);
        end
    end

end