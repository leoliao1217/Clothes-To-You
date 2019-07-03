using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 
using System.Net.Sockets;
using UnityEngine.UI;
using System;


public class SignUp : MonoBehaviour {


    private SocketThread ct;
    private bool isSend;
    private bool isReceive;
    private String SignUpString;
    //private String ErrorMessage;

    private Boolean toggle_man;
    private Boolean toggle_woman;
    private String gender;
    public Text ErrorMessagePassWord;
    public Text ErrorMessageGender;
	// Use this for initialization

	void Start () {

        ErrorMessagePassWord = GameObject.Find("ErrorMessagePassWord").GetComponent<Text>();
        ErrorMessagePassWord.enabled = false;
        ErrorMessageGender = GameObject.Find("ErrorMessageGender").GetComponent<Text>();
        ErrorMessageGender.enabled = false;

        ct = new SocketThread(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp, "163.13.201.90", 36000);
        ct.StartConnect();
        isSend = true;


		
	}
	
	// Update is called once per frame
	void Update () {
		
        ct.Receive();
        String[] Message = ct.receiveMessage.Split('/');
        if(ct.receiveMessage != null){
            if (Message[0] == "1") {
                Debug.Log("yes");
                ChangeToLogin();
            } else {
                Debug.Log("receive: "+ ct.receiveMessage);
                Debug.Log("error");
                     
            }
        }


	}


    public IEnumerator delaySend()
    {
        //str = count.ToString();
        //Debug.Log("111");
        signUpString();
        isSend = false;
        yield return new WaitForSeconds(0);
        //ct.Send("1,1,1");
        ct.SendUTF8(SignUpString);
        //ct.Send(str);
        Debug.Log("send   " +SignUpString);
        //count++;

        isSend = true;
    }
    public void ChangeToLogin()
    {
        SceneManager.LoadScene("Login");
    }


    public void signUpString(){




        checkgender();

        if (gender != null)
        {
            
            if (GameObject.Find("input_password").GetComponent<InputField>().text == GameObject.Find("input_passwordagain").GetComponent<InputField>().text)
            {

                SignUpString = GameObject.Find("input_account").GetComponent<InputField>().text +
                             "/" + GameObject.Find("input_password").GetComponent<InputField>().text +
                                         "/" + GameObject.Find("input_name").GetComponent<InputField>().text +
                                         "/" + gender;
                Debug.Log("sign up string is : " + SignUpString);

            }
            else
            {
                SignUpString = null;
                Debug.Log("error password");
                ErrorMessagePassWord.enabled = true;
            }


        } 



    }

    public void SignUpToServer(){


        ErrorMessageGender.enabled = false;
        ErrorMessagePassWord.enabled = false;


        StartCoroutine(delaySend());
        Debug.Log("Signuptoserver");

        
    }

    public void checkgender(){

        toggle_man = GameObject.Find("Toggle_man").GetComponent<Toggle>().isOn;
        toggle_woman = GameObject.Find("Toggle_woman").GetComponent<Toggle>().isOn;

        if(toggle_man == true && toggle_woman == false){
            gender = "man";
        }
        else if(toggle_woman == true && toggle_man == false){
            gender = "woman";
        }
        else{
            gender = null;
            Debug.Log("gender error");
            ErrorMessageGender.enabled = true;

        }


    }


}
