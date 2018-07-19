function nameCells= FUrlCells2NameCells(urlCells)
%
l =length(urlCells);
            nameCells =  cell(l,1);
            for i=1:l
                url = urlCells{i};
                [path, name, ext, version] = fileparts(url);
                nameCells{i} = name;
            end
end