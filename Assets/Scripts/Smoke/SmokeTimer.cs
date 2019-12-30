using System.Collections;
using UnityEngine;

public class SmokeTimer : MonoBehaviour
{
    public int odds = 10;

    private ParticleSystem smoke;
    private bool standBy;

    void Start() {
        smoke = GetComponentInChildren<ParticleSystem>();
        smoke.Stop();
    }

    void Update() {
        if (!standBy) {
            StartCoroutine(ToggleSmoke());
        }
    }

    IEnumerator ToggleSmoke() {
        standBy = true;
        yield return new WaitForSeconds(10);
        standBy = false;
        if (smoke.isStopped) {
            if (Random.Range(0, odds) == 0) {
                smoke.Play();
            }
        } else if (smoke.isPlaying) {
            if (Random.Range(0, 2) == 0) {
                smoke.Stop();
            }
        }
    }
}