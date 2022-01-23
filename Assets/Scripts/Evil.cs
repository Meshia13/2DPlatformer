using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/////////////////////////////////////////////////////////////////////////////
///Class Evil is the parent class. The enemies will inherit the methods from
///this class.
/////////////////////////////////////////////////////////////////////////////

public class Evil : MonoBehaviour
{
    //Protected- meaning only the class children can inherit or has access
    protected Animator anim;

    //////////////////////////////////////////////////////////////
    //VIRTUAL FUNCTION - The child or derived class can override
    //and make changes in this function
    //////////////////////////////////////////////////////////////
    protected virtual void Start()
    {
        anim = GetComponent<Animator>();
    }
    public void JumpedOn()
    {
        anim.SetTrigger("Death");
    }

    /////////////////////////////////////////////////////////
    //The enemy will be destoyed when the player jumps on it
    /////////////////////////////////////////////////////////
    void Death()
    {
        Destroy(this.gameObject);
    }
}
