using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ChangeBoy : MonoBehaviour
{

    private SkinnedMeshRenderer oldSmr = null;
    private SkinnedMeshRenderer newSmr = null;

    private Object oldObj = null;
    private Object newObj = null;

    private GameObject oldInstance = null;
    private GameObject newInstance = null;

    public static ChangeBoy instance;
    void OnGUI()
    {
        
    }

    void Start()
    {
        instance = this;

    }

    public void ChangeFeet(string item)
    {
        if (gameObject.transform.GetChild(2).name == "jiao")
        {
            gameObject.transform.GetChild(2).SetSiblingIndex(0);
        }
        else if (gameObject.transform.GetChild(1).name == "jiao")
        {
            gameObject.transform.GetChild(1).SetSiblingIndex(0);
        }


        newObj = Resources.Load("prefab-boy/" + item);
        newInstance = Instantiate(newObj) as GameObject;

        oldSmr = gameObject.GetComponentInChildren<SkinnedMeshRenderer>();
        newSmr = newInstance.GetComponentInChildren<SkinnedMeshRenderer>();

        Transform[] oldBones = gameObject.GetComponentsInChildren<Transform>();

        Debug.Log("oldBones.Length: " + oldBones.Length);
        Transform[] newBones = newSmr.bones;
        Debug.Log("newBones.Length: " + newBones.Length);


        List<Transform> bones = new List<Transform>();
        foreach (Transform bone in newBones)
        {
            foreach (Transform oldBone in oldBones)
            {
                if (bone != null && oldBone != null)
                {
                    if (bone.name != oldBone.name)
                    {
                        continue;
                    }
                    bones.Add(oldBone);
                }
            }
        }

        oldSmr.bones = bones.ToArray();
        oldSmr.sharedMesh = newSmr.sharedMesh;
        oldSmr.materials = newSmr.materials;


        GameObject.DestroyImmediate(newInstance);
        GameObject.DestroyImmediate(newSmr);
    }
    public void Changecloth(string item)
    {
        if (gameObject.transform.GetChild(2).name == "shen")
        {
            gameObject.transform.GetChild(2).SetSiblingIndex(0);
        }
        else if (gameObject.transform.GetChild(1).name == "shen")
        {
            gameObject.transform.GetChild(1).SetSiblingIndex(0);
        }


        newObj = Resources.Load("prefab-boy/" + item);
        newInstance = Instantiate(newObj) as GameObject;

        oldSmr = gameObject.GetComponentInChildren<SkinnedMeshRenderer>();
        newSmr = newInstance.GetComponentInChildren<SkinnedMeshRenderer>();

        Transform[] oldBones = gameObject.GetComponentsInChildren<Transform>();
        Debug.Log("oldBones.Length: " + oldBones.Length);
        Transform[] newBones = newSmr.bones;
        Debug.Log("newBones.Length: " + newBones.Length);

        List<Transform> bones = new List<Transform>();
        foreach (Transform bone in newBones)
        {
            foreach (Transform oldBone in oldBones)
            {
                if (bone != null && oldBone != null)
                {
                    if (bone.name != oldBone.name)
                    {
                        continue;
                    }
                    bones.Add(oldBone);
                }
            }
        }
        oldSmr.bones = bones.ToArray();
        oldSmr.sharedMesh = newSmr.sharedMesh;
        oldSmr.materials = newSmr.materials;

        GameObject.DestroyImmediate(newInstance);
        GameObject.DestroyImmediate(newSmr);
    }

}