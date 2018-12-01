function [featuresArray ]  = EFExtract(fileUrl)
extractor = CompositeExtractor.GetDefault();
featuresArray=extractor.Extract(fileUrl);
end