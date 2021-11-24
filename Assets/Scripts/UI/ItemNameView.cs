using TMPro;
using UnityEngine;

public class ItemNameView : MonoBehaviour
{
    private TextMeshProUGUI itemName;

    private void Awake()
    {
        itemName = GetComponent<TextMeshProUGUI> ();
    }

    private void OnEnable()
    {
        if ( GameEvents.current != null )
            GameEvents.current.OnSpellItemViewOpenedEvent += UpdateView;
    }

    private void OnDisable()
    {
        GameEvents.current.OnSpellItemViewOpenedEvent -= UpdateView;
    }


    private void UpdateView( UnitTemplate unitTemplate )
    {
        itemName.text = Localization.GetString (unitTemplate.spellTag);
    }
}
