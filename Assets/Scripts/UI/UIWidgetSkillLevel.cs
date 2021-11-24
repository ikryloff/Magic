using UnityEngine;
using UnityEngine.UI;

public class UIWidgetSkillLevel : MonoBehaviour
{
    [SerializeField] private ProgressBar bar;
    [SerializeField] private Button addButton;
    [SerializeField] private Material inactiveMaterial;
    [Header ("0 Attack, 1 Defence, 2 Intelligence, 3 Learning, 4 Alteration, 5 Regeneration, 6 FastReading")]
    [SerializeField] private int skillIndex;

    private int [] tempSkillLevels = new int [] { 0, 0, 0, 0, 0, 0, 0 };

    private void Awake()
    {
        bar = GetComponentInChildren<ProgressBar> ();
        addButton = GetComponentInChildren<Button> ();
        inactiveMaterial = Resources.Load ("Materials/GrayScale") as Material;
        addButton.onClick.AddListener (AddOnePoint);
    }

    private void OnEnable()
    {
        CopyToTempValues ();
        CalcValueAndSetToBar ();
    }

    private void AddOnePoint()
    {
        AddOnePointToTempSkillLevel (skillIndex);
        CalcValueAndSetToBar ();
    }

    private void CalcValueAndSetToBar()
    {
        float barValue = tempSkillLevels [skillIndex] * (1f / Constants.PLAYER_SKILLS_MAX_LEVEL);
        SetValue (barValue);
        if ( tempSkillLevels [skillIndex] == Constants.PLAYER_SKILLS_MAX_LEVEL )
        {
            addButton.image.material = inactiveMaterial;
            addButton.interactable = false;
        }
    }

    private void SetValue( float value )
    {
        bar.SetValue (value);
    }

    private void AddOnePointToTempSkillLevel( int index )
    {
        if ( tempSkillLevels [index] < Constants.PLAYER_SKILLS_MAX_LEVEL )
            tempSkillLevels [index] += 1;
    }

    private void ApplyNewSkillValues ( )
    {
        for ( int i = 0; i < tempSkillLevels.Length; i++ )
        {
            Player.SetPlayerSkillLevel(i, tempSkillLevels [i]);
        }
    }

    private void CopyToTempValues()
    {
        for ( int i = 0; i < tempSkillLevels.Length; i++ )
        {
            tempSkillLevels [i] = Player.GetPlayerSkillLevel(i);
        }
    }

}
