using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Net.Sockets;
using System;
using UnityEngine.UI;


public class Social_new2 : MonoBehaviour
{

    AsyncOperation async;

    private SocketThread ct;
    private bool isSend;
    private bool isReceive;

    private bool cansend;

    private string SendMessage;

    private string Color;
    private string Pic;
    private string Hat;
    private string Fit;
    private string Zip;
    private string Long;
    private string Temp;
    private string Type;
    private string Place;
    private string Time;
    private string Gender;

    private string message;
   

    public Text Text;
    public Text Text2;

    public static Social_new2 instance;

    public Text Single;
    public CanvasGroup SingleCanvas;


    // Use this for initialization
    void Start()
    {
        cansend = false;

        ThreadStart();

        instance = this;

        Text = GameObject.Find("TextSingle").GetComponent<Text>();
        Text.enabled = false;
        Text2 = GameObject.Find("TextSingle2").GetComponent<Text>();
        Text2.enabled = false;
        Text.text = "3";
        //Single = GameObject.Find("Single").GetComponent<Text>();
        //SingleCanvas = GameObject.Find("Single").GetComponent<CanvasGroup>();


    }

    // Update is called once per frame
    void Update()


    {

        if (cansend == true)
        {
            StartCoroutine(delaySend());
        }

        ct.ReceiveUTF8();

        Debug.Log(ct.receiveMessage);

        if (ct.receiveMessage != null)
        {

            cansend = false;


            //Debug.Log(ct.receiveMessage);



            message = ct.receiveMessage;

            Text.text = message;

            Text.enabled = true;
            Text2.enabled = true;

            ct.receiveMessage = null;


            ct.StopConnect();

            ThreadStart();
            Debug.Log("restart thread");
        }

       
    }



    public void ThreadStart()
    {



        ct = new SocketThread(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp, "163.13.201.90", 36005);
        ct.StartConnect();
        isSend = true;

    }

    private IEnumerator delaySend()
    {

        //str = count.ToString();
        //isSend = false;
        yield return new WaitForSeconds(0);
        //SendMessage = "4/ / /2";
        ct.SendUTF8(SendMessage);
        Debug.Log("send: " + SendMessage);
        //Debug.Log("sented");

        //ct.Send(str);

        //count++;

        //isSend = true;



    }

    public void submit()
    {

        Color = (GameObject.Find("Color").GetComponent<Dropdown>().value+1).ToString();
        Pic = (GameObject.Find("Pic").GetComponent<Dropdown>().value+ 1).ToString();
        Hat = (GameObject.Find("Hat").GetComponent<Dropdown>().value+ 1).ToString();
        Fit = (GameObject.Find("Fit").GetComponent<Dropdown>().value+ 1).ToString();
        Zip = (GameObject.Find("Zip").GetComponent<Dropdown>().value+ 1).ToString();
        Long = (GameObject.Find("Long").GetComponent<Dropdown>().value+ 1).ToString();
        Temp = (GameObject.Find("Temp").GetComponent<Dropdown>().value+ 1).ToString();
        Type = (GameObject.Find("Type").GetComponent<Dropdown>().value+ 1).ToString();
        Place = (GameObject.Find("Place").GetComponent<Dropdown>().value+ 1).ToString();
        Time = (GameObject.Find("Time").GetComponent<Dropdown>().value+ 1).ToString();
        Gender = (GameObject.Find("Gender").GetComponent<Dropdown>().value+ 1).ToString();



        SendMessage = Color + "," + Pic + "," + Hat + "," + Fit + "," + Zip + "," + Long + "," + Temp + "," + Type + "," + Place + "," + Time + "," + Gender;

        Text.enabled = false;
        Text2.enabled = false;

        cansend = true;



    }

    public void disconnect()
    {
        ct.StopConnect();
    }

    public void ChangeToDaily()
    {
        if (Login.Instance.ID == "liao")
        {
            disconnect();
            //RatingBar.instance.disconnect();
            SceneManager.LoadScene("DailyBoy");
        }
        else if (Login.Instance.ID == "asdzxc")
        {
            disconnect();
            //RatingBar.instance.disconnect();
            SceneManager.LoadScene("DailyBoy");
        }
        else if (Login.Instance.ID == "ling")
        {
            disconnect();
            //RatingBar.instance.disconnect();
            SceneManager.LoadScene("DailyGirl");
        }
    }

    public void ChangeToUnion()
    {
        disconnect();
        //RatingBar.instance.disconnect();
        SceneManager.LoadScene("Union");
    }

    public void ChangeToFashion()
    {
        disconnect();
        //RatingBar.instance.disconnect();

    }


    public void startGoodorBadsingle()
    {
        StartCoroutine(GoodorBadsingle());

    }

    IEnumerator GoodorBadsingle()
    {
        Single.text = Text.text;
        SingleCanvas.alpha = 1f;
        SingleCanvas.interactable = true;
        SingleCanvas.blocksRaycasts = true;
        Debug.Log("Canvas open");
        //Bad11.enabled = true;
        yield return new WaitForSeconds(3);

        SingleCanvas.alpha = 0f;
        SingleCanvas.interactable = false;
        SingleCanvas.blocksRaycasts = false;
        Debug.Log("close");
        //Bad11.enabled = false;
    }
}
