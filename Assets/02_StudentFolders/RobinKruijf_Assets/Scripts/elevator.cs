using System.Collections;
using System.Collections.Generic;
using Unity.FPS;
using UnityEngine;

namespace Unity.FPS.Gameplay
{
	[RequireComponent(typeof(Collider))]
public class elevator : MonoBehaviour
{
	public Transform myElevator;
	public Transform Destination;
    private bool isClicked = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isClicked && myElevator.transform.position.y < Destination.transform.position.y)
        {
            myElevator.transform.position += new Vector3(0, 1 * Time.deltaTime, 0);
        }
    }
    void OnTriggerEnter(Collider other)
    {
        isClicked = true;
            //myElevator.transform.position.y = Destination.transform.position.y;
    }
}
}
