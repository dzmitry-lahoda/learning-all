somShow=false;
if somShow
    figure
    sD = som_data_struct(normFeatures','name','Demo3 data');
    sM = som_make(sD);
    somHits=som_hits(sM,sD,'kernel');
    [Pd,V,me] = pcaproj(sD.data,2);        % project the data
    Pm        = pcaproj(sM.codebook,V,me); % project the prototypes
    colormap(hot);                         % colormap used for values
    som_grid(sM,'Coord',Pm,'Linecolor','k');

    somSize= 10;
    clusters = CSom.SomTraining(normFeatures,somSize,somSize,2);
    meanClusters = mean(clusters,2);
    labels = cell(somSize*somSize,1);


    for i=1:size(normFeatures,1)
        currentvalue =  mean(normFeatures(i));
        index=1;
        for j=2:length(meanClusters)
            if abs(currentvalue-meanClusters(j))<abs(currentvalue-meanClusters(index))
                index=j;
            end
        end
        labels(index)={strvcat(labels{index},nameCells{i})};
    end

    figure
    plot(1:somSize,1:somSize)
    for i=1:length(labels)
        x=1+floor(i/somSize);
        y=mod(i,somSize);
        labels(i)={strvcat(labels{i},'')};
        text(x,y,labels{i});
    end
end