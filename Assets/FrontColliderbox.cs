using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class FrontColliderbox : MonoBehaviour
{
    public MultiAimConstraint headIK;
    public MultiAimConstraint bodyIK;
    public MultiAimConstraint rHnadIK;

    public BoxCollider frontBoxCollider;
    public bool isWall;
    // Start is called before the first frame update
    void Start()
    {
        frontBoxCollider = GetComponent<BoxCollider>();
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Wall")
        {
            headIK.weight = 0;
            bodyIK.weight = 0;
            rHnadIK.weight = Mathf.Lerp(rHnadIK.weight, 0, 3f * Time.deltaTime);
            isWall = true;
        }
            
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Wall")
        {
            headIK.weight = 1;
            bodyIK.weight = 1;
            rHnadIK.weight = Mathf.Lerp(rHnadIK.weight, 1, 3f * Time.deltaTime);
            isWall = false;
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
