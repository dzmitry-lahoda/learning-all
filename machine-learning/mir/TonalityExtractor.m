classdef TonalityExtractor < Extractor
    %
    properties
        Descriptions='chromagram:12 mode hcdf';
    end

    methods
        function [featuresArray] = Extract(obj, cAudio)
            featuresArray = [];
            data = cAudio.Data;
            sf = cAudio.SamplingFrequency;
            audio = miraudio(data,sf);

            mmns = [];
            mstd = [];
            kmns = [];
            kstd = [];

            fr = mirframe(audio,Extractor.TW);
            chromagram = mirchromagram(fr,'Normal');
            keys = mirkeystrength(chromagram);
            key = mirkey(keys);
            mode = mirmode(chromagram);
            z = mgd(mode);
            za = mgd(key);

            stats = mirstat(mode);
            mns =stats.Mean;
            std = stats.Std;
            mmns = [mmns mns];
            mstd = [mstd std];
            stats = mirstat(key);
            mns =stats.Mean;
            std = stats.Std;
            kmns = [kmns mns];
            kstd = [kstd std];

            featuresArray = [mmns mstd kmns kstd];

            frames = mirframe(audio,6);
            cuts = mirgetdata(frames);
            c = min(size(cuts));
            hmns = [];
            hstds = [];
            for i=1:2:c
                cut= cuts(:,i);
                cutAudio = miraudio(cut,cAudio.SamplingFrequency,'Normal');

                fr = mirframe(cutAudio,Extractor.TW,0.1);
                hcdf = mirhcdf(fr);
                stats = mirstat(hcdf);
                hmn =stats.Mean;
                hstd = stats.Std;

                hmns =[hmns hmn];
                hstds = [hstds hstd];
            end

            featuresArray = [mean(hmns) mean(hstds)];

        end
    end
end