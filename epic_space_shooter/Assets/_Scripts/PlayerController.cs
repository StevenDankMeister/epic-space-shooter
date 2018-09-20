using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Boundary
{
    public float xMin, xMax, zMin, zMax;
}

[RequireComponent(typeof(AudioSource))]
public class PlayerController : MonoBehaviour {

    public float speed;
    public float tilt;

    public Boundary boundary;
    public float fireRate;
    public GameObject shot;
    public Transform shotSpawn;

    private Rigidbody rb;
    AudioSource audio;


    private float nextFire;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audio = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (Input.GetButton("Fire1") && Time.time > nextFire)
        {
            if (Input.GetButton("Fire2"))
            {
                print("shift held");

                Instantiate(shot, shotSpawn.position + new Vector3(-0.20f, 0.0f, 0.0f), shotSpawn.rotation);
                Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
                Instantiate(shot, shotSpawn.position + new Vector3(0.20f, 0.0f, 0.0f), shotSpawn.rotation);
            }
            else
            {
                Instantiate(shot, shotSpawn.position + new Vector3(-1, 0.0f, 0.0f), shotSpawn.rotation);
                Instantiate(shot, shotSpawn.position + new Vector3(1, 0.0f, 0.0f), shotSpawn.rotation);
            }            
            nextFire = Time.time + fireRate;
            audio.Play();
        }
    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        rb.velocity = movement * speed;

        rb.position = new Vector3(Mathf.Clamp(rb.position.x, boundary.xMin, boundary.xMax), 
            0.0f,
            Mathf.Clamp(rb.position.z, boundary.zMin, boundary.zMax)
            );

        rb.rotation = Quaternion.Euler(0.0f, 0.0f, rb.velocity.x * -tilt);
    }
}
