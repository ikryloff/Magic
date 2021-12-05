using TMPro;
using UnityEngine;

public class WizardView : MonoBehaviour
{
    [SerializeField] private GameObject resetButton;
    [SerializeField] private TextMeshProUGUI skillPointsText;


    private void OnEnable()
    {
        LevelBook.ResetSkillLevelsAndPoints ();
        ShowSkillPoints ();
        GameEvents.current.OnWizardLevelChangeEvent += ShowSkillPoints;

    }

    private void OnDisable()
    {
        GameEvents.current.OnWizardLevelChangeEvent -= ShowSkillPoints;
    }

    private void ShowSkillPoints()
    {
        ResetButtonView ();
        int points = LevelBook.GetPlayerSkillPoints ();
        skillPointsText.text = $"{points}";
    }

    private void ResetButtonView()
    {
        if ( !resetButton.activeSelf && LevelBook.WasSkillPointChanged () )
            resetButton.SetActive (true);
        if( !LevelBook.WasSkillPointChanged () )
            DeActivateResetButton ();
    }

    private void DeActivateResetButton()
    {
        resetButton.SetActive (false);
    }
}
