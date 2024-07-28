using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    public GameObject explosionPrefab;
    public bool isInHole = false;
    public bool isInGround = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Hole")
        {
            isInHole = true;
        }

        if (other.tag == "Ground")
        {
            isInGround = true;
        }

        UpdateBulletState(other.ClosestPoint(transform.position));

    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Hole")
        {
            isInHole = false;
        }

        UpdateBulletState(other.ClosestPoint(transform.position));
    }

    private void UpdateBulletState(Vector3 hitPoint)
    {
        if (!isInHole && isInGround)
        {
            SpawnHole(hitPoint);
        }
    }

    private void SpawnHole(Vector3 hitPoint)
    {
        Instantiate(explosionPrefab, hitPoint, Quaternion.identity);
        Destroy(gameObject);
    }
}
