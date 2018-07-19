[wavBet,fs1]=wavread('bet3');
[wavNs,fs2]=wavread('ns2');
figure
subplot(3,1,1), plot(wavBet)
subplot(3,1,2), plot(wavNs)
betl = length(wavBet);
nsl = length(wavNs);
adding = zeros(betl-nsl,1);
wavNs = [wavNs' adding']';
wavNs = wavNs./10;
wavMix = (wavNs+wavBet)./2;
subplot(3,1,3), plot(wavMix)
wavwrite(wavMix,fs2,16,'E:\mix.wav');