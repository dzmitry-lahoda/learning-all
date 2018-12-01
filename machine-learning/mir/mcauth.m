function mcauth

cdir  = 'C:\auth\';
a(1) = {mcstr2jstr('barry_phillips')};
a(2) = {mcstr2jstr('curandero-curandero')};
%a(3) = {mcstr2jstr('dj_markitos')};
%a(4) = {mcstr2jstr('doc_rossi')};
%a(5) = {mcstr2jstr('dr_kuch')};
%a(6) = {mcstr2jstr('drop_trio')};
%a(7) = {mcstr2jstr('ed_martin')};
%a(8) = {mcstr2jstr('electric_frankenstein')};
%a(9) = {mcstr2jstr('falik-streaks_and_strokes')};
%a(10) = {mcstr2jstr('gerard_satamian')};

SVM = false
NB = true;
GC = false;
NN = false;
loadMfcc = false
if ~loadMfcc
[fileArray] = FGetFileArray(cdir,'*.mfcc',1);

step = 4;

[d1,c1] = getdata(1,step,fileArray,a);
[d2,c2] =  getdata(3,step,fileArray,a);
trd = [d1 ;d2];
trc = [c1 ; c2];
[d1,c1] = getdata(2,step,fileArray,a);
[d2,c2] = getdata(4,step,fileArray,a);
ted = [d1 ;d2];
tec = [c1 ;c2];

% [idx,centers] = kmeans(trd,30,'Replicates',2);
%save idx.mat idx
%save centers.mat centers
%load idx.mat idx
%load centers.mat centers
% l = length(idx);
% for cls=1:l
% trd(cls,:) = centers(idx(cls),:);
% end
dlmwrite([cdir 'training_data.txt'],trd,'delimiter',' ');
dlmwrite([cdir 'training_categories.txt'],trc,'delimiter',' ');
dlmwrite([cdir 'test_data.txt'],ted,'delimiter',' ');
dlmwrite([cdir 'test_categories.txt'],tec,'delimiter',' ');
else
trd = dlmread([cdir 'training_data.txt'],' ');
trc = dlmread([cdir 'training_categories.txt'],' ');
ted = dlmread([cdir 'test_data.txt'],' ');
tec = dlmread([cdir 'test_categories.txt'],' ');
end



if SVM
SVMStruct = svmtrain(trd, trc);
classes = svmclassify(SVMStruct, trd);
[c,cm] = confusion(classes,trc)
cm

classes = svmclassify(SVMStruct, ted);
[c,cm] = confusion(classes,tec)
cm
cp = classperf(classes,tec);
cp.CorrectRate
end

if NB
    O1 = NaiveBayes.fit(trd,trc);
    C1 = O1.predict(trd);
    cp = classperf(C1,trc);
    cp.CorrectRate

    C2 = O1.predict(ted);    
    cp = classperf(C2,tec);
    cp.CorrectRate
end

function index1 = indexOf(jstrArr,jstr)
   index1 = false;
   for i=1:length(jstrArr)
       asd = jstrArr{i};
        if jstr.contains(asd)
          index1 = i;
          return;
        end
   end
end

function [d,c] = getdata(start,step,fileArray,jstrArr)
nOfFiles = length(fileArray);
d =[];
c = [];
for i=start:step:nOfFiles
   url = fileArray(i).url;
   jurl = mcstr2jstr(url);
   index1 = indexOf(jstrArr,jurl);
   if index1>0
    cat = index1-1;
   data = dlmread(url,' '); 
      cl = 100;   
      for k =1:10;
       nd = data(1:5,1+(k-1)*cl:k*cl);
       d = [d; nd(1,:) nd(2,:) nd(3,:) nd(4,:) nd(5,:)];
       c = [c; cat]; %repmat(cat,5,1)];
      end
   end  
end
end
end
