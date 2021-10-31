using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafeArea : MonoBehaviour
{
    private void Awake()
    {
        UpdateSafeArea ();
    }

    private void UpdateSafeArea()
    {
        var safeArea = Screen.safeArea;
        var rectTransform = GetComponent<RectTransform> ();

        var anchorMin = safeArea.position;
        var anchorMax = safeArea.position + safeArea.size;

        anchorMin.x /= Screen.width;
        anchorMax.x /= Screen.width;
        anchorMin.y /= Screen.height;
        anchorMax.y /= Screen.height;

        rectTransform.anchorMin = anchorMin;
        rectTransform.anchorMax = anchorMax;
    }
}
