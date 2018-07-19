classdef StatsExtractor < Extractor
    %TZANETAKIS 2002
    properties
        Descriptions = 'zero_crossing_rate::2 centroid::2 spread::2 rolloff::2 flux::2 skewness::2 flatness::2 entropy::2 lowenergy::1';
    end

    methods
        function [featuresArray] = Extract(obj, cAudio)
            featuresArray = [];
            data = cAudio.Data;
            sf = cAudio.SamplingFrequency;
            audio = miraudio(data,sf);

            fr = mirframe(audio,Extractor.TW);
            frfr = mirframe(fr);

            zcr = mirzerocross(frfr);
            dzcr = mgd(zcr);
            mnzcr = mean(dzcr);
            sdzcr = std(dzcr);
            featuresArray = mnzcr;
%            featuresArray = [featuresArray mnzcr sdzcr];

%             cntrd = mircentroid(frfr);
%             dcntrd = mgd(cntrd);
%             mncntrd = mean(dcntrd);
%             sdcntrd = std(dcntrd);
%             featuresArray = [featuresArray mncntrd sdcntrd];
% 
%             sprd = mirspread(frfr);
%             dsprd = mgd(sprd);
%             mnsprd = mean(dsprd);
%             sdsprd = std(dsprd);
%             featuresArray = [featuresArray mnsprd sdsprd];
% 
%             rolloff = mirrolloff(frfr);
%             drolloff = mgd(rolloff);
%             mnrolloff = mean(drolloff);
%             sdrolloff = std(drolloff);
%             featuresArray = [featuresArray mnrolloff sdrolloff];
% 
%             flux = mirflux(frfr);
%             dflux = mgd(flux);
%             mnflux = mean(dflux);
%             sdflux = std(dflux);
%             featuresArray = [featuresArray mnflux sdflux];
% 
%             skewness = mirskewness(frfr);
%             dskewness = mgd(skewness);
%             mnskewness = mean(dskewness);
%             sdskewness = std(dskewness);
%             featuresArray = [featuresArray mnskewness sdskewness];
% 
%             flatness = mirflatness(frfr);
%             dflatness = mgd(flatness);
%             mnflatness = mean(dflatness);
%             sdflatness = std(dflatness);
%             featuresArray = [featuresArray mnflatness sdflatness];
% 
%             entr = mirentropy(frfr);
%             dentr = mgd(entr);
%             mnentr = mean(dentr);
%             sdentr = std(dentr);
%             featuresArray = [featuresArray mnentr sdentr];
% 
%             le = mirlowenergy(audio);
%             featuresArray = [featuresArray mgd(le)];
        end
    end
end