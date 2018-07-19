function [featureArray] = cutExtract(audio,@fun,
frames = mirframe(audio,6,0.3);
cuts = mirgetdata(frames);
c = min(size(cuts));
ats = [];
ass = [];
for i=1:3:c
    cut= cuts(:,i);
    cutAudio = miraudio(cut,cAudio.SamplingFrequency,'Normal');
    onsets = mironsets(cutAudio,'Attacks');
    attacks = mironsets(onsets,'Attacks');
    attacktime = mirattacktime(attacks);
    attackslope = mirattackslope(attacks);
    ats = [ats mirgetdata(attacktime)'];
    ass = [ass mirgetdata(attackslope)'];
end



%atsHist = histc(ats,0:0.03:0.27)./length(ats);
%assHist = histc(ass,0:2:10)./length(ass);
featuresArray = [featuresArray mean(ats)];
featuresArray = [featuresArray mean(ass)];
featuresArray = [featuresArray std(ats)];
featuresArray = [featuresArray std(ass)];
end