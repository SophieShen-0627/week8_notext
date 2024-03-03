using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnSquareByMouse : MonoBehaviour
{
    public bool CanDoSpawn = false;

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

            CurrentColor = HSBToRGB(h_hsb, s, b);
        }

        if (BoxScale <= 0.5f)
        {
            float s = BoxScale * 0.43f * 2f;

            CurrentColor = HSBToRGB(h_hsb, s, 1);
        }

        CurrentBox.GetComponentInChildren<SpriteRenderer>().color = CurrentColor;
    }

    private float GetBoxInitialColor()
    {
        float i = Random.Range(0, 1.00f);
        CurrentColor = HSBToRGB(i, 0.43f, 1);

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

    public static Color HSBToRGB(float h_hsb, float s_hsb, float b_hsb)
    {
        h_hsb = Mathf.Clamp01(h_hsb);
        s_hsb = Mathf.Clamp01(s_hsb);
        b_hsb = Mathf.Clamp01(b_hsb);

        float r = 0, g = 0, b = 0;
        if (s_hsb == 0)
        {
            r = g = b = b_hsb; // 如果饱和度为0，则颜色为灰度，即RGB相等，都为亮度值
        }
        else
        {
            var sector = h_hsb * 6f; // 将色调范围从0-1映射到0-6
            var sectorPos = sector - Mathf.Floor(sector); // 找到在当前扇区内的位置

            var temp1 = b_hsb * (1 - s_hsb);
            var temp2 = b_hsb * (1 - s_hsb * sectorPos);
            var temp3 = b_hsb * (1 - s_hsb * (1 - sectorPos));

            switch ((int)sector)
            {
                case 0:
                    r = b_hsb;
                    g = temp3;
                    b = temp1;
                    break;
                case 1:
                    r = temp2;
                    g = b_hsb;
                    b = temp1;
                    break;
                case 2:
                    r = temp1;
                    g = b_hsb;
                    b = temp3;
                    break;
                case 3:
                    r = temp1;
                    g = temp2;
                    b = b_hsb;
                    break;
                case 4:
                    r = temp3;
                    g = temp1;
                    b = b_hsb;
                    break;
                default: // case 5:
                    r = b_hsb;
                    g = temp1;
                    b = temp2;
                    break;
            }
        }

        return new Color(Mathf.Clamp01(r), Mathf.Clamp01(g), Mathf.Clamp01(b), 1f);
    }
}
