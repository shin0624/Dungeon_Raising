using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteAlways]
public class RadarChart : MonoBehaviour
{
    /* CharacterSelectScene���� ���� ĳ���� �� �ɷ�ġ ���̴� ��Ʈ ������Ʈ. �����͸� �����Ű�� �ʾƵ� ��ũ��Ʈ�� �ݹ��Լ��� ����ǵ��� ExecuteAlways�Ӽ��� �ٿ��ش�.
    1. ���� ��� :  ������ ����� ���̴� ��Ʈ����, ������ �� �������� ���� �������� ������ �Ÿ��� ��ġ
    ���� �߽ɰ��� 360�� -> �������̹Ƿ� 360/5 = 72�� -> ù ���������� �����Ͽ� 72���� ȸ���ϸ� ���� �������� ����Ѵ�.

    2. ��ǥ ��� : ���� �߽ɰ��� �������� �̿��� 2D ��ǥ�� ���
    ������ ������ ���� ����ǥ(r, theta)�� ����Ƽ������ ���� ��ġ ǥ���� ����ϴ� ������ǥ��(x,y,z)�� ��ȯ�Ͽ� ���� ��Ʈ ���·� �׷��� ��.
    ��->���� ��ȯ : x = rcos(theta) / y = rsin(theta)

    3. ������ ����ȭ : �ν����Ϳ��� �Է¹��� ���� 0~1������ ��ȯ�Ͽ� �׷����� ������ ũ�� ����, �� ���� ���� ���� ���� ����ȭ(4/10 -> 0.4)

    4. ���� �������� �� ���� : ���� �� ��ǥ�� ���η������� �߰��Ͽ� ���� �׷��� ��. 5���� ���������� �̷���� �ٰ����� �ϼ��ϱ� ����, ������ ��ǥ�� ù ��ǥ�� ���ư��� �ؼ� �׷����� �ݾ��־�� �Ѵ�.

    
    */
    [Range(0, 10)] public float attack = 8;
    [Range(0, 10)] public float health = 4;
    [Range(0, 10)] public float defence = 6;
    [Range(0, 10)] public float speed = 10;
    [Range(0, 10)] public float mana = 2;

    public RawImage chartImage;  // UI Raw Image ������Ʈ
    private Texture2D chartTexture;
    public int textureSize = 512;  // �ؽ�ó �ػ�
    public Color lineColor = Color.blue;
    public float lineWidth = 2f;

    void Update()
    {
        if (chartTexture == null)
        {
            chartTexture = new Texture2D(textureSize, textureSize);
            chartImage.texture = chartTexture;
        }

        // �ؽ�ó �ʱ�ȭ
        Color[] pixels = new Color[textureSize * textureSize];
        for (int i = 0; i < pixels.Length; i++)
            pixels[i] = Color.clear;
        chartTexture.SetPixels(pixels);

        // �� ���
        Vector2[] points = new Vector2[5];
        points[0] = GetPoint(attack, 0);
        points[1] = GetPoint(health, 72);
        points[2] = GetPoint(defence, 144);
        points[3] = GetPoint(speed, 216);
        points[4] = GetPoint(mana, 288);

        // �ؽ�ó�� �� �׸���
        for (int i = 0; i < points.Length; i++)
        {
            Vector2 start = ConvertToTextureSpace(points[i]);
            Vector2 end = ConvertToTextureSpace(points[(i + 1) % points.Length]);
            DrawLine(start, end);
        }

        chartTexture.Apply();
    }

    private Vector2 GetPoint(float value, float angleDegree)
    {
        float angleRadian = angleDegree * Mathf.Deg2Rad;
        float normalizedValue = value / 10.0f;
        return new Vector2(Mathf.Cos(angleRadian) * normalizedValue, 
                         Mathf.Sin(angleRadian) * normalizedValue);
    }

    private Vector2 ConvertToTextureSpace(Vector2 point)
    {
        // -1~1 ������ �ؽ�ó ��ǥ�� ��ȯ
        return new Vector2(
            (point.x + 1) * textureSize / 2,
            (point.y + 1) * textureSize / 2
        );
    }

    private void DrawLine(Vector2 start, Vector2 end)
    {
        int x0 = (int)start.x;
        int y0 = (int)start.y;
        int x1 = (int)end.x;
        int y1 = (int)end.y;

        int dx = Mathf.Abs(x1 - x0);
        int dy = Mathf.Abs(y1 - y0);
        int sx = x0 < x1 ? 1 : -1;
        int sy = y0 < y1 ? 1 : -1;
        int err = dx - dy;

        while (true)
        {
            // �� �β��� ���� �ֺ� �ȼ��� ĥ��
            for (int i = -Mathf.FloorToInt(lineWidth/2); i <= lineWidth/2; i++)
            {
                for (int j = -Mathf.FloorToInt(lineWidth/2); j <= lineWidth/2; j++)
                {
                    int px = x0 + i;
                    int py = y0 + j;
                    if (px >= 0 && px < textureSize && py >= 0 && py < textureSize)
                    {
                        chartTexture.SetPixel(px, py, lineColor);
                    }
                }
            }

            if (x0 == x1 && y0 == y1) break;
            int e2 = 2 * err;
            if (e2 > -dy)
            {
                err -= dy;
                x0 += sx;
            }
            if (e2 < dx)
            {
                err += dx;
                y0 += sy;
            }
        }
    }
}
