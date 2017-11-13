// excercise.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"

#include <string>

using namespace std;

int fibb_iter(int pos,int& result);

int main(int argc, char** argv)
{

    while (true){
		cout << "Enter Fibonacci element position" << endl;
		int result=0;
		int pos = 0;
		cin >> pos;
		fibb_iter(pos,result);
	    cout << result << endl;
		cout << "Next y[es]/n[o]?";
         string a;
		cin >> a;
		if (a == "n"){
			break;
		}
	}

	return 0;
}

// Exercise 2.1
int fibb_iter(int pos,int& result){
	if (pos == 0 || pos == 1){
	    result = pos;
	    return 0;
	}

	int curr = 1, next = 1,elem =1;
	for (int i=2;i<pos;i++){
		elem  = next + curr;
		curr = next;
		next = elem;
	}
	result = elem;
	return 0;
}
