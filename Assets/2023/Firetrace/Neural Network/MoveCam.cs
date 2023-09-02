using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[ExecuteInEditMode]
public class MoveCam : MonoBehaviour
{
    SceneView sceneCam;
    // Start is called before the first frame update
    void Start()
    {
        sceneCam = SceneView.lastActiveSceneView;
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<Camera>().transform.SetPositionAndRotation(sceneCam.camera.transform.position, sceneCam.camera.transform.rotation);
    }
}
