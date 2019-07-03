using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Threading;
using UnityEngine.UI;

public class Social_Upload : MonoBehaviour {
void HandleMediaPickCallback(string path)
    {
    }


    string url = "http://163.13.201.90:8234/Upload/"; // 伺服器上傳網址 
    public string picpath;
    public Text UploadText;


    private SocketThread ct;




    private string SendMessage;

	// Use this for initialization
	void Start () {

        UploadText = GameObject.Find("UploadText").GetComponent<Text>();
        UploadText.enabled = false;


        ThreadStart();
		
	}
	
	// Update is called once per frame
    void Update()
    {

        ct.ReceiveUTF8();

        if (ct.receiveMessage != null)
        {


            Debug.Log(ct.receiveMessage);

            GameObject.Find("qqq").GetComponent<Text>().text = ct.receiveMessage;

            ct.receiveMessage = null;


            ct.StopConnect();

            ThreadStart();
            Debug.Log("restart thread");
        }
        
    
    }



    public void UPLoad(){

        UploadText.enabled = false;


        PickImage(512);



    }

    private string UploadFilesToRemoteUrl(string url, string file)
    {
        // Create a boundry
        string boundary = "----------------------------" + DateTime.Now.Ticks.ToString("x");

        // Create the web request
        HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
        httpWebRequest.ContentType = "multipart/form-data; boundary=" + boundary;
        httpWebRequest.Method = "POST";
        httpWebRequest.KeepAlive = true;
        httpWebRequest.AllowWriteStreamBuffering = false;

        httpWebRequest.Credentials =
            System.Net.CredentialCache.DefaultCredentials;

        // Get the boundry in bytes
        byte[] boundarybytes = System.Text.Encoding.ASCII.GetBytes("\r\n--" + boundary + "\r\n");

        // Get the header for the file upload
        string headerTemplate = "Content-Disposition: form-data; name=\"{0}\";filename=\"{1}\"\r\n Content-Type: application/octet-stream\r\n\r\n";

        // Add the filename to the header
        string header = string.Format(headerTemplate, "file", file);

        //convert the header to a byte array
        byte[] headerbytes = System.Text.Encoding.UTF8.GetBytes(header);

        // Add all of the content up.
        httpWebRequest.ContentLength = new FileInfo(file).Length + headerbytes.Length + (boundarybytes.Length * 2) + 2;

        // Get the output stream
        Stream requestStream = httpWebRequest.GetRequestStream();

        // Write out the starting boundry
        requestStream.Write(boundarybytes, 0, boundarybytes.Length);

        // Write the header including the filename.
        requestStream.Write(headerbytes, 0, headerbytes.Length);

        // Open up a filestream.
        FileStream fileStream = new FileStream(file, FileMode.Open, FileAccess.Read);

        // Use 4096 for the buffer
        byte[] buffer = new byte[4096];

        int bytesRead = 0;
        // Loop through whole file uploading parts in a stream.
        while ((bytesRead = fileStream.Read(buffer, 0, buffer.Length)) != 0)
        {
            requestStream.Write(buffer, 0, bytesRead);
            requestStream.Flush();
        }

        boundarybytes = System.Text.Encoding.ASCII.GetBytes("\r\n--" + boundary + "--\r\n");

        // Write out the trailing boundry
        requestStream.Write(boundarybytes, 0, boundarybytes.Length);

        // Close the request and file stream
        requestStream.Close();
        fileStream.Close();

        WebResponse webResponse = httpWebRequest.GetResponse();

        Stream responseStream = webResponse.GetResponseStream();
        StreamReader responseReader = new StreamReader(responseStream);

        string responseString = responseReader.ReadToEnd();

        // Close response object.
        webResponse.Close();

        return responseString;
    }






    private IEnumerator TakeScreenshotAndSave()
    {
        yield return new WaitForEndOfFrame();

        Texture2D ss = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
        ss.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        ss.Apply();

        // Save the screenshot to Gallery/Photos
        Debug.Log("Permission result: " + NativeGallery.SaveImageToGallery(ss, "GalleryTest", "My img {0}.png"));

        // To avoid memory leaks
        Destroy(ss);
    }

    private void PickImage(int maxSize)
    {
        NativeGallery.Permission permission = NativeGallery.GetImageFromGallery((path) =>
        {
            picpath = path;
            Debug.Log("Image path: " + path);


            UploadFilesToRemoteUrl(url, picpath);

            SendMessage = "http://163.13.201.90:8234/Upload/tmp.png";

            StartCoroutine(delaySend());

            UploadText.enabled = true;


        }, "Select a PNG image", "image/png", maxSize);





    }
    #if UNITY_IOS
    private void PickVideo()
    {
        NativeGallery.Permission permission = NativeGallery.GetVideoFromGallery((path) =>
        {
            Debug.Log("Video path: " + path);
            if (path != null)
            {
                // Play the selected video
                Handheld.PlayFullScreenMovie("file://" + path);
            }
        }, "Select a video");

        Debug.Log("Permission result: " + permission);
    }

    #endif


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


    public void ThreadStart()
    {



        ct = new SocketThread(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp, "163.13.201.90", 36006);
        ct.StartConnect();

    }

    public void disconnect()
    {
        ct.StopConnect();
    }
}
