using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SortSelect : MonoBehaviour
{

    [SerializeField] GameObject selectionSort;

    private GameObject sliderCol;
    private GameObject transformCol;
    private Transform transformColTransform;

    private bool swapping = false;
    private GameObject slider1;
    private GameObject slider2;
    private GameObject sliderswap;
    private int moveIndex1;
    private int moveIndex2;
    private string nameSwap;

    GameObject test;

    private void Awake()
    {
        sliderCol = GameObject.Find("Sliders");
        transformCol = GameObject.Find("Transforms");
        //test = sliderCol.transform.GetChild(0).gameObject;
    }

    private void Update()
    {
        //test.transform.localPosition = new Vector3(Random.Range(1, 200), 0 , 0);
        //Debug.Log(sliderCol.transform.GetChild(0).gameObject.transform.localPosition.x);
        if (swapping)
        {
            //Mathf.Lerp(slider1, transformCol.transform.GetChild(moveIndex2).transform.localPosition.x, 0.25f);
            //Mathf.Lerp(slider2, transformCol.transform.GetChild(moveIndex1).transform.localPosition.x, 0.25f);
        }
    }

    public void SwapPillars(int pil1, int pil2)
    {
        /*
        if(pil1 != pil2)
        {
            moveIndex1 = pil1;
            moveIndex2 = pil2;
            slider1 = sliderCol.transform.GetChild(moveIndex1).gameObject;
            slider2 = sliderCol.transform.GetChild(moveIndex2).gameObject;
            GameObject transform1 = transformCol.transform.GetChild(moveIndex2).gameObject;
            GameObject transform2 = transformCol.transform.GetChild(moveIndex1).gameObject;
            slider1.transform.localPosition = transform1.transform.localPosition;
            slider2.transform.localPosition = transform2.transform.localPosition;
            Debug.Log("done");
        }
        */
    }

    public void SelectionSort()
    {
        Instantiate(selectionSort, transform.position, Quaternion.identity);
    }

    public void StartTest()
    {
        GameObject tester = GameObject.Find("SelectionSort(Clone)");
        tester.GetComponent<SelectionSort>().test111();
    }
}
