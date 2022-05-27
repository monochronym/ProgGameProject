using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehavour: MonoBehaviour
{
    // Start is called before the first frame update
    public Vector2 move;
    public Rigidbody2D body;
    public float speedX;
    public float speedY;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        move.x = Input.GetKey(KeyCode.S) ? -speedX : 0;
        move.x = Input.GetKey(KeyCode.D) ?  speedX: move.x;
        body.position += move;
    }
}
