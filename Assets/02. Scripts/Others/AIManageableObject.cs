using UnityEngine;

public class AIManageableObject : MonoBehaviour
{
    public bool IsFriendly = false;

    private void Start()
    {
        AIManager.Instance.AddObject(gameObject);
    }

    // Update is called once per frame
    private void OnDestroy()
    {
        AIManager.Instance.RemoveObject(gameObject);
    }
}
