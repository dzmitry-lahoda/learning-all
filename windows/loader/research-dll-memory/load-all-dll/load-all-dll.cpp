

#include "stdafx.h"
#include "stdafx.h"
#include <Windows.h>
#include <stdio.h>

//TODO: for better precision preload windows dlls (rpc, user, ole, net, etc) before basing on load-all-dll.preload file list
int _tmain(int argc, _TCHAR* argv[])
{
	char userInput[MAX_PATH];
	wchar_t currentDir[MAX_PATH];
	DWORD hr;
	hr = GetCurrentDirectory(MAX_PATH,currentDir);
    if (hr == 0)
		puts("Failed to get current directory");
	wcscat(currentDir,_TEXT("\\*"));
	wprintf(_TEXT("%s"),currentDir);    
	gets(userInput);
	FILE* report = fopen("load-all-dll.csv","w");

	WIN32_FIND_DATA file;
	HANDLE hFind = INVALID_HANDLE_VALUE;

	hFind = FindFirstFile(currentDir,&file);

	do
	{
		if (!(file.dwFileAttributes & FILE_ATTRIBUTE_DIRECTORY))
		{
		   auto ext = wcsstr(file.cFileName,_TEXT(".dll"));
		   if (ext != NULL && (ext[4] == NULL))
		   {			   

			   HINSTANCE hModule = LoadLibrary(file.cFileName);
			   if (hModule == NULL)
			   {
				   wprintf(_TEXT("Failed to load %s \n"),file.cFileName);
			   }
			   else
			   {
				   LARGE_INTEGER fileSize;
				   fileSize.LowPart = file.nFileSizeLow;
				   fileSize.HighPart = file.nFileSizeHigh;
				   wprintf(_TEXT("Loaded %s \t %ld \n"),file.cFileName,fileSize.QuadPart);
				   fwprintf(report,_TEXT("%s \t %ld \n"),file.cFileName,fileSize.QuadPart);
				   
			   }
		   }
		}
	}
	while (FindNextFile(hFind,&file) != 0);

    FindClose(hFind);
	gets(userInput);

	return 0;
}

