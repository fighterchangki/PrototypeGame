using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class UIManager : MonoBehaviour
{
    [Header("GameObject")]
    [SerializeField] private GameObject hitUI;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject weaponPanel;
    [SerializeField] private GameObject itemPickUpText;
    [Header("Button")]
    [SerializeField] private Button homeButton;
    [SerializeField] private Button ReStarthomeButton;
    [SerializeField] private Button ReStartButton;
    [Header("Text")]
    [SerializeField] private TMP_Text bulletText;
    [SerializeField] private TMP_Text vacinText;
    [Header("Manager")]
    [SerializeField] private GameManager gameManager;
    [SerializeField] private FadeInOut fadeInOutManager;
    [Header("Test")]
    public bool isFade;
    //[SerializeField] private Action onCompleteCallback;
    public class WeaponUI
    {
        public GameObject weapon;
        public GameObject weaponUI;
    }
    public WeaponUI weaponUI = new WeaponUI();
    // Start is called before the first frame update
    void Start()
    {
        if (homeButton != null)
        {
            homeButton.onClick.AddListener(HomeButtonClick);
        }
        if (ReStartButton != null)
        {
            ReStartButton.onClick.AddListener(ReStartClick);
        }
        if (ReStarthomeButton != null)
        {
            ReStarthomeButton.onClick.AddListener(HomeButtonClick);
        }
        ReStartButton.gameObject.SetActive(false);
        ReStarthomeButton.gameObject.SetActive(false);
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        weaponPanel = GameObject.Find("WeaponPanel");
        player = GameObject.Find("Player");
        itemPickUpText = GameObject.Find("PickUpText");
        ChangeWeapon();
        UpdateVacin();
    }
    public void HomeButtonClick()
    {
        SceneChangeManager.Instance.SceneChange("StartScene");
    }
    public void ReStartClick()
    {
        SceneChangeManager.Instance.SceneChange("GameScene");
    }
    public void ChangeWeapon()
    {
        Transform[] allPlayerchildren = player.GetComponentsInChildren<Transform>();
        Transform[] allWeaponPanelchildren = weaponPanel.GetComponentsInChildren<Transform>();
        for (int i = 0; i < allPlayerchildren.Length; i++)
        {
            if (allPlayerchildren[i].gameObject.layer == 9)
            {
                weaponUI.weapon = allPlayerchildren[i].gameObject;
                UpdateBulletInfo();
            }
        }
        for (int i = 0; i < allWeaponPanelchildren.Length; i++)
        {
            if (allWeaponPanelchildren[i].gameObject.layer == 10)
            {
                weaponUI.weaponUI = allWeaponPanelchildren[i].gameObject;
            }
        }
    }
    public void UpdateVacin()
    {
        vacinText.text = "Vacin : "+gameManager.vacinCount + "/20";
    }
    public void UpdateBulletInfo()
    {
        bulletText.text = weaponUI.weapon.GetComponent<WeaponAmmo>().currentAmmo + " / " + weaponUI.weapon.GetComponent<WeaponAmmo>().extraAmmo;
    }
    // Update is called once per frame
    void Update()
    {

    }
    public void PickUp(bool isPickUp)
    {
        itemPickUpText.SetActive(isPickUp);
    }
    #region HitÈ¿°ú
    public void Hit()
    {
        StartCoroutine("HitUI");
    }
    IEnumerator HitUI()
    {
        hitUI.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        hitUI.SetActive(false);
    }
    #endregion
}
