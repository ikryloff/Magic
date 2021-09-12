using UnityEngine;

public class BoardUnit : Unit
{
    public enum States
    {
        Idle, //common state
        Seeking, //looking to the target
        Attacking, //attack cycle animation
        Dead //dead animation, before removal from play field
    }

    public static float Displace = 0f;
    protected int _linePosition;
    protected int _columnPosition;

    protected string _name;

    public float attackRange;
    public float attackRate;

    public int GetLinePosition()
    {
        return _linePosition;
    }

    public void SetLinePosition( int linePosition )
    {
        _linePosition = linePosition;
    }

    public int GetColumnPosition()
    {
        return _columnPosition;
    }

    public void SetColumnPosition( int columnPosition )
    {
        _columnPosition = columnPosition;
    }

    public string GetUnitName()
    {
        return _name;
    }

    protected float GetSpriteDisplace()
    {
        if ( Displace > 0.9999f )
            Displace = 0.0001f;
        Displace += 0.0001f;
        return Displace;
    }

    protected void DisplaceZPosition()
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer> ();
        float dp = GetSpriteDisplace ();
        sr.sortingOrder = GetLinePosition ();
        transform.position = new Vector3 (transform.position.x, transform.position.y, transform.position.z + dp);

    }
}
