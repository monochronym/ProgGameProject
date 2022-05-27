using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonSceneChanger : MonoBehaviour
{
    //public string Name;
    public string Name;
    public float time;
    private float starttime;
    public void SceneChanger(string name)
    {
        SceneManager.LoadScene(name);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void Start()
    {
        starttime = Time.time;
    }
    public void Update()
    {
        if (Time.time > starttime + time) SceneManager.LoadScene(Name);
    }
}
