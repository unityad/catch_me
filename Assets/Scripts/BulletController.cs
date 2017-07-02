using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour {

    public float bulletSpeed;
    Rigidbody2D myBody;
    private void Awake()
    {
        myBody = GetComponent<Rigidbody2D>();
        if (transform.localRotation.z > 0)
        {
            myBody.AddForce(new Vector2(-1, 0) * bulletSpeed, ForceMode2D.Impulse);

        }
        else
        {
            myBody.AddForce(new Vector2(1, 0) * bulletSpeed, ForceMode2D.Impulse);
        }
    }
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
