using UnityEngine;

public class SwordWielder : MonoBehaviour
{
    [SerializeField][Tooltip("List of swords this wielder owns.")]
    private BasicSwordMachine[] _OwnedSwords;
    [SerializeField][Tooltip("Index of the owned sword to be set as the main weapon.")]
    private int _MainWeaponIndex = 0;
    [SerializeField][Tooltip("Index of the owned sword to be set as the alternate weapon")]
    private int _AltWeaponIndex = 1;

    [SerializeField][Tooltip("Transform to set the follow point for each owned sword to.")]
    private Transform _SwordFollowPoint;
    [SerializeField][Tooltip("Transform to set the attack point for each owned sword to.")]
    private Transform _SwordAttackPoint;

    void Awake()
    {
        foreach (BasicSwordMachine sword in _OwnedSwords)
        {
            if (sword != null)
            {
                sword.SetWeilder(this);
                sword.gameObject.SetActive(false);
            }
        }
        if (_OwnedSwords[_MainWeaponIndex] != null)
            _OwnedSwords[_MainWeaponIndex].gameObject.SetActive(true);
        if (_OwnedSwords[_AltWeaponIndex] != null)
            _OwnedSwords[_AltWeaponIndex].gameObject.SetActive(true);
    }

    void Update()
    {
        if (Input.GetButtonDown("AttackMain") && _OwnedSwords[_MainWeaponIndex] != null)
        {
            if (Input.GetButton("WeaponSecondary"))
                _OwnedSwords[_MainWeaponIndex].DoSecondaryAttack();
            else
                _OwnedSwords[_MainWeaponIndex].DoPrimaryAttack();
        }
        else if (Input.GetButtonDown("AttackAlt") && _OwnedSwords[_AltWeaponIndex] != null)
        {
            if (Input.GetButton("WeaponSecondary"))
                _OwnedSwords[_AltWeaponIndex].DoSecondaryAttack();
            else
                _OwnedSwords[_AltWeaponIndex].DoPrimaryAttack();
        }
    }

    void FixedUpdate()
    {
        Vector2 delta = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        _OwnedSwords[_MainWeaponIndex].SetDirection(delta.x > 0);
        _OwnedSwords[_AltWeaponIndex].SetDirection(delta.x > 0);
    }
}
