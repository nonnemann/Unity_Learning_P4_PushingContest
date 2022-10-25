using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float speed = 2.0f;
    [SerializeField] private bool hasPowerup = false;
    [SerializeField] private float powerupStrength = 15.0f;
    [SerializeField] private GameObject powerupIndicator;
    
    private Rigidbody _rigidbody;
    private GameObject _focalPoint;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _focalPoint = GameObject.Find("Focal Point");
    }

    private void Update()
    {
        float forwardInput = Input.GetAxis("Vertical");
        _rigidbody.AddForce(_focalPoint.transform.forward * speed * forwardInput);
        powerupIndicator.transform.position = transform.position + new Vector3(0, -0.5f, 0);
    }

    private void OnTriggerEnter(Collider otherObject)
    {
        if (otherObject.CompareTag("Powerup"))
        {
            hasPowerup = true;
            Destroy(otherObject.gameObject);
            powerupIndicator.gameObject.SetActive(true);
            StartCoroutine(PowerupCountdownRoutine());
        }
    }

    private IEnumerator PowerupCountdownRoutine()
    {
        yield return new WaitForSeconds(7);
        hasPowerup = false;
        powerupIndicator.gameObject.SetActive(false);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && hasPowerup)
        {
            Rigidbody enemyRigidbody = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFromPlayer = collision.gameObject.transform.position - transform.position;
            enemyRigidbody.AddForce(awayFromPlayer * powerupStrength, ForceMode.Impulse);
        }
    }
}
