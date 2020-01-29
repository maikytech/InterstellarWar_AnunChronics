using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextController : MonoBehaviour
{

    [SerializeField] GameObject sentence1;
    [SerializeField] string[] Texts;

    private Text sentenceText;
    private NextScene nextSceneClass;

    private void Awake()
    {
        sentenceText = sentence1.GetComponent<Text>();
        nextSceneClass = gameObject.GetComponent<NextScene>();
    }

    private void Start()
    {
        StartCoroutine(TextConfig());
    }

    IEnumerator TextConfig()
    {
        for(int i = 0; i < Texts.Length; i++)
        {
            sentence1.SetActive(true);

            sentenceText.text = Texts[i];

            yield return new WaitForSeconds(7f);

            sentence1.SetActive(false);
        }

        nextSceneClass.goToNextScene = true;
        nextSceneClass.InvokeNextStage();
    }


}
