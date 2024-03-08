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
        if (extraAmmo >= clipSize)//���� �Ѿ��� źâũ�� ���� ������
        {
            int ammoToReload = clipSize - currentAmmo;
            extraAmmo -= ammoToReload;
            currentAmmo += ammoToReload;
        }
        else if (extraAmmo > 0)//���� �Ѿ��� źâũ�� ���� ������
        {
            if (extraAmmo + currentAmmo > clipSize)//���� �Ѿ� + ���� �Ѿ��� źâũ�� ���� Ŭ��
            {
                int leftOverAmmo = extraAmmo + currentAmmo - clipSize;// ���� �Ѿ�
                extraAmmo = leftOverAmmo;// ���� �Ѿ��� ���� �Ѿ˷�
                currentAmmo = clipSize;// Ŭ�� ũ�⸦ ���� �Ѿ˷�
            }
            else//���� �Ѿ� + ���� �Ѿ��� źâũ�� ���� ������ 
            {
                currentAmmo += extraAmmo;// ���� ���� �Ѿ˵��� ���� �Ѿ˿� ����
                extraAmmo = 0;// ���� �Ѿ��� �پ�
            }
        }

    }
    // Update is called once per frame

}
