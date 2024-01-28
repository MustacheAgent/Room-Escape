using DG.Tweening;
using UnityEngine;

public class Dissolve : MonoBehaviour
{
    [SerializeField] private float duration = 2f;
    
    private Material _mat;

    public void Switch(bool hide)
    {
        if (!_mat) _mat = GetComponent<Renderer>().material;
        _mat.DOFloat(hide ? 1 : 0, "_AlphaClip", duration);
    }
}
