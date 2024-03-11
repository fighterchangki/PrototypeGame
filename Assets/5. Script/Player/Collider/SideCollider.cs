using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideCollider : MonoBehaviour
{
    public FrontColliderbox frontColliderbox;
    // Start is called before the first frame update
    void Start()
    {
        frontColliderbox = GetComponentInParent<FrontColliderbox>();
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Wall")
        {
            frontColliderbox.headIK.weight = 0;
            frontColliderbox.bodyIK.weight = 0;
            frontColliderbox.rHnadIK.weight = Mathf.Lerp(frontColliderbox.rHnadIK.weight, 0, 3f * Time.deltaTime);
            frontColliderbox.isWall = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Wall")
        {
            frontColliderbox.headIK.weight = 1;
            frontColliderbox.bodyIK.weight = 1;
            frontColliderbox.rHnadIK.weight = Mathf.Lerp(frontColliderbox.rHnadIK.weight, 1, 3f * Time.deltaTime);
            frontColliderbox.isWall = false;
        }
    }
}
