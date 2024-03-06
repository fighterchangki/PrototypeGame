using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;
public class WeaponClassManager : MonoBehaviour
{
    [SerializeField] public TwoBoneIKConstraint leftHandIk;
    public Transform recoilFollowPos;
    ActionStateManager actions;

    public WeaponManager[] weapons;
    public int currentWeaponIndex;
    private void Awake()
    {
        currentWeaponIndex = 0;
        for (int i = 0; i < weapons.Length; i++)
        {
            if (i == 0) weapons[i].gameObject.SetActive(true);
            else weapons[i].gameObject.SetActive(false);
        }
    }
    public void SetCurrentWeapon(WeaponManager weapon)
    {
        if (actions == null) actions = GetComponent<ActionStateManager>();
        leftHandIk.data.target.localPosition = weapon.leftHandPositionTarget;
        leftHandIk.data.target.eulerAngles = weapon.leftHandRotationTarget;
        leftHandIk.data.hint.localPosition = weapon.leftHandPositionHint;
        leftHandIk.data.hint.eulerAngles = weapon.leftHandRotationHint;
        actions.SetWeapon(weapon);
    }

    //public void ChangeWeapon(float direction)
    //{
    //    weapons[currentWeaponIndex].gameObject.SetActive(false);
    //    if (direction < 0)
    //    {
    //        if (currentWeaponIndex == 0) currentWeaponIndex = weapons.Length - 1;
    //        else currentWeaponIndex--;
    //    }
    //    else
    //    {
    //        if (currentWeaponIndex == weapons.Length - 1) currentWeaponIndex = 0;
    //        else currentWeaponIndex++;
    //    }
    //    weapons[currentWeaponIndex].gameObject.SetActive(true);
    //}
    public void ChangeWeapon(int direction)
    {
        if (direction > weapons.Length-1) return;
        if (direction == currentWeaponIndex) return;//같은 값일때
        weapons[currentWeaponIndex].gameObject.SetActive(false);
        currentWeaponIndex = direction;
        weapons[currentWeaponIndex].gameObject.SetActive(true);
        
        
    }
    public void WeaponPutAway()
    {
        //ChangeWeapon(actions.Default.scrollDirection);
        ChangeWeapon(actions.Default.scrollWeapon);
    }
    public void WeaponPulledOut()
    {
        actions.SwitchState(actions.Default);
    }
}
