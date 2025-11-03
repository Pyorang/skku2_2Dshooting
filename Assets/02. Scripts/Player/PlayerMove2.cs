using UnityEngine;

public class PlayerMove2 : MonoBehaviour
{
    public int Speed = 1;
    public int MaxSpeed = 10;

    public void SpeedControl()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            Speed++;
            if(Speed >= MaxSpeed)
            {
                Speed = MaxSpeed;
            }
        }

        if(Input.GetKeyDown(KeyCode.E))
        {
            Speed--;
            if(Speed < 1)
            {
                Speed = 1;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        SpeedControl();

        float h = Input.GetAxis("Horizontal");    // 수평 입력에 대한 값을 -1 ~ 0 ~ 1로 가져온다.
        float v = Input.GetAxis("Vertical");      // 수직 입력에 대한 값을 -1 ~ 0 ~ 1로 가져온다.

        Vector2 direction = new Vector2(h, v);
        Debug.Log($"direction : {direction.x} {direction.y}");

        Vector2 position = this.transform.position;     // 현재 위치

        Vector2 newPosition = position + direction * Speed * Time.deltaTime;     // 새로운 위치

        newPosition.y = -1 * Mathf.Abs(newPosition.y);

        Camera cam = Camera.main;

        if (cam != null)
        {
            float halfHeight = cam.orthographicSize;
            float halfWidth = cam.orthographicSize * cam.aspect;
            Vector3 camPos = cam.transform.position;

            float minX = camPos.x - halfWidth;
            float maxX = camPos.x + halfWidth;
            float minY = camPos.y - halfHeight;
            float maxY = camPos.y + halfHeight;

            newPosition.x = Mathf.Clamp(newPosition.x, minX, maxX);
            newPosition.y = Mathf.Clamp(newPosition.y, minY, 0);
        }

        transform.position = newPosition;
    }
}
