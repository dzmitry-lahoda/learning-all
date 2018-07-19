function mfccFileArray = mcmfcccalc(audioFileArray)
nOfFiles = length(audioFileArray);
file.url = '';
mfccFileArray = [];
for i=1:nOfFiles
   try 
    audio = miraudio(audioFileArray(i).url,'Label',0);
   %mfcc = mirmfcc(audio,'Frame','Rank',1:5);
   frames = mirframe(audio);
   mfcc = mirmfcc(frames,'Rank',1:5);
   data = mgd(mfcc);
   file.url = [audioFileArray(i).url '.mfcc'];
   mfccFileArray = [mfccFileArray file];
   dlmwrite(file.url,data,'delimiter',' ');
   catch
        dlmwrite([audioFileArray(i).url '.broken'],[]);
   end
end
end