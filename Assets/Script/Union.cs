using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using UnityEngine.UI;

public class Union : MonoBehaviour {



    private SocketThread ct;
    private bool isSend;
    private bool isReceive;
    private String IDname;
    private Image Image;
    private Texture Texture;

    private bool cansend;

    RawImage image;
    private string SendMessage;

    AsyncOperation async;

    public GameObject Up;
    public GameObject down;
    public GameObject RectUp;
    public GameObject RectDown;


	// Use this for initialization
	void Start () {

        cansend = true;
        //GameObject.Find("ViewAllUp").SetActive(false);
        //GameObject.Find("ViewAllDown").SetActive(false);



        ThreadStart();

        if (Login.Instance.ID == "liao")
        {
            IDname = "7";
        }
        else if (Login.Instance.ID == "ling")
        {
            IDname = "4";
        }
        else if (Login.Instance.ID == "asdzxc")
        {
            IDname = "7";
        }


        //StartCoroutine(delaySend());

        async = SceneManager.LoadSceneAsync("Fashion");
        async.allowSceneActivation = false;
	}
	
	// Update is called once per frame
	void Update () {
        
        ct.ReceiveUTF8();

        if(cansend = true){
            StartCoroutine(delaySend());
        }

        if(ct.receiveMessage != null){

            cansend = false;

            Debug.Log(ct.receiveMessage);

            //GameObject.Find("Text111").GetComponent<UnityEngine.UI.Text>().text = "Server:" + ct.receiveMessage;


            String[] Message = ct.receiveMessage.Split(',');

            for (int i = 0; i < 5; i++){

//                Debug.Log(Message[i]);
////                Image.sprite = Resources.Load("cloth-png/" + Message[i]+".png", typeof(Sprite)) as Sprite;

                //Texture2D tex = (Texture2D)Resources.Load("cloth-png/"+Message[i]+".png");
                ////Image = GameObject.Find("Canvas/Panel/Rect/Image_Area/" + cnt).GetComponent<Image>();
                //Sprite sprite = Sprite.Create(tex,
                                              //new Rect(0, 0, tex.width, tex.height), new Vector2(0,0));
                image = GameObject.Find("Canvas/Panel/UnionRect/Rate/" + (i+1)).GetComponent<RawImage>();
                Texture = (Texture)Resources.Load(Message[i]);
                image.texture = Texture;

                //Image.GetComponent<SpriteRenderer>().sprite = (Sprite)Resources.Load<Sprite>(Message[i]);

                //Image.sprite = sprite;
            }

            for (int i = 10; i < 15; i++)
            {

                image = GameObject.Find("Canvas/Panel/UnionRect/Rate/" + (i + 1)).GetComponent<RawImage>();
                Texture = (Texture)Resources.Load(Message[i-5]);
                image.texture = Texture;
            }

            for (int i = 20; i < 25; i++)
            {

                image = GameObject.Find("Canvas/Panel/UnionRect/Rate/" + (i + 1)).GetComponent<RawImage>();
                Texture = (Texture)Resources.Load(Message[i - 10]);
                image.texture = Texture;
            }

            for (int i = 30; i < 35; i++)
            {
                
                image = GameObject.Find("Canvas/Panel/UnionRect/Rate/" + (i + 1)).GetComponent<RawImage>();
                Texture = (Texture)Resources.Load(Message[i - 15]);
                image.texture = Texture;
            }







            ct.receiveMessage = null;


            ct.StopConnect();

            //ThreadStart();

            //Debug.Log("restart thread");

            //cansend = false;
        }





        Debug.Log(async.progress);
	}

    public void ThreadStart()
    {

        ct = new SocketThread(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp, "163.13.201.90", 36003);
        ct.StartConnect();
        isSend = true;

    }

    private IEnumerator delaySend()
    {
        
        //str = count.ToString();
            //isSend = false;
            yield return new WaitForSeconds(0);
            SendMessage = IDname + "," + "rank";
            //SendMessage = "4/ / /2";
            ct.SendUTF8(SendMessage);
            Debug.Log("send: " + SendMessage);
            //Debug.Log("sented");

            //ct.Send(str);

            //count++;

            //isSend = true;

    }

    public void disconnect(){
        ct.StopConnect();
    }

    public void ChangeToDaily()
    {
        if (Login.Instance.ID == "liao")
        {
            disconnect();
            SceneManager.LoadScene("DailyBoy");
        }
        else if (Login.Instance.ID == "asdzxc")
        {
            disconnect();
            SceneManager.LoadScene("DailyBoy");
        }
        else if (Login.Instance.ID == "ling")
        {
            disconnect();
            SceneManager.LoadScene("DailyGirl");
        }
    }

 //   public void OnMouseEnter()
	//{
 //       GameObject.Find("BB").transform.localScale += new Vector3(1, 1, 1);
 //       Debug.Log("tt");
	//}

	public void ChangeToSocial()
    {
        disconnect();
        SceneManager.LoadScene("Social");



    }

    public void ChangeToFashion()
    {
        disconnect();
        async.allowSceneActivation = true;
    }

    public void ChangeToOtherCatalog(){
        SceneManager.LoadScene("OtherCatalog");
    }

    public void UpDownHide(){


        RectUp.SetActive(false);
        RectDown.SetActive(false);
  



    }

    public void UpDownOn()
    {


        RectUp.SetActive(true);
        RectDown.SetActive(true);




    }


}
