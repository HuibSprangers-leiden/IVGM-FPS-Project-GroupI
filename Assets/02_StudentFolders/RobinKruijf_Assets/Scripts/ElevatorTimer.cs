using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.FPS;

namespace Unity.FPS.Gameplay
{
	[RequireComponent(typeof(Collider))]
public class ElevatorTimer : MonoBehaviour
{
    public Transform myElevator;
	public Transform myDestination1;
	public Transform myDestination2;
    public float myTimeUp;
    private bool isClicked = false;
    private float isUp = 1;
    private float myTime = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (myElevator.transform.position.y < myDestination1.transform.position.y || myElevator.transform.position.y > myDestination2.transform.position.y)
        {
            myTime += 1 * Time.deltaTime;
        }
        if ((isClicked || isUp == -1) && myElevator.transform.position.y > myDestination1.transform.position.y && myElevator.transform.position.y < myDestination2.transform.position.y)
        {
            myElevator.transform.position += new Vector3(0, isUp * Time.deltaTime, 0);
        } 
        if (isClicked && myTime > myTimeUp)
        {
        	isUp *= -1;
        	myTime =0;
            myElevator.transform.position += new Vector3(0, isUp * Time.deltaTime, 0);
            isClicked = false;
        }

    }
    void OnTriggerEnter(Collider other)
    {
        isClicked = true;
        //myElevator.transform.position += new Vector3(0, isUp * Time.deltaTime, 0);
            //myElevator.transform.position.y = Destination.transform.position.y;
    }
}
}