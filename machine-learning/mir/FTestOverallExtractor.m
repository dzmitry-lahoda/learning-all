function FTestOverallExtractor
extractor = OverallExtractor;
cAudioArray = FGetTestingCAudioArray;
featureMatrix = extractor.BatchExtract(cAudioArray);
end