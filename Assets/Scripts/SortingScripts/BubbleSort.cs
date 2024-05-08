using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BubbleSort : MonoBehaviour
{
    public float aSpeed = 1;

    private SortSelect mainS;       // Main Project Script
    private BaseSortScript mainSortS;   // Main Sorting Script
    private int arrayLength;

    private int j, temp;
    private bool swapped;

    void Start()
    {
        mainS = FindObjectOfType<SortSelect>();
        mainSortS = gameObject.GetComponent<BaseSortScript>();
        arrayLength = mainSortS.numArray.Length;
        mainSortS.arrowMode = BaseSortScript.arrowModes.BubbleSort;
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
            swapped = false;
            for (j = 0; j < arrayLength - mainSortS.i - 1; j++)
            {
                mainSortS.j++;
                //mainS.PillarSelect(j, j + 1, true);
                if (mainSortS.numArray[j] > mainSortS.numArray[j + 1])
                {
                    // Swap arr[j] and arr[j+1]
                    temp = mainSortS.numArray[j];
                    mainSortS.numArray[j] = mainSortS.numArray[j + 1];
                    mainSortS.numArray[j + 1] = temp;
                    mainS.MovePillars(j, j + 1);
                    swapped = true;
                }
                aSpeed = GameObject.Find("SpeedSlider").GetComponent<Slider>().value;
                yield return new WaitForSecondsRealtime(1 - ((aSpeed - 1) * 0.1f));
                mainS.PillarSelect(j, j + 1, false);
            }
            mainSortS.j = 0;
            // If no two elements were
            // swapped by inner loop, then break
            if (swapped == false) { }
            StartCoroutine(SortStart());
        }
        else if (mainSortS.i >= arrayLength) mainSortS.ResetPillars();
        //for (i = 0; i < arrayLength - 1; i++) { }
        //PrintArray();
    }

    private void PrintArray() // Debug Function
    {
        string str = "";
        for (int z = 0; z < mainSortS.numArray.Length; z++)
        {
            str += mainSortS.numArray[z].ToString() + ", ";
        }
        Debug.Log(str);
    }
}
