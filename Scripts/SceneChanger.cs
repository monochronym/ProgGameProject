using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{

    public Transform Camera;
    public Transform Player;
    public string Name;
    public Player plr;
    public float time;
    void Update()
    {
        if (Camera.position.x + 20 <= Player.position.x)
            SceneManager.LoadScene(Name);
        if (!plr.enabled)
            if (plr.timeOfDeath + time < Time.time)
                SceneManager.LoadScene("GameOver");
    }
}
