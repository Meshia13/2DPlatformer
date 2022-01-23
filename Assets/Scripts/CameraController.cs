using System.Collections;
using System.Collections.Generic;
using UnityEngine;


///////////////////////////////////////////////////////
/// This script allows for the camera to follow player
///////////////////////////////////////////////////////
public class CameraController : MonoBehaviour
{
    public Transform player;

    ////////////////////////////////////////////////////////////////////////
    //Camera follows player. X and Y values of camera is being build
    //by players x and y transforms. Camera is keeping it's own z transform
    ////////////////////////////////////////////////////////////////////////
    void Update()
    {
     transform.position = new Vector3(player.position.x, player.position.y, transform.position.z);
    }
}
