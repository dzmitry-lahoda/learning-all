function FTestPitchExtractor
extractor = PitchExtractor;
fileArray = FGetTestingFileArray;
featureMatrix = extractor.BatchExtract(fileArray);
end