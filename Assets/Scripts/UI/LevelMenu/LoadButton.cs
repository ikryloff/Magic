using TMPro;
using UnityEngine;

public class LoadButton : UIButton
{
    [SerializeField] private Material _greyScaleMat;

    private TextMeshProUGUI textTMP;
    private GameManager gameManager;
    private int number;

    private new void Awake()
    {
        base.Awake ();
        textTMP = transform.GetComponentInChildren<TextMeshProUGUI> ();
        gameManager = FindObjectOfType<GameManager> ();
    }

    public void Init(int stageNumber)
    {
        number = stageNumber;
        textTMP.text = number.ToString ();
        SetAvailabilityView ();
    }

    private void OnEnable()
    {
        SetAvailabilityView ();
    }

    private void SetAvailabilityView()
    {
        if ( number == 0 ) return;
        if ( Storage.IsStageAvaliable (number) )
        {
            _button.image.material = null;
            _button.interactable = true;
        }
        else
        {
            _button.image.material = _greyScaleMat;
            _button.interactable = false;
        }
    }

    public override void Action()
    {
        gameManager.InitLevel (number);
    }
}
