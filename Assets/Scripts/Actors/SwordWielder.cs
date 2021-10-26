using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordWielder : MonoBehaviour
{
    [SerializeField]
    private SwordController[] ownedSwords;
    [SerializeField]
    private int mainWeapon = 0;
    [SerializeField]
    private int altWeapon = 1;

    [SerializeField]
    private Transform swordFollowPoint;
    [SerializeField]
    private Transform swordAttackPoint;

    void Awake()
    {
        foreach (SwordController sword in ownedSwords)
        {
            if (sword != null)
            {
                sword.SetWeilder(this, swordFollowPoint, swordAttackPoint);
                sword.gameObject.SetActive(false);
            }
        }
        if (ownedSwords[mainWeapon] != null)
            ownedSwords[mainWeapon].gameObject.SetActive(true);
        if (ownedSwords[altWeapon] != null)
            ownedSwords[altWeapon].gameObject.SetActive(true);
    }

    void Update()
    {
        if (Input.GetButtonDown("AttackMain") && ownedSwords[mainWeapon] != null)
        {
            if (Input.GetButton("WeaponSecondary"))
                ownedSwords[mainWeapon].DoSecondaryAttack();
            else
                ownedSwords[mainWeapon].DoPrimaryAttack();
        }
        else if (Input.GetButtonDown("AttackAlt") && ownedSwords[altWeapon] != null)
        {
            if (Input.GetButton("WeaponSecondary"))
                ownedSwords[altWeapon].DoSecondaryAttack();
            else
                ownedSwords[altWeapon].DoPrimaryAttack();
        }
    }
}
