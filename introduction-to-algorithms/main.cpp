
#include <stdio.h>
#include <assert.h>
#include <stdlib.h>
#include <iostream>


#include "utils.h"
#include "2.h"
#include "7.h"
#include "9.h"

using namespace std;

void test_2_1();
void test_2_3();
void test_problem_2_4a();
void test_problem_2_4b();
void test_problem_2_4d();
void test_problem_2_4d_sub(int a[],int n);

void test_problem_7_2();
void test_problem_7_2a();

void test_problem_9_1();

int main (int argc, char *argv[]){	
	//test_2_1();
	//test_2_3();
	//test_problem_2_4a();
	//test_problem_2_4b();
	//test_problem_2_4d();
	//test_problem_7_2();
	test_problem_9_1();
	return 0;
}

void test_problem_9_1(){
	int a4[] = {7, 5, 4, 6};

    int is405 = randomized_select(a4,0,3,0,choose_pivot_random);
	assert(is405 == 4);   
	
	int is415 = randomized_select(a4,0,3,1,choose_pivot_random);
	assert(is415 == 5);
	
	
	 int is425 = randomized_select(a4,0,3,2,choose_pivot_random);
	assert(is425 == 6);   
	
	 int is435 = randomized_select(a4,0,3,3,choose_pivot_random);
	 assert(is435 == 7);      
}

void test_problem_7_2a(){
	
	int a5[] = {1, 3, 8, 6, 2};
    int median53 = choose_pivot_median_of_three(a5,2,4);
	assert(median53 == 3);
	
	int a4[] = {4, 5, 6, 7};
	int median42 = choose_pivot_median_of_three(a4,0,3);
	assert(median42 == 1);
	
	int a3[] = {1, 5, 3};
	int median33 = choose_pivot_median_of_three(a3,0,2);
	assert(median33 == 2);
	
	int a2[] = {10, 4};
	int median21 = choose_pivot_median_of_three(a2,0,1);
	assert(median21 == 0);
	
	int a1[] = {2};
	int median11 = choose_pivot_median_of_three(a1,0,0);
	assert(median11 == 0);
	
}
	

void test_problem_7_2(){
	test_problem_7_2a();

   int a8[] = {3,8,2,5,1,4,7,6};
   long comparisons8 =quick_sort(a8,0, 7,choose_pivot_median_of_three);
   array_printf(a8 ,8);
   printf("%d\n",comparisons8);
   	char buffer[10];
	int n[10000];
	FILE* f =  fopen("IntegerArray7.txt","r");
	int i=0;
	while ( ! feof (f) )
	{
		if ( fgets (buffer , 9999 , f) != NULL )
			n[i]=atoi(buffer);
		i++;
	}
	
   long comparisons =quick_sort(n,0, 9999,choose_pivot_first);
   printf("%ld\n",comparisons);

   
   
}

void test_problem_2_4d(){

	char buffer[10];
	int n[100000];
	FILE* f =  fopen("IntegerArray2.txt","r");
	int i=0;
	while ( ! feof (f) )
	{
		if ( fgets (buffer , 99999 , f) != NULL )
			n[i]=atoi(buffer);
		i++;
	}

    test_problem_2_4d_sub(n,100000);
    


	int an[]={5,2,4,6,1,3};
    test_problem_2_4d_sub(an,6);

	int a3[]={3,2,1};
    test_problem_2_4d_sub(a3,3);
	
	int a5[]={2,3,8,6,1};
    test_problem_2_4d_sub(a5,5);
	
	int a0[]={1,2,3,4};
    test_problem_2_4d_sub(a0,4);
		
	int a10[]={4,3,2,1};
    test_problem_2_4d_sub(a10,4);
}

void test_problem_2_4d_sub(int a[],int n){
    unsigned long cm = 0;
    unsigned long c=0;
    //inversions(a,n,false,&c);
    merge_sort(&a[0],0,n-1,n,false,&cm);
	assert(cm ==c);
}

void test_problem_2_4b(){
	int a[]={5,4,3,2,1};
	 unsigned long n=0;
    inversions(a,5,false,&n);
    assert(n==10);
	}

void test_problem_2_4a(){
	int a[]={2,3,8,6,1};
	 unsigned long n=0;
    inversions(a,5,false,&n);
        assert(n==5);
	}
	

void test_2_1(){
	int n = 6;
	int a[]={5,2,4,6,1,3};
	insertion_sort(a,n); 
	insertion_sort_desc(a,n); 
}

void test_2_3(){
	//int n = 2;
	//int a[] = {2,1};
	//int n = 8;
	//int a[] = {2,4,5,7,1,2,3,6};
    //int n = 4;
	//int a[] = {3,2,1,5};
	int n = 9;
	int a[] = {23,2,4,5,7,1,2,3,6};
    unsigned long count=0;
	merge_sort(&a[0],0,n-1,n,false,&count);
	array_printf(a ,n);
}







