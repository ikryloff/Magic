using UnityEngine;
using UnityEngine.EventSystems;

public class CellAction : MonoBehaviour, IPointerEnterHandler
{
    private Cell cell;
    private SpellCaster castManager;
    private bool _isActive;


    private void Awake()
    {
        cell = GetComponent<Cell> ();
        GameEvents.current.OnSwitchTouch += SwitchActivity;
    }

    private void OnDisable()
    {
        GameEvents.current.OnSwitchTouch -= SwitchActivity;
    }


    public void OnPointerEnter( PointerEventData eventData )
    {
        if ( _isActive )
        {
            Debug.Log (eventData.currentInputModule);
            if ( !cell.IsLoaded )
            {
                cell.LoadCell ();
            }
        }
    }

    private void SwitchActivity( bool isOn )
    {
        _isActive = isOn;
    }
}
