#include <stdio.h>
#include <assert.h>
#include <stdlib.h>
#include <iostream>
#include "7.h"
#include "2.h"

int randomized_select(int* a,int q,int r, int i,int (*choose_pivot)(int*,int,int)){
	if (q==r) return a[q];
	int pivot = choose_pivot(a,q,r);
	if (pivot!=q) {
		int buf = a[pivot];
		a[pivot] = a[q];
		a[q] = buf;
		pivot = q;
	}
    int newPivot = partition(a,q,r);
	
	if (i<newPivot) return randomized_select(a,q,newPivot-1,i,choose_pivot);
	if (i>newPivot) return randomized_select(a,newPivot+1,r,i,choose_pivot);
	return a[newPivot];
}