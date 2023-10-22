using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrowingBehaviour : MonoBehaviour
{
    [SerializeField] GameObject flowerParent;
    [SerializeField] GameObject flowerPrefab;
    public void PlayGrowingSequence()
    {
        GameObject newFlower = Instantiate(flowerPrefab, flowerParent.transform);
        newFlower.transform.position = transform.position;
        newFlower.transform.localPosition = new Vector2(newFlower.transform.position.x, 0);
        newFlower.GetComponent<Animator>().SetTrigger("Grow");
    }

    public void FinishGrowing()
    {
        GameController.Instance.ChangeToLaunching();
    }
}
