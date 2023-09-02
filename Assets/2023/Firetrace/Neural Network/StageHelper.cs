using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageHelper : MonoBehaviour
{
    [SerializeField] private float stageSize;
    [SerializeField] private Transform stageObject;
    // Start is called before the first frame update
    void Start()
    {
        stageObject.localScale = new Vector3(stageSize, 0.05f, stageSize);
    }
}
