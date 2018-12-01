classdef  Reader
%CReader Baseclass for reading data from audio files.
    methods (Abstract)
        audio = Read(obj, fileUrl)
    end
end