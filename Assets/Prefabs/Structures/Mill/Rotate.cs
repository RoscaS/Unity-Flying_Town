using UnityEngine;

public class Rotate : MonoBehaviour
{
    public Vector3 rotationSpeed;
    
    void Update() {
        this.transform.Rotate(rotationSpeed * Time.deltaTime);
    }
}
