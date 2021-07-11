using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayUI : MonoBehaviour
{
    [SerializeField] float showDuration;
    CanvasGroup canvasGroup;
    float currentTimer = 0;
    void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    void Update()
    {
        if (currentTimer <= 0)
            Hide();
        else
            currentTimer -= (1 / showDuration) * Time.unscaledDeltaTime;
    }

    public void Show()
    {
        canvasGroup.alpha = 1;
        currentTimer = showDuration;
    }
    public IEnumerator Hide()
    {
        currentTimer = showDuration;
        yield return new WaitForSeconds(1);
        while (canvasGroup.alpha > 0)
        {
            canvasGroup.alpha -= 0.01f;
            yield return new WaitForSeconds(0.01f);
        }
    }
}
