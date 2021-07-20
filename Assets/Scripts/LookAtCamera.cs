using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    public GameObject lookAt;
    void Update()
    {
        this.transform.LookAt(lookAt.transform.position);
    }
}
