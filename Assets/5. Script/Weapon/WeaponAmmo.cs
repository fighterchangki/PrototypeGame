using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAmmo : MonoBehaviour
{
    public int clipSize;
    public int extraAmmo;
    public int currentAmmo;

    public AudioClip magInSound;
    public AudioClip magOutSound;
    public AudioClip releaseSlideSound;
    // Start is called before the first frame update
    void Awake()
    {
        currentAmmo = clipSize;
    }

    public void Reload()
    {
        if (extraAmmo >= clipSize)//º¸Á¶ ÃÑ¾ËÀÌ ÅºÃ¢Å©±â º¸´Ù ¸¹À»¶§
        {
            int ammoToReload = clipSize - currentAmmo;
            extraAmmo -= ammoToReload;
            currentAmmo += ammoToReload;
        }
        else if (extraAmmo > 0)//º¸Á¶ ÃÑ¾ËÀÌ ÅºÃ¢Å©±â º¸´Ù ÀÛÀ»¶§
        {
            if (extraAmmo + currentAmmo > clipSize)//ÇöÀç ÃÑ¾Ë + º¸Á¶ ÃÑ¾ËÀÌ ÅºÃ¢Å©±â º¸´Ù Å¬¶§
            {
                int leftOverAmmo = extraAmmo + currentAmmo - clipSize;// ³²Àº ÃÑ¾Ë
                extraAmmo = leftOverAmmo;// ³²Àº ÃÑ¾ËÀ» º¸Á¶ ÃÑ¾Ë·Î
                currentAmmo = clipSize;// Å¬¸³ Å©±â¸¦ ÇöÀç ÃÑ¾Ë·Î
            }
            else//ÇöÀç ÃÑ¾Ë + º¸Á¶ ÃÑ¾ËÀÌ ÅºÃ¢Å©±â º¸´Ù ÀÛÀ»¶§ 
            {
                currentAmmo += extraAmmo;// ³²Àº º¸Á¶ ÃÑ¾ËµéÀ» ÇöÀç ÃÑ¾Ë¿¡ ´õÇÔ
                extraAmmo = 0;// º¸Á¶ ÃÑ¾ËÀº ´Ù¾¸
            }
        }

    }
    // Update is called once per frame

}
