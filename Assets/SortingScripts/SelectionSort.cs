using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SelectionSort : MonoBehaviour
{
    private SortSelect sortMainS;
    private GameObject pillarCol;
    private List<GameObject> pillars = new List<GameObject>();
    private int[] numArray;

    private int sortNum;        
    private int compareNum;     
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
        //Debug.Log(numArray[0] + " | " + numArray[1] + " | " + numArray[2]);
        arrayLength = numArray.Length;

        print();
    }

    private void Sort()
    {
        for(int i = 0; i < arrayLength; i++)
        {
            int minInx = i;
            for(int j = i + 1; j < arrayLength; j++)
            {
                if (numArray[j] < numArray[minInx]) minInx = j;
            }
            
            swapStore = numArray[minInx];
            numArray[minInx] = numArray[i];
            numArray[i] = swapStore;
            UpdateText();
            

            Debug.Log(i + "  " + minInx);
            sortMainS.SwapPillars(i, minInx);
        }
    }

    public void test111()
    {
        StartCoroutine(test());
    }

    private IEnumerator test()
    {
        i++;
        if (i < arrayLength)
        {
            int minInx = i;
            Debug.Log(minInx);
            for (int j = i + 1; j < arrayLength; j++)
            {
                if (numArray[j] < numArray[minInx]) minInx = j;
            }

            swapStore = numArray[minInx];
            numArray[minInx] = numArray[i];
            numArray[i] = swapStore;
            UpdateText();

            yield return new WaitForSecondsRealtime(0.25f);
            StartCoroutine(test());
        }
    }


    private void print()
    {
        Debug.Log(numArray[0] + " | " + numArray[1] + " | " + numArray[2]);
    }

    private void UpdateText()
    {
        for(int i = 0; i < arrayLength; i++)
        {
            Text pilText = pillarCol.transform.GetChild(i).GetComponentInChildren<Text>();
            pilText.text = numArray[i].ToString();
        }
    }
}
