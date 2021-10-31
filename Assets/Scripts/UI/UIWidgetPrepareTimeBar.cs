using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIWidgetPrepareTimeBar : MonoBehaviour
{
    [SerializeField] private ProgressBar bar;

    private void Start()
    {
        GameEvents.current.OnPrepareTimeValueChangedEvent += SetValue;
    }

    private void OnDestroy()
    {
        GameEvents.current.OnPrepareTimeValueChangedEvent -= SetValue;
    }

    private void SetValue( float value )
    {
        bar.SetValue (value);
    }
}
