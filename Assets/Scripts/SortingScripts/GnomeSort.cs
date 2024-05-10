using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GnomeSort : MonoBehaviour
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
        StartCoroutine(SortStart());
        mainS.ButtonInteractivity(GameObject.Find("StartSort").gameObject, false);
    }

    private IEnumerator SortStart()
    {
        mainSortS.i = 0;
        mainSortS.j = 1;
        while (mainSortS.i < arrayLength)
        {
            if (mainSortS.i == 0)
            {
                mainSortS.i++;
                mainSortS.j++;
            }

            if (mainSortS.numArray[mainSortS.i] >= mainSortS.numArray[mainSortS.i - 1]) 
            {
                mainSortS.i++;
                if(mainSortS.j < arrayLength - 1) mainSortS.j++;
            }
            else
            {
                int temp;
                temp = mainSortS.numArray[mainSortS.i];
                mainSortS.numArray[mainSortS.i] = mainSortS.numArray[mainSortS.i - 1];
                mainSortS.numArray[mainSortS.i - 1] = temp;
                mainS.MovePillars(mainSortS.i, mainSortS.i - 1);
                mainSortS.i--;
                mainSortS.j--;
            }

            aSpeed = GameObject.Find("SpeedSlider").GetComponent<Slider>().value;
            yield return new WaitForSecondsRealtime(1 - ((aSpeed - 1) * 0.1f));
            try
            {
                if (mainSortS.i < arrayLength - 1) mainS.PillarSelect(mainSortS.i, mainSortS.i + 1, false);
                else mainS.PillarSelect(mainSortS.i - 1, mainSortS.i, false);
            }
            catch(Exception ex)
            {
                Debug.Log(ex);                  // this spits out an "index out of bounds"
                mainSortS.ResetPillars();       // error but it works so don't touch
            }
        }
        mainSortS.ResetPillars();
    }
}
