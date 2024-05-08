using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderScript : MonoBehaviour
{
    private Slider slider;

    // Value vars
    public float moveSpeed = 0.4f;
    private bool raisePillar = false;
    private int targetValue;

    // Move vars
    private Vector3 posVec;
    private bool movePil;

    void Start()
    {
        slider = GetComponent<Slider>();
    }

    void FixedUpdate()
    {
        if (movePil)
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, posVec, moveSpeed);
        }

        if (raisePillar)
        {
            slider.value = Mathf.Lerp(slider.value, targetValue, 0.5f);
        }
    }

    private bool AltPillarAlignment()
    {
        if (GameObject.Find("CountingSort(Clone)") != null || GameObject.Find("HeapSort(Clone)") != null) return true;
        else return false;
    }

    public void SliderPrep2(int min, int max, int target)
    {
        if (AltPillarAlignment())
        {
            slider.minValue = min / 2;
            slider.maxValue = max;
            targetValue = target / 2;
            raisePillar = true;
        }
        else
        {
            slider.minValue = min;
            slider.maxValue = max;
            targetValue = target;
            raisePillar = true;
        }
    }

    public void Move(Vector3 pos)
    {
        posVec = pos;
        movePil = true;
    }

    public void MoveInstant(Vector3 pos)
    {
        transform.localPosition = pos;
    }

    public void StopMove()
    {
        movePil = false;
    }
}
