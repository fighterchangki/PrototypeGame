using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject hitUI;

    // Start is called before the first frame update
    void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        
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
