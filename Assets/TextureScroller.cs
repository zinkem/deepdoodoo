using UnityEngine;

public class TextureScroller : MonoBehaviour
{
    public GameObject target;
    public float scrollSpeed;


    private SpriteRenderer sr;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void LateUpdate() {
        float positionMod = target.transform.position.y * scrollSpeed;
        float height = sr.size.y;
        int segments = (int)(positionMod / height);

        float remainder = positionMod - (segments * height);
        float test = (remainder * 2 / height) - 1;

        Vector2 offset = new Vector2(0, test);

        sr.material.SetVector("_offset", offset);
    }
}
