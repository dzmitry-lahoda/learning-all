#include "stdafx.h"
#include <stdio.h>
#include <windows.h>
#include <assert.h>
#include <string>
#include <vector>
#include <list>

using namespace std;

class custom_request;
class custom_response;

class custom_request {
	
		public:
		//typedef std::list<CAdapt<CComBSTR>> TComBSTRList; - doubly linked list
		std::list<std::string> m_ids;
		std::list<std::string> m_types;
		std::string  m_name;
		int idss;
		int typess;

		int getSize();
		void fromArray(unsigned char * buffer,int* size);
		
		void toArray(unsigned char* buffer,int size);
};



class custom_response {
	
		public:
		//NOTE: just for simplicy - in real we should store somehow column and row info
		vector<string> m_data;
		int getSize();
		void fromArray(unsigned char * bufferm,int* size);
		
		void toArray(unsigned char* buffer,int size);
};