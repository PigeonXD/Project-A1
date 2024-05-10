using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class MergeSort : MonoBehaviour
{
    public float aSpeed = 1;

    private SortSelect mainS;       // Main Project Script
    private BaseSortScript mainSortS;   // Main Sorting Script
    private int arrayLength;

    void Start()
    {
        mainS = FindObjectOfType<SortSelect>();
        mainSortS = gameObject.GetComponent<BaseSortScript>();
        arrayLength = mainSortS.numArray.Length;
        mainSortS.arrowMode = BaseSortScript.arrowModes.BubbleSort;
    }

    public void StartSortingCoroutine()
    {
        StartCoroutine(SortStart(0, arrayLength - 1));
        mainS.ButtonInteractivity(GameObject.Find("StartSort").gameObject, false);
    }

    private IEnumerator SortStart(int l, int r)
    {
        if (l < r)
        {
            // Find the middle point
            int m = l + (r - l) / 2;

            // Sort first and second halves
            StartCoroutine(SortStart(l, m));
            StartCoroutine(SortStart(m + 1, r));

            // Merge the sorted halves
            Merge(l, m, r);
        }
        PrintArray();
        aSpeed = GameObject.Find("SpeedSlider").GetComponent<Slider>().value;
        yield return new WaitForSecondsRealtime(1 - ((aSpeed - 1) * 0.1f));
    }

    private void Merge(int l, int m, int r)
    {
        // Find sizes of two
        // subarrays to be merged
        int n1 = m - l + 1;
        int n2 = r - m;

        // Create temp arrays
        int[] L = new int[n1];
        int[] R = new int[n2];
        int i, j;

        // Copy data to temp arrays
        for (i = 0; i < n1; ++i)
            L[i] = mainSortS.numArray[l + i];
        for (j = 0; j < n2; ++j)
            R[j] = mainSortS.numArray[m + 1 + j];

        // Merge the temp arrays

        // Initial indexes of first
        // and second subarrays
        i = 0;
        j = 0;

        // Initial index of merged
        // subarray array
        int k = l;
        while (i < n1 && j < n2)
        {
            if (L[i] <= R[j])
            {
                mainSortS.numArray[k] = L[i];
                i++;
                //mainS.MovePillarsInstant(k, n1);
            }
            else
            {
                mainSortS.numArray[k] = R[j];
                j++;
                //mainS.MovePillarsInstant(k, n2);
            }
            k++;
        }

        // Copy remaining elements
        // of L[] if any
        while (i < n1)
        {
            mainSortS.numArray[k] = L[i];
            i++;
            k++;
        }

        // Copy remaining elements
        // of R[] if any
        while (j < n2)
        {
            mainSortS.numArray[k] = R[j];
            j++;
            k++;
        }
    }

    private void PrintArray() // Debug Function
    {
        string str = "";
        for (int z = 0; z < mainSortS.numArray.Length; z++)
        {
            str += mainSortS.numArray[z].ToString() + ", ";
        }
        mainS.DebugText.GetComponent<Text>().text = str;
        mainS.DebugTextSwitch(true);
    }
}
