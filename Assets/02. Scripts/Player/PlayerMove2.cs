using UnityEngine;

public class PlayerMove2 : MonoBehaviour
{
    [Header("´É·ÂÄ¡")]
    public int Speed = 1;
    public int MaxSpeed = 10;
    public int MinSpeed = 1;
    public float SpeedMultiplier = 1.2f;

    private bool BoostOn = false;

    // Update is called once per frame
    void Update()
    {
        SpeedControl();

        float h, v;

        if(Input.GetKey(KeyCode.R))
        {
            h = -transform.position.x;
            v = -transform.position.y;
        }

        else
        {
            h = Input.GetAxisRaw("Horizontal");
            v = Input.GetAxisRaw("Vertical");
        }

        Vector2 direction = new Vector2(h, v).normalized;

        Vector2 position = this.transform.position;

        Vector2 newPosition = BoostOn ? position + direction * SpeedMultiplier * Speed * Time.deltaTime : position + direction * Speed * Time.deltaTime;

        Camera cam = Camera.main;

        if (cam != null)
        {
            float halfHeight = cam.orthographicSize;
            float halfWidth = cam.orthographicSize * cam.aspect;
            Vector3 camPos = cam.transform.position;

            float minX = camPos.x - halfWidth - (float)transform.localScale.x / 2;
            float maxX = camPos.x + halfWidth + (float)transform.localScale.x / 2;
            float minY = camPos.y - halfHeight;

            if (newPosition.x < minX)
                newPosition.x = maxX;
            if(newPosition.x > maxX)
                newPosition.x = minX;
            newPosition.y = Mathf.Clamp(newPosition.y, minY, 0);
        }

        transform.position = newPosition;
    }

    public void SpeedControl()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Speed++;
            if (Speed >= MaxSpeed)
            {
                Speed = MaxSpeed;
            }
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            Speed--;
            if (Speed < MinSpeed)
            {
                Speed = MinSpeed;
            }
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            BoostOn = true;
        }

        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            BoostOn = false;
        }
    }
}
