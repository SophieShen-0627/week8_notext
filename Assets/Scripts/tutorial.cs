using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tutorial : MonoBehaviour
{
    [SerializeField] ParticleSystem indicator;
    [SerializeField] ParticleSystem particle2;
    private bool HasClick = false;
    // Start is called before the first frame update
    void Start()
    {
        transform.localScale = new Vector3(.15f, .15f, 1);
    }

    // Update is called once per frame
    void Update()
    {
        NoTutorial();
        //HasTutorial();
    }


    private void NoTutorial()
    {
        FindObjectOfType<SpawnSquareByMouse>().CanDoSpawn = true;
        if (!indicator.isStopped) indicator.Stop();
        if (particle2.isStopped) particle2.Play();

        Vector2 MousePostion = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float Dis = Vector2.Distance(MousePostion, transform.position);
        transform.position = MousePostion;


        if (Input.GetMouseButtonDown(0) && Dis <= 0.15f)
        {
            transform.localScale = new Vector3(.08f, .08f, 1);
        }

        if (Input.GetMouseButtonUp(0))
        {
            transform.localScale = new Vector3(.15f, .15f, 1);
        }
    }


    private void HasTutorial()
    {
        if (!HasClick)
        {
            Vector2 MousePostion = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            float Dis = Vector2.Distance(MousePostion, transform.position);

            float RateOverTime = Mathf.Clamp(1 / Dis * 5, 1, 4);

            var emission = indicator.emission;
            emission.rateOverTime = RateOverTime;

            if (Input.GetMouseButtonDown(0) && Dis <= 0.2f)
            {
                transform.localScale = new Vector3(.1f, .1f, 1);
                HasClick = true;
            }


            if (Dis <= 0.2f)
            {
                FindObjectOfType<SpawnSquareByMouse>().CanDoSpawn = true;
            }
            else
            {
                FindObjectOfType<SpawnSquareByMouse>().CanDoSpawn = false;
            }
        }

        if (HasClick)
        {
            if (!indicator.isStopped) indicator.Stop();
            if (particle2.isStopped) particle2.Play();

            Vector2 MousePostion = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            float Dis = Vector2.Distance(MousePostion, transform.position);
            transform.position = MousePostion;


            if (Input.GetMouseButtonDown(0) && Dis <= 0.15f)
            {
                transform.localScale = new Vector3(.1f, .1f, 1);
            }

            if (Input.GetMouseButtonUp(0))
            {
                transform.localScale = new Vector3(.25f, .25f, 1);
            }
        }
    }
}
