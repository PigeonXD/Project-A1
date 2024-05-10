using System.Collections;
using UnityEngine;
using UnityEngine.UI;
public class InsertionSort : MonoBehaviour
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
        mainSortS.arrowMode = BaseSortScript.arrowModes.LeftToRight;
    }

    public void StartSortingCoroutine()
    {
        StartCoroutine(SortStart());
        mainS.ButtonInteractivity(GameObject.Find("StartSort").gameObject, false);
    }

    private IEnumerator SortStart()
    {
        mainSortS.i++;
        if (mainSortS.i < arrayLength)
        {
            for (int i = 1; i < arrayLength; ++i)
            {
                int key = mainSortS.numArray[i];
                int j = i - 1;

                while (j >= 0 && mainSortS.numArray[j] > key)
                {
                    mainS.MovePillars(j + 1, j);
                    mainSortS.numArray[j + 1] = mainSortS.numArray[j];
                    j--;
                    yield return new WaitForSecondsRealtime(1 - ((9 - 1) * 0.1f));
                    mainS.PillarSelect(j + 1, j + 2, false);
                }
                aSpeed = GameObject.Find("SpeedSlider").GetComponent<Slider>().value;
                yield return new WaitForSecondsRealtime(1 - ((aSpeed - 1) * 0.1f));
                mainSortS.i++;
                mainSortS.numArray[j + 1] = key;
            }
        }
        else if (mainSortS.i >= arrayLength) mainSortS.ResetPillars();
    }
}
