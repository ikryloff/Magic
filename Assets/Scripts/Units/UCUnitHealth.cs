using UnityEngine;

public class UCUnitHealth : MonoBehaviour
{
    [SerializeField] private Transform _bar;
    [SerializeField] private GameObject _barGo;
    [SerializeField] private SpriteRenderer _sprite;
    private BoardUnit _unit;
    public Color32 highColor;
    public Color32 lowColor;
    public float height;
    public float counter = 1f;
    private float _health;
    private float _currentHealth;
    private UnitTemplate _unitTemplate;
    private UCEffects _effects;



    public void Init( BoardUnit unit, UCEffects effects )
    {
        _unit = unit;
        _unitTemplate = _unit.GetUnitTemplate ();
        _health = _unitTemplate.health;
        _effects = effects;
        _currentHealth = _health;
        _sprite.sortingOrder = 300;
        _effects.ShowBorn ();
        _barGo.SetActive (false);

        GameEvents.current.OnNewHit += TakeDamage;
        GameEvents.current.OnDieEvent += MakeDeath;
    }

    private void OnDestroy()
    {
        GameEvents.current.OnNewHit -= TakeDamage;
        GameEvents.current.OnDieEvent -= MakeDeath;
    }

    private void TakeDamage( BoardUnit unit, UnitTemplate damageTemplate )
    {
        if ( unit != _unit )
            return;
        if ( damageTemplate.damage > 0 ) // if it is hit, not heal
        {
            _effects.ShowHit ();
        }
        ShowHealthBar (damageTemplate);
    }


    private void SetHBSize( float size )
    {
        if ( size <= 0 )
        {
            _bar.localScale = new Vector3 (0f, 1f);
            SetHBColor (0f);
            return;
        }
        _bar.localScale = new Vector3 (size, 1f);
        SetHBColor (size);
    }

    private void SetHBColor( float sizeNorm )
    {
        _sprite.color = Color32.Lerp (lowColor, highColor, sizeNorm);
    }


    private void ShowHealthBar( UnitTemplate damageTemplate )
    {
        float damage = damageTemplate.damage;
        ChangeUnitCurrentHealth (damage);
        CheckHP ();
        float ratio = CalcRatio ();
        if ( ratio <= 1 && ratio >= 0 )
        {
            if ( !_barGo.activeSelf )
                _barGo.SetActive (true);
            SetHBSize (ratio);
        }

        if ( ratio >= 1 )
        {
            ratio = 1;
            SetHBSize (ratio);
            _barGo.SetActive (false);
        }
    }

    public void ChangeUnitCurrentHealth( float damage )
    {
        _currentHealth -= damage;
    }

    private float CalcRatio()
    {
        return _currentHealth / _health;
    }

    private void CheckHP()
    {
        if ( _currentHealth <= 0 )
        {
            MakeDeath (_unit);
            return;
        }
        if ( _currentHealth >= _health )
            _currentHealth = _health;
    }

    public float GetCurrentHealth()
    {
        return _currentHealth;
    }

    public void MakeDeath(BoardUnit unit)
    {
        if ( unit != _unit ) return;
        _effects.ShowDeath ();
        RemoveUnit ();
        Destroy (_unit.gameObject);
    }

    private void RemoveUnit()
    {
        if ( _unitTemplate.unitType == Unit.UnitType.Human )
        {
            UnitsOnBoard.RemoveHumanFromLineHumansList (_unit.GetComponent<Human> ());
        }
        else
        {
            _unit.GetCurrentCell ().SetFreefromTower ();
            UnitsOnBoard.RemoveTowerFromLineTowersList (_unit.GetComponent<TowerUnit> ());
        }
    }
}

