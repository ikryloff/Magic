using TMPro;
using UnityEngine;

public class ExperiencePropertyButton : ItemPropertyButton
{
    [SerializeField] private TextMeshProUGUI textTMP;
    public override void UpdateView( UnitTemplate unitTemplate )
    {
        textTMP.text = unitTemplate.xp.ToString ();
    }
}
