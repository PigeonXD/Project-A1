using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ShotgunSort : MonoBehaviour
{
    public float aSpeed = 1;

    private SortSelect mainS;       // Main Project Script
    private BaseSortScript mainSortS;   // Main Sorting Script
    private int arrayLength;

    private bool[] bools;

    void Start()
    {
        mainS = FindObjectOfType<SortSelect>();
        mainSortS = gameObject.GetComponent<BaseSortScript>();
        arrayLength = mainSortS.numArray.Length;
        mainSortS.arrowMode = BaseSortScript.arrowModes.LeftToRight;
    }

    public void StartSortingCoroutine()
    {
        StartCoroutine(SortStart());
        mainS.ButtonInteractivity(GameObject.Find("StartSort").gameObject, false);
    }

    private IEnumerator SortStart()
    {
        while (!IsSorted()) 
        {
            Shuffle();
            aSpeed = GameObject.Find("SpeedSlider").GetComponent<Slider>().value;
            yield return new WaitForSecondsRealtime(1 - ((aSpeed - 1) * 0.1f));
        }
        mainSortS.ResetPillars();
    }

    public void Shuffle()
    {
        bools = new bool[arrayLength];
        for (int i = 0; i < arrayLength; i++)
        {
            bools[i] = true;
        }

        for (int i = 0; i < arrayLength; i++)
        {
            int randomIndex = Random.Range(0, arrayLength);
            while (bools[randomIndex] == false)
            {
                randomIndex = Random.Range(0, arrayLength);
            }

            Swap(i, randomIndex);
            bools[randomIndex] = false;
        }
    }

    public bool IsSorted()
    {
        int i = 0;
        while (i < arrayLength - 1)
        {
            if (mainSortS.numArray[i] > mainSortS.numArray[i + 1]) return false;
            i++;
        }
        return true;
    }
    private void Swap(int a, int b)
    {
        int temp;
        temp = mainSortS.numArray[a];
        mainSortS.numArray[a] = mainSortS.numArray[b];
        mainSortS.numArray[b] = temp;
        mainS.MovePillarsInstant(a, b);
    }
}
