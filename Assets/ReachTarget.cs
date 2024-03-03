using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct GoalLineRenderer
{
    public LineRenderer lineRenderer; 
    public float height; 
    public bool hasReached; 

    public GoalLineRenderer(LineRenderer lineRenderer, float height, bool hasReached)
    {
        this.lineRenderer = lineRenderer;
        this.height = height;
        this.hasReached = hasReached;
    }
}

public class ReachTarget : MonoBehaviour
{
    public List<Box> boxes = new List<Box>();

    [SerializeField] float CurrentTargetheight = 10;
    [SerializeField] float CurrentInterval = 10;
    [SerializeField] List<GoalLineRenderer> Lines = new List<GoalLineRenderer>();
    [SerializeField] ParticleSystem CelebrateParticle;
    [SerializeField] List<Material> LineMaterial = new List<Material>();
    [SerializeField] LineRenderer GoalLines;

    // Update is called once per frame
    void Update()
    {
        Vector3 leftTop = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height, Camera.main.nearClipPlane));
        Vector3 rightTop = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.nearClipPlane));

        DrawLines(leftTop.x, rightTop.x);

        if (leftTop.y >= CurrentTargetheight)
        {
            AddTargetLine(leftTop.y);
            CurrentTargetheight += CurrentInterval;
            CurrentInterval += CurrentTargetheight;
        }

        CheckTargetReached();
    }

    private void CheckTargetReached()
    {
        if (boxes.Count != 0)
        {
            foreach (var box in boxes)
            {
                if (!box.GetComponent<Rigidbody2D>().isKinematic)
                {
                    for (int i = 0; i < Lines.Count; i ++)
                    {
                        if (box.GetComponent<Rigidbody2D>().velocity.magnitude <= 0.1f && box.transform.position.y >= Lines[i].height && !Lines[i].hasReached)
                        {
                            Lines[i] = UpdateHasReached(Lines[i], true);
                            CelebrateParticle.transform.position = box.transform.position;
                            CelebrateParticle.Play();

                            break;
                        }
                    }
                }
            }
        }
    }

    public GoalLineRenderer UpdateHasReached(GoalLineRenderer inputStruct, bool newValue)
    {
        inputStruct.hasReached = newValue;
        inputStruct.lineRenderer.startColor = Color.white;
        inputStruct.lineRenderer.endColor = Color.white;

        return inputStruct; 
    }

    private void AddTargetLine(float Height)
    {
        LineRenderer renderer = Instantiate(GoalLines, Vector3.zero, Quaternion.identity);
        float i = Random.Range(0, 1.0f);
        renderer.startColor = MyUtility.HSBToRGB(i, 0.4f, 1);
        renderer.endColor = MyUtility.HSBToRGB(i, 0.4f, 1);
        renderer.SetMaterials(LineMaterial);
        renderer.startWidth = 0.05f;
        renderer.endWidth = 0.05f;

        GoalLineRenderer instance = new GoalLineRenderer(renderer, Height, false);

        Lines.Add(instance);
    }

    private void DrawLines(float x1, float x2)
    {
        foreach (var line in Lines)
        {
            line.lineRenderer.SetPosition(0, new Vector3(x1, line.height, 0));
            line.lineRenderer.SetPosition(1, new Vector3(x2, line.height, 0));
        }
    }
}
