using UnityEngine;

public class GlobalConstants : MonoBehaviour
{
    private static GlobalConstants _Instance = null;

    public static GlobalConstants GetInstance()
    {
        if (_Instance == null)
        {
            GameObject go = new GameObject();
            _Instance = go.AddComponent<GlobalConstants>();
        }
        return _Instance;
    }
}
