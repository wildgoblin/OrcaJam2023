using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController Instance;

    public bool Flying { get; private set; }
    public bool Launching { get; private set; }

    [SerializeField] float gravityScale;

    [SerializeField] float launchSpeed;

    [SerializeField] float windSpeedVertical;
    [SerializeField] float windSpeedHorizontal;
    
    
    private void Awake()
    {
        // Ensure that there is only one instance of this object in the scene
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        Launching = true;
        Flying = false;
    }

    public void ChangeToFlying()
    {
        Launching = false;
        Flying = true;
    }

    public float GetLaunchSpeed()
    {
        return launchSpeed;
    }

    public void SetLaunchSpeed(float newLaunchSpeed)
    {
       launchSpeed = newLaunchSpeed;
    }

    public float GetGravityScale()
    {
        return gravityScale;
    }

    public void SetGravityScale(float newGravityScale)
    {
        gravityScale = newGravityScale;
    }

    public float GetWindSpeedVertical()
    {
        return windSpeedVertical;
    }

    public float GetWindSpeedHorizontal()
    {
        return windSpeedHorizontal;
    }
    public void SetWindSpeedVertical(float newWindSpeed)
    {
        windSpeedVertical = newWindSpeed;
    }
    public void SetWindSpeedHorizontal(float newWindSpeed)
    {
        windSpeedHorizontal = newWindSpeed;
    }


}
