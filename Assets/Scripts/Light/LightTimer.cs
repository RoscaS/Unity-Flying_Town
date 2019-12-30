using System.Collections;
using UnityEngine;

public class LightTimer : MonoBehaviour
{
    public int odds = 5;

    private Transform sun;
    private Light light;
    private bool standBy;

    void Start() {
        sun = GameObject.FindWithTag("Sun").transform;
        light = GetComponent<Light>();
        light.enabled = false;
    }

    void Update() {
        Vector3 angles = sun.eulerAngles;
        if (angles.x > 180) {
            if (!standBy) {
                StartCoroutine(ToggleLight());
            }
        }
        else if (angles.x < 180) {
            light.enabled = false;
            standBy = false;
        }
    }

    IEnumerator ToggleLight() {
        standBy = true;
        if (!light.enabled) {
            yield return new WaitForEndOfFrame();
            standBy = false;
            if (Random.Range(0, odds) == 0) {
                light.enabled = true;
            }
        }
    }
}