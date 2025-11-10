using UnityEngine;

public class Test : MonoBehaviour
{
    public int centerpos = 4;

    void Update()
    {
        if (centerpos == 4)
        {
            transform.position = new Vector3(centerpos, 0, 0);
        }
    }

    void Fly()
    {
        transform.position += new Vector3(centerpos + 8, 0, 0);
    }

    void Cook()
    {
        Debug.Log(centerpos + "cooking");
    }
}

