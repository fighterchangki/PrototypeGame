using System.Collections;
using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using UnityEngine;
public class AimState : AimBaseState
{
    public override void EnterState(AimStateManager aim)
    {
        aim.anim.SetBool("Aiming", true);
        aim.currentFov = aim.adsFov;
        switch (aim.weaponClassManager.weapons[aim.weaponClassManager.currentWeaponIndex].weaponStyle)
        {
            case WeaponStyle.rifle:
                Debug.Log("라이플입니다");
                aim.lHandIk.data.targetPositionWeight = Mathf.Lerp(0.15f, 1, 0.00001f * Time.deltaTime);
                aim.weaponClassManager.leftHandIk.data.targetRotationWeight = 1;
                aim.weaponClassManager.leftHandIk.data.hintWeight = 1;
                break;
            case WeaponStyle.pistol:
                aim.weaponClassManager.leftHandIk.data.targetPositionWeight = 0;
                aim.weaponClassManager.leftHandIk.data.targetRotationWeight = 0;
                aim.weaponClassManager.leftHandIk.data.hintWeight = 0;
                Debug.Log("권총입니다");
                break;
            case WeaponStyle.throwing:
                break;
        }
    }
    public override void UpdateState(AimStateManager aim)
    {
        if (Input.GetKeyUp(KeyCode.Mouse1)) aim.SwitchState(aim.Hip);
        


    }
    
}
