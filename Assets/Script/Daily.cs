using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Linq;
using System.Net.Sockets;
using System;

public class Daily : MonoBehaviour {

    private SocketThread ct;
    private bool isSend;
    private bool isReceive;
    private string IDname;

    public string MLmessage;
    public string SendMessage;
    public int Up = 0;
    public int low = 1;
    public int Upsamiliar = 0;
    public int lowsamiliar = 1;

    public int set = 1;


    public String[] samiliarcloth = new string[12];
    public String[] setcloth = new string[6];
    public string tt;

    public Text upponwarn;
    public Text lowerwarn;
    public Text setwarn;
   

    AsyncOperation async;


    // Use this for initialization
    void Start () {

        ThreadStart();

        if(Login.Instance.ID == "liao"){
            IDname = "7";
        } else if (Login.Instance.ID == "ling"){
            IDname = "4";
        } else if (Login.Instance.ID == "asdzxc"){
            IDname = "7";
        }

        upponwarn = GameObject.Find("upponwarn").GetComponent<Text>();
        lowerwarn = GameObject.Find("lowerwarn").GetComponent<Text>();
        setwarn = GameObject.Find("setwarn").GetComponent<Text>();
        upponwarn.enabled = false;
        lowerwarn.enabled = false;
        setwarn.enabled = false;

        async = SceneManager.LoadSceneAsync("Fashion");

        async.allowSceneActivation = false;
	}

    public void ThreadStart(){

        ct = new SocketThread(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp, "163.13.201.90", 36002);
        ct.StartConnect();
        isSend = true;

    }

	
	// Update is called once per frame
	void Update () {

        Debug.Log(async.progress);

        ct.ReceiveUTF8();
        tt = ct.receiveMessage;
        Debug.Log(tt);
        if (ct.receiveMessage != null)
        {
            upponwarn.enabled = false;
            lowerwarn.enabled = false;
            GameObject.Find("SimiliarChangeBTN").GetComponent<Button>().interactable = true;
            GameObject.Find("set").GetComponent<Button>().interactable = true;
            GameObject.Find("up").GetComponent<Button>().interactable = true;
            GameObject.Find("low").GetComponent<Button>().interactable = true;
            GameObject.Find("both").GetComponent<Button>().interactable = true;

            String[] Message = ct.receiveMessage.Split(',');
            //GameObject.Find("Canvas/Textbox").GetComponent<UnityEngine.UI.Text>().text = "Server:" + ct.receiveMessage;
            //Debug.Log(Message[17]);
            ct.receiveMessage = null;

            for (int z = 0; z < 6 ;z++){
                setcloth[z] = Message[z];
                //Debug.Log(z);
                //Debug.Log(setcloth[z]);
            }

            for (int q = 0; q < 12;q++){
                samiliarcloth[q] = Message[q + 6];

            }


            if (IDname == "4"){
                ChangeFoot.instance.Changecloth(setcloth[Up]);
                ChangeFoot.instance.ChangeFeet(setcloth[low]);
                Debug.Log(setcloth[Up]);
                Debug.Log(setcloth[low]);

            } else if (IDname == "7") {
                ChangeBoy.instance.Changecloth(setcloth[Up]);
                ChangeBoy.instance.ChangeFeet(setcloth[low]);
                Debug.Log(setcloth[Up]);
                Debug.Log(setcloth[low]);

            }

            //Capture();

            ct.StopConnect();

            ThreadStart();

            Debug.Log("restart thread");
        }

	}



    public void ChangeSet(){

        set++;
        GameObject.Find("SimiliarChangeBTN").GetComponent<Button>().interactable = true;
        GameObject.Find("up").GetComponent<Button>().interactable = true;
        GameObject.Find("low").GetComponent<Button>().interactable = true;
        GameObject.Find("both").GetComponent<Button>().interactable = true;
        upponwarn.enabled = false;
        lowerwarn.enabled = false;

        if (set == 2){
            Upsamiliar = 4;
            lowsamiliar = 5;
        } else if (set == 3){
            Upsamiliar = 8;
            lowsamiliar = 9;
        }




        Up = Up + 2;
        low = low + 2;

        if (IDname == "4")
        {
            ChangeFoot.instance.Changecloth(setcloth[Up]);
            ChangeFoot.instance.ChangeFeet(setcloth[low]);
            Debug.Log(setcloth[Up]);
            Debug.Log(setcloth[low]);

        }
        else if (IDname == "7")
        {
            ChangeBoy.instance.Changecloth(setcloth[Up]);
            ChangeBoy.instance.ChangeFeet(setcloth[low]);
            Debug.Log(setcloth[Up]);
            Debug.Log(setcloth[low]);

        }

        if(Up == 4&& low == 5){
            setwarn.enabled = true;
            GameObject.Find("set").GetComponent<Button>().interactable = false;
        }


    }


