using UnityEngine;

public class LevelMenuPanel : MonoBehaviour
{
    [SerializeField] private GameObject loadButtonPrefab;

    public void InitMenu()
    {
        for ( int i = 0; i < Constants.STAGES_QUANTITY; i++ )
        {
            GameObject levelButton = Instantiate (loadButtonPrefab, transform, false);
            levelButton.GetComponent<LoadButton> ().Init (i);
        }
    }
}
