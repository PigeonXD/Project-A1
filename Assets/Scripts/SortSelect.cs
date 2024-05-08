using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SortSelect : MonoBehaviour
{
    // Preview Variables
    public bool lockScroll = false;
    [SerializeField] GameObject sliderCol;
    private GameObject previewCol;
    private GameObject currentPreview;
    private GameObject previewMenu;
    private Animator animator;
    private GameObject check = null;
    [Space]

    // Algorithm Gameobjects
    [SerializeField] GameObject selectionSort;
    [SerializeField] GameObject bubbleSort;
    [SerializeField] GameObject insertionSort;
    [SerializeField] GameObject mergeSort;
    [SerializeField] GameObject quickSort;
    [SerializeField] GameObject heapSort;
    [SerializeField] GameObject countingSort;
    [SerializeField] GameObject gnomeSort;
    [SerializeField] GameObject shotgunSort;

    private GameObject startSortObj;

    private Vector3 swapPos; // Variable used for swaping pillars
    private GameObject algorithmPanel; // Panel to display Algorithm behaviour

    // Moving Variables
    GameObject moveToObject;
    Vector3 moveToTarget;
    public int moveIndex;

    // Scaling Variables
    private GameObject scaleToObject;
    private Vector3 scaleToTarget;

    private GameObject pillarCol;
    public bool algoReset = true;

    public GameObject DebugText;

    private void Awake()
    {
        animator = GameObject.Find("Canvas").GetComponent<Animator>();
        algorithmPanel = GameObject.Find("Algorithm");
        previewCol = GameObject.Find("PreviewPanels");
        previewMenu = GameObject.Find("PreviewMenu");
        pillarCol = GameObject.Find("Sliders");
        DebugText = GameObject.Find("DebugText");
        DebugTextSwitch(false);
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

        if (GameObject.Find("Algorithm").transform.localPosition.x > 680 && algoReset)
        {
            ResetAlgo();
            algoReset = false;
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
        if(!ObjectExists(SortObject)) Instantiate(SortObject, transform.position, Quaternion.identity);
        startSortObj = GameObject.Find(SortObject.name + "(Clone)");
    }

    private bool ObjectExists(GameObject obj)
    {
        check = GameObject.Find(obj.name + "(Clone)");
        //Debug.Log(check);
        if (check == null) return false;
        else
        {
            //Destroy(check); // I did a little bit of trolling
            return true;
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
        switch (startSortObj.name)
        {
            case "SelectionSort(Clone)":
                startSortObj.GetComponent<SelectionSort>().StartSortingCoroutine();
                break;
            case "BubbleSort(Clone)":
                startSortObj.GetComponent<BubbleSort>().StartSortingCoroutine();
                break;
            case "InsertionSort(Clone)":
                startSortObj.GetComponent<InsertionSort>().StartSortingCoroutine();
                break;
            case "MergeSort(Clone)":
                startSortObj.GetComponent<MergeSort>().StartSortingCoroutine();
                break;
            case "QuickSort(Clone)":
                startSortObj.GetComponent<QuickSort>().StartSortingCoroutine();
                break;
            case "HeapSort(Clone)":
                startSortObj.GetComponent<HeapSort>().StartSortingCoroutine();
                break;
            case "CountingSort(Clone)":
                startSortObj.GetComponent<CountingSort>().StartSortingCoroutine();
                break;
            case "GnomeSort(Clone)":
                startSortObj.GetComponent<GnomeSort>().StartSortingCoroutine();
                break;
            case "ShotgunSort(Clone)":
                startSortObj.GetComponent<ShotgunSort>().StartSortingCoroutine();
                break;
        }
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
    public void MovePillarsCounting(int inx1, int inx2)
    {
        GameObject slider = sliderCol.transform.GetChild(inx1).gameObject;
        slider.GetComponent<SliderScript>().Move(new Vector3(-150 + (50 * inx2), -30, transform.localPosition.z));
    }
    public void MovePillarsHeap(int inx1, int inx2)
    {
        GameObject slider1 = sliderCol.transform.GetChild(inx1).gameObject;
        GameObject slider2 = sliderCol.transform.GetChild(inx2).gameObject;
        swapPos = slider1.transform.localPosition;
        slider1.GetComponent<SliderScript>().Move(new Vector3(-150 + (50 * inx2), -30, transform.localPosition.z));
        slider2.GetComponent<SliderScript>().Move(swapPos);
        slider1.transform.SetSiblingIndex(inx2);
        slider2.transform.SetSiblingIndex(inx1);

        //PillarSelect(inx1, inx2, true);
    }
    public void MovePillarsInstant(int inx1, int inx2)
    {
        GameObject slider1 = sliderCol.transform.GetChild(inx1).gameObject;
        GameObject slider2 = sliderCol.transform.GetChild(inx2).gameObject;
        swapPos = slider1.transform.localPosition;
        slider1.GetComponent<SliderScript>().MoveInstant(slider2.transform.localPosition);
        slider2.GetComponent<SliderScript>().MoveInstant(swapPos);
        slider1.transform.SetSiblingIndex(inx2);
        slider2.transform.SetSiblingIndex(inx1);

        //PillarSelect(inx1, inx2, true);
    }
    public void SwapIndex(int inx1, int inx2)
    {
        GameObject slider1 = sliderCol.transform.GetChild(inx1).gameObject;
        GameObject slider2 = sliderCol.transform.GetChild(inx2).gameObject;
        slider1.transform.SetSiblingIndex(inx2);
        slider2.transform.SetSiblingIndex(inx1);
    }

    public void PillarSelect(int i1, int i2, bool color)
    {
        if (color)
        {
            sliderCol.transform.GetChild(i1).transform.GetChild(1).transform.GetChild(0).GetComponent<Image>().color = Color.cyan;
            sliderCol.transform.GetChild(i2).transform.GetChild(1).transform.GetChild(0).GetComponent<Image>().color = Color.cyan;
            sliderCol.transform.GetChild(i1).transform.GetChild(2).GetComponent<Text>().color = Color.cyan;
            sliderCol.transform.GetChild(i2).transform.GetChild(2).GetComponent<Text>().color = Color.cyan;
        }
        else
        {
            sliderCol.transform.GetChild(i1).transform.GetChild(1).transform.GetChild(0).GetComponent<Image>().color = Color.white;
            sliderCol.transform.GetChild(i2).transform.GetChild(1).transform.GetChild(0).GetComponent<Image>().color = Color.white;
            sliderCol.transform.GetChild(i1).transform.GetChild(2).GetComponent<Text>().color = Color.white;
            sliderCol.transform.GetChild(i2).transform.GetChild(2).GetComponent<Text>().color = Color.white;
        }
        
    }

    public void ButtonInteractivity(GameObject button, bool status)
    {
        button.GetComponent<Button>().interactable = status;
        if (status) button.transform.GetChild(0).GetComponent<Text>().color = Color.white;
        else if (!status) button.transform.GetChild(0).GetComponent<Text>().color = Color.gray;
    }

    // Sorting Algorithm Buttons

    public void PrepareButton()
    {
        MoveTo(algorithmPanel, Vector3.zero);
        selectTheSort();
        ButtonInteractivity(previewMenu.transform.GetChild(1).transform.GetChild(1).gameObject, true);
        ButtonInteractivity(previewMenu.transform.GetChild(1).transform.GetChild(2).gameObject, true);
        previewMenu.transform.GetChild(1).transform.GetChild(3).transform.GetChild(3).GetComponent<Text>().color = Color.white;
        previewMenu.transform.GetChild(1).transform.GetChild(3).transform.GetChild(4).GetComponent<Text>().color = Color.white;
    }

    private void selectTheSort()
    {
        switch (moveIndex)
        {
            case 0: SpawnSortingObject(selectionSort); break;
            case 1: SpawnSortingObject(bubbleSort); break;
            case 2: SpawnSortingObject(insertionSort); break;
            case 3: SpawnSortingObject(mergeSort); break;
            case 4: SpawnSortingObject(quickSort); break;
            case 5: SpawnSortingObject(heapSort); break;
            case 6: SpawnSortingObject(countingSort); break;
            case 7: SpawnSortingObject(gnomeSort); break;
            case 8: SpawnSortingObject(shotgunSort); break;
        }
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
        DebugTextSwitch(false);
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

    private void ResetAlgo()
    {
        for (int j = 0; j < 12; j++) pillarCol.transform.GetChild(j).GetComponent<SliderScript>().Move(new Vector3(-150 + (50 * j), 0, 0));
    }

    public void DebugTextSwitch(bool status)
    {
        DebugText.SetActive(status);
    }
}
