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

    public static float GetSpriteDisplace()
    {
        if ( Displace > 0.9999f )
            Displace = 0.0001f;
        Displace += 0.0001f;
        return Displace;
    }

    // to prevent flickering
    public static void DisplaceZPosition( BoardUnit unit )
    {
        SpriteRenderer sr = unit.GetComponent<SpriteRenderer> ();
        float dp = GetSpriteDisplace ();
        sr.sortingOrder = unit.GetLinePosition ();
        unit.transform.position = new Vector3 (unit.transform.position.x, unit.transform.position.y, unit.transform.position.z + dp);

    }
}
