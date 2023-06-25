using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerBlock : MonoBehaviour
{
    float alpha = -1;
    SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        StartCoroutine(Timer());
    }

    IEnumerator Timer()
    {
        yield return new WaitForSeconds(3);
        StartCoroutine(Blink());
        yield return new WaitForSeconds(2);
        Destroy(gameObject);

    }

    IEnumerator Blink()
    {
        while (true)
        {
            spriteRenderer.color += new Color(0, 0, 0, alpha);
            alpha *= -1;
            yield return new WaitForSeconds(0.1f);
        }

    }
}
