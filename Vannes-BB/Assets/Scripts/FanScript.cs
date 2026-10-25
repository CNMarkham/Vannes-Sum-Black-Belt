using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class FanScript : MonoBehaviour
{
    public float fanOnTime;
    public float fanOffTime;
    public GameObject wind;
    public Animation fanon;
    public Animation fanoff;
    public Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();    
        InvokeRepeating("FanOn", 0f, fanOnTime + fanOffTime);
    }

    public void FanOn()
    {
        anim.SetBool("isOn", true);
        wind.gameObject.SetActive(true);
        Invoke("FanOff", fanOnTime);
    }

    public void FanOff()
    {

        anim.SetBool("isOn", false);
        wind.gameObject.SetActive(false);
    }
}
