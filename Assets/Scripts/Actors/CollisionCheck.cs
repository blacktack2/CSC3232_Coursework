using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionCheck : MonoBehaviour
{
    [SerializeField]
    private LayerMask whatIsGround;

    public List<Collider2D> triggerObjects = new List<Collider2D>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (1 << other.gameObject.layer == whatIsGround.value)
            triggerObjects.Add(other);
    }

    void OnTriggerExit2D(Collider2D other)
    {
        triggerObjects.Remove(other);
    }

    public bool IsTriggered()
    {
        return triggerObjects.Count > 0;
    }
}
