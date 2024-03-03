using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailSpawner : MonoBehaviour
{
    [SerializeField] float DistanceToSpawnParticle = 0.4f;
    //[SerializeField] GameObject ParticleToSpawn;
    [SerializeField] float ParticleLifeTime = 0.4f;

    public List<GameObject> ParticleList;         

    private Vector3 lastPosition;
    private Vector3 CurrentPosition;
    private ObjectPool Pool;

    void Start()
    {
        lastPosition = transform.position;
        Pool = FindObjectOfType<ObjectPool>();
    }

    private void Update()
    {
        CurrentPosition = transform.position;

        if (Vector2.Distance(CurrentPosition, lastPosition) >= DistanceToSpawnParticle)
        {
            SpawnWithDistance(lastPosition);
            lastPosition = CurrentPosition;
        }

        RenderLines();
    }

    private void SpawnWithDistance(Vector3 Pos)
    {
        float x = Random.Range(-DistanceToSpawnParticle / 2.0f, DistanceToSpawnParticle / 2.0f) + Pos.x;
        float y = Random.Range(-DistanceToSpawnParticle / 2.0f , DistanceToSpawnParticle / 2.0f) + Pos.y;

        var temp = Pool.GetObject();
        temp.transform.position = new Vector3(x, y, 0);
        temp.GetComponent<FadeOverTime>().LifeTime = ParticleLifeTime;

        ParticleList.Add(temp);
    }

    private void RenderLines()
    {
        /*if (ParticleList.Count >= 1)
        {
            foreach (var particle in ParticleList)
            {
                if (!particle.activeInHierarchy)
                {
                    ParticleList.Remove(particle);
                }
            }
        }*/


        GameObject[] particles = ParticleList.ToArray();

        for (int i = particles.Length - 2; i >= 0; i--)
        {
            particles[i].GetComponent<LineRenderer>().SetPosition(0, particles[i].transform.position);
            particles[i].GetComponent<LineRenderer>().SetPosition(1, particles[i + 1].transform.position);

            particles[i].GetComponent<LineRenderer>().startColor = particles[i].GetComponent<SpriteRenderer>().color;
            particles[i].GetComponent<LineRenderer>().endColor = particles[i].GetComponent<SpriteRenderer>().color;
        }

        if (particles.Length != 0)
        {
            int j = particles.Length - 1;
            particles[j].GetComponent<LineRenderer>().SetPosition(0, particles[j].transform.position);
            particles[j].GetComponent<LineRenderer>().SetPosition(1, transform.position);

            particles[j].GetComponent<LineRenderer>().startColor = particles[j].GetComponent<SpriteRenderer>().color;
            particles[j].GetComponent<LineRenderer>().endColor = particles[j].GetComponent<SpriteRenderer>().color;
        }
    }
}
