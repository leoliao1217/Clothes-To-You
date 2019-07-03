using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class changeSprite : MonoBehaviour
{

    // Use this for initialization


    public Sprite Sprite1 ,Sprite2 , Sprite3 , Sprite4 , Sprite5 , Sprite6;


    private void Update()
    {


    }
    //public float width = 1;
    //public float height = 1;
    //public Vector3 position = new Vector3(-2.5f, 0.3f, 0);

    //void Awake()
    //{
    //    // set the scaling
    //    Vector3 scale = new Vector3(width, height, 1f);
    //    transform.localScale = scale;
    //    // set the position
    //    transform.position = position;
    //}
   
    public void Onclick1(){

        this.GetComponent<SpriteRenderer>().sprite = Sprite1;

        // Sp.rectTransform.sizeDelta = new Vector2(width, height);
        //float width = 300;
        //float height = 3;
        //Vector2 scale = new Vector2(width, height);
        //transform.localScale = scale;
        //transform.position ;

        //Sprite sprite = ...;// Get sprite
        Bounds bounds = Sprite1.bounds;
        var pivotX = -bounds.center.x / bounds.extents.x / 100 + 3f;
        var pivotY = -bounds.center.y / bounds.extents.y / 200 + 100f;
        var pixelsToUnits = Sprite1.textureRect.width / bounds.size.x;

        Sprite sameSprite = Sprite.Create(Sprite1.texture, Sprite1.rect,
                                             new Vector2(pivotX, pivotY),
                                             pixelsToUnits);
      
    }

    public void Onclick2()
    {

        this.GetComponent<SpriteRenderer>().sprite = Sprite2;
    }

    public void Onclick3()
    {

        this.GetComponent<SpriteRenderer>().sprite = Sprite3;
    }

    public void Onclick4()
    {

        this.GetComponent<SpriteRenderer>().sprite = Sprite4;
    }

    public void Onclick5()
    {

        this.GetComponent<SpriteRenderer>().sprite = Sprite5;
    }

    public void Onclick6()
    {

        this.GetComponent<SpriteRenderer>().sprite = Sprite6;
    }
}

