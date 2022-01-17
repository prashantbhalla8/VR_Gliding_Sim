using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlidingMovement : MonoBehaviour
{
    public float forwardSpeed = 40.0f;
    public float gravity = -15.0f;

    // Variables for debugging
    public float checkVertSpeed;
    public float checkRotZ;
    public float checkGravity;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Gliding Movement Script added to: " + gameObject.name);
    }

    // Update is called once per frame
    void Update()
    {
        // Debugging Variables
        checkVertSpeed = transform.position.y;
        checkRotZ = transform.localEulerAngles.x;
        checkGravity = gravity * Time.deltaTime * Time.deltaTime;
        // Push forward
        transform.position += transform.forward * Time.deltaTime * forwardSpeed;

        // Slow down going up and yeet when going down
        forwardSpeed -= transform.forward.y * 20.0f * Time.deltaTime;
        
        // Minimum forward speed
        if(forwardSpeed < 35.0f)
        {
            forwardSpeed = 35.0f;
        }

        // Move player up/down or rotate around z-axis
        transform.Rotate(Input.GetAxis("Vertical") * Time.deltaTime * 50.0f, 0.0f, -Input.GetAxis("Horizontal") * Time.deltaTime * 80.0f);

        // Check for plane crashing with terrain
        float terrainHeight = Terrain.activeTerrain.SampleHeight(transform.position);

        if(terrainHeight > transform.position.y)
        {
            transform.position = new Vector3(transform.position.x, terrainHeight, transform.position.y);
        }

        // Affect of gravity

        transform.position += transform.up * gravity * Time.deltaTime;
        //hmmm
        if(transform.localEulerAngles.x > 15 && transform.localEulerAngles.x < 70)
        {
            gravity = 0.0f;
        }
        else
        {
            gravity = -15.0f;
        }

    }
}
