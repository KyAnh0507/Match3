using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveParticalImage : MonoBehaviour
{
    public GameObject gameObject;

    public void ActivePartical()
    {
        gameObject.SetActive(false);
    }
}
