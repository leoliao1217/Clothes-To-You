using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net.Sockets;
using UnityEngine.UI;

public class SocialAsk : MonoBehaviour {


    private SocketThread ct;
    private bool isSend;
    private bool isReceive;

    private bool cansend;

    private string SendMessage;

    Image image;
    private Texture Texture;

	// Use this for initialization
	void Start () {

        cansend = false;

        ThreadStart();
		
	}
	
	// Update is called once per frame
	void Update () {

        //if (cansend == true)
        //{
        //    StartCoroutine(delaySend());
        //}

        ct.ReceiveUTF8();

        if (ct.receiveMessage != null)
        {

            cansend = false;


            Debug.Log(ct.receiveMessage);

            GameObject.Find("qqq").GetComponent<Text>().text = ct.receiveMessage;

            imgchange();

            ct.receiveMessage = null;


            ct.StopConnect();

            ThreadStart();
            Debug.Log("restart thread");
        }
		
	}


    private IEnumerator delaySend()
    {

        //str = count.ToString();
        //isSend = false;
        yield return new WaitForSeconds(0);
        SendMessage = "GO";
        ct.SendUTF8(SendMessage);
        Debug.Log("send: " + SendMessage);
        //Debug.Log("sented");

        //ct.Send(str);

        //count++;

        //isSend = true;



    }

    public void ThreadStart()
    {



        ct = new SocketThread(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp, "163.13.201.90", 36007);
        ct.StartConnect();
        isSend = true;

    }

    public void BTNClick(){

        StartCoroutine(delaySend());
        cansend = true;
    }



    public void imgchange(){


        //image = GameObject.Find("img").GetComponent<Image>();

        //Texture = (Texture)Resources.Load(ct.receiveMessage);

        //Texture = (Texture)Resources.Load("http://163.13.201.90:8234/Upload/unimgpicker.png");
        //image.texture = Texture;


        //StartCoroutine(LoadPic(ct.receiveMessage));
        StartCoroutine(LoadPic("http://163.13.201.90:8234/Upload/tmp.png"));
        Debug.Log("loaded");
    }

    IEnumerator LoadPic(string URL)
    {


        WWW www = new WWW(URL);
        while (!www.isDone)
        {
            //Debug.Log("Download image on progress" + www.progress);
            yield return null;
        }



        if (!string.IsNullOrEmpty(www.error))
        {
            Debug.Log("Download failed");
        }
        else
        {
            //Debug.Log("Download succes");
            Texture2D texture = new Texture2D(1, 1);
            www.LoadImageIntoTexture(texture);

            Sprite sprite = Sprite.Create(texture,
                new Rect(0, 0, texture.width, texture.height), Vector2.zero);
            image = GameObject.Find("img").GetComponent<Image>();
            image.sprite = sprite;


        }

    }


}
