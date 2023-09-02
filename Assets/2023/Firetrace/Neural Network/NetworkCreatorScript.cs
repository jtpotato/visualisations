using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;

public class NetworkCreatorScript : MonoBehaviour
{
    [SerializeField] private GameObject nodePrefab;
    [SerializeField] private string layerListString;
    [SerializeField] private float nodeSpacing;
    [SerializeField] private float layerSpacing;
    [Header("Bugs")]
    [SerializeField] public bool rotationBug;
    public void CreateNetwork()
    {
        // remove spaces from layerListJSON
        layerListString = layerListString.Replace(" ", "");
        string[] layerList = layerListString.Split(',');

        // convert to int[]
        int[] layerListInt = new int[layerList.Length];
        for (int i = 0; i < layerList.Length; i++)
        {
            layerListInt[i] = int.Parse(layerList[i]);
        }

        // find largest layer
        int largestLayer = layerListInt.Max();

        // create nodes for each layer
        int layerIndex = 0;
        foreach (int layerSize in layerListInt)
        {
            // calculate width and height for each layer
            (int width, int height) = MathHelper.FindClosestSquareRootPair(layerSize);

            int nodeIndex = 0;
            // create nodes for each layer
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++, nodeIndex++)
                {
                    // calculate node position
                    Vector3 position = new(j * nodeSpacing - (width * nodeSpacing / 2), i * nodeSpacing - (height * nodeSpacing / 2), layerIndex * layerSpacing - (layerListInt.Length * layerSpacing / 2));
                    CreateNode(position, nodeIndex, layerIndex);
                }
            }

            layerIndex++;
        }
    }

    void CreateNode(Vector3 position, int nodeID, int layerIndex)
    {
        // see if we can find the layer parent
        Transform layerParent = transform.Find($"{layerIndex}");
        if (layerParent == null)
        {
            // create layer parent
            layerParent = new GameObject($"{layerIndex}").transform;
            layerParent.parent = transform;
        }

        // create node
        GameObject node = Instantiate(nodePrefab, new Vector3(0, 0, 0), Quaternion.identity);
        // set node position
        node.transform.position = position;
        // set node name
        node.name = $"{nodeID}";
        // set node parent
        node.transform.parent = layerParent;
    }

    public void DestroyNetwork()
    {
        // destroy all nodes
        while (transform.childCount > 0)
        {
            DestroyImmediate(transform.GetChild(0).gameObject);
        }
    }

    public void Start()
    {
        CreateNetwork();
        BroadcastMessage("CalculateConnectionRecipients");
    }
}
