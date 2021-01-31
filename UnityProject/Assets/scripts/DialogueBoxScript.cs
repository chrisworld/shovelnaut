using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueBoxScript : MonoBehaviour
{

    //Number of current dialogue
    private int dialogueCount;

    private string[] texts;

    private Sprite[] sprites;

    // Start is called before the first frame update
    void Start()
    {

        //Center dialogue box

        transform.position = new Vector2(Screen.width / 2, transform.position.y);

        HideText();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && texts != null)
        {
            dialogueCount++;

            if (dialogueCount < texts.Length)
            {
                ShowText(texts[dialogueCount], sprites[dialogueCount]);
            }
            else
            {

                HideText();

                //Unfreeze player
                GameObject.Find("Player").GetComponent<Player>().enabled = true;
            }


        }
    }

    public void ShowDialogue(string[] texts, Sprite[] sprites)
    {

        if (texts.Length > 0 && sprites.Length > 0)
        {


            //freeze player
            GameObject.Find("Player").GetComponent<Rigidbody2D>().velocity = Vector3.zero;
            GameObject.Find("Player").GetComponent<Player>().enabled = false;
            GameObject.Find("Player").GetComponent<Animator>().SetBool("isWalking",false);

            // GameObject.Find("Light").GetComponent<LightMovement>().enabled = false;


            this.texts = texts;
            this.sprites = sprites;

            dialogueCount = 0;
            GetComponent<CanvasGroup>().alpha = 1;

            ShowText(texts[dialogueCount], sprites[dialogueCount]);
        }


        gameObject.SetActive(true);
    }



    public void ShowText(string text, Sprite sprite)
    {
        GameObject.Find("DialogueText").GetComponent<Text>().text = text;

        GameObject.Find("DialogueImage").GetComponent<Image>().sprite = sprite;

        gameObject.SetActive(true);
    }

    public void HideText()
    {
        GetComponent<CanvasGroup>().alpha = 0;

    }
}
