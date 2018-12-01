function [fileArray] = FGetFileArray(directory,ext,depth)
if isempty(ext);
    ext = '*.*';
end
if isempty(depth);
    depth = 1;
end
urls = fuf([directory ext],'detail');
fileArray = [];
for i=1:length(urls)
    url = urls{i};
    file = dir(url);
    file.url = url;
    fileArray = [fileArray file];
end
end
