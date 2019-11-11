﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hole : MonoBehaviour
{
    public float ejectionForce;
    public GameObject exit;
    public Vector3 ejectionVector;
    public float maxAngle;

    private void Start()
    {
        ejectionVector = exit.transform.up;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            IEnumerator coroutine;
            coroutine = Ejection(other.gameObject);
            StartCoroutine(coroutine);
        }
    }

    IEnumerator Ejection(GameObject cc)
    {
        //cc.SetActive(false);
        Vector3 toExit = exit.transform.position - cc.transform.position;
        Collider[] cols = cc.transform.parent.GetComponentsInChildren<Collider>();
        Renderer[] rends = cc.transform.parent.GetComponentInParent<Transform>().GetComponentsInChildren<Renderer>();
        foreach (Collider c in cols)
        {

            c.enabled = false;
            if(c.GetComponent<Rigidbody>()!=null)
            c.gameObject.GetComponent<Rigidbody>().velocity = (gameObject.transform.position - cc.transform.position)*2;
        }

        //foreach (Renderer r in rends)
        //{
        //    r.enabled = false;
        //}

        yield return new WaitForSeconds(1.0f);

        foreach (Collider c in cols)
        {
            c.gameObject.transform.position = exit.transform.position+Vector3.up*3;
            if (c.GetComponent<Rigidbody>() != null)

                c.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
        float angle = UnityEngine.Random.Range(-maxAngle*Mathf.Deg2Rad, maxAngle * Mathf.Deg2Rad);

        foreach(Collider c in cols)
        {
            if (c.GetComponent<Rigidbody>() != null)
                c.GetComponent<Rigidbody>().velocity = (ejectionVector + new Vector3(Mathf.Cos(angle) * ejectionForce, ejectionForce, Mathf.Sin(angle) *ejectionForce));
        }

        //foreach (Renderer r in rends)
        //{
        //    r.enabled = true;
        //}       
        yield return new WaitForSeconds(0.3f);



        foreach (Collider c in cols)
        {
            c.enabled = true;
        }
        //cc.SetActive(true);
    }
}
