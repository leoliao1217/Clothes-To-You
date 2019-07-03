using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Net.Sockets;
using System;
using UnityEngine.UI;


public class Social_new : MonoBehaviour {

    AsyncOperation async;

    private SocketThread ct;
    private bool isSend;
    private bool isReceive;

    private bool cansend;

    private string SendMessage;

    private string UpColor;
    private string UpPic;
    private string UpType;
    private string LowColor;
    private string LowType;
    private string showMessage;
    private int message;

    public Text Text;
    public CanvasGroup Good;
    public CanvasGroup Bad;





    public static Social_new instance;





    // Use this for initialization
    void Start()
    {
        cansend = false;

        ThreadStart();

        Text = GameObject.Find("TextSuit").GetComponent<Text>();
        Good = GameObject.Find("Good").GetComponent<CanvasGroup>();
        Bad = GameObject.Find("Bad11").GetComponent<CanvasGroup>();






        Text.enabled = false;

        instance = this;



        async = SceneManager.LoadSceneAsync("Fashion");

        async.allowSceneActivation = false;
    }

    //public void GoodOrBad()
    //{
    //    if (message == 0)
    //    {
    //        Debug.Log("start0");
    //        Bad.enabled = true;

    //    }
    //    else if (message == 1)
    //    {
    //        Debug.Log("start1");
    //        Good.enabled = true;
    //    }
    //}

    // Update is called once per frame
    void Update()


    {

        if (cansend == true)
        {
            StartCoroutine(delaySend());
        }

        ct.ReceiveUTF8();
        Debug.Log(ct.receiveMessage);

        if(ct.receiveMessage !=null){

            cansend = false;


            Debug.Log(ct.receiveMessage);

            message = int.Parse(ct.receiveMessage);

            if(message == 0){
                showMessage = "不好看！！";

            } else if(message ==1){
                showMessage = "好看！！";

            }

            Text.text = showMessage;



            Text.enabled = true;

            ct.receiveMessage = null;


            ct.StopConnect();

            ThreadStart();
            Debug.Log("restart thread");
        }

        Debug.Log(async.progress);
    }



    IEnumerator GoodorBad()
    {
        if (message == 0)
        {
            Bad.alpha = 1f;
            Bad.interactable = true;
            Bad.blocksRaycasts = true;
            //Bad11.enabled = true;
            yield return new WaitForSeconds(3);
            Bad.alpha = 0f;
            Bad.interactable = false;
            Bad.blocksRaycasts = false;
            //Bad11.enabled = false;

        }
        else if (message == 1)
        {
            Good.alpha = 1f;
            Good.interactable = true;
            Good.blocksRaycasts = true;
            //Good11.enabled = true;
            yield return new WaitForSeconds(3);
            Good.alpha = 0f;
            Good.interactable = false;
            Good.blocksRaycasts = false;
            //Good11.enabled = false;

        }

    }

    public void startGoodorBad(){
        StartCoroutine(GoodorBad());

    }













    public void ThreadStart()
    {



        ct = new SocketThread(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp, "163.13.201.90", 36004);
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

    public void submit(){

        UpColor = (GameObject.Find("UpColor").GetComponent<Dropdown>().value+ 1).ToString();
        UpPic = (GameObject.Find("UpPic").GetComponent<Dropdown>().value+ 1).ToString();
        UpType = (GameObject.Find("UpType").GetComponent<Dropdown>().value+ 1).ToString();
        LowColor = (GameObject.Find("LowColor").GetComponent<Dropdown>().value+ 1).ToString();
        LowType = (GameObject.Find("LowType").GetComponent<Dropdown>().value+ 1).ToString();



        SendMessage = UpColor+","+UpPic+","+UpType+","+LowColor+","+LowType;

        Text.enabled = false;

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
            //RatingBar.instance.disconnect();
            disconnect();
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
        //RatingBar.instance.disconnect();
        disconnect();
        SceneManager.LoadScene("Union");
    }

    public void ChangeToFashion()
    {
        //RatingBar.instance.disconnect();
        disconnect();
        async.allowSceneActivation = true;

    }
}
