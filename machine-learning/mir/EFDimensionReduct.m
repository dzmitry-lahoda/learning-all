function componentScores = EFDimensionReduct(featuresMatix,significanceVector)
reductor = PrincipalComponentAnalyser;
componentScores = reductor.Reduct(featuresMatix,significanceVector);
end