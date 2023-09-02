using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeLightPulse : MonoBehaviour
{
    [SerializeField] private Material pulseMaterial;
    private Material defaultMaterial;

    void Start()
    {
        defaultMaterial = GetComponent<Renderer>().material;

        StartCoroutine(Pulse());
    }

    IEnumerator Pulse()
    {
        while (true)
        {
            GetComponent<Renderer>().material = defaultMaterial;
            // wait for a random amount of time
            float waitTime = Random.Range(0.1f, 10f);
            yield return new WaitForSeconds(waitTime);

            GetComponent<Renderer>().material = pulseMaterial;
            yield return new WaitForSeconds(0.2f);
        }
    }
}
