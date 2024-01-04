using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Transform target;
    private float speed = 20.0f;
    private bool isTargetLocked;

    private float bulletStrength = 15.0f;
    private float delayTime = 5.0f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (isTargetLocked && target != null)
        {
            Debug.Log("SOmething happened");
            Vector3 moveDirection = (target.transform.position - transform.position).normalized;
            transform.position += moveDirection * speed * Time.deltaTime;
            transform.LookAt(target);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (target != null)
        {

            if (collision.gameObject.CompareTag(target.tag))
            {
                Rigidbody targetRigidbody = collision.gameObject.GetComponent<Rigidbody>();
                Vector3 contact = -collision.contacts[0].normal;
                targetRigidbody.AddForce(contact * bulletStrength, ForceMode.Impulse);
                Destroy(gameObject);
            }
        }
    }
    public void Shoot(Transform newTarget)
    {
        target = newTarget;
        isTargetLocked = true;
        Destroy(gameObject, delayTime);
    }
}