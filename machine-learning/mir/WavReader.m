classdef  WavReader < Reader
%Reads data from Wav audio files.
    methods 
        function audio = Read(obj, fileUrl) %Reads data from file. Returns CAudio
            [data,sf] = mp3read(fileUrl);
            audio=Audio(fileUrl,data,sf);
        end
    end
end