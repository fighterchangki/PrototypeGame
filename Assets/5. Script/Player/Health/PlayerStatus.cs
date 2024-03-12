using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerStatus : MonoBehaviour
{
    [SerializeField] private Slider hpBar;
    [SerializeField] private float maxHp = 100;
    [SerializeField] private float curHp = 100;
    
    float hpBarP;
    // Start is called before the first frame update
    void Start()
    {
        hpBar.value = (float)curHp / (float)maxHp;
    }
    // Update is called once per frame
    public void Damege(int damege)
    {
        if (curHp > 0)
        {
            curHp -= damege;
        }
        else
        {
            curHp = 0;
        }
        hpBarP = (float)curHp / (float)maxHp;
        HandleHp();
    }   
    private void HandleHp()
    {
        hpBar.value = Mathf.Lerp(hpBar.value, (float)curHp / (float)maxHp,Time.deltaTime*10);
    }


}
