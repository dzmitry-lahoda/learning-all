function mcmffcprep1(directory,depth)
%MCMFCCPREP finds MFCCs for each music peace in folder and subfolders.
%Reads wav and mp3 files.
%MFFC stored in files with name like 'music_peace_name.wav.mfcc'.
%'music_peace_name.mp3.broken' - file is stored if routin could not
%calculate MFCCs.
audioFileArray = FGetFileArray(directory,'*.wav',depth);
mfccFileArray = mcmfcccalc1(audioFileArray);
end