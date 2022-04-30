using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManagment : MonoBehaviour
{
    private Transform myCamera;
    private float newX, newY;
    private bool change = false;
    float smoothTime = 0.5f;
    float yVelocity = 0.0f;

    public Transform player;

    // Start is called before the first frame update
    void Start()
    {
        myCamera = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Mathf.Abs(player.position.x) - Mathf.Abs(myCamera.position.x) > 8 )
        {
            newX = player.position.x;
            change = true;
        }

        if(change)
        {
            change = false;
            float newPosition;
            do
            {
                newPosition = Mathf.SmoothDamp(myCamera.position.x, player.position.x, ref yVelocity, smoothTime);
                myCamera.position = new Vector3(newPosition, myCamera.position.y, myCamera.position.z);
            } while (myCamera.position.x == player.position.x);
        }
        
        
    }
}
