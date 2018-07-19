function FSave(fileFeaturePath,featuresMatrix,fileAudioPath,fileArray)
save(fileFeaturePath,'featuresMatrix','-ascii','-tabs');

audiosFileId = fopen(fileAudioPath, 'w+', 'n', 'UTF-8');
for i=1:length(fileArray)
    url = fileArray(i).url; 
    fwrite(audiosFileId,[url sprintf('\n')],'char');
end
fclose(audiosFileId);
end