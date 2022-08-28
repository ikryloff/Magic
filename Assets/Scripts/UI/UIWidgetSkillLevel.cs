using UnityEngine;
using UnityEngine.UI;

public class UIWidgetSkillLevel : MonoBehaviour
{
    [SerializeField] private ProgressBar bar;
    [SerializeField] private Button addButton;
    [SerializeField] private Material inactiveMaterial;
    [SerializeField] private int skillIndex;


    private void Awake()
    {
        bar = GetComponentInChildren<ProgressBar> ();
        addButton = GetComponentInChildren<Button> ();
        inactiveMaterial = Resources.Load ("Materials/GrayScale") as Material;
        addButton.onClick.AddListener (AddOnePointToTempSkillLevel);
    }

    public int GetIndex()
    {
        return skillIndex;
    }

    public void CalcValueAndSetToBar(int level)
    {
        float barValue = level * (1f / Constants.PLAYER_SKILLS_MAX_LEVEL);
        SetValue (barValue);
        if ( level == Constants.PLAYER_SKILLS_MAX_LEVEL )
            SetButtonUnActive ();
        else
            SetButtonActive ();
               
    }

    private void SetValue( float value )
    {
        bar.SetValue (value);
    }

    private void AddOnePointToTempSkillLevel()
    {
        GameEvents.current.WizardLevelChangeAction (skillIndex);
    }

    public void SetButtonUnActive()
    {
        addButton.image.material = inactiveMaterial;
        addButton.interactable = false;
    }

    private void SetButtonActive()
    {
        addButton.image.material = null;
        addButton.interactable = true;
    }

}
