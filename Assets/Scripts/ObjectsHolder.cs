using UnityEngine;
using UnityEngine.EventSystems;

public class ObjectsHolder : MonoBehaviour
{
    public Sprite spellSprite;
    public Sprite cellSprite;
    public Sprite colorSprite;
    public Sprite untouchableSprite;
    public static ObjectsHolder Instance { get; private set; }
    public Physics2DRaycaster Raycaster;
    public Camera mainCamera;
    public SpellsMaps spells;
    public Board field;
    public TowerBuilder buildingManager;
    public AttackSpeller attackManager;
    public Wizard wizard;
    public UIManager uIManager;
    public TouchController touchController;
    public FirePoints firePoints;
    public SpellCaster castManager;




    private void Awake()
    {
        if ( !Instance )
        {
            Instance = this;
           // DontDestroyOnLoad (this);
        }
        else
            Destroy (this);

        mainCamera = Camera.main;
        Raycaster = mainCamera.GetComponent<Physics2DRaycaster> ();
        spells = FindObjectOfType<SpellsMaps> ();
        field = FindObjectOfType<Board> ();
        buildingManager = FindObjectOfType<TowerBuilder> ();
        attackManager = FindObjectOfType<AttackSpeller> ();
        castManager = FindObjectOfType<SpellCaster> ();
        touchController = FindObjectOfType<TouchController>();
        wizard = FindObjectOfType<Wizard> ();
        uIManager = FindObjectOfType<UIManager> ();
        firePoints = FindObjectOfType<FirePoints> ();
    }



}
