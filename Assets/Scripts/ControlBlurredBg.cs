using System;
using UnityEngine;
using UnityEngine.UI;
[ExecuteAlways]
public class ControlBlurredBg : MonoBehaviour
{
    [SerializeField] private float blur;
    [SerializeField] private Color dark;

    private void Update()
    {
        GetComponent<Image>().material.SetFloat("_Size", blur);
        GetComponent<Image>().material.SetColor("_Color", dark);

    }
}
