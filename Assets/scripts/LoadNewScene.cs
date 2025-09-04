using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadNewScene : MonoBehaviour
{
   
    void Start()
    {
        StartCoroutine(CountdownCoroutine());
    }



    IEnumerator CountdownCoroutine()
    {
        float duration = 5f;
        float elapsed = 0f;
        Slider slider = GetComponent<Slider>();
        slider.maxValue = duration;
        slider.value = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            slider.value = elapsed;
            yield return null;
        }

        slider.value = duration;
        SceneManager.LoadScene("GameScene");
    }

     
}
