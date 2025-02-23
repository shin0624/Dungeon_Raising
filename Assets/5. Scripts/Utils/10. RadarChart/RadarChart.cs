using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteAlways]
public class RadarChart : MonoBehaviour
{
    /* CharacterSelectScene에서 사용될 캐릭터 별 능력치 라이다 차트 컴포넌트. 에디터를 실행시키지 않아도 스크립트의 콜백함수가 실행되도록 ExecuteAlways속성을 붙여준다.
    1. 각도 계산 :  오각형 모양의 라이다 차트에서, 오각형 각 꼭짓점은 원의 중점에서 동일한 거리에 위치
    원의 중심각은 360도 -> 오각형이므로 360/5 = 72도 -> 첫 꼭짓점에서 시작하여 72도씩 회전하며 다음 꼭짓점을 계산한다.

    2. 좌표 계산 : 원의 중심각과 반지름을 이용해 2D 좌표를 계산
    원점과 각도로 계산된 극좌표(r, theta)를 유니티에서의 공간 위치 표현에 사용하는 직교좌표계(x,y,z)로 변환하여 실제 차트 형태로 그려야 함.
    극->직교 변환 : x = rcos(theta) / y = rsin(theta)

    3. 반지름 정규화 : 인스펙터에서 입력받은 값을 0~1범위로 변환하여 그래프의 균일한 크기 조정, 값 비율 유지 등을 위해 정규화(4/10 -> 0.4)

    4. 라인 렌더러로 선 연결 : 계산된 각 좌표를 라인렌더러에 추가하여 선을 그려야 함. 5개의 꼭짓점으로 이루어진 다각형을 완성하기 위해, 마지막 좌표는 첫 좌표로 돌아가게 해서 그래프를 닫아주어야 한다.

    
    */
    [Range(0, 10)] public float attack = 8;
    [Range(0, 10)] public float health = 4;
    [Range(0, 10)] public float defence = 6;
    [Range(0, 10)] public float speed = 10;
    [Range(0, 10)] public float mana = 2;

    public RawImage chartImage;  // UI Raw Image 컴포넌트
    private Texture2D chartTexture;
    public int textureSize = 512;  // 텍스처 해상도
    public Color lineColor = Color.blue;
    public float lineWidth = 2f;

    void Update()
    {
        if (chartTexture == null)
        {
            chartTexture = new Texture2D(textureSize, textureSize);
            chartImage.texture = chartTexture;
        }

        // 텍스처 초기화
        Color[] pixels = new Color[textureSize * textureSize];
        for (int i = 0; i < pixels.Length; i++)
            pixels[i] = Color.clear;
        chartTexture.SetPixels(pixels);

        // 점 계산
        Vector2[] points = new Vector2[5];
        points[0] = GetPoint(attack, 0);
        points[1] = GetPoint(health, 72);
        points[2] = GetPoint(defence, 144);
        points[3] = GetPoint(speed, 216);
        points[4] = GetPoint(mana, 288);

        // 텍스처에 선 그리기
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
        // -1~1 범위를 텍스처 좌표로 변환
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
            // 선 두께를 위해 주변 픽셀도 칠함
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
