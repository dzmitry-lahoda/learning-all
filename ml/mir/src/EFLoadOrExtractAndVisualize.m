directory = 'D:\WORK\MIR\Samples\cool\';
fileFeaturePath = 'D:\WORK\MIR\Work\MIRBase\Results\features.dat';
fileAudioPath = 'D:\WORK\MIR\Work\MIRBase\Results\audios.dat';
descriptionsPath = 'D:\WORK\MIR\Work\MIRBase\Results\descriptions.dat';
restore =  true;
extractor = CompositeExtractor.GetDefault();
if ~restore
    [featuresMatrix, fileArray] = FExtract(directory, extractor);
    FSave(fileFeaturePath,featuresMatrix,fileAudioPath,fileArray)
end
[featuresMatrix, urlCells,descriptionsCells] = FLoad(fileFeaturePath, fileAudioPath,descriptionsPath);
sv = ones(1,size(featuresMatrix,2));
sv(11)=0;
sv(22)=0;
sv(33)=0;
sv(44)=0;
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

ScatterVisualizator.Visualize(scores,colors,nameCells)