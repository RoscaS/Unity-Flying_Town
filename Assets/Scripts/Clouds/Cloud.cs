using UnityEngine;
using Random = UnityEngine.Random;

public class Cloud : MonoBehaviour
{
    private float speed;

    private Vector3 direction;
    

    private void OnEnable() {
        speed = (float) Random.Range(10, 30) / 250;
        direction = Vector3.forward;
    }

    private void FixedUpdate() {
        this.transform.Translate(direction * speed, Space.World);
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("CloudsDestructor")) {
            Destroy(this.gameObject);
            CloudsProducer.cloudsCount--;
        }
    }
}
