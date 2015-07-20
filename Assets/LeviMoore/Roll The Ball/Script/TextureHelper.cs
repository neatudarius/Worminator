using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class TextureHelper : MonoBehaviour
{
    public float scale = 1;
    public Texture2D texture;

    void Awake()
    {
        GetComponent<Renderer>().sharedMaterial = new Material(Shader.Find("Diffuse"));
        GetComponent<Renderer>().sharedMaterial.mainTexture = texture;
    }

	void Update ()
    {
        GetComponent<Renderer>().sharedMaterial.SetTextureScale("_MainTex", new Vector2(transform.localScale.x * scale,
                                                                        transform.localScale.z * scale));
	}
}
