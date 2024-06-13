using System;
using UnityEngine;

public class MergeSortExample : MonoBehaviour
{
    void Start()
    {
        int[] array = { 7, 9, 0, 1, 6, 5, 3 };
        Debug.Log("Original Array: " + string.Join(", ", array));

        MergeSort(array, 0, array.Length - 1);

        Debug.Log("Sorted Array: " + string.Join(", ", array));
    }

    void MergeSort(int[] array, int left, int right)
    {
        if (left < right)
        {
            int middle = (left + right) / 2; 

            MergeSort(array, left, middle);
            MergeSort(array, middle + 1, right);

            Merge(array, left, middle, right);
        }
    }

    void Merge(int[] array, int left, int middle, int right)
    {
        int leftArraySize = middle - left + 1;
        int rightArraySize = right - middle;

        int[] leftArray = new int[leftArraySize];
        int[] rightArray = new int[rightArraySize];

        Array.Copy(array, left, leftArray, 0, leftArraySize);
        Array.Copy(array, middle + 1, rightArray, 0, rightArraySize);

        int i = 0, j = 0, k = left;

        while (i < leftArraySize && j < rightArraySize)
        {
            if (leftArray[i] <= rightArray[j])
            {
                array[k] = leftArray[i];
                i++;
            }
            else
            {
                array[k] = rightArray[j];
                j++;
            }
            k++;
        }

        while (i < leftArraySize)
        {
            array[k] = leftArray[i];
            i++;
            k++;
        }

        while (j < rightArraySize)
        {
            array[k] = rightArray[j];
            j++;
            k++;
        }
    }
}
