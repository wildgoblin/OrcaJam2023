using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopGrowing : MonoBehaviour
{
    public void StopGrowingMethod()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        player.GetComponent<GrowingBehaviour>().FinishGrowing();
    }
}
