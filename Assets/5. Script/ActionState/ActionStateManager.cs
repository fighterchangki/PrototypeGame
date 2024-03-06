using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;
public class ActionStateManager : MonoBehaviour
{
    public ActionBaseState currentState;
    public ReloadState Reload = new ReloadState();
    public DefaultState Default = new DefaultState();
    public SwapState Swap = new SwapState();
    [HideInInspector] public WeaponClassManager weaponClass;
    [HideInInspector] public WeaponManager currentWeapon;
    [HideInInspector] public WeaponAmmo ammo;
     public Animator anim;
    public MultiAimConstraint rHandAim;
    public TwoBoneIKConstraint lHandIk;
    AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        SwitchState(Default);
        
        anim = GetComponent<Animator>();
        weaponClass = GetComponent<WeaponClassManager>();
    }

    // Update is called once per frame
    void Update()
    {
        currentState.UpdateState(this);
    }
    public void SwitchState(ActionBaseState state,int scrollWeapon = 0)
    {
        if (state == Swap)
        {
            if (scrollWeapon > weaponClass.weapons.Length - 1)
                return;
            if (scrollWeapon == weaponClass.currentWeaponIndex)
                return;
        }
        currentState = state;
        currentState.EnterState(this);
    }
    public void WeaponReload()
    {
        ammo.Reload();
        SwitchState(Default);
    }
    public void MagOut()
    {
        audioSource.PlayOneShot(ammo.magOutSound);
    }
    public void MagIn()
    {
        audioSource.PlayOneShot(ammo.magInSound);
    }
    public void ReleaseSlide()
    {
        audioSource.PlayOneShot(ammo.releaseSlideSound);
        
    }
    public void SetWeapon(WeaponManager weapon)
    {
        currentWeapon = weapon;
        audioSource = weapon.audioSource;
        ammo = weapon.ammo;
    }
}
