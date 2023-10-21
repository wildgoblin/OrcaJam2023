using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    //References
    GameController gc;
    FlyingBehaviour fb;
    



    private void Start()
    {
        gc = GameController.Instance;
        fb = GetComponent<FlyingBehaviour>();
    }
    private void Update()
    {
        if (gc.Launching)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                fb.Launch();
            }

        }
        if (gc.Flying &&  !gc.ResettingStall)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                fb.TwistDown();
            }
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                fb.TwistUp();
            }

        }
    }
}
