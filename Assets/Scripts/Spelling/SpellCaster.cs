using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellCaster : MonoBehaviour
{
    public static int CellsCount = 0;
    private ObjectsHolder objects;
    public List<Cell> CastLine;
    public SpellDecoder spellDecoder;
    private UIManager ui;

    private void Awake()
    {
        ui = ObjectsHolder.Instance.uIManager;
        CastLine = new List<Cell> ();
        objects = FindObjectOfType<ObjectsHolder> ();
        spellDecoder = FindObjectOfType<SpellDecoder> ();
    }

    private void OnEnable()
    {
        GameEvents.current.OnStopCastingEvent += StopCasting;
    }

    private void OnDisable()
    {
        GameEvents.current.OnStopCastingEvent -= StopCasting;
    }

    public void CastSpell()
    {
        spellDecoder.MakeSpell (CastLine);
    }

    public void DeleteCast()
    {
        CastLine.Clear ();
        CellsCount = 0;
    }

    public void MakeCast()
    {
        StartCoroutine (MakeCastWithDelay ());
    }

    public void ClearCast()
    {
        DeleteCast ();
        GameEvents.current.CastResetEvent ();
    }

    IEnumerator MakeCastWithDelay()
    {
        yield return new WaitForSeconds (.1f);
        CastSpell ();
        ColorizeSpell (CastLine);
    }

    private void ColorizeSpell( List<Cell> castLine )
    {
        if ( castLine == null )
            return;
        for ( int i = 0; i < castLine.Count; i++ )
        {
            castLine [i].ColorCellToPrepare ();
        }
    }

    private void StopCasting()
    {
        ClearCast ();
        GameEvents.current.GameStateChangedAction (GameManager.GameState.BoardActive);
        ui.SetPrepareValue (100);
        ui.SetDefaultPrepareIcon ();
    }
}