    public void ChangeUppon(){

        if (IDname == "4")
        {
            ChangeFoot.instance.Changecloth(samiliarcloth[Upsamiliar]);
            Debug.Log(samiliarcloth[Upsamiliar]);
            Debug.Log(samiliarcloth[lowsamiliar]);
        }
        else if (IDname == "7")
        {
            ChangeBoy.instance.Changecloth(samiliarcloth[Upsamiliar]);
//            Debug.Log(samiliarcloth[Upsamiliar]);
//            Debug.Log(samiliarcloth[lowsamiliar]);
        }




        if (set == 1){



            if (Upsamiliar >= 2)
            {
                upponwarn.enabled = true;
                GameObject.Find("up").GetComponent<Button>().interactable = false;
                GameObject.Find("both").GetComponent<Button>().interactable = false;
            }
            if (Upsamiliar >= 2 && lowsamiliar >= 3)
            {
                GameObject.Find("SimiliarChangeBTN").GetComponent<Button>().interactable = false;
            }
            
        } else if (set == 2)
        {
            
            if (Upsamiliar >= 6)
            {
                upponwarn.enabled = true;
                GameObject.Find("up").GetComponent<Button>().interactable = false;
                GameObject.Find("both").GetComponent<Button>().interactable = false;
            }
            if (Upsamiliar >= 6 && lowsamiliar >= 7)
            {
                GameObject.Find("SimiliarChangeBTN").GetComponent<Button>().interactable = false;
            }

        } else if (set == 3)
        {

            if (Upsamiliar >= 10)
            {
                upponwarn.enabled = true;
                GameObject.Find("up").GetComponent<Button>().interactable = false;
                GameObject.Find("both").GetComponent<Button>().interactable = false;
            }
            if (Upsamiliar >= 10 && lowsamiliar >= 11)
            {
                GameObject.Find("SimiliarChangeBTN").GetComponent<Button>().interactable = false;
            }

        }


        Upsamiliar = Upsamiliar + 2;




    }

    public void ChangeBottom(){


        if (IDname == "4")
        {
            ChangeFoot.instance.ChangeFeet(samiliarcloth[lowsamiliar]);
            Debug.Log(samiliarcloth[Upsamiliar]);
            Debug.Log(samiliarcloth[lowsamiliar]);
        }
        else if (IDname == "7")
        {
            ChangeBoy.instance.ChangeFeet(samiliarcloth[lowsamiliar]);
            Debug.Log(samiliarcloth[Upsamiliar]);
            Debug.Log(samiliarcloth[lowsamiliar]);
        }



        if (set == 1){
            
            if (lowsamiliar >= 3)
            {
                lowerwarn.enabled = true;
                GameObject.Find("low").GetComponent<Button>().interactable = false;
                GameObject.Find("both").GetComponent<Button>().interactable = false;
            }
            if (Upsamiliar >= 2 && lowsamiliar >= 3)
            {
                GameObject.Find("SimiliarChangeBTN").GetComponent<Button>().interactable = false;
            }

        } else if (set == 2)
        {

            if (lowsamiliar >= 7)
            {
                lowerwarn.enabled = true;
                GameObject.Find("low").GetComponent<Button>().interactable = false;
                GameObject.Find("both").GetComponent<Button>().interactable = false;
            }
            if (Upsamiliar >= 6 && lowsamiliar >= 7)
            {
                GameObject.Find("SimiliarChangeBTN").GetComponent<Button>().interactable = false;
            }

        } else if (set == 3)
        {
            
            if (lowsamiliar >= 11)
            {
                lowerwarn.enabled = true;
                GameObject.Find("low").GetComponent<Button>().interactable = false;
                GameObject.Find("both").GetComponent<Button>().interactable = false;
            }
            if (Upsamiliar >= 10 && lowsamiliar >= 11)
            {
                GameObject.Find("SimiliarChangeBTN").GetComponent<Button>().interactable = false;
            }

        }

        lowsamiliar = lowsamiliar + 2;

        
    }

