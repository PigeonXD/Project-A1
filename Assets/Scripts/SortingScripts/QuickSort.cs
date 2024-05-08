using System.Collections;
using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class QuickSort : MonoBehaviour
{
    public float aSpeed = 1;

    private SortSelect mainS;       // Main Project Script
    private BaseSortScript mainSortS;   // Main Sorting Script
    private int arrayLength;

    int pi = 1;

    void Start()
    {
        mainS = FindObjectOfType<SortSelect>();
        mainSortS = gameObject.GetComponent<BaseSortScript>();
        arrayLength = mainSortS.numArray.Length;
        mainSortS.arrowMode = BaseSortScript.arrowModes.LeftToRight;
    }

    public void StartSortingCoroutine()
    {
        //StartCoroutine(SortStart(0, arrayLength - 1));
        SortStart(0, arrayLength - 1);
        mainS.ButtonInteractivity(GameObject.Find("StartSort").gameObject, false);
    }

    private void SortStart(int low, int high)
    {
        mainSortS.i++;
        if (mainSortS.i < arrayLength)
        {
            if (low < high)
            {
                /*
                // pi is partitioning index, arr[p]
                // is now at right place
                aSpeed = GameObject.Find("SpeedSlider").GetComponent<Slider>().value;
                yield return new WaitForSecondsRealtime(1 - ((aSpeed - 1) * 0.1f));
                StartCoroutine(Partition(low, high));

                // Separately sort elements before
                // and after partition index
                yield return new WaitForSecondsRealtime(1 - ((aSpeed - 1) * 0.1f));
                StartCoroutine(SortStart(low, pi - 1));
                yield return new WaitForSecondsRealtime(1 - ((aSpeed - 1) * 0.1f));
                StartCoroutine(SortStart(pi + 1, high));
                */

                SortStart(low, pi - 1);
                SortStart(pi + 1, high);
                Partition(low, high);

                PrintArray();
            }
        }
        else if (mainSortS.i >= arrayLength) mainSortS.ResetPillars();
    }

    private void Partition(int low, int high)
    {
        // Choosing the pivot
        int pivot = mainSortS.numArray[high];
        mainS.PillarSelect(high, high, true);
        // Index of smaller element and indicates
        // the right position of pivot found so far
        int i = (low - 1);

        for (int j = low; j <= high - 1; j++)
        {

            // If current element is smaller than the pivot
            if (mainSortS.numArray[j] < pivot)
            {
                //yield return new WaitForSecondsRealtime(1 - ((aSpeed - 1) * 0.1f));
                // Increment index of smaller element
                i++;
                //StartCoroutine(Swap(i, i+1));
                Swap(i, i + 1);
            }
        }
        //yield return new WaitForSecondsRealtime(1 - ((aSpeed - 1) * 0.1f));
        //StartCoroutine(Swap(i + 1, high));
        Swap(i + 1, high);
        mainS.PillarSelect(high, high, false);
        pi = i + 1;
    }

    private void Swap(int i, int j)
    {
        //yield return new WaitForSecondsRealtime(1 - ((aSpeed - 1) * 0.1f));
        int temp = mainSortS.numArray[i];
        mainSortS.numArray[i] = mainSortS.numArray[j];
        mainSortS.numArray[j] = temp;
        mainS.MovePillars(i, j);
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
