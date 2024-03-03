using UnityEngine;
public class MyUtility
{
    public static Color HSBToRGB(float h_hsb, float s_hsb, float b_hsb)
    {
        h_hsb = Mathf.Clamp01(h_hsb);
        s_hsb = Mathf.Clamp01(s_hsb);
        b_hsb = Mathf.Clamp01(b_hsb);

        float r = 0, g = 0, b = 0;
        if (s_hsb == 0)
        {
            r = g = b = b_hsb; // ������Ͷ�Ϊ0������ɫΪ�Ҷȣ���RGB��ȣ���Ϊ����ֵ
        }
        else
        {
            var sector = h_hsb * 6f; // ��ɫ����Χ��0-1ӳ�䵽0-6
            var sectorPos = sector - Mathf.Floor(sector); // �ҵ��ڵ�ǰ�����ڵ�λ��

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
