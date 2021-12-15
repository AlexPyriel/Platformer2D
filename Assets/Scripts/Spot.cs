using UnityEngine;

public class Spot : MonoBehaviour
{
    private void Awake()
    {
        if (transform.parent == null)
        {
            Debug.LogError($"Parent for {transform.name} does NOT exist");
        }
        else if (transform.parent.TryGetComponent<Spawner>(out Spawner spawner) == false)
        {
            Debug.LogError($"Parent for {transform.name} is NOT valid");
        }
    }
}
