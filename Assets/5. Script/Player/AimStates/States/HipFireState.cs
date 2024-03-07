using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HipFireState : AimBaseState
{
    // Start is called before the first frame update
    public override void EnterState(AimStateManager aim)
    {
        aim.anim.SetBool("Aiming", false);
        aim.currentFov = aim.hipFov;
        switch (aim.weaponClassManager.weapons[aim.weaponClassManager.currentWeaponIndex].weaponStyle)
        {
            case WeaponStyle.rifle:
                aim.lHandIk.data.targetPositionWeight = Mathf.Lerp(1, 0.15f, 0.0000001f * Time.deltaTime);
                aim.weaponClassManager.leftHandIk.data.targetRotationWeight = 1;
                aim.weaponClassManager.leftHandIk.data.hintWeight = 1;
                break;
            case WeaponStyle.pistol:
                aim.weaponClassManager.leftHandIk.data.targetPositionWeight = 0;
                aim.weaponClassManager.leftHandIk.data.targetRotationWeight = 0;
                aim.weaponClassManager.leftHandIk.data.hintWeight = 0;
                break;
            case WeaponStyle.throwing:
                break;
        }
    }
    public override void UpdateState(AimStateManager aim)
    {
        if (Input.GetKey(KeyCode.Mouse1)) aim.SwitchState(aim.Aim);
        

    }
}
