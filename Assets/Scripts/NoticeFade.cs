using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NoticeFade : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text;
    [SerializeField] float beginTextTime;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(TextFade());
    }

    IEnumerator TextFade()
    {
        float alpha = 0;
        text.color = new Color(1, 1, 1, alpha);

        yield return new WaitForSeconds(beginTextTime);

        while (text.color.a < 1) 
        {
            alpha += Time.deltaTime * 3f;
            text.color = new Color(1, 1, 1, alpha);
            yield return null;
        }

        yield return new WaitForSeconds(2f);

        while (text.color.a > 0)
        {
            alpha -= Time.deltaTime * 1.2f;
            text.color = new Color(1, 1, 1, alpha);
            yield return null;
        }

        yield return new WaitForSeconds(2f);
    }
}
