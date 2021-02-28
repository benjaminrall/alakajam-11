using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrokenCrate : MonoBehaviour
{

    public float destroyDelay;

    void Start()
    {
        StartCoroutine(DestroyPieces());
    }

    IEnumerator DestroyPieces()
    {
        yield return new WaitForSeconds(destroyDelay);
        foreach(Transform child in transform)
        {
            child.GetComponent<Animator>().SetBool("active", true);
        }
        yield return new WaitForSeconds(2.0f);
        Destroy(gameObject);
    }
}