    public void ChangeBoth(){


        if (IDname == "4")
        {
            ChangeFoot.instance.Changecloth(samiliarcloth[Up]);
            ChangeFoot.instance.ChangeFeet(samiliarcloth[low]);
            Debug.Log(samiliarcloth[Up]);
            Debug.Log(samiliarcloth[low]);
        }
        else if (IDname == "7")
        {
            ChangeBoy.instance.Changecloth(samiliarcloth[Up]);
            ChangeBoy.instance.ChangeFeet(samiliarcloth[low]);
            Debug.Log(samiliarcloth[Up]);
            Debug.Log(samiliarcloth[low]);
        }




        if (set == 1){
            if (Upsamiliar >= 2 && lowsamiliar >= 3)
            {
                GameObject.Find("SimiliarChangeBTN").GetComponent<Button>().interactable = false;
                upponwarn.enabled = true;
                lowerwarn.enabled = true;
            }
            else if (Up >= 2)
            {
                upponwarn.enabled = true;
                GameObject.Find("up").GetComponent<Button>().interactable = false;
                GameObject.Find("both").GetComponent<Button>().interactable = false;
            }
            else if (low >= 3)
            {
                lowerwarn.enabled = true;
                GameObject.Find("low").GetComponent<Button>().interactable = false;
                GameObject.Find("both").GetComponent<Button>().interactable = false;
            }
        } else if (set == 2)
        {
            if (Up >= 6 && low >= 7)
            {
                GameObject.Find("SimiliarChangeBTN").GetComponent<Button>().interactable = false;
                upponwarn.enabled = true;
                lowerwarn.enabled = true;
            }
            else if (Up >= 6)
            {
                upponwarn.enabled = true;
                GameObject.Find("up").GetComponent<Button>().interactable = false;
                GameObject.Find("both").GetComponent<Button>().interactable = false;
            }
            else if (low >= 7)
            {
                lowerwarn.enabled = true;
                GameObject.Find("low").GetComponent<Button>().interactable = false;
                GameObject.Find("both").GetComponent<Button>().interactable = false;
            }
        } else if (set == 3)
        {
            if (Up >= 10 && low >= 11)
            {
                GameObject.Find("SimiliarChangeBTN").GetComponent<Button>().interactable = false;
                upponwarn.enabled = true;
                lowerwarn.enabled = true;
            }
            else if (Up >= 10)
            {
                upponwarn.enabled = true;
                GameObject.Find("up").GetComponent<Button>().interactable = false;
                GameObject.Find("both").GetComponent<Button>().interactable = false;
            }
            else if (low >= 11)
            {
                lowerwarn.enabled = true;
                GameObject.Find("low").GetComponent<Button>().interactable = false;
                GameObject.Find("both").GetComponent<Button>().interactable = false;
            }
        }


        Upsamiliar = Upsamiliar + 2;
        lowsamiliar = lowsamiliar + 2;

        
    }

    private IEnumerator delaySend()
    {
        //str = count.ToString();
        //isSend = false;
        yield return new WaitForSeconds(0);
        SendMessage = IDname + "," + MLmessage;
        ct.SendUTF8(SendMessage);
        Debug.Log(SendMessage);
        //Debug.Log("sented");

        //ct.Send(str);

        //count++;

        //isSend = true;
    }

    public void ClickPlace(Button button){
        
        MLmessage = button.name;
        //Debug.Log(MLmessage);
        StartCoroutine(delaySend());
    }





    public void LogOut()
    {
        disconnect();
        SceneManager.LoadScene("Login");
        Login.Instance.RestartConnect();
        ct.StopConnect();

    }

    public void ChangeToUnion()
    {
        disconnect();
        SceneManager.LoadScene("Union");
    }

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

    public void ChangeToDailyBoy(){
        disconnect();
        SceneManager.LoadScene("DailyBoy");
    }

    public void ChangeToDailyGirl(){
        disconnect();
        SceneManager.LoadScene("DailyGirl");
    }

    public void disconnect()
    {
        ct.StopConnect();
    }

    public void DemoChangeBoy()
    {

        ChangeBoy.instance.Changecloth("shen2-17");
        ChangeBoy.instance.ChangeFeet("jiao2-10");

    }


    public void DemoChangeGirl()
    {

        ChangeFoot.instance.Changecloth("shen3-13");
        ChangeFoot.instance.ChangeFeet("jiao3-9");

    }



}
