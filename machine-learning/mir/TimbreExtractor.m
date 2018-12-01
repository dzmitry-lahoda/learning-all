classdef TimbreExtractor < Extractor
    %
    properties
        Descriptions='roughness attacktime::2 attackslope::2 brightness regularity';
    end

    methods
        function [featuresArray] = Extract(obj, cAudio)
            featuresArray = [];
            audio = miraudio(cAudio.Data,cAudio.SamplingFrequency,'Normal');

            frames = mirframe(audio,6);
            cuts = mirgetdata(frames);
            c = min(size(cuts));
            ats = [];
            ass = [];
            rs = [];
            for i=1:2:c
                cut= cuts(:,i);
                cutAudio = miraudio(cut,cAudio.SamplingFrequency,'Normal');
                onsets = mironsets(cutAudio,'Attacks');
                attacks = mironsets(onsets);
                attacktime = mirattacktime(attacks);
                attackslope = mirattackslope(attacks);
                r = mirroughness(cutAudio);
                rs = [rs mean(mgd(r))];
                ats = [ats mgd(attacktime)'];
                ass = [ass mgd(attackslope)'];
            end

            featuresArray = [featuresArray mean(rs)];

            featuresArray = [featuresArray mean(ats)];
            featuresArray = [featuresArray std(ats)];

            featuresArray = [featuresArray mean(ass)];
            featuresArray = [featuresArray std(ass)];

            brightness = mgd(mirbrightness(audio));
            featuresArray = [featuresArray brightness];

            regularity = mgd(mirregularity(audio));
            featuresArray = [featuresArray regularity];
        end
    end
end