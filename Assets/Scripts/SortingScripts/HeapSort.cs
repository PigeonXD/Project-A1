using System.Collections;
using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using UnityEngine;
using UnityEngine.UI;

public class HeapSort : MonoBehaviour
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
        mainSortS.arrowMode = BaseSortScript.arrowModes.Invisible;
        mainSortS.CountingSortPillarMove();
    }

    public void StartSortingCoroutine()
    {
        StartCoroutine(SortStart());
        mainS.ButtonInteractivity(GameObject.Find("StartSort").gameObject, false);
    }

    private IEnumerator SortStart()
    {
        // Build heap (rearrange array)
        for (int i = arrayLength / 2 - 1; i >= 0; i--)
            Heapify(arrayLength, i);

        // One by one extract an element from heap
        for (int i = arrayLength - 1; i > 0; i--)
        {
            // Move current root to end
            int temp = mainSortS.numArray[0];
            mainSortS.numArray[0] = mainSortS.numArray[i];
            mainSortS.numArray[i] = temp;
            mainS.MovePillarsHeap(0, i);

            aSpeed = GameObject.Find("SpeedSlider").GetComponent<Slider>().value;
            yield return new WaitForSecondsRealtime(1 - ((aSpeed - 1) * 0.1f));

            // call max heapify on the reduced heap
            Heapify(i, 0);
        }
        mainS.MovePillarsCounting(0, 0);
        yield return new WaitForSecondsRealtime(1 - ((aSpeed - 1) * 0.1f));
        mainSortS.ResetPillarsCounting();
    }
    private void Heapify(int N, int i)
    {
        int largest = i; // Initialize largest as root
        int l = 2 * i + 1; // left = 2*i + 1
        int r = 2 * i + 2; // right = 2*i + 2

        // If left child is larger than root
        if (l < N && mainSortS.numArray[l] > mainSortS.numArray[largest]) 
        {
            largest = l;
        }

        // If right child is larger than largest so far
        if (r < N && mainSortS.numArray[r] > mainSortS.numArray[largest])
        {
            largest = r;
        }

        // If largest is not root
        if (largest != i)
        {
            int swap = mainSortS.numArray[i];
            mainSortS.numArray[i] = mainSortS.numArray[largest];
            mainSortS.numArray[largest] = swap;
            mainS.SwapIndex(i, largest);

            // Recursively heapify the affected sub-tree
            Heapify(N, largest);
        }
    }
}
