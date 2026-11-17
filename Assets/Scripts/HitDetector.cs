using UnityEngine;

public class HitDetector : MonoBehaviour
{
    public GameObject hitObject;
    
    void OnTriggerEnter(Collider other)
    {
        hitObject = other.gameObject;
    }
}
