using DG.Tweening;
using UnityEngine;

public class Dissolve : MonoBehaviour
{
    [SerializeField] private float duration = 2f;
    
    private Material _mat;
    
    private void Awake()
    {
        _mat = GetComponent<Renderer>().material;
    }

    public void Show()
    {
        _mat.DOFloat(0, "_AlphaClip", duration);
    }
    
    public void Hide()
    {
        _mat.DOFloat(1, "_AlphaClip", duration);
    }
}
