using UnityEngine;
using System.Collections;

public class Rotator : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
	
    }
	
    // Update is called once per frame
    void Update()
    {   
        GetComponent<Rigidbody2D>().MoveRotation(5000 * Time.deltaTime);// rot Rotate(0, 0, 50 * Time.deltaTime, Space.World);
    }
}
