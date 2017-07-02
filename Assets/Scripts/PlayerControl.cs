using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : Photon.MonoBehaviour
{
    public float maxSpeed;
    public float jumpHeight;

    bool facingRight;
    bool grounded;

    public Transform gunTip;
    public GameObject bullet;
    float fireRate = 0.5f;
    float nextFire = 0;

    Rigidbody2D myBody;
    Animator myAnim;

    private bool moveleft, moveright;

    private Vector3 realPos;

	// Use this for initialization
	void Start ()
    {
        myBody = GetComponent<Rigidbody2D>();
        myAnim = GetComponent<Animator>();
        facingRight = true;

        if (!photonView.isMine)
        {
            Destroy(myBody);
        }
	}

    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            //here we send the turret rotation angle to other clients
            //stream.SendNext(transform.position);
            stream.SendNext(facingRight);
        }
        else
        {
            //here we receive the turret rotation angle from others and apply it
            //realPos = (Vector3)stream.ReceiveNext();
            facingRight = (bool)stream.ReceiveNext();
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!photonView.isMine)
        {
            Vector3 theScale = transform.localScale;
            theScale.x = facingRight ? 1 : -1;
            transform.localScale = theScale;

            //transform.position = Vector3.Lerp(transform.position, realPos, Time.deltaTime * 5f);

            return;
        }

        MoveKeyBoard();
    }
   
    void MoveKeyBoard()
    {
        float move = Input.GetAxis("Horizontal");
        myAnim.SetFloat("Speed", Mathf.Abs(move));

        myBody.velocity = new Vector2(move * maxSpeed, myBody.velocity.y);

        if (move > 0 && !facingRight)
        {
            flip();
        }
        else if (move < 0 && facingRight)
        {
            flip();
        }
        if (Input.GetKey(KeyCode.Space))
        {
            if (grounded)
            {
                grounded = false;
                myBody.velocity = new Vector2(myBody.velocity.x, jumpHeight);
            }
        }
        if (Input.GetAxisRaw("Fire1") > 0)
        {
            fireBullet();
        }
        //FireByKeyboard();
    }

    void flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Wall")
        {
            grounded = true;
        }
    }

    void fireBullet()
    {
        if (Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            photonView.RPC("PunCreateBullet", PhotonTargets.All, new object[] { new object[] { gunTip.position.x, gunTip.position.y }, facingRight });
        }
    }

    [PunRPC]
    void PunCreateBullet(object[] shootPos, bool isFacingRight)
    {
        Vector3 shootPosVec = new Vector3((float)shootPos[0], (float)shootPos[1], 0);
        Instantiate(bullet, shootPosVec, Quaternion.Euler(new Vector3(0, 0, isFacingRight ? 0 : 180)));
    }
}
