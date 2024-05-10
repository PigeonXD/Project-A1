using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountingSort : MonoBehaviour
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
        List<int> list = new List<int>(mainSortS.numArray);
        StartCoroutine(CountSort(list));
        mainS.ButtonInteractivity(GameObject.Find("StartSort").gameObject, false);
    }

    private IEnumerator CountSort(List<int> inputArray)
    {
        int M = 0;
        for (int i = 0; i < arrayLength; i++) M = Mathf.Max(M, inputArray[i]);  // Finding the maximum element of the array inputArray[].
        List<int> countArray = new List<int>(new int[M + 1]);                   // Initializing countArray[] with 0
        for (int i = 0; i < arrayLength; i++)   // Mapping each element of inputArray[] as an index of countArray[] array
        {
            countArray[inputArray[i]]++;  
        }
        for (int i = 1; i <= M; i++)            // Calculating prefix sum at every index of array countArray[]
        {
            countArray[i] += countArray[i - 1];        
        }
        List<int> outputArray = new List<int>(new int[arrayLength]);            // Creating outputArray[] from the countArray[] array
        for (int i = arrayLength - 1; i >= 0; i--)
        {
            mainS.MovePillarsCounting(i, countArray[inputArray[i]] - 1);
            outputArray[countArray[inputArray[i]] - 1] = inputArray[i];
            countArray[inputArray[i]]--;
            aSpeed = GameObject.Find("SpeedSlider").GetComponent<Slider>().value;
            yield return new WaitForSecondsRealtime(1 - ((aSpeed - 1) * 0.095f));
        }

        mainSortS.ResetPillarsCounting();

        /*
        string str = "";
        for (int z = 0; z < outputArray.Count; z++)
        {
            str += outputArray[z].ToString() + ", ";
        }
        Debug.Log(str);
        */
    }
}
