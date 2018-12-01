function mfccFileArray = mcmfcccalc1(audioFileArray)
nOfFiles = length(audioFileArray);
file.url = '';
mfccFileArray = [];
for i=1:nOfFiles
   try 
   audio = miraudio(audioFileArray(i).url,'Label',0);
   catch
      dlmwrite([audioFileArray(i).url '.broken'],[]);
   end
   frames = mirframe(audio,'Hop',10,'%');
   mfcc = mirmfcc(frames,'Rank',1:6);
   data = mgd(mfcc);
   file.url = [audioFileArray(i).url '.mfcc'];
   mfccFileArray = [mfccFileArray file];
   dlmwrite(file.url,data,'delimiter',' ');
end
end