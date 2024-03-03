using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeOverTime : MonoBehaviour
{
    public float LifeTime = 0.4f;

    private float CurrentLifetime;
    private Vector3 MoveDirection;
    private bool HasSetInitialData = false;
    private float InitialAlpha;
    private ObjectPool pool;

    private void OnEnable()
    {
        InitialAlpha = .8f;
        pool = FindObjectOfType<ObjectPool>();
        HasSetInitialData = false;
    }
    // Update is called once per frame
    void Update()
    {
        if (!HasSetInitialData)
        {
            SetUp();
            HasSetInitialData = true;
        }

        CurrentLifetime -= Time.deltaTime;
        transform.position += MoveDirection * Time.deltaTime;

        GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, CurrentLifetime / LifeTime * InitialAlpha);

        if (CurrentLifetime <= 0)
        {
            FindObjectOfType<TrailSpawner>().ParticleList.Remove(this.gameObject);
            pool.ReturnObject(this.gameObject);
        }
    }

    private void SetUp()
    {
        CurrentLifetime = LifeTime;

        float x = Random.Range(-1f, 1.0f);
        float y = Random.Range(-1f, 1.0f);

        MoveDirection = new Vector3(x, y, 0) * 0.2f;
    }
}
