using UnityEngine;

public class BackgroundScroll : MonoBehaviour
{
    // 목표 : 배경 스크롤이 되도록 하고 싶다.

    // 필요 속성
    public Material BackgroundMaterial;
    public float ScrollSpeed = 0.1f;

    private void Update()
    {
        Vector2 direction = Vector2.up;
        BackgroundMaterial.mainTextureOffset += direction * ScrollSpeed * Time.deltaTime;
    }
}
