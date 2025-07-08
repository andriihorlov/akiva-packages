using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class UnityUIExtensions 
{
    public static void SetActive(this CanvasGroup group, bool state, float alphaOnDeactivate = 0.5f)
    {
        group.alpha = state ? 1f : alphaOnDeactivate;
        group.interactable = state;
    }
}
