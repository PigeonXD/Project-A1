using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SortSelect : MonoBehaviour
{

    [SerializeField] GameObject selectionSort;

    private GameObject sliderCol;
    private GameObject transformCol;

    private Vector3 swapPos;

    private void Awake()
    {
        sliderCol = GameObject.Find("Sliders");
        transformCol = GameObject.Find("Transforms");
    }

    public void SelectionSort()
    {
        Instantiate(selectionSort, transform.position, Quaternion.identity);
    }

    public void StartTest()
    {
        GameObject tester = GameObject.Find("SelectionSort(Clone)");
        tester.GetComponent<SelectionSort>().StartSortingCoroutine();
    }

    public void PillarPrep1(int min, int max, int[] array)
    {
        int i = -1;
        StartCoroutine(PillarPrepCR(min, max, array, i));   // Initiate Coroutine
    }

    private IEnumerator PillarPrepCR(int min, int max, int[] array, int idx)
    {
        if(idx < array.Length - 1)
        {
            idx++;
            sliderCol.transform.GetChild(idx).GetComponent<SliderScript>().SliderPrep2(min, max, array[idx]);   
            yield return new WaitForSecondsRealtime(0.1f);          // send data to individual slider objects ^
            StartCoroutine(PillarPrepCR(min, max, array, idx));     // Repeat until all sliders get data
        }
        else yield return null;
    }

    public void MovePillars(int inx1, int inx2)
    {
        GameObject slider1 = sliderCol.transform.GetChild(inx1).gameObject;         
        GameObject slider2 = sliderCol.transform.GetChild(inx2).gameObject;
        swapPos = slider1.transform.localPosition;
        slider1.GetComponent<SliderScript>().Move(slider2.transform.localPosition); 
        slider2.GetComponent<SliderScript>().Move(swapPos);
        slider1.transform.SetSiblingIndex(inx2);
        slider2.transform.SetSiblingIndex(inx1);
    }
}
