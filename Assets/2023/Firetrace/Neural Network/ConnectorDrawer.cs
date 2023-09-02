using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ConnectorDrawer : MonoBehaviour
{
    [SerializeField] private Material connectionMaterial;
    Transform[] nextLayerGameObjects;
    GameObject[] cylinders;
    void Start()
    {

    }

    public void CalculateConnectionRecipients()
    {
        int myLayerIndex = int.Parse(transform.parent.gameObject.name);
        int nextLayer = myLayerIndex + 1;

        // Get all nodes in that layer
        Transform nextLayerTransform = transform.parent.parent.Find($"{nextLayer}");
        // if the next layer doesnt exist
        if (nextLayerTransform == null) return;

        nextLayerGameObjects = nextLayerTransform.Cast<Transform>().ToArray();

        // Create a cylinder for each connection
        cylinders = new GameObject[nextLayerGameObjects.Length];
        for (int i = 0; i < nextLayerGameObjects.Length; i++)
        {
            cylinders[i] = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
            cylinders[i].transform.localScale = new Vector3(0.05f, 0.1f, 0.05f);
            cylinders[i].GetComponent<Renderer>().material = connectionMaterial;
        }
    }

    void RedrawCylinder(Transform cylinder, Vector3 p1, Vector3 p2)
    {
        Vector3 delta = p2 - p1;

        // First apply the world space rotation so the forward vector points p1 to p2
        cylinder.rotation = Quaternion.LookRotation(delta);

        // Check if we've enabled the rotation bug
        if (!transform.parent.parent.GetComponent<NetworkCreatorScript>().rotationBug)
        {
            // Then rotate around the forward vector to point the up vector to the delta direction...
            // whatever that means.
            cylinder.Rotate(-90, 0, 0);
        };

        // Set the position to the center between p1 and p2
        cylinder.position = (p1 + p2) / 2f;

        // Set the Y scale to the half of the distance between p1 and p2    
        var scale = cylinder.localScale;
        scale.y = delta.magnitude / 2f;
        cylinder.localScale = scale;
    }

    // Update is called once per frame
    void Update()
    {
        if (nextLayerGameObjects == null) return;

        for (int i = 0; i < nextLayerGameObjects.Length; i++)
        {
            // Get the positions of the two nodes
            Vector3 p1 = transform.position;
            Vector3 p2 = nextLayerGameObjects[i].position;

            // Draw a cylinder between them
            RedrawCylinder(cylinders[i].transform, p1, p2);
        }
    }
}
