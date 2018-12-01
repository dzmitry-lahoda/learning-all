classdef PitchExtractor < Extractor
    %
    properties
        Descriptions='inharmonicity::2 folded_pitch_histo::12 pitch_tzan::5';
    end

    methods
        function [featuresArray] = Extract(obj, cAudio)
            featuresArray = [];
            data = cAudio.Data;
            sf = cAudio.SamplingFrequency;
            audio = miraudio(data,sf);

            frames = mirframe(audio,4);
            cuts = mirgetdata(frames);
            c = min(size(cuts));
            ph = [];
            phs = [];
            imn = [];
            sd = [];
            for i=1:2:c
                cut= cuts(:,i);
                cutAudio = miraudio(cut,cAudio.SamplingFrequency,'Normal');
                pitches = mirpitch(cutAudio,'Frame','Total',3);
                inharm = mirinharmonicity(cutAudio,'Frame');
                stats = mirstat(inharm);
                mn = stats.Mean;
                sd = stats.Std;
                imn = [imn mn];
                sd = [imn sd];
                ph = mgd(pitches);
                sz = min(size(ph));
                ph = reshape(ph,1,sz*length(ph));
                phs = [phs ph];
            end
            %featuresArray =[featuresArray mean(imn) mean(isd)];
            
            phs(isnan(phs))=0;
            phs = nonzeros(phs);

            unfold = 12 * log2 (phs/440)+ 69;

            c =  mod(unfold,12);

            fold = mod(7*c,12);

            ue = 15:1:140;
            uph = histc(unfold, ue);

            fe = 0:1:12;
            fph = histc(fold, fe);           
            %featuresArray = [featuresArray fph];
            
            fs = sum(fph);
            us = sum(uph);

            [fa0, i0 ] = max(fph);
            fph(i0) = 0;
            [fa1, i1 ] = max(fph);
            fph(i0) = fa0;

            upo1 = i0 - i1;

            ua0 = max(uph);

            fp0 = fa0 / fs;
            up0 = ua0 / us;

            s = length(fold);

            nfa = [fa0 upo1 fp0 up0 s]./cAudio.Duration;

            featuresArray = [featuresArray nfa];
        end
    end
end