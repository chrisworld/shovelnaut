using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ping : MonoBehaviour
{
   public enum Playing { Slow, Fast, None };

    public AudioSource pingSound;

    public bool isFast;

    public static Playing playing = Playing.None;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && ((isFast && playing != Playing.Fast) || (!isFast && playing == Playing.None)))
        {
            pingSound.Play();

            if (isFast)
            {
                playing = Playing.Fast;
            }
            else
            {
                playing = Playing.Slow;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            pingSound.Stop();

            playing = Playing.None;
        }
    }


}
