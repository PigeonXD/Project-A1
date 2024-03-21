using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectionSort : MonoBehaviour
{
    private SortSelect sortMainS;
    private GameObject pillarCol;
    private List<GameObject> pillars = new List<GameObject>();
    private int[] numArray;

    private int maxNum;         // Used to determine
    private int minNum;         // Pillar height
    private int swapStore;      // Variable for storing a number in order to swap values
    private int arrayLength;

    private int i = -1;

    void Start()
    {
        sortMainS = FindObjectOfType<SortSelect>();
        pillarCol = GameObject.Find("Sliders");
        numArray = new int[pillarCol.transform.childCount];
        AssignNumbers();
    }

    private void AssignNumbers()
    {
        for (int i = 0; i < pillarCol.transform.childCount; i++)
        {
            pillars.Add(pillarCol.transform.GetChild(i).gameObject);                        // Add GameObject to list
            Text pilText = pillarCol.transform.GetChild(i).GetComponentInChildren<Text>();  // Get text component from pillar
            int randomNum = Random.Range(1, 101);                                           // Get Random Number
            pilText.text = randomNum.ToString();
            numArray[i] = randomNum;
        }
        arrayLength = numArray.Length;

        AssignPillarHeight();
    }

    private void AssignPillarHeight()
    {
        minNum = numArray[0]; maxNum = numArray[0];
        for (int i = 0; i < arrayLength; i++)               // Find lowest and highest value
        {
            if (numArray[i] > maxNum) maxNum = numArray[i];
            else if (numArray[i] < minNum) minNum = numArray[i];
        }
        sortMainS.PillarPrep1(minNum, maxNum, numArray);    // Send data to the main script
    }

    public void StartSortingCoroutine()
    {
        StartCoroutine(SortStart());
    }

    private IEnumerator SortStart()
    {
        i++;
        if (i < arrayLength)
        {
            int minInx = i;
            for (int j = i + 1; j < arrayLength; j++)
            {
                if (numArray[j] < numArray[minInx]) minInx = j; // Find lowest number
            }
            swapStore = numArray[minInx];                       // |
            numArray[minInx] = numArray[i];                     // Swap values in the array
            numArray[i] = swapStore;                            // |
            sortMainS.MovePillars(minInx, i);                   
            yield return new WaitForSecondsRealtime(0.25f);
            StartCoroutine(SortStart());
        }
    }

    private void UpdateText() // No longer needed due to proper feature implementation
    {
        for(int i = 0; i < arrayLength; i++)
        {
            Text pilText = pillarCol.transform.GetChild(i).GetComponentInChildren<Text>();
            pilText.text = numArray[i].ToString();
        }
    }

    private void displayArray(int inx) // debug tool
    {
        string str = " ";
        for(int i = 0; i < arrayLength; i++)
        {
            str += numArray[i];
            str += ", ";
        }
        Debug.Log(str + " | " + inx + " " + i);
    }
}
