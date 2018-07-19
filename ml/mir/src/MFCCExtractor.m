classdef MFCCExtractor < Extractor
    %
    properties
        Descriptions='mfcc_mean::5 mfcc_std::5';
    end

    methods
        function [featuresArray] = Extract(obj, cAudio)
            featuresArray = [];
            data = cAudio.Data;
            sf = cAudio.SamplingFrequency;
            audio = miraudio(data,sf);

            frames = mirframe(audio,6);

            cuts = mirgetdata(frames);
            c = min(size(cuts));
            mfccs = [];
            for i=1:2:c
                cut= cuts(:,i);
                cutAudio = miraudio(cut,cAudio.SamplingFrequency,'Normal');
                mfcc = mirmfcc(cutAudio,'Frame');
                mfccs = [mfccs mirgetdata(mfcc)];
            end
            means = mean(mfccs,2);
            stds = std(mfccs,0,2);
            featuresArray = [featuresArray means(1:5)'];
            featuresArray = [featuresArray stds(1:5)'];
        end
    end
end