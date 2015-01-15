#include "utils.h"
#include "2.h"
#include <stdlib.h>
#include <stdio.h>
#include <assert.h>
#include </usr/include/linux/audit.h>



void insertion_sort_desc(int a[],int n){
	for(int j=1;j<n;j++){
		int key=a[n-1-j];
		int i=n-j;		
		while (i<n && a[i]>key){
			a[i-1]=a[i];			
			i++;
		}
		a[i-1]=key;
		}
}

void insertion_sort(int a[],int n){
	for(int j=1;j<n;j++){
		int key=a[j];
		int i=j-1;		
		while (i>=0 && a[i]>key){
			a[i+1]=a[i];			
			i--;
		}
		a[i+1]=key;
		}
}

void merge_sort(int* A,int p,int r,int n,bool output,unsigned long* count){
	if (p < r){ // p != r
		int q = (p + r)/2;
		merge_sort(A,p,q,n,output,count);
		merge_sort(A,q+1,r,n,output,count);
		if (output) {
			array_printf(A ,n);  
		}
		merge(A,p,q,r,count);
	}
}

void merge(int* A,int p,int q,int r,unsigned long* count){
	int n1 = q - p + 1;
	int n2 = r-q;
	int* L = (int*)malloc(n1*sizeof(int));
	for (int i=0;i<n1;i++){
		L[i]=A[p+i];		
	} 
	int* R = (int*)malloc(n2*sizeof(int));
	for (int i=0;i<n2;i++){
		R[i]=A[q+1+i];		
	};
	int li=0;
	int ri=0;
	int ended = 0;//-1 - left,0 - none, 1 - rigth
	for (int i=p;i<=r;i++){
		if (li==n1){
			A[i] = R[ri];
			ri++;
			continue;
		}
		if (ri==n2){
			A[i]= L[li];
			li++;
			continue;
		}
		if (L[li]<R[ri]){
			assert(ended !=-1);
			A[i]= L[li];
			li++;
		}
		else{
			assert(ended != 1);
			A[i] = R[ri];
			// 1 3 5  2 4 6
			// 2 was later then 3 5 - split inversions (2,3) and (2,5) 
			// 4 was later then 5 - (4,5)
		    // add the amount of left elements right was earlier
			*count += (n1-li);
			ri++;
		}
	}	
	free(R);free(L);
}


void inversions(int a[],int n,bool output,unsigned long* count){
	unsigned long nInversions=0;
	for(int j=0;j<n;j++){
		for (int i=0;i<j;i++){
			if (a[i]>a[j]){
					nInversions++;
			    	if (output) printf("(%d, %d)\n",a[i],a[j]);
				}
			}
		}
		*count =  nInversions;
}
