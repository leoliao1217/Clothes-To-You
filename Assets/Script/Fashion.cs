using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;
using System.Text;
using AngleSharp;
using AngleSharp.Dom;
using UnityEngine.UI;
using System;
using UnityEngine.EventSystems;

public class Fashion : MonoBehaviour
{

    private int cnt = 1;

    private string URl;
    private Image Image;
    private int path;
    public Text SearchTextBox;

    private string URL2;

    string rectname;
    int intrectname;

    private String[] arrayrect = new string[8];

    public static Fashion instance;

    void Start()
    {
        instance = this;

        firstcatch();


    }

    public void firstcatch()
    {
        IConfiguration config = Configuration.Default.WithDefaultLoader();
        string url = "https://wear.tw";
        IDocument doc = BrowsingContext.New(config).OpenAsync(url).Result;

        /*CSS Selector寫法*/

        IHtmlCollection<IElement> imgs = doc.QuerySelectorAll("ul > li.large > div.image_container > p.img > a.over > img , ul > li.large > div.image_container > p.img > a.over ");//取得圖片

        path = 1;

        foreach (IElement img in imgs)
        {
            URl = "https:" + img.GetAttribute("data-original");

            if(URl == "https:"){
                continue;
            }
            Debug.Log(URl);


            StartCoroutine(LoadPic(URl, path));

            path++;

        }


        int n = 0;


        foreach (IElement href in imgs){
            

            URL2 = "https://wear.tw" + href.GetAttribute("href");

            if(URL2 == "https://wear.tw"){
                continue;
            }
            Debug.Log(URL2);
            arrayrect[n] = URL2;
            n++;
        }


    }

    public void ClickOpen(){

        rectname = EventSystem.current.currentSelectedGameObject.name;

        intrectname = int.Parse(rectname)-1;

        Debug.Log(intrectname);

        Application.OpenURL(arrayrect[intrectname]);


        //Application.OpenURL("http://www.google.com.tw");
    }


    // Update is called once per frame
    void Update () {
		
	}

    public void ChangeToDaily()
    {
        if(Login.Instance.ID == "liao"){
            SceneManager.LoadScene("DailyBoy");
        } else if (Login.Instance.ID == "asdzxc"){
            SceneManager.LoadScene("DailyBoy");
        } else if (Login.Instance.ID == "ling"){
            SceneManager.LoadScene("DailyGirl");
        }
    }

    public void ChangeToSocial()
    {
        SceneManager.LoadScene("Social");
    }

    public void ChangeToUnion()
    {
        SceneManager.LoadScene("Union");
    }

    public void Reload()
    {
        SceneManager.LoadScene("Fashion");
    }

