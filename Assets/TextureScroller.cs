using UnityEngine;

public class TextureScroller : MonoBehaviour
{
    public GameObject target;


    private SpriteRenderer sr;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void LateUpdate() {
        //int segments = (int)(target.transform.position.y / sr.bounds.max.y);
        sr.material.mainTextureOffset = Vector2.up * target.transform.position.y;
    }
}
