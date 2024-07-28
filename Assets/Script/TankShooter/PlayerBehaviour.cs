using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    public bool isInHole = false;
    public bool isInGround = false;
    public bool isBullet = false;


    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.tag == "Ground")
        {
            isInGround = true;
        }

        if (other.tag == "Hole")
        {
            isInHole = true;
        }

        if (other.tag == "Bullet")
        {
            isBullet = true;
        }

        Debug.Log("isInGround, isInHole, IsBullet: " + isInGround + isInHole + isBullet);

        if (isBullet && !isInGround && !isInHole)
        {
            GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        }
        else if (!isBullet && !isInGround && isInHole)
        {
            GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        }
        else if (!isBullet && isInGround && !isInHole)
        {
            GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Bullet")
        {
            isBullet = false;
        }

        if (other.tag == "Ground")
        {
            isInGround = false;
        }

        if (other.tag == "Hole")
        {
            isInHole = false;
        }

        Debug.Log("OUT isInGround, isInHole, IsBullet: " + isInGround + isInHole + isBullet);
        if (!isBullet && !isInGround && !isInHole)
        {
            GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        }

    }


}
