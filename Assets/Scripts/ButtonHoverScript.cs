using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonHoverScript : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private SortSelect mainScript;
    private GameObject PreviewCol;

    private void Start()
    {
        mainScript = FindObjectOfType<SortSelect>();
        PreviewCol = GameObject.Find("PreviewPanels");
    }

    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        if(!mainScript.lockScroll) mainScript.moveIndex = transform.GetSiblingIndex() - 1;
    }

    public void OnPointerExit(PointerEventData pointerEventData)
    {

    }

    public void PreviewButton()
    {
        mainScript.lockScroll = true;
        int index = transform.GetSiblingIndex() - 1;
        mainScript.SelectPreview(PreviewCol.transform.GetChild(index).gameObject);
    }
}
