function fileArray= FGetTestingFileArray
directory = 'D:\WORK\MIR\Work\MIRBase\Testing\TestSamples\small\';
directory = 'D:\WORK\MIR\Work\MIRBase\Testing\TestSamples\diff\';
fileArray = FGetFileArray(directory,'*.wav',[]);
end