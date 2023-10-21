using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    //References
    GameController gc;

    

    private void Start()
    {
        gc = GameController.Instance;
    }
    private void Update()
    {
        if (gc.Launching)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                GetComponent<FlyingBehaviour>().Launch();
            }
        }
    }
}
