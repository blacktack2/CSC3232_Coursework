using System.Collections.Generic;
using UnityEngine;

public class SwordWielder : MonoBehaviour
{
    [SerializeField, Tooltip("List of swords this wielder owns.")]
    private List<BasicSwordMachine> _OwnedSwords;
    [SerializeField, Tooltip("Index of the owned sword to be set as the main weapon.")]
    private int _MainWeaponIndex = 0;
    [SerializeField, Tooltip("Index of the owned sword to be set as the alternate weapon")]
    private int _AltWeaponIndex = 1;

    private BasicSwordMachine _MainWeapon {get {return _OwnedSwords[_MainWeaponIndex];}}
    private BasicSwordMachine _AltWeapon {get {return _OwnedSwords[_AltWeaponIndex];}}

    void Start()
    {
        foreach (BasicSwordMachine sword in _OwnedSwords)
        {
            if (sword != null)
            {
                sword.SetWeilder(this);
                DeactivateSword(sword);
            }
        }
        if (_MainWeapon != null)
            ActivateSword(_MainWeapon);
        if (_AltWeapon != null)
            ActivateSword(_AltWeapon);
    }

    void Update()
    {
        if (Input.GetButtonDown("AttackMain") && _MainWeapon != null)
        {
            if (Input.GetButton("WeaponSecondary"))
                _MainWeapon.DoSecondaryAttack();
            else
                _MainWeapon.DoPrimaryAttack();
        }
        else if (Input.GetButtonDown("AttackAlt") && _AltWeapon != null)
        {
            if (Input.GetButton("WeaponSecondary"))
                _AltWeapon.DoSecondaryAttack();
            else
                _AltWeapon.DoPrimaryAttack();
        }
    }

    void FixedUpdate()
    {
        Vector2 delta = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        if (_MainWeapon != null)
            _MainWeapon.SetDirection(delta.x > 0);
        if (_AltWeapon != null)
            _AltWeapon.SetDirection(delta.x > 0);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Rigidbody2D otherRigidbody2D = other.attachedRigidbody;
        if (otherRigidbody2D != null)
        {
            BasicSwordMachine otherSword = otherRigidbody2D.GetComponent<BasicSwordMachine>();
            if (otherSword != null && !_OwnedSwords.Contains(otherSword))
                AddSword(otherSword);
        }
    }

    private void ActivateSword(BasicSwordMachine sword)
    {
        sword.gameObject.SetActive(true);
        sword.transform.position = transform.position;
    }

    private void DeactivateSword(BasicSwordMachine sword)
    {
        sword.gameObject.SetActive(false);
    }

    public void AddSword(BasicSwordMachine sword)
    {
        for (int i = 0; i < _OwnedSwords.Count; i++)
        {
            if (_OwnedSwords[i] == null)
            {
                _OwnedSwords[i] = sword;
                sword.SetWeilder(this);
                if (i == _MainWeaponIndex || i == _AltWeaponIndex)
                    ActivateSword(sword);
                break;
            }
        }
    }
}
