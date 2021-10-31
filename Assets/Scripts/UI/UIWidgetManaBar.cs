using UnityEngine;

public class UIWidgetManaBar : MonoBehaviour
{
    [SerializeField] private ProgressBar bar;

    private void Start()
    {
        GameEvents.current.OnManaValueChangedEvent += SetValue;
    }

    private void OnDestroy()
    {
        GameEvents.current.OnManaValueChangedEvent -= SetValue;
    }

    private void SetValue( float value )
    {
        bar.SetValue (value);
    }
}
