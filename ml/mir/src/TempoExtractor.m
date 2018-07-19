classdef TempoExtractor < Extractor
    %
    properties
        Descriptions='tempo::2';
    end

    methods
        function [featuresArray] = Extract(obj, cAudio)
            featuresArray = [];
            data = cAudio.Data;
            sf = cAudio.SamplingFrequency;
            audio = miraudio(data,sf);
            
            %TODO:Use N seconds cuts
            frames = mirframe(audio,6);
            cuts = mirgetdata(frames);
            c = min(size(cuts));
            fts = [];
            for i=1:1:c
                cut= cuts(:,i);
                cutAudio = miraudio(cut,cAudio.SamplingFrequency,'Normal');
                tempos = mirtempo(cutAudio,'Total',1);
                nfts = mirgetdata(tempos);
                nfts(isnan(nfts))=0;
                fts = [fts nfts];
            end

            featuresArray = [featuresArray mean(fts)];
            %featuresArray = [featuresArray std(fts)];

        end
    end
end