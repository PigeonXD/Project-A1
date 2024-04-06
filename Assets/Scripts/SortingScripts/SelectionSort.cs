using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectionSort : MonoBehaviour
{
    public float aSpeed = 1;
    private int swapStore;      // Variable for storing a number in order to swap values

    private SortSelect mainS;       // Main Project Script
    private BaseSortScript mainSortS;   // Main Sorting Script
    private int arrayLength;

    void Start()
    {
        mainS = FindObjectOfType<SortSelect>();
        mainSortS = gameObject.GetComponent<BaseSortScript>();
        arrayLength = mainSortS.numArray.Length;
    }

    public void StartSortingCoroutine()
    {
        StartCoroutine(SortStart());
        mainS.ButtonInteractivity(GameObject.Find("StartSort").gameObject, false);
    }

    private IEnumerator SortStart()
    {
        mainSortS.i++;
        if (mainSortS.i < arrayLength)
        {
            int minInx = mainSortS.i;
            
            for (int j = mainSortS.i + 1; j < arrayLength; j++)
            {
                if (mainSortS.numArray[j] < mainSortS.numArray[minInx]) minInx = j; // Find lowest number
            }
            swapStore = mainSortS.numArray[minInx];                       // |
            mainSortS.numArray[minInx] = mainSortS.numArray[mainSortS.i];                     // Swap values in the array
            mainSortS.numArray[mainSortS.i] = swapStore;                            // |
            mainS.MovePillars(minInx, mainSortS.i);
            aSpeed = GameObject.Find("SpeedSlider").GetComponent<Slider>().value;
            yield return new WaitForSecondsRealtime(1 - ((aSpeed - 1) * 0.1f));
            mainS.PillarSelect(minInx, mainSortS.i, false);
            StartCoroutine(SortStart());
        }
        else if (mainSortS.i >= arrayLength) mainSortS.ResetPillars();
    }
}
