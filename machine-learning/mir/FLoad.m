function [featuresMatrix, urlArray,descriptionsCells] = FLoad(fileFeaturePath,fileAudioPath,descriptionsPath)
featuresMatrix = load(fileFeaturePath);
urlArray = textread(fileAudioPath,'%s','delimiter','\n','whitespace','');
descriptionsCells= {};
end