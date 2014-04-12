
int choose_pivot_last(int* a,int q,int r);
int choose_pivot_first(int* a,int q,int r);
int choose_pivot_median_of_three(int* a,int q,int r);
int choose_pivot_random(int* a,int q,int r);

int partition(int* a,int q,int r);
long quick_sort(int* a,int q,int r,int (*choose_pivot)(int*,int,int));

