using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Text;
using AngleSharp;
using AngleSharp.Dom;

            
public class Spider : MonoBehaviour {

	// Use this for initialization
	
	
	// Update is called once per frame
	void Update () {
		
	}


    void Start()
        
    { 
        IConfiguration config = Configuration.Default.WithDefaultLoader();
        string url = "https://wear.tw"; 
        IDocument doc =   BrowsingContext.New(config).OpenAsync(url).Result;
           
            /*CSS Selector寫法*/
        //IHtmlCollection<IElement> imgs = doc.QuerySelectorAll("ul li div.image_container p.img a.over img");//取得圖片
        IHtmlCollection<IElement> imgs = doc.QuerySelectorAll("ul > li.large > div.image_container > p.img > a.over > img");//取得圖片

        foreach (IElement img in imgs)
        {
            Debug.Log("https:"+img.GetAttribute("data-original"));
        }

    }


   
}
