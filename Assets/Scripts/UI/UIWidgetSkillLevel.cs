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

    private void OnEnable()
    {
        CalcValueAndSetToBar ();
        GameEvents.current.OnWizardLevelChangeEvent += CalcValueAndSetToBar;
        
    }

    private void OnDisable()
    {
        GameEvents.current.OnWizardLevelChangeEvent -= CalcValueAndSetToBar;
    }

    private void CalcValueAndSetToBar()
    {
        int level = LevelBook.GetPlayerSkillLevel (skillIndex);
        float barValue = level * (1f / Constants.PLAYER_SKILLS_MAX_LEVEL);
        SetValue (barValue);
        if ( level == Constants.PLAYER_SKILLS_MAX_LEVEL )
            SetButtonUnActive ();
        else
            SetButtonActive ();

        if ( !LevelBook.HasSkillPoints () )
            SetButtonUnActive ();
    }

    private void SetValue( float value )
    {
        bar.SetValue (value);
    }

    private void AddOnePointToTempSkillLevel()
    {
        if ( LevelBook.GetPlayerSkillLevel(skillIndex) >= Constants.PLAYER_SKILLS_MAX_LEVEL ) return;
        if ( LevelBook.HasSkillPoints() )
            LevelBook.AddOnePointToPlayerSkillLevel (skillIndex);
        GameEvents.current.WizardLevelChangeAction ();
    }

    private void SetButtonUnActive()
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
