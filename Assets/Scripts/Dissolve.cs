using DG.Tweening;
using UnityEngine;

public class Dissolve : MonoBehaviour
{
    [SerializeField] private float duration = 1f;
    
    private Material _mat;
    private static readonly int AlphaClip = Shader.PropertyToID("_AlphaClip");

    public void SetVisible(bool hide)
    {
        if (!_mat) _mat = GetComponent<Renderer>().material;
        _mat.DOFloat(hide ? 1.1f : 0, AlphaClip, duration).SetEase(Ease.Linear);
    }
}
