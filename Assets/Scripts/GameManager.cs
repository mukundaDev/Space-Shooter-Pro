using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private bool _isGameOVer;
   


   public  void GameOver()
    {
        _isGameOVer = true;
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && _isGameOVer == true) 
        {
            SceneManager.LoadScene(1);
        }
    }
}
