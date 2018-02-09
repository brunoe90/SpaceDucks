using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Boundary
{
    public float xMin, yMin, xMax, yMax;
}

public class PlayerController : MonoBehaviour {

    public float speed;
    public Boundary boundary;
    public float tilt;
    public GameObject shot;
    public Transform shotSpawn;
    public float fireRate;
    private float nextFire;
    // Update every time frame once per frame
    private void Update()
    {
        if(Input.GetButton("Fire1") && Time.time > nextFire) {
            GetComponent<AudioSource>().Play();
            nextFire = Time.time + fireRate;
            Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
        }
    }
    // Update every time physics change once per frame
    void FixedUpdate () {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        Vector2 movement= new Vector2 (moveHorizontal, moveVertical);
        GetComponent<Rigidbody2D>().velocity = movement * speed;
        GetComponent<Rigidbody2D>().position = new Vector2(
            Mathf.Clamp(GetComponent<Rigidbody2D>().position.x, boundary.xMin, boundary.xMax),
            Mathf.Clamp(GetComponent<Rigidbody2D>().position.y, boundary.yMin, boundary.yMax)
        );

        GetComponent<Rigidbody2D>().rotation = GetComponent<Rigidbody2D>().velocity.x * tilt;

    }
}
