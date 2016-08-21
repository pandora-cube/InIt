using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class MouseHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
    public float maxScale = 1.05f;
    bool Hovered = false;

    void Update() {
        float scale = transform.localScale.x;
        float delta = Time.deltaTime;

        if(Hovered)
            scale = scale+delta < maxScale ? scale+delta : maxScale;
        else
            scale = scale-delta > 1f ? scale-delta : 1f;
        transform.localScale = new Vector3(scale, scale, 1f);
    }

    public void OnPointerEnter(PointerEventData eventData) {
        Hovered = true;
    }

    public void OnPointerExit(PointerEventData eventData) {
        Hovered = false;
    }
}
