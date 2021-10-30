using UnityEngine;

[RequireComponent(typeof(Animator))]
public class BasicSwordAnimHandler : MonoBehaviour
{
    [SerializeField][Tooltip("Animator being handled.")]
    private Animator _Animator;
    [SerializeField][Tooltip("Sword controller responsible for the animator.")]
    private SwordController _Controller;

    [SerializeField]
    private float _IdleFlashDelay = 1.0f;
    [SerializeField]
    private string _IdleFlashAnimationTrigger;

    [SerializeField]
    private GameObject _SideSwingCollider;
    [SerializeField]
    private GameObject _UpSwingCollider1;
    [SerializeField]
    private GameObject _UpSwingCollider2;

    private float _IdleFlashCounter = 0.0f;

    void Awake()
    {
        _SideSwingCollider.SetActive(false);
        _UpSwingCollider1.SetActive(false);
        _UpSwingCollider2.SetActive(false);
    }

    void Update()
    {
        _IdleFlashCounter += Time.deltaTime;
    }

    public void IdleFlashEvent()
    {
        if (_IdleFlashCounter >= _IdleFlashDelay)
        {
            _IdleFlashCounter = 0.0f;
            _Animator.SetTrigger(_IdleFlashAnimationTrigger);
        }
    }

    public void SetSideSwingCollider()
    {
        _SideSwingCollider.SetActive(true);
    }

    public void StopSideSwingColliders()
    {
        _SideSwingCollider.SetActive(false);
    }

    public void EndSideSwing()
    {
        _Controller.SwingStopped();
    }

    public void SetUpSwingCollider1()
    {
        _UpSwingCollider1.SetActive(true);
    }

    public void SetUpSwingCollider2()
    {
        _UpSwingCollider1.SetActive(false);
        _UpSwingCollider2.SetActive(true);
    }

    public void StopUpSwingColliders()
    {
        _UpSwingCollider1.SetActive(false);
        _UpSwingCollider2.SetActive(false);
    }

    public void EndUpSwing()
    {
        _Controller.SwingStopped();
    }
}
