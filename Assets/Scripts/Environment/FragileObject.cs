using UnityEngine;

public class FragileObject : MonoBehaviour
{
    [SerializeField, Tooltip("Force required to break this object if is fragile.")]
    private float _BreakingForce = 1.0f;
    [SerializeField, Tooltip("True if this object should be destroyed when receiving a force greater than the breaking force.")]
    private bool _IsFragile = true;

    public void ForceApplied(float magnitude)
    {
        if (_IsFragile && magnitude >= _BreakingForce)
            Destroy(gameObject);
    }
}
