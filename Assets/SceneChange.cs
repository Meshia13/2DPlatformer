using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    [SerializeField] private string sceneName;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //When player collides into door, The End scene will be triggered. 
        if (collision.gameObject.tag == "Player")
        {
            SceneManager.LoadScene(sceneName); 
        }
    }
}
