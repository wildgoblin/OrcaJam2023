using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrowingBehaviour : MonoBehaviour
{
    [SerializeField] Transform startingTransform;
    [SerializeField] SpriteRenderer seedImage;
    [SerializeField] GameObject flowerParent;
    [SerializeField] GameObject flowerPrefab;

    //References
    Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        transform.position = startingTransform.position;
        HideSeed();
    }
    public void PlayGrowingSequence()
    {
        HideSeed();
        rb.gravityScale = 0;
        rb.velocity = Vector2.zero;
        rb.angularVelocity = 0;
        transform.position = new Vector2(transform.position.x, startingTransform.position.y);
        
        
        
        GameObject newFlower = Instantiate(flowerPrefab, flowerParent.transform);
        newFlower.transform.position = transform.position;
        newFlower.transform.localPosition = new Vector2(newFlower.transform.position.x, 0);
        newFlower.GetComponent<Animator>().SetTrigger("Grow");
    }

    public void FinishGrowing()
    {
        GameController.Instance.ChangeToLaunching();
        GetComponent<FlyingBehaviour>().ResetCollided();
        
    }

    public void HideSeed()
    {
        seedImage.enabled = false;
    }

    public void ShowSeed()
    {
        seedImage.enabled = true;
    }


}
