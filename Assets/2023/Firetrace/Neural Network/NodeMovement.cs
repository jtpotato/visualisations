using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class NodeMovement : MonoBehaviour
{
    // Start is called before the first frame update
    private float xOffset;
    private float yOffset;
    private float zOffset;
    private Vector3 originalPosition;
    void Start()
    {
        // Get position, create unique seed from this position
        Vector3 position = transform.position;
        originalPosition = position;

        // if zero, replace with one
        float offsetElX = position.x == 0 ? 1 : position.x;
        float offsetElY = position.y == 0 ? 1 : position.y;
        float offsetElZ = position.z == 0 ? 1 : position.z;

        xOffset = (float) (100f * offsetElX * offsetElY * offsetElZ * Random.Range(0.5f, 1.5f) % Math.PI);
        yOffset = xOffset + Random.Range(0.5f, 1.5f);
        zOffset = xOffset + Random.Range(0.5f, 1.5f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(originalPosition.x, originalPosition.y, originalPosition.z + (Mathf.Sin(xOffset * Time.time) + Mathf.Sin(yOffset * Time.time) + Mathf.Sin(zOffset * Time.time))/3);
    }
}
