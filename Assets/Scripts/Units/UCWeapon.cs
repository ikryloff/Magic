
using UnityEngine;

public class UCWeapon : MonoBehaviour
{
    protected BoardUnit _unit;
    private GameObject _bullet;
    protected UnitTemplate _unitTemplate;

    public void Init( BoardUnit unit )
    {
        _unit = unit;
        _unitTemplate = unit.GetUnitTemplate ();
        _bullet = _unitTemplate?.bulletPrefab;
    }



    public virtual void Fire( BoardUnit enemy )
    {
        if ( enemy )
        {
            
            if ( _unitTemplate.attackRange > 1 )
            {
                GameObject bulletGO = Instantiate (_bullet, transform.position, Quaternion.identity);
                Bullet bullet = bulletGO.GetComponent<Bullet> ();
                if ( bullet != null )
                {
                    bullet.SeekTarget (enemy, _unitTemplate);
                }
            }
            else
            {
                GameEvents.current.NewHit (enemy, _unitTemplate);
            }
        }
    }

}
