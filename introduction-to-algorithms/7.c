#include <stdio.h>
#include <assert.h>
#include <stdlib.h>
#include <iostream>
#include "7.h"
#include "2.h"

int choose_pivot_first(int* a,int q,int r){
      return q;
}

int choose_pivot_last(int* a,int q,int r){
      return r;
}

int choose_pivot_random(int* a,int q,int r){
      int range = r- 1;
      return rand() % range + 1 + q;
}

int choose_pivot_median_of_three(int* a,int q,int r){
    int f = a[q];
    int l = a[r];    
    int n = r-q;
    int m = n/2+q;
    int three[] = {f,l,a[m]};
    insertion_sort(three,3);
    int median = three[1];
    if (median == f) return q;
    if (median == l) return r;
    return m;
}

static long counter = 0;
int partition(int* a,int q,int r){
  ///*

  int i = q+1; //partition lesser/greater of what seen
  int j = q+1; //seen / unseen partition
  int pivot = a[q];
  for (j; j<=r;j++){
	 //counter++;
     if (a[j]<pivot){
        int buf = a[j];
        a[j] = a[i];
        a[i] = buf;   
        i++;
     }
  }
  a[q] = a[i-1];
  a[i-1] = pivot;
  return i-1;
  //*/
  
  /*
   int compare = a[pivot];
   int right = 0;
   int newPivot = pivot;
   for (int i = q;i<=r;i++){
    if (a[i]<compare){
       int buf = a[i];      
       for (int j = i-1; j>= i - right;j--){
           a[j+1] = a[j];
           newPivot = j+1;
       }
       a[i-right]=buf;
    }   
    else
       right++;
   }
  return newPivot;   
  */
}


long quick_sort(int* a,int q,int r,int (*choose_pivot)(int*,int,int)){
	if ((r-q)<=0){ 
	   return 0;
	}
	int pivot = choose_pivot(a,q,r);
	if (pivot!=q) {
		int buf = a[pivot];
		a[pivot] = a[q];
		a[q] = buf;
		pivot = q;
	}
	int newPivot = partition(a,q,r);
	long comparisons = r-q;
	comparisons+=quick_sort(a,q,newPivot-1,choose_pivot);
	comparisons+=quick_sort(a,newPivot+1,r,choose_pivot);
	//if (q==0 && r==9999)    printf("%ld\n",counter);
	return comparisons;
}