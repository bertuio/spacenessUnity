using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeTweaker : MonoBehaviour
{
    private static float _rememberedTimescale;
    public static void Puase()
    {
        _rememberedTimescale = Time.timeScale;
        Time.timeScale = 0;
    }

    public static void Unpause()
    {
        Time.timeScale = _rememberedTimescale;
    }
}
