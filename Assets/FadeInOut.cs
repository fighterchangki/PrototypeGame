using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class FadeInOut : MonoBehaviour
{
    [SerializeField] private GameObject fadeImage;
        public void FadeIn(float fadeOutTime, System.Action nextEvent = null)
        {
            StartCoroutine(CoFadeIn(fadeOutTime, nextEvent));
        }

        public void FadeOut(float fadeOutTime, System.Action nextEvent = null)
        {
            StartCoroutine(CoFadeOut(fadeOutTime, nextEvent));
        }

        // 투명 -> 불투명
        IEnumerator CoFadeIn(float fadeOutTime, System.Action nextEvent = null)
        {

            Color tempColor = fadeImage.GetComponent<Image>().color;
        while (tempColor.a < 1f)
            {
                tempColor.a += Time.deltaTime / fadeOutTime;
            fadeImage.GetComponent<Image>().color = tempColor;

                if (tempColor.a >= 1f) tempColor.a = 1f;

                yield return null;
            }
            fadeImage.GetComponent<Image>().color = tempColor;
            if (nextEvent != null) StartCoroutine(CoFadeOut(fadeOutTime, nextEvent)); ;
        }

        // 불투명 -> 투명
        IEnumerator CoFadeOut(float fadeOutTime, System.Action nextEvent = null)
        {
            StopCoroutine(CoFadeOut(fadeOutTime, nextEvent));
            if (nextEvent != null) nextEvent();
            Color tempColor = fadeImage.GetComponent<Image>().color;
            while (tempColor.a > 0f)
            {
                tempColor.a -= Time.deltaTime / fadeOutTime;
                fadeImage.GetComponent<Image>().color = tempColor;
                if (tempColor.a <= 0f) tempColor.a = 0f;
                yield return null;
            }
            fadeImage.GetComponent<Image>().color = tempColor;
        }
        // Start is called before the first frame update
        void Start()
        {
        fadeImage = GameObject.Find("FadeImage");
        }
    // Update is called once per frame
}
