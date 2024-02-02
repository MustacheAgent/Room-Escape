using System;
using DG.Tweening;
using UnityEngine;

public class Dissolve : MonoBehaviour
{
    [SerializeField] private float duration = 1f;
    
    private Material[] _mat = Array.Empty<Material>();
    private static readonly int AlphaClip = Shader.PropertyToID("_AlphaClip");

    public void SetVisible(bool hide)
    {
        if (_mat.Length == 0) _mat = GetComponent<Renderer>().materials;

        foreach (var material in _mat)
        {
            material.DOFloat(hide ? 1.1f : 0, AlphaClip, duration).SetEase(Ease.Linear);
        }
    }
}
