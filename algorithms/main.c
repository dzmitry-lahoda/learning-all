#include <stdio.h>
#include <stdlib.h>

int binary_search(int arr[], int l, int h, int x)
{
    while (l <= h)
    {
        int mid = (l+h) / 2;

        // if 'x' is greater than or equal to arr[mid],
        // then search in arr[mid+1...h]
        if (arr[mid] <= x)
            l = mid + 1;

        // else search in arr[l...mid-1]
        else
            h = mid - 1;
    }

    // required index
    return h;
}

// function to count for each element in 1st array,
// elements less than or equal to it in 2nd array
void countEleLessThanOrEqual(int nums[], int maxes[],
                             int m, int n)
{
    // sort the 2nd array
    sort(maxes, maxes+n);

    // for each element of 1st array
    for (int i=0; i<m; i++)
    {
        // last index of largest element
        // smaller than or equal to x
        int index = binary_search(maxes, 0, n-1, nums[i]);

        // required count for the element arr1[i]
        cout << (index+1) << " ";
    }
}

         int* counts(int nums_size, int* nums, int maxes_size, int* maxes, int* result_size)
        {
              qsort (nums, nums_size * sizeof(int), sizeof(*x), compare_function);
        }

int main()
{
    int* nums = {1, 2, 3};
    int* maxes = {2, 4};
    int result = 0;
    int* vals = counts(3, nums, 2, maxes, &result);
    printf("Hello world!\n");
    return 0;
}
