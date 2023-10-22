using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;

public class QuoteGenerator : MonoBehaviour
{
    [SerializeField] List<string> quotes = new List<string>();
    [SerializeField] TextMeshProUGUI textPrefab;
    [SerializeField] float timeToFadeIn = 5f;
    public void GenerateNewQuote(Transform parentTransform)

    {
        if(quotes.Count>0)
        {
            TextMeshProUGUI newQuote = Instantiate(textPrefab, transform);
            newQuote.text = quotes[0];
            quotes.RemoveAt(0);
            newQuote.transform.SetParent(parentTransform, true);

            StartCoroutine(FadeInQuote(newQuote));
        }


        

    }

    IEnumerator FadeInQuote(TextMeshProUGUI newQuote)
    {

        Color originalColor = newQuote.color;
        originalColor.a = 0;
        newQuote.color = originalColor;

        yield return new WaitForSeconds(1);
        float elapsedTime = 0f;

        while (elapsedTime < timeToFadeIn)
        {
            float alpha = Mathf.Lerp(0f, 1f, elapsedTime / timeToFadeIn);
            newQuote.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        newQuote.color = new Color(originalColor.r, originalColor.g, originalColor.b, 1f);

    }
}
