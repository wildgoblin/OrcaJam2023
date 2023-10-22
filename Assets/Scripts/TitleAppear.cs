using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class TitleAppear : MonoBehaviour
{
    SpriteRenderer spriteRend;
    TextMeshProUGUI textMesh;
    [SerializeField] float timeToFadeIn = 0;
    private void Start()
    {
        spriteRend = GetComponent<SpriteRenderer>();
        textMesh = GetComponent<TextMeshProUGUI>();
        HideTitle();
        
    }
    public void HideTitle()
    {
        gameObject.SetActive(false);
    }

    public void ShowTitle()
    {
        gameObject.SetActive(true);
    }

    public void FadeIn()

    {
        ShowTitle();
        if(spriteRend != null)
        {
            StartCoroutine(FadeInSpriteRenderer());
        }
        else if(textMesh != null)
        {
            Debug.Log("FADING IN TEXTMESH");
            StartCoroutine(FadeInTextMesh());
        }
        
    }

    IEnumerator FadeInSpriteRenderer()
    {
        
        Color originalColor = spriteRend.color;
        originalColor.a = 0;
        spriteRend.color = originalColor;
        
        yield return new WaitForSeconds(1);
        float elapsedTime = 0f;

        while (elapsedTime < timeToFadeIn)
        {
            float alpha = Mathf.Lerp(0f, 1f, elapsedTime / timeToFadeIn);
            spriteRend.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        spriteRend.color = new Color(originalColor.r, originalColor.g, originalColor.b, 1f);

    }

    IEnumerator FadeInTextMesh()
    {
        
        Color originalColor = textMesh.color;
        originalColor.a = 0;
        textMesh.color = originalColor;

        yield return new WaitForSeconds(1);
        float elapsedTime = 0f;

        while (elapsedTime < timeToFadeIn)
        {
            float alpha = Mathf.Lerp(0f, 1f, elapsedTime / timeToFadeIn);
            textMesh.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        textMesh.color = new Color(originalColor.r, originalColor.g, originalColor.b, 1f);

    }
}
