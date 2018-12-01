function FTestMFCCExtractor
extractor = MFCCExtractor;
fileArray = FGetTestingFileArray;
featureMatrix = extractor.BatchExtract(fileArray);
end