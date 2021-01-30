using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ping : MonoBehaviour
{
   public enum Playing { Slow, Fast, None };

    public AudioSource pingSound;

    public bool isFast;

    public static Playing playing = Playing.None;

    public static int pingCount;

    public static AudioSource currentPing;

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
        if (collision.CompareTag("Player")){
            if (((isFast && playing != Playing.Fast) || (!isFast && playing == Playing.None)))
        {
                pingSound.Play();

                currentPing?.Stop();
                currentPing = pingSound;

                if (isFast)
                {
                    playing = Playing.Fast;
                }
                else
                {
                    playing = Playing.Slow;
                }
            }

            pingCount++;

        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            pingSound.Stop();

            pingCount--;

            if(pingCount == 0)
            playing = Playing.None;
        }
    }


}
