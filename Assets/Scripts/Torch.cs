using UnityEngine;

public class Torch : InteractableObject
{
    public override void ActivateToggle()
    {
        if (!active)
        {
            FindObjectOfType<AudioManager>().Play("TorchLit");
            active = true;
        }
    }
}
