using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

///////////////////////////////////////////////////////////////////////////////////////
//Thorns are placed throughout the game. If the play steps on or jumps into a thorn
//player will be placed at the start position
///////////////////////////////////////////////////////////////////////////////////////

public class Thorn : MonoBehaviour
{
  private void OnTriggerEnter2D(Collider2D collision)
    {
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //If player collides with 'GameObject - Thorns' then the scene will restart and player will be at the start position
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        if(collision.gameObject.tag == "Player")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name); //Re-load the current scene back to the beginning of game//
        }
    }
}
