using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleableButton : FloorButton
{

    public IEnumerator Deactivate()
    {
        yield return new WaitForSeconds(1.0f);

        transform.position = startPos;

        activated = false;
    }
}
