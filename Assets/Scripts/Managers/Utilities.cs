using UnityEngine;
using UnityEngine.UIElements;

public class Utilities
{
    public static float Displace = 0f;


    public static void SetFontSize40( Label label )
    {
        int size = 40;
        label.style.fontSize = (int)(size * CameraViewportHandler.Instance.heightKoef);
    }

    public static void SetFontSize42( Label label )
    {
        int size = 42;
        label.style.fontSize = (int)(size * CameraViewportHandler.Instance.heightKoef);
    }

    public static void SetFontSize50( Label label )
    {
        int size = 50;
        label.style.fontSize = (int)(size * CameraViewportHandler.Instance.heightKoef);
    }

    public static float GetSpriteZDisplace()
    {
        if ( Displace > 0.9999f )
            Displace = 0.0001f;
        Displace += 0.0001f;
        return Displace;
    }

    // to see group of units
    public static float GetPositionDisplace()
    {
        return Random.Range (-0.1f, 0.1f);
    }

    // to prevent flickering
    public static void DisplaceZPosition( BoardUnit unit, int line )
    {
        SpriteRenderer sr = unit.GetComponent<SpriteRenderer>();
        float dp = GetSpriteZDisplace ();
        sr.sortingOrder = line;
        
        // only for traps
        if ( unit.GetUnitTemplate ().unitType == Unit.UnitType.Tower )
        {
            if ( unit.GetUnitTemplate ().towerType == TowerUnit.TowerType.Trap )
            {
                dp = 1;
            }
        }

        unit.transform.position = new Vector3 (unit.transform.position.x, unit.transform.position.y, unit.transform.position.z + dp);
    }

}
