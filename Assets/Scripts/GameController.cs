using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController Instance;

    public bool Flying { get; private set; }
    public bool Launching { get; private set; }

    public bool Landing { get; private set; }

    public bool ResettingStall { get; set; }

    public bool FirstLandingPassed { get; set; }
    public bool SecondLandingPassed { get; set; }

    [SerializeField] float gravityScale;
    [Header("Launching Control")]
    [SerializeField] float launchSpeed;
    [Header("Wind Control")]
    [SerializeField] float windSpeedVertical;
    [SerializeField] float windSpeedHorizontal;

    [Header("Flying Control")]
    [SerializeField] float speedLimit;
    [Tooltip("Applied immediate force when turning up")]
    [SerializeField] float twistUpForce;
    [Tooltip("Applied immediate force when turning down")]
    [SerializeField] float twistDownForce;
    [Tooltip("Applied immediate intensity to forces")]
    [SerializeField] float twistMultiplier;
    [Tooltip("How far the seed can twist forward/down before stalling")]
    [SerializeField] int twistPositionMin;
    [Tooltip("How far the seed can twist backwards/up before stalling")]
    [SerializeField] int twistPositionMax;
    [Tooltip("The rotating angle. decrease this angle when increasing the positions")]
    [SerializeField] int rotationAngle;

    [Tooltip("How long it takes to regain play control")]
    [SerializeField] float stallWaitTime;
    [Tooltip("Penalty of downward force when stalling")]
    [SerializeField] float stallDownwardForce;
    [Tooltip("How fast the vertical down speed is when pitching forward/down")]
    [SerializeField] float twistDownSpeed;
    [Tooltip("How slow the vertical up speed is when pitching backward/up")]
    [SerializeField] float twistUpSpeed;

    [Header("Spawn Controls")]
    [SerializeField] int windSpawnMin;
    [SerializeField] int windSpawnMax;
    [SerializeField] float windSpawnSpacingX;
    [SerializeField] float windSpawnSpacingY;
    
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
        Landing = false;       
        
    }

    public void ChangeToFlying()
    {
        Landing = false;
        Launching = false;
        Flying = true;
    }
    public void ChangeToLanding()
    {
        Flying = false;
        Launching = false;
        Landing = true;
    }

    public void ChangeToLaunching()
    {
        Flying = false;
        Launching = true;
        Landing = false;
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

    public float GetSpeedLimit()
    {
        return speedLimit;
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

    public float GetTwistDownForce()
    {
        return twistDownForce;
    }

    public float GetTwistUpForce()
    {
        return twistUpForce;
    }

    public float GetTwistMultiplier()
    {
        return twistMultiplier;
    }

    public float GetTwistPositionMax()
    {
        return twistPositionMax;
    }

    public float GetTwistPositionMin()
    {
        return twistPositionMin;
    }

    public float GetRotationAngle()
    {
        return rotationAngle;
    }

    public float GetStallWaitTime()
    {
        return stallWaitTime;
    }

    public float GetTwistDownSpeed()
    {
        return twistDownSpeed;
    }

    public float GetTwistUpSpeed()
    {
        return twistUpSpeed;
    }

    public float GetStallDownwardForce()
    {
        return stallDownwardForce;
    }    

    public int GetWindSpawnTimeMin()
    {
        return windSpawnMin;
    }

    public int GetWindSpawnTimeMax()
    {
        return windSpawnMax;
    }

    public float GetWindSpawnSpacingX()
    {
        return windSpawnSpacingX;
    }

    public float GetWindSpawnSpacingY()
    {
        return windSpawnSpacingY;
    }



    




}
