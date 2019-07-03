using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class Loading : MonoBehaviour {



    public RawImage rawImage;
    public VideoPlayer videoPlayer;
    public CanvasGroup canvasGroup;

    public double time;
    public double currentTime;
    public CanvasGroup Good;
    public CanvasGroup Bad;
    public bool sendgoodbad;
    public bool sendgoodbadsingle;
    public bool play;




	// Use this for initialization
	void Start () {
        time = gameObject.GetComponent<VideoPlayer>().clip.length;

        Good = GameObject.Find("Good").GetComponent<CanvasGroup>();
        Bad = GameObject.Find("Bad11").GetComponent<CanvasGroup>();

        sendgoodbad = false;
        sendgoodbadsingle = false;
        play = false;

	}

    // Update is called once per frame
    void Update () {
        currentTime = gameObject.GetComponent<VideoPlayer>().time;

        if (play == true){
            
            if (currentTime >= time)
            {
                canvasGroup.alpha = 0f;
                canvasGroup.blocksRaycasts = false;
                play = false;
                if (sendgoodbad == true)
                {
                    Social_new.instance.startGoodorBad();

                    sendgoodbad = false;
                }

                if (sendgoodbadsingle == true)
                {
                    Social_new2.instance.startGoodorBadsingle();

                    sendgoodbadsingle = false;
                }

                //Invoke("DestroyWho", 3f);

            }
        }

	}




    IEnumerator PlayVideo(){

        videoPlayer.Prepare();
        WaitForSeconds waitForSeconds = new WaitForSeconds(1);
        while(!videoPlayer.isPrepared){
            yield return waitForSeconds;
            break;
        }

        rawImage.texture = videoPlayer.texture;
        videoPlayer.Play();

       



    }

    public void playvideo()
    {
        play = true;
        StartCoroutine(PlayVideo());
        sendgoodbad = true;
        Debug.Log("playvideo");

    }

    public void playvideosingle()
    {
        play = true;
        StartCoroutine(PlayVideo());
        sendgoodbadsingle = true;
        Debug.Log("playvideo");

    }


}
