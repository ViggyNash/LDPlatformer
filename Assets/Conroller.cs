using UnityEngine;
using System.Collections;

public class Conroller : MonoBehaviour {

    //Input
    float moveAxis = 0;
    bool jump = false;
    bool slow = false;

    //Movement
    public float moveSpeed;
    public float jumpPower;
    bool grounded;

    //SloMo
    float baseTimeScale;
    float baseDeltaTime;
    public float slowMotionSpeed;

    //Rigidbody
    Rigidbody rBody;

	// Use this for initialization
	void Start () {
        rBody = GetComponent<Rigidbody>();

        baseTimeScale = Time.timeScale;
        baseDeltaTime = Time.fixedDeltaTime;

        grounded = false;
	}
	
	// Update is called once per frame
    void FixedUpdate()
    {
        moveAxis = Input.GetAxis("Horizontal");
        jump = Input.GetButtonDown("Jump");
        slow = false;
        slow = Input.GetButton("Fire3");

        Debug.Log(slow);

        if (slow)
        {
            Time.timeScale = slowMotionSpeed;
            Time.fixedDeltaTime = slowMotionSpeed * 0.02f;
        }
        else
        {
            Time.timeScale = baseTimeScale;
            Time.fixedDeltaTime = baseDeltaTime;
        }

        if (jump && grounded)
        {
            rBody.velocity = Vector3.zero;
            rBody.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
            grounded = false;
        }

        Debug.Log(moveAxis * moveSpeed);
        
        rBody.velocity = new Vector3(moveAxis * moveSpeed, rBody.velocity.y, rBody.velocity.z);
        Debug.Log(rBody.velocity);
    }

    public void OnCollisionEnter(Collision c)
    {
        if (c.transform.CompareTag("ground"))
        {
            grounded = true;
        }
        else if (c.transform.CompareTag("wall"))
        {

        }
    }

    public void OnCollisionStay(Collision c)
    {
        if (c.transform.CompareTag("wall"))
        {
            rBody.velocity = new Vector3(0, rBody.velocity.y, rBody.velocity.z);
        }
    }

}
