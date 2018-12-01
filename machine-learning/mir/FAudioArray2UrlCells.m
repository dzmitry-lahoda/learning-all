function urlCells = FCAudioArray2UrlCells(cAudioArray)
%
l = length(cAudioArray);
urlCells = cell(l,1);
for i=1:l
    urlCells{i} = cAudioArray(i).URL;
    qwe = urlCells{i} ;
end
end