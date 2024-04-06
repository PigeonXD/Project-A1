using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaseSortScript : MonoBehaviour
{
    private SortSelect sortMainS;
    private GameObject pillarCol;

    private List<GameObject> pillars = new List<GameObject>();
    private bool pillarBool = true;

    private GameObject arrow;

    public int[] numArray;
    private int arrayLength;

    private int maxNum;         // Used to determine
    private int minNum;         // Pillar height


    public int i = -1;

    private void Start()
    {
        sortMainS = FindObjectOfType<SortSelect>();
        pillarCol = GameObject.Find("Sliders");
        numArray = new int[pillarCol.transform.childCount];
        arrow = pillarCol.transform.parent.transform.GetChild(2).gameObject;
    }

    private void Update()
    {
        if (GameObject.Find("Algorithm").transform.localPosition.x < 300 && pillarBool)
        {
            AssignNumbers();
            pillarBool = false;
        }
    }
    private void FixedUpdate()
    {
        if (i >= 0) arrow.transform.localPosition = Vector3.Lerp(arrow.transform.localPosition, new Vector3(-150 + (50 * i), -270, 0), 0.3f);
        else if (i == 11) arrow.transform.localPosition = Vector3.Lerp(arrow.transform.localPosition, new Vector3(400, -270, 0), 0.3f);
        else arrow.transform.localPosition = Vector3.Lerp(arrow.transform.localPosition, new Vector3(-150, -270, 0), 0.3f);
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
    public void ResetPillars()
    {
        i = -1;
        for (int k = 0; k < arrayLength - 1; k++) pillarCol.transform.GetChild(k).GetComponent<SliderScript>().StopMove();
        for (int j = 0; j < arrayLength - 1; j++) pillarCol.transform.GetChild(j).localPosition = new Vector3(-150 + (50 * j), 0, 0);
        Destroy(gameObject);
    }
}
