function FTestTimbreExtractor
extractor = TimbreExtractor;
fileArray = FGetTestingFileArray;
featureMatrix = extractor.BatchExtract(fileArray);
end