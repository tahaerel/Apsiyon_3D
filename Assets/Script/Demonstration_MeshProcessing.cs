using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Demonstration_MeshProcessing : MonoBehaviour
{
    public Material mat;

    void Start()
    {
        ProcessTaggedObjects("building");
        ProcessTaggedObjects("apartment");
        ProcessTaggedObjects("apartment-noelectric");
        ProcessTaggedObjects("apartment-borc");
        ProcessTaggedObjects("apartment-internet");
    }

   public void ProcessTaggedObjects(string tag)
    {
        GameObject[] taggedObjects = GameObject.FindGameObjectsWithTag(tag);

        foreach (GameObject o in taggedObjects)
        {
            Mesh m = o.GetComponent<MeshFilter>().mesh;
            Mesh r = MeshProcessor.processForOutlineMesh(m);

            GameObject f = new GameObject();
            f.transform.position = o.transform.position;
            f.transform.rotation = o.transform.rotation;
            f.transform.localScale = o.transform.localScale;
            f.name = o.name + " processed outline";

            f.AddComponent<MeshFilter>().mesh = r;
            f.AddComponent<MeshRenderer>().material = mat;
            f.GetComponent<MeshRenderer>().material.SetFloat("_Width", 0.05f / o.transform.localScale.x);
        }
    }
}
