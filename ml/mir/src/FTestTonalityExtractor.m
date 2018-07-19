function FTestTonalityExtractor
extractor = TonalityExtractor;
fileArray = FGetTestingFileArray;
featureMatrix = extractor.BatchExtract(fileArray);
end