using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{

    public GameObject destroyedBox;

    private void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case "BoxHole": Debug.Log("Box in box hole"); break;
            case "Spikes": DestroyBox(); break;
            case "LogTrap": DestroyBox(); break;
        }
    }

    private void DestroyBox()
    {
        Instantiate(destroyedBox, transform.position, transform.rotation);
        Destroy(gameObject);
    }

}
