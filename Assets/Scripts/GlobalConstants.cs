using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalConstants : MonoBehaviour
{
    [SerializeField]
    public static LayerMask whatIsGround;

    private static GlobalConstants instance = null;

    public static GlobalConstants GetInstance()
    {
        if (instance == null)
        {
            GameObject go = new GameObject();
            instance = go.AddComponent<GlobalConstants>();
        }
        return instance;
    }
}
