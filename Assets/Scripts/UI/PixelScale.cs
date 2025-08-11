using UnityEngine;

namespace Weavebound.UI {
  [RequireComponent(typeof(Camera))]
  public class PixelScale : MonoBehaviour {
    public int referenceWidth = 320;
    public int referenceHeight = 180;
    public bool integerScale = true;

    Camera cam;
    void Awake(){ cam = GetComponent<Camera>(); cam.orthographic = true; }

    void OnPreCull(){
      float scaleX = (float)Screen.width  / referenceWidth;
      float scaleY = (float)Screen.height / referenceHeight;
      float scale = Mathf.Min(scaleX, scaleY);
      if (integerScale) scale = Mathf.Max(1f, Mathf.Floor(scale));

      float vw = (referenceWidth * scale) / Screen.width;
      float vh = (referenceHeight * scale) / Screen.height;
      float vx = (1f - vw) * 0.5f;
      float vy = (1f - vh) * 0.5f;
      cam.rect = new Rect(vx, vy, vw, vh);
    }
  }
}