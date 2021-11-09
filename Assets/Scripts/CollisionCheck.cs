using System.Collections.Generic;
using UnityEngine;

public class CollisionCheck : MonoBehaviour
{
    [SerializeField][Tooltip("Layers of which objects should trigger this collision check.")]
    private LayerMask _WhatIsGround;

    public readonly List<Collider2D> triggerObjects = new List<Collider2D>();

    void OnTriggerEnter2D(Collider2D other)
    {
        if ((_WhatIsGround.value | (1 << other.gameObject.layer)) == _WhatIsGround.value)
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
