using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyName : MonoBehaviour
{
    // Start is called before the first frame update
    public string name;
    public Vector2 move;
    void Start()
    {
        Debug.Log(name);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.A))
            Debug.Log("Test");
        if (Input.GetKeyUp(KeyCode.S))
            move.x+=5;
    }
}
