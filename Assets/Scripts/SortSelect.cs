using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SortSelect : MonoBehaviour
{
    // Preview Variables
    public bool lockScroll = false;
    private GameObject sliderCol;
    private GameObject previewCol;
    private GameObject currentPreview;
    private GameObject previewMenu;
    private Animator animator;

    // Algorithm Gameobjects
    [SerializeField] GameObject selectionSort;
    [SerializeField] GameObject bubbleSort;
    [SerializeField] GameObject insertionSort;
    [SerializeField] GameObject mergeSort;
    [SerializeField] GameObject quickSort;
    [SerializeField] GameObject heapSort;
    [SerializeField] GameObject countingSort;
    [SerializeField] GameObject radixSort;
    [SerializeField] GameObject bucketSort;
    [SerializeField] GameObject cycleSort;

    private Vector3 swapPos; // Variable used for swaping pillars
    private GameObject algorithmPanel; // Panel to display Algorithm behaviour

    // Moving Variables
    GameObject moveToObject;
    Vector3 moveToTarget;
    public int moveIndex;

    // Scaling Variables
    private GameObject scaleToObject;
    private Vector3 scaleToTarget;

    private void Awake()
    {
        animator = GameObject.Find("Canvas").GetComponent<Animator>();
        sliderCol = GameObject.Find("Sliders");
        algorithmPanel = GameObject.Find("Algorithm");
        previewCol = GameObject.Find("PreviewPanels");
        previewMenu = GameObject.Find("PreviewMenu");
    }

    private void FixedUpdate()
    {
        if(moveToObject != null) moveToObject.transform.localPosition = Vector3.Lerp(moveToObject.transform.localPosition, moveToTarget, 0.1f);
        if(scaleToObject != null) scaleToObject.transform.localScale = Vector3.Lerp(scaleToObject.transform.localScale, scaleToTarget, 0.1f);

        for(int i = 0; i < previewCol.transform.childCount; i++)    // For moving preview panels
        {
            GameObject iObj = previewCol.transform.GetChild(i).gameObject;
            if(i < moveIndex) iObj.transform.localPosition = Vector3.Lerp(iObj.transform.localPosition, new Vector3(130, 650, 0), 0.1f);
            else if(i == moveIndex) iObj.transform.localPosition = Vector3.Lerp(iObj.transform.localPosition, new Vector3(130, 0, 0), 0.1f);
            else if(i > moveIndex) iObj.transform.localPosition = Vector3.Lerp(iObj.transform.localPosition, new Vector3(130, -650, 0), 0.1f);
        }
    }

    private void MoveTo(GameObject moveObject, Vector3 target)
    {
        moveToObject = moveObject;
        moveToTarget = target;
    }
    private void ScaleTo(GameObject scaleObject, Vector3 target)
    {
        scaleToObject = scaleObject;
        scaleToTarget = target;
    }

    private void SpawnSortingObject(GameObject SortObject)
    {
        if(!CheckIfExists(SortObject)) Instantiate(SortObject, transform.position, Quaternion.identity);
    }

    private bool CheckIfExists(GameObject obj)
    {
        GameObject check = null;
        check = GameObject.Find(obj.name + "(Clone)");
        if (check == null) return false;
        else
        {
            Destroy(check); // I did a little bit of trolling
            return false;
        }
    }

    public void SelectPreview(GameObject gObj)
    {
        currentPreview = gObj;
        ScaleTo(currentPreview, Vector3.one);
        animator.SetBool("Preview", true);
    }

    // Sorting Functions
    public void StartSort()
    {
        GameObject tester = GameObject.Find("SelectionSort(Clone)");
        tester.GetComponent<SelectionSort>().StartSortingCoroutine();
    }

    public void PillarPrep1(int min, int max, int[] array)
    {
        int i = -1;
        StartCoroutine(PillarPrepCR(min, max, array, i));   // Initiate Coroutine
        ButtonInteractivity(previewMenu.transform.GetChild(1).transform.GetChild(0).gameObject, false);
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
        else 
        {
            ButtonInteractivity(previewMenu.transform.GetChild(1).transform.GetChild(0).gameObject, true);
            yield return null;
        } 
                
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

        PillarSelect(inx1, inx2, true);
    }

    public void PillarSelect(int i1, int i2, bool color)
    {
        if (color)
        {
            //sliderCol.transform.GetChild(i1).transform.GetChild(1).transform.GetChild(0).GetComponent<Image>().color = Color.cyan;
            sliderCol.transform.GetChild(i2).transform.GetChild(1).transform.GetChild(0).GetComponent<Image>().color = Color.cyan;
        }
        else
        {
            //sliderCol.transform.GetChild(i1).transform.GetChild(1).transform.GetChild(0).GetComponent<Image>().color = Color.white;
            sliderCol.transform.GetChild(i2).transform.GetChild(1).transform.GetChild(0).GetComponent<Image>().color = Color.white;
        }
        
    }

    public void ButtonInteractivity(GameObject button, bool status)
    {
        button.GetComponent<Button>().interactable = status;
        if (status) button.transform.GetChild(0).GetComponent<Text>().color = Color.white;
        else if (!status) button.transform.GetChild(0).GetComponent<Text>().color = Color.gray;
    }

    // Sorting Algorithm Buttons

    public void SelectionSort()
    {
        MoveTo(algorithmPanel, Vector3.zero);
        SpawnSortingObject(selectionSort);
        ButtonInteractivity(previewMenu.transform.GetChild(1).transform.GetChild(1).gameObject, true);
        ButtonInteractivity(previewMenu.transform.GetChild(1).transform.GetChild(2).gameObject, true);
        previewMenu.transform.GetChild(1).transform.GetChild(3).transform.GetChild(3).GetComponent<Text>().color = Color.white;
        previewMenu.transform.GetChild(1).transform.GetChild(3).transform.GetChild(4).GetComponent<Text>().color = Color.white;
    }

    public void AlgorithmPanelOff()
    {
        MoveTo(algorithmPanel, new Vector3(700, 0, 0));
        ButtonInteractivity(previewMenu.transform.GetChild(1).transform.GetChild(1).gameObject, false);
        ButtonInteractivity(previewMenu.transform.GetChild(1).transform.GetChild(2).gameObject, false);
        previewMenu.transform.GetChild(1).transform.GetChild(3).transform.GetChild(3).GetComponent<Text>().color = Color.gray;
        previewMenu.transform.GetChild(1).transform.GetChild(3).transform.GetChild(4).GetComponent<Text>().color = Color.gray;
    }

    public void DeSelectPreviewButton()
    {
        animator.SetBool("Preview", false);
        MoveTo(algorithmPanel, new Vector3(700, 0, 0));
        ScaleTo(currentPreview, new Vector3(0.95f, 0.95f, 1));
        currentPreview = null;
        lockScroll = false;
        ButtonInteractivity(previewMenu.transform.GetChild(1).transform.GetChild(1).gameObject, false);
        ButtonInteractivity(previewMenu.transform.GetChild(1).transform.GetChild(2).gameObject, false);

    }

    public void SpeedSliderUpdate()
    {
        GameObject speedSlider = GameObject.Find("SpeedSlider");
        speedSlider.transform.GetChild(4).GetComponent<Text>().text = speedSlider.GetComponent<Slider>().value.ToString() + "x";
        for(int i = 0; i < 12; i++)
        {
            SliderScript sliderS = sliderCol.transform.GetChild(i).GetComponent<SliderScript>();
            if ((speedSlider.GetComponent<Slider>().value * 0.05f) > 0.25f) sliderS.moveSpeed = speedSlider.GetComponent<Slider>().value * 0.06f;
            else sliderS.moveSpeed = 0.25f;
        }
    }
}
