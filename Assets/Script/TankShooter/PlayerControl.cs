using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControl : MonoBehaviour
{
    private PlayerControls _playerControls;
    public Transform gunTransform;
    public Transform shootingPointTransform;
    public GameObject bulletPrefab;

    private void Awake()
    {
        _playerControls = new PlayerControls();
        _playerControls.MiniGames.Fire.performed += PressTime;
    }

    private void OnEnable()
    {
        _playerControls.Enable();
    }

    private void OnDisable()
    {
        _playerControls.Disable();
    }

    private void OnFire()
    {
        GameObject bullet = Instantiate(bulletPrefab, shootingPointTransform.position, Quaternion.identity);
        Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();
        if (bulletRb != null)
        {
            Vector2 shootingDirection = (shootingPointTransform.position - gunTransform.position).normalized;
            bulletRb.AddForce(shootingDirection * 10, ForceMode2D.Impulse);
        }
        else
        {
            Debug.LogError("Bullet prefab is missing Rigidbody2D component.");
        }
    }

    private void PressTime(InputAction.CallbackContext context)
    {
        Debug.Log("Press Time: " + context.duration);
    }
}