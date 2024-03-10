using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultState : ActionBaseState
{
    public float scrollDirection;
    public int preScrollWeapon;
    public int scrollWeapon;
    public override void EnterState(ActionStateManager actions)
    {
    }
    public override void UpdateState(ActionStateManager actions)
    {
        if(!actions.frontColliderBox.isWall)
            actions.rHandAim.weight = Mathf.Lerp(actions.rHandAim.weight, 1, 15f * Time.deltaTime);
        if (actions.lHandIk.weight == 0) actions.lHandIk.weight = 1;
        if (Input.GetKeyDown(KeyCode.R) && CanReload(actions))
        {
            actions.SwitchState(actions.Reload);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ScrollWeapon(0, actions);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            ScrollWeapon(1, actions);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            ScrollWeapon(2, actions);
        }
    }
    void ScrollWeapon(int scrollWeapon, ActionStateManager actions)
    {
        if (preScrollWeapon == scrollWeapon) return;
        this.scrollWeapon = scrollWeapon;
        preScrollWeapon = scrollWeapon;

        actions.SwitchState(actions.Swap,scrollWeapon);
    }
    bool CanReload(ActionStateManager action)
    {
        if (action.ammo.currentAmmo == action.ammo.clipSize)
        {
            return false;//현재 탄창이 다찼을때
        }
        else if (action.ammo.extraAmmo == 0)
        {
            return false;// 남은 탄약이 없을때
        } 
        else return true;
    }
}
