namespace HSA.FingerGymnastics.UI
{
    using UnityEngine;

    public class ImageHeightToScreen : MonoBehaviour
    {
        void Awake()
        {
            GUITexture texture = GetComponent<GUITexture>();
            float width_height_ratio = (float)texture.texture.width / (float)texture.texture.height;

            float width = width_height_ratio * Screen.height;
            float x_offset = (Screen.width - width) / 2.0f;
            texture.pixelInset = new Rect(x_offset, 0.0f, width, Screen.height);
        }
    }
}