using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
    public float daySpeed = 0.01f;
    public float seasonSpeed = 0.0005f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update() {
        // Transform tr = GetComponent<Transform>();
        float time = Time.deltaTime;
        transform.Rotate(daySpeed + time, seasonSpeed + time, 0);

    }
}