    IEnumerator LoadPic(String URL,int Path){


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
            Image = GameObject.Find("Canvas/Panel/Rect/Image_Area/" + Path).GetComponent<Image>();
            Image.sprite = sprite;



        }

    }

    public void Search(){
        

        IConfiguration config = Configuration.Default.WithDefaultLoader();
        string url = "https://wear.tw/coordinate/?search_word="+GameObject.Find("SearchInput").GetComponent<InputField>().text;
        IDocument doc = BrowsingContext.New(config).OpenAsync(url).Result;

        /*CSS Selector寫法*/
        //IHtmlCollection<IElement> imgs = doc.QuerySelectorAll("ul li div.image_container p.img a.over img");//取得圖片
        IHtmlCollection<IElement> imgs = doc.QuerySelectorAll("ul > li > div.image > a.over > p.img > img, ul > li > div.image > a.over > p.img > img");//取得圖片

        path = 1;

        foreach (IElement img in imgs)
        {
            URl = "https:" + img.GetAttribute("data-original");

            if (URl == "https:"){
                continue;
            }
            Debug.Log(URl);

            StartCoroutine(LoadPic(URl, path));

            path++;

        }

        int n = 0;
        foreach (IElement href in imgs ){

            URL2 = "https://wear.tw" + href.GetAttribute("href");

            if(URL2 == "https://wear.tw"){
                continue;
            }

            Debug.Log(URL2);
            arrayrect[n] = URL2;
            n++;

        }



    }

    public void Uppon()
    {

        IHtmlCollection<IElement> imgs;

        IConfiguration config = Configuration.Default.WithDefaultLoader();
        string url = "https://wear.tw/category/tops/";
        IDocument doc = BrowsingContext.New(config).OpenAsync(url).Result;

        /*CSS Selector寫法*/
        //IHtmlCollection<IElement> imgs = doc.QuerySelectorAll("ul li div.image_container p.img a.over img");//取得圖片
        imgs = doc.QuerySelectorAll("ul > li > div.image > a.over > p.img > img , ul > li > div.image > a.over");//取得圖片

        path = 1;

        foreach (IElement img in imgs)
        {
            URl = "https:" + img.GetAttribute("data-original");
            if (URl == "https:")
            {
                continue;
            }

            Debug.Log(URl);

            StartCoroutine(LoadPic(URl, path));

            path++;

        }

        int n = 0;

        foreach (IElement href in imgs)
        {

            URL2 = "https://wear.tw" + href.GetAttribute("href");

            if (URL2 == "https://wear.tw")
            {
                continue;
            }

            Debug.Log(URL2);
            arrayrect[n] = URL2;
            n++;

        }


    }

  

    public void Jacket()
    {
        IConfiguration config = Configuration.Default.WithDefaultLoader();
        string url = "https://wear.tw/category/jacket-outerwear/";
        IDocument doc = BrowsingContext.New(config).OpenAsync(url).Result;

        /*CSS Selector寫法*/
        //IHtmlCollection<IElement> imgs = doc.QuerySelectorAll("ul li div.image_container p.img a.over img");//取得圖片
        IHtmlCollection<IElement> imgs = doc.QuerySelectorAll("ul > li > div.image > a.over > p.img > img , ul > li > div.image > a.over");//取得圖片

        path = 1;

        foreach (IElement img in imgs)
        {
            URl = "https:" + img.GetAttribute("data-original");

            if (URl == "https:")
            {
                continue;
            }

            Debug.Log(URl);

            StartCoroutine(LoadPic(URl, path));

            path++;

        }

        int n = 0;

        foreach (IElement href in imgs)
        {

            URL2 = "https://wear.tw" + href.GetAttribute("href");

            if (URL2 == "https://wear.tw")
            {
                continue;
            }

            Debug.Log(URL2);
            arrayrect[n] = URL2;
            n++;

        }

    }

    public void Skirt()
    {

        cnt = 1;

        IConfiguration config = Configuration.Default.WithDefaultLoader();
        string url = "https://wear.tw/category/skirt/";
        IDocument doc = BrowsingContext.New(config).OpenAsync(url).Result;

        /*CSS Selector寫法*/
        //IHtmlCollection<IElement> imgs = doc.QuerySelectorAll("ul li div.image_container p.img a.over img");//取得圖片
        IHtmlCollection<IElement> imgs = doc.QuerySelectorAll("ul > li > div.image > a.over > p.img > img , ul > li > div.image > a.over");//取得圖片

        path = 1;

        foreach (IElement img in imgs)
        {
            URl = "https:" + img.GetAttribute("data-original");

            if (URl == "https:")
            {
                continue;
            }

            Debug.Log(URl);

            StartCoroutine(LoadPic(URl, path));

            path++;

        }

        int n = 0;

        foreach (IElement href in imgs)
        {

            URL2 = "https://wear.tw" + href.GetAttribute("href");

            if (URL2 == "https://wear.tw")
            {
                continue;
            }

            Debug.Log(URL2);
            arrayrect[n] = URL2;
            n++;

        }

    }

    public void Lower()
    {
        
        IConfiguration config = Configuration.Default.WithDefaultLoader();
        string url = "https://wear.tw/category/pants/";
        IDocument doc = BrowsingContext.New(config).OpenAsync(url).Result;

        /*CSS Selector寫法*/
        //IHtmlCollection<IElement> imgs = doc.QuerySelectorAll("ul li div.image_container p.img a.over img");//取得圖片
        IHtmlCollection<IElement> imgs = doc.QuerySelectorAll("ul > li > div.image > a.over > p.img > img , ul > li > div.image > a.over");//取得圖片

        path = 1;

        foreach (IElement img in imgs)
        {
            URl = "https:" + img.GetAttribute("data-original");

            if (URl == "https:")
            {
                continue;
            }

            Debug.Log(URl);

            StartCoroutine(LoadPic(URl, path));

            path++;

        }

        int n = 0;

        foreach (IElement href in imgs)
        {

            URL2 = "https://wear.tw" + href.GetAttribute("href");

            if (URL2 == "https://wear.tw")
            {
                continue;
            }

            Debug.Log(URL2);
            arrayrect[n] = URL2;
            n++;

        }

    }

    public void Shoes()
    {

        IConfiguration config = Configuration.Default.WithDefaultLoader();
        string url = "https://wear.tw/category/shoes/";
        IDocument doc = BrowsingContext.New(config).OpenAsync(url).Result;

        /*CSS Selector寫法*/
        //IHtmlCollection<IElement> imgs = doc.QuerySelectorAll("ul li div.image_container p.img a.over img");//取得圖片
        IHtmlCollection<IElement> imgs = doc.QuerySelectorAll("ul > li > div.image > a.over > p.img > img , ul > li > div.image > a.over");//取得圖片

        path = 1;

        foreach (IElement img in imgs)
        {
            URl = "https:" + img.GetAttribute("data-original");

            if (URl == "https:")
            {
                continue;
            }

            Debug.Log(URl);

            StartCoroutine(LoadPic(URl, path));

            path++;

        }

        int n = 0;

        foreach (IElement href in imgs)
        {

            URL2 = "https://wear.tw" + href.GetAttribute("href");

            if (URL2 == "https://wear.tw")
            {
                continue;
            }

            Debug.Log(URL2);
            arrayrect[n] = URL2;
            n++;

        }

    }

    public void Bag()
    {

        IConfiguration config = Configuration.Default.WithDefaultLoader();
        string url = "https://wear.tw/category/bag/";
        IDocument doc = BrowsingContext.New(config).OpenAsync(url).Result;

        /*CSS Selector寫法*/
        //IHtmlCollection<IElement> imgs = doc.QuerySelectorAll("ul li div.image_container p.img a.over img");//取得圖片
        IHtmlCollection<IElement> imgs = doc.QuerySelectorAll("ul > li > div.image > a.over > p.img > img , ul > li > div.image > a.over");//取得圖片

        path = 1;

        foreach (IElement img in imgs)
        {
            URl = "https:" + img.GetAttribute("data-original");

            if (URl == "https:")
            {
                continue;
            }

            Debug.Log(URl);

            StartCoroutine(LoadPic(URl, path));

            path++;

        }

        int n = 0;

        foreach (IElement href in imgs)
        {

            URL2 = "https://wear.tw" + href.GetAttribute("href");

            if (URL2 == "https://wear.tw")
            {
                continue;
            }

            Debug.Log(URL2);
            arrayrect[n] = URL2;
            n++;

        }

    }

    public void hat()
    {

        IConfiguration config = Configuration.Default.WithDefaultLoader();
        string url = "https://wear.tw/category/hat/";
        IDocument doc = BrowsingContext.New(config).OpenAsync(url).Result;

        /*CSS Selector寫法*/
        //IHtmlCollection<IElement> imgs = doc.QuerySelectorAll("ul li div.image_container p.img a.over img");//取得圖片
        IHtmlCollection<IElement> imgs = doc.QuerySelectorAll("ul > li > div.image > a.over > p.img > img , ul > li > div.image > a.over");//取得圖片

        path = 1;

        foreach (IElement img in imgs)
        {
            URl = "https:" + img.GetAttribute("data-original");

            if (URl == "https:")
            {
                continue;
            }

            Debug.Log(URl);

            StartCoroutine(LoadPic(URl, path));

            path++;

        }

        int n = 0;

        foreach (IElement href in imgs)
        {

            URL2 = "https://wear.tw" + href.GetAttribute("href");

            if (URL2 == "https://wear.tw")
            {
                continue;
            }

            Debug.Log(URL2);
            arrayrect[n] = URL2;
            n++;

        }

    }








}
