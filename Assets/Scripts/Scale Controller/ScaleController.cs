using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleController : MonoBehaviour
{
    public Transform RelatveObject;

    // Update is called once per frame
    private void Update()
    {
        transform.localScale = new Vector3((RelatveObject.localScale.x / Mathf.Abs(RelatveObject.localScale.x) * transform.localScale.z), transform.localScale.y, transform.localScale.z);
    }
}
