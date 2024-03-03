using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class SpawnSquareByMouse : MonoBehaviour
{
    public bool CanDoSpawn = false;

    [SerializeField] CinemachineTargetGroup group;

    [SerializeField] GameObject BoxPrefab;
    [SerializeField] float MouseCameraScaleParameter = 1;

    private GameObject CurrentBox;
    private bool SpawnMod = false;
    private Vector3 MouseInitialPos;
    private Color CurrentColor;
    private float h_hsb;

    // Update is called once per frame
    void Update()
    {


        if (CanDoSpawn)
        {
            if (Input.GetMouseButtonDown(0))
            {
                MouseInitialPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                MouseInitialPos = new Vector3(MouseInitialPos.x, MouseInitialPos.y, 0);

                CurrentBox = InstantiateBox(MouseInitialPos);
                h_hsb = GetBoxInitialColor();

                CurrentBox.GetComponentInChildren<SpriteRenderer>().color = CurrentColor;

                //GetComponent<LineRenderer>().SetPosition(0, MouseInitialPos);


                SpawnMod = true;
            }

            if (SpawnMod)
            {
                ChangeBoxScale(MouseInitialPos, CurrentBox);
                ChangeBoxColor();
            }

            if (Input.GetMouseButtonUp(0))
            {
                SpawnMod = false;
                CurrentBox.GetComponentInChildren<Rigidbody2D>().isKinematic = false;

                CurrentBox = null;
            }
        }

    }

    private void ChangeBoxColor()
    {
        float BoxScale = Mathf.Abs( CurrentBox.transform.localScale.x * CurrentBox.transform.localScale.y);

        if (BoxScale >= 3)
        {
            float s = 0.43f - (BoxScale - 3) * 0.1f;
            float b = 1 - (BoxScale - 3) * 0.1f;

            CurrentColor = MyUtility.HSBToRGB(h_hsb, s, b);
        }

        if (BoxScale <= 0.5f)
        {
            float s = BoxScale * 0.43f * 2f;

            CurrentColor = MyUtility.HSBToRGB(h_hsb, s, 1);
        }

        CurrentBox.GetComponentInChildren<SpriteRenderer>().color = CurrentColor;
    }

    private float GetBoxInitialColor()
    {
        float i = Random.Range(0, 1.00f);
        CurrentColor = MyUtility.HSBToRGB(i, 0.43f, 1);

        return i;
    }

    private GameObject InstantiateBox(Vector3 Pos)
    {
        var Box = Instantiate(BoxPrefab, Pos, Quaternion.identity);
        Box.transform.localScale = Vector3.zero;
        Box.GetComponentInChildren<Rigidbody2D>().isKinematic = true;

        return Box;
    }

    private void ChangeBoxScale(Vector3 MouseIniPos, GameObject Box)
    {

        Vector3 MousePos = Camera.main.ScreenToWorldPoint( Input.mousePosition);
        MousePos = new Vector3(MousePos.x, MousePos.y, 0);
        Vector3 Dir = MousePos - MouseIniPos;

        Vector3 NewScale = new Vector3(Dir.x, Dir.y, 1);

        //GetComponent<LineRenderer>().SetPosition(1, MousePos);

        //no idea why I should do this but the pivot of its child do change.
        Transform child = CurrentBox.GetComponentInChildren<SpriteRenderer>().transform;
        child.transform.localPosition = new Vector3(0.5f, 0.5f, 0);

        Box.transform.localScale = NewScale;
    }

}
