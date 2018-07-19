function [featuresMatrix, fileArray ] = FExtractAll(directory,extractor)
if isempty(extractor)
    extractor = CompositeExtractor.GetDefault();
end
fileArray = FGetFileArray(directory,'*.*',1);
featuresMatrix=extractor.BatchExtract(fileArray);
end