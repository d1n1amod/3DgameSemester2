using UnityEngine;

public class BGScroller : MonoBehaviour
{
    [Range(-1f, 1f)]

    public float scrollSpeed = 0.5f;
    private float offSet;
    private Material mat;

    private void Start()
    {
       mat = GetComponent<Renderer>().material;
    }

    private void Update()
    {
        offSet += (Time.deltaTime * scrollSpeed) / 10f;
        mat.SetTextureOffset("_MainTex", new Vector2(offSet, 0));
    }

}
