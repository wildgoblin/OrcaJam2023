
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class FlyingBehaviour : MonoBehaviour
{
    //References
    Rigidbody2D rb;
    GameController gc;
    private int twistPositionCurrent;
    [SerializeField] GameObject seedSprite;
    [SerializeField] TitleAppear titleAppear;
    [SerializeField] TitleAppear creditsAppear;
    [SerializeField] QuoteGenerator quoteGenerator;
    [SerializeField] Transform textsParent;
    [SerializeField] MusicController musicController;
    bool twistingUp;
    bool twistingDown;
    bool collided;

    

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        gc = GameController.Instance;

        twistPositionCurrent = 0;
        gc.ResettingStall = false;
        twistingUp = false;
        twistingDown = false;
        collided = false;

        if(gc.Launching)
        {
            rb.gravityScale = 0;
        }
        if(gc.Flying)
        {
            rb.gravityScale = gc.GetGravityScale();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Ground" && !collided)
        {
            LandLogic();
        }
    }

    private void LandLogic()
    {
        collided = true;
        rb.angularVelocity = 0;
        rb.transform.rotation = Quaternion.Euler(0, 0, 0);
        rb.velocity = Vector2.zero;
        gc.ChangeToLanding();
        GetComponent<GrowingBehaviour>().PlayGrowingSequence();
        if(gc.SecondLandingPassed && gc.FirstLandingPassed)
        {
            quoteGenerator.GenerateNewQuote(textsParent);

        }
        if (!gc.SecondLandingPassed && gc.FirstLandingPassed)
        {
            Debug.Log("PLAYING SECOND PASS");
            creditsAppear.FadeIn();
            creditsAppear.transform.SetParent(textsParent, true);
            gc.SecondLandingPassed = true;
        }
        if (!gc.FirstLandingPassed)
        {
            titleAppear.FadeIn();
            titleAppear.transform.SetParent(textsParent, true);
            musicController.StartMusic();
            gc.FirstLandingPassed = true;
        }


    }

    public void Launch()
    {
        transform.position = GetComponent<GrowingBehaviour>().GetCenterWaypoint().transform.position;
        rb.gravityScale = gc.GetGravityScale();
        rb.AddForce(new Vector2(gc.GetLaunchSpeed(), gc.GetLaunchSpeed()));
        gc.ChangeToFlying();
    }

    public void WindEffect()
    {
        rb.AddForce(new Vector2(gc.GetWindSpeedHorizontal(), gc.GetWindSpeedVertical()));
    }
    public void TwistDown()
    {
        
        if (twistingUp && twistPositionCurrent < 0)
        {
            twistingDown = false;
            return;
        }
        twistingDown = true;
        twistingUp = false;
        if (twistPositionCurrent <= 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, 0);
            rb.AddForce(new Vector2(rb.velocity.x * gc.GetTwistDownSpeed(), gc.GetTwistDownForce() * (twistPositionCurrent * gc.GetTwistMultiplier())));
        }


        twistPositionCurrent -= 1;
        RotateSeedSprite();

        if (twistPositionCurrent <= gc.GetTwistPositionMin())
        {
            twistPositionCurrent += 1;
            RotateSeedSprite();
            StartCoroutine(ResetDownStall());
        }

    }

    public void TwistUp()
    {
        
        if (twistingDown &&  twistPositionCurrent > 0)
        {
            twistingUp = false;
            return;
        }

        twistingUp = true;
        twistingDown = false;
        if (twistPositionCurrent >= 0 )
        {
            rb.velocity = new Vector2(rb.velocity.x, 0);
            rb.AddForce(new Vector2(rb.velocity.x * gc.GetTwistUpSpeed(), gc.GetTwistUpForce() * (twistPositionCurrent * gc.GetTwistMultiplier())));
        }


        twistPositionCurrent += 1;
        RotateSeedSprite();

        if (twistPositionCurrent >= gc.GetTwistPositionMax())
        {
            twistPositionCurrent += 1;
            RotateSeedSprite();
            StartCoroutine(ResetUpStall());
        }  
    }

    public void RotateSeedSprite()
    {
        float rotationAngle = gc.GetRotationAngle() * twistPositionCurrent;
        // Apply the rotation to seedSprite
        seedSprite.transform.rotation = Quaternion.Euler(0, 0, rotationAngle);
    }

    IEnumerator ResetUpStall()
    {
        gc.ResettingStall = true;
        float waitTime = gc.GetStallWaitTime() / gc.GetTwistPositionMax();
        while (gc.ResettingStall)
        {
            rb.AddForce(new Vector2(rb.velocity.x / 2, -gc.GetStallDownwardForce()));
            while(twistPositionCurrent > 0)
            {
                twistPositionCurrent -= 1;
                RotateSeedSprite();
                yield return new WaitForSeconds(waitTime);
            }
            
            yield return null;
            gc.ResettingStall = false;
        }        
    }

    IEnumerator ResetDownStall()
    {
        gc.ResettingStall = true;
        float waitTime = gc.GetStallWaitTime() / Mathf.Abs(gc.GetTwistPositionMin());
        while (gc.ResettingStall)
        {
            while (twistPositionCurrent < 0)
            {
                twistPositionCurrent += 1;
                RotateSeedSprite();
                yield return new WaitForSeconds(waitTime);
            }

            yield return null;
            gc.ResettingStall = false;
        }
    }

    private void LateUpdate()
    {
        if (rb.velocity.x > gc.GetSpeedLimit() )
        {
            rb.velocity = new Vector2(gc.GetSpeedLimit(), rb.velocity.y );
        }        
    }

    public void ResetCollided()
    {
        collided = false;
    }
}
