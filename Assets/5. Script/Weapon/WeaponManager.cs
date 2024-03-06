using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum WeaponStyle
{
    rifle,
    pistol,
    throwing
}
public class WeaponManager : MonoBehaviour
{
    
    public WeaponStyle weaponStyle;
    [Header("Fire Rate")]
    [SerializeField] float fireRate;
    float fireRateTimer;
    [SerializeField] bool semiAuto;
    [Header("Bullet Properties")]
    [SerializeField] GameObject bullet;
    [SerializeField] Transform barrelPos;
    [SerializeField] float bulletVelocity;
    [SerializeField] int bulletPerShot;
    [Header("Sound")]
    //총 소리
    [SerializeField] AudioClip gunShot;
    public AudioSource audioSource;
    [Header("Mag")]
    // 탄창
    public WeaponAmmo ammo;
    [Header("StateManager")]
    AimStateManager aim;
    public ActionStateManager actions;
    [Header("recoil")]
    WeaponRecoil recoil;
    [Header("muzzleFlash")]
    public GameObject muzzleFlash;
    Light muzzleFlashLight;
    float lightIntensity;
    [SerializeField]float LightReturnSpeed = 1;
    [SerializeField] float FlashReturnSpeed = 0.1f;
    [Header("Bloom")]
    WeaponBloom bloom;

    [Header("Enemy")]
    public float enemyKickbackForce = 20;
    [Header("WeaponChange")]
    public Vector3 leftHandPositionTarget, leftHandPositionHint;
    public Vector3 leftHandRotationTarget, leftHandRotationHint;

    public WeaponClassManager weaponClass;
    [Header("Animator")]
    public Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        aim = GetComponentInParent<AimStateManager>();
        bloom = GetComponent<WeaponBloom>();
        actions = GetComponentInParent<ActionStateManager>();
        muzzleFlashLight = GetComponentInChildren<Light>();
        lightIntensity = muzzleFlashLight.intensity;
        muzzleFlash.transform.localScale = new Vector3(0, 0, 0);
        muzzleFlashLight.intensity = 0;
        fireRateTimer = fireRate;
    }
    private void OnEnable()
    {
        if (weaponClass == null)
        {
            anim = GetComponentInParent<Animator>();
            weaponClass = GetComponentInParent<WeaponClassManager>();
            ammo = GetComponent<WeaponAmmo>();
            recoil = GetComponent<WeaponRecoil>();
            recoil.recoilFollowPos = weaponClass.recoilFollowPos;
            audioSource = GetComponent<AudioSource>();
        }
        switch (weaponStyle)
        {
            case WeaponStyle.rifle:
                SwitchWeaponHand(0);
                break;
            case WeaponStyle.pistol:
                SwitchWeaponHand(1);
                break;
            case WeaponStyle.throwing:
                SwitchWeaponHand(2);
                break;
        }
        weaponClass.SetCurrentWeapon(this);
    }
    public void SwitchWeaponHand(int weapon)
    {
        for (int i = 0; i < anim.layerCount; i++)
        {
            if (i == 0)
            {
                anim.SetLayerWeight(i, 1);
            }
            if (i == weapon + 1)
            {
                anim.SetLayerWeight(i, 1);
            }
            else
            {
                anim.SetLayerWeight(i, 0);
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (shouldFire()) Fire();
        muzzleFlashLight.intensity = Mathf.Lerp(muzzleFlashLight.intensity, 0, LightReturnSpeed * Time.deltaTime);
        muzzleFlash.transform.localScale = new Vector3(Mathf.Lerp(muzzleFlash.transform.localScale.x, 0, FlashReturnSpeed * Time.deltaTime), Mathf.Lerp(muzzleFlash.transform.localScale.y, 0, FlashReturnSpeed * Time.deltaTime), Mathf.Lerp(muzzleFlash.transform.localScale.z, 0, FlashReturnSpeed * Time.deltaTime));
    }
    bool shouldFire()
    {
        fireRateTimer += Time.deltaTime;
        if (fireRateTimer < fireRate) return false;//차탄까지 텀이 아직 남았을때
        if (ammo.currentAmmo == 0) return false;//현재 총알이 0일때
        if (actions.currentState == actions.Reload) return false;// 현재 상태가 재장전 상태일때
        if (actions.currentState == actions.Swap) return false;
        if (semiAuto && Input.GetKeyDown(KeyCode.Mouse0)) return true;//반자동
        if (!semiAuto && Input.GetKey(KeyCode.Mouse0)) return true;// 자동
        return false;
    }
    void Fire()
    {
        fireRateTimer = 0;
        if (barrelPos != null)
        {
            barrelPos.LookAt(aim.aimPos);
        }
        recoil.TriggerRecoil();
        TriggerMuzzleFlash();

        ammo.currentAmmo--;

        barrelPos.localEulerAngles = bloom.BloomAngle(barrelPos);
        audioSource.PlayOneShot(gunShot);
        for (int i = 0; i < bulletPerShot; i++)
        {
            GameObject currentBullet = Instantiate(bullet, barrelPos.position, barrelPos.rotation);
            Rigidbody rb = currentBullet.GetComponent<Rigidbody>();
            Bullet bulletScript = currentBullet.GetComponent<Bullet>();
            bulletScript.weapon = this;
            bulletScript.dir = barrelPos.transform.forward;

            rb.AddForce(barrelPos.forward * bulletVelocity, ForceMode.Impulse);
            //muzzleFlash.SetActive(false);
        }
    }
    void TriggerMuzzleFlash()
    {
        muzzleFlash.SetActive(true);
        muzzleFlashLight.intensity = lightIntensity;
        muzzleFlash.transform.localScale = new Vector3(0.05f, 0.05f, 0.05f);
    }
}
