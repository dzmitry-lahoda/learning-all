function FTestRhythmExtractor
extractor = RhythmExtractor;
fileArray = FGetTestingFileArray;
featureMatrix = extractor.BatchExtract(fileArray);
end