using UnityEngine;

public class UIDefinitions
{
    public enum UITypes
    {
        Generate,
        Levels,
        Gameplay
    }
}

public class UIType : MonoBehaviour
{
    public UIDefinitions.UITypes uiType;
}
