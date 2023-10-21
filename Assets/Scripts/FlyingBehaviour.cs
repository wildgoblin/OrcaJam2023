using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingBehaviour : MonoBehaviour
{
    //References
    Rigidbody2D rb;
    GameController gc;

    
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        gc = GameController.Instance;

        if(gc.Launching)
        {
            rb.gravityScale = 0;
        }
        if(gc.Flying)
        {
            rb.gravityScale = gc.GetGravityScale();
        }
    }

    public void Launch()
    {
        rb.gravityScale = gc.GetGravityScale();
        rb.AddForce(new Vector2(gc.GetLaunchSpeed(), gc.GetLaunchSpeed()));
    }

    public void WindEffect()
    {
        rb.AddForce(new Vector2(gc.GetWindSpeedHorizontal(), gc.GetWindSpeedVertical()));
    }
}
