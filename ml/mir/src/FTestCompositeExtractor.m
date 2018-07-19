function FTestCompositeExtractor
fileArray = FGetTestingFileArray;
featuresMatrix=CompositeExtractor.GetDefault().BatchExtract(fileArray);
end