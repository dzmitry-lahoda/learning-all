 src = 'D:\FLASH\MULTIMEDIA\SUPER\OutPut\';
 dst = 'D:\WORK\MIR\Samples\cool\birds\';
 
 list = dir([src '*.wav']);
 l = length(list);
for i=1:l
    f= list(i);
    name = [src f.name];
[wav,fs]=wavread(name);
wavwrite(wav(50*fs:80*fs),fs,16,[dst f.name]);
%wavwrite(wav(25*fs:30*fs),fs,16,[dst f.name '5']);
end