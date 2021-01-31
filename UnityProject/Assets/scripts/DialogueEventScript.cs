using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueEventScript : MonoBehaviour
{

    public string[] texts;

    public Sprite[] sprites;

    bool isActivated;
    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !isActivated)
        {
            DialogueBoxScript dialogueBox = GameObject.Find("DialogueBox").GetComponent<DialogueBoxScript>();

            dialogueBox.gameObject.SetActive(true);
            dialogueBox.ShowDialogue(texts, sprites);
            isActivated = true;
        }
    }



   
}
