using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitSoundPlay : MonoBehaviour
{
    [SerializeField] AudioClip Hit;
    [SerializeField] float MinInterval = 0.25f;

    private float timer;

    private void OnEnable()
    {
        GetComponent<AudioSource>().pitch = Random.Range(0.8f, 1.2f);
    }

    private void Update()
    {
        timer += Time.deltaTime;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!GetComponentInParent<Rigidbody2D>().isKinematic && timer >= MinInterval)
        {
            if (collision.collider.GetComponent<Rigidbody2D>())
            {
                if (GetComponentInParent<Rigidbody2D>().velocity.magnitude > collision.collider.GetComponentInParent<Rigidbody2D>().velocity.magnitude)
                {
                    GetComponent<AudioSource>().PlayOneShot(Hit);
                    timer = 0;
                }
            }
            else
            {
                GetComponent<AudioSource>().PlayOneShot(Hit);
                timer = 0;
            }
        }
    }
}
