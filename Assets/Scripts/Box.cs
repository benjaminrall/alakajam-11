using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{

    public GameObject destroyedBox;

    public InteractableObject[] affectedObjects;

    private void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case "BoxHole": EnterHole(other.transform); break;
            case "Spikes": DestroyBox(); break;
            case "LogTrap": DestroyBox(); break;
        }
    }

    private void EnterHole(Transform t)
    {
        t.parent.gameObject.SetActive(false);

        FindObjectOfType<AudioManager>().Play("Button");
        foreach (InteractableObject obj in affectedObjects)
        {
            obj.ActivateToggle();
        }
    }

    private void DestroyBox()
    {
        if (destroyedBox)
        {
            Instantiate(destroyedBox, transform.position, transform.rotation);
        }
        gameObject.SetActive(false);
        Destroy(gameObject);
    }

}
