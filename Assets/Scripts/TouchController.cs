using System.Collections;
using UnityEngine;

public class TouchController : MonoBehaviour
{
    private SpellCaster castManager;
    private ObjectsHolder objects;

    private bool _isTouchable;


    private void Awake()
    {
        castManager = FindObjectOfType<SpellCaster> ();
        objects = FindObjectOfType<ObjectsHolder> ();
        GameEvents.current.OnSwitchTouch += SwitchTouchability;

    }

    private void OnDisable()
    {
        GameEvents.current.OnSwitchTouch -= SwitchTouchability;
    }

    private void Update()
    {
        if ( _isTouchable )
        {
            if ( Input.touchCount > 0 )
            {
                if ( Input.GetTouch (0).phase == TouchPhase.Ended )
                {
                    GameEvents.current.CastOver ();
                    castManager.MakeCast ();
                }
            }
        }
    }

    private void SwitchTouchability( bool isOn )
    {
        _isTouchable = isOn;
    }

   

}
