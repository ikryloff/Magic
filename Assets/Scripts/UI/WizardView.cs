using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WizardView : MonoBehaviour
{
    [SerializeField] private Player player;

    [SerializeField] private Button resetButton;
    [SerializeField] private Button applyButton;
    [SerializeField] private TextMeshProUGUI skillPointsText;
    [SerializeField] private UIWidgetSkillLevel [] skillLevels;

    private TempWizardView tempWizardView;

    private void OnEnable()
    {
        GameEvents.current.OnWizardLevelChangeEvent += UpdateView;
        ResetView ();
        resetButton.onClick.AddListener (ResetView);
        applyButton.onClick.AddListener (ApplyChanges);
    }

    private void OnDisable()
    {
        GameEvents.current.OnWizardLevelChangeEvent -= UpdateView;
    }

    private void ResetView()
    {
        tempWizardView = new TempWizardView (player.GetSkillPoints (), player.GetSkillLevels ());
        ShowView ();
    }

    private void ApplyChanges()
    {
        player.SetSkillPoints (tempWizardView.skillPoints);
        player.SetSkillLevels (tempWizardView.skillLevels);
        ShowView ();
    }

    private void ShowView()
    {
        ResetButtonView ();
        int points = tempWizardView.skillPoints;
        skillPointsText.text = $"{points}";
        ShowLevels ();
    }

    public void ShowLevels()
    {
        for ( int i = 0; i < skillLevels.Length; i++ )
        {
            int index = skillLevels [i].GetIndex ();
            skillLevels [i].CalcValueAndSetToBar (tempWizardView.skillLevels [index]);
            if ( tempWizardView.skillPoints <= 0 )
                skillLevels [i].SetButtonUnActive ();
        }
    }

    private void UpdateView( int skillIndex )
    {
        tempWizardView.skillLevels [skillIndex] += 1;
        tempWizardView.skillPoints -= 1;
        ShowView ();
    }


    private void ResetButtonView()
    {
        if ( tempWizardView.skillPoints < player.GetSkillPoints () )
            resetButton.gameObject.SetActive (true);
        else
            resetButton.gameObject.SetActive (false);
    }
}

public class TempWizardView
{
    public int skillPoints;
    public int [] skillLevels;

    public TempWizardView( int skillPoints, int [] skillLevels )
    {
        this.skillPoints = skillPoints;
        this.skillLevels = new int [skillLevels.Length];
        Array.Copy (skillLevels, this.skillLevels, skillLevels.Length);
    }
}
