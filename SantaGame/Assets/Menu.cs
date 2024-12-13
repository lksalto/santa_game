using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [SerializeField] GameObject miniMenu;
    
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(miniMenu != null)
            {
                miniMenu.SetActive(!miniMenu.active);
            }
        }
    }

    public void ShowRestart()
    {
        transform.GetChild(0).gameObject.SetActive(true);
    }
    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }
    public void GoToScene(int idx)
    {
        SceneManager.LoadScene(idx);
    }


}
