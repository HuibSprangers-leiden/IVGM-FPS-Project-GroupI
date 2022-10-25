using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HS_SlidingDoor_Door : MonoBehaviour
{

    public float speed = 3f;

    bool isOpen = false;
    bool opening = false;
    float timer;
    float timerLength = 1f;

    public float counter = 0f;
    public float countToOpen = 2f;

    public Transform door1;
    public Transform door2;

    // Update is called once per frame
    void Update()
    {
        if(opening && timer > 0f)
        {
            door1.Translate(Vector3.forward * Time.deltaTime * speed);
            door2.Translate(-Vector3.forward * Time.deltaTime * speed);
            timer -= Time.deltaTime;
        } else if (!isOpen && counter >= countToOpen){
            isOpen = true;
            timer = timerLength;
            opening = true;
        }
    }
}
