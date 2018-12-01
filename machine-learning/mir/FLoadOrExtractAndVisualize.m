function FLoadOrExtractAndVisualize
directory = 'D:\WORK\MIR\Samples\cool\';
directory = 'D:\WORK\MIR\Samples\real\';
fileFeaturePath = 'D:\WORK\MIR\Work\MIRBase\Results\featuresReal.dat';
fileAudioPath = 'D:\WORK\MIR\Work\MIRBase\Results\audiosReal.dat';
descriptionsPath = 'D:\WORK\MIR\Work\MIRBase\Results\descriptionsReal.dat';
restore =  false;
if ~restore
    extractor =CompositeExtractor.GetDefault();
    [featuresMatrix, fileArray] = FExtractAll(directory, extractor);
    FSave(fileFeaturePath,featuresMatrix,fileAudioPath,fileArray)
end
[featuresMatrix, urlCells,descriptionsCells] = FLoad(fileFeaturePath, fileAudioPath,descriptionsPath);
sv = ones(1,size(featuresMatrix,2));
nameCells = FUrlCells2NameCells(urlCells);
reductor = PrincipalComponentAnalyser;
scores = reductor.Reduct(featuresMatrix,sv);
l = size(scores,1);
colors = zeros(l,3);
for i=1:l
    c = url2color(urlCells{i});
    arr = c.arr;
    colors(i,:)=arr(:)/256;
end
labelCells{1} = 'Tempo';
labelCells{2} = 'Zero Crossing Rate'; 
figureId = ScatterVisualizator.Visualize(scores,colors,[],labelCells);

end