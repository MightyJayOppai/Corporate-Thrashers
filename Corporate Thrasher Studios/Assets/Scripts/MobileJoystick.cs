using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MobileJoystick : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    #region Public Variables
    public Image mjBgImage;
    public Image mjJoyImage;
    public Image cjBgImage;
    public Image cjJoyImage;
    #endregion

    #region Private Variables
    [SerializeField]
    private Vector3 inputVec;
    #endregion

    #region Callbacks
    void Start()
    {
        mjBgImage = GetComponent<Image>();
        mjJoyImage = transform.GetChild(0).GetComponent<Image>();
        cjBgImage = GetComponent<Image>();
        cjJoyImage = transform.GetChild(0).GetComponent<Image>();
    }
    #endregion

    #region Pointer Events
    public virtual void OnDrag(PointerEventData data)
    {
        Vector2 pos;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(mjBgImage.rectTransform, data.position, data.pressEventCamera, out pos))
        {
            pos.x = (pos.x / mjBgImage.rectTransform.sizeDelta.x);
            pos.y = (pos.y / mjBgImage.rectTransform.sizeDelta.y);

            inputVec = new Vector3(pos.x * 2f, 0f, pos.y * 2f);
            inputVec = (inputVec.magnitude > 1f) ? inputVec.normalized : inputVec;

            //Movement of Joystick Img;
            mjJoyImage.rectTransform.anchoredPosition = new Vector3(inputVec.x * (mjBgImage.rectTransform.sizeDelta.x / 3), inputVec.z * (mjBgImage.rectTransform.sizeDelta.y / 3));
        }

        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(cjBgImage.rectTransform, data.position, data.pressEventCamera, out pos))
        {
            pos.x = (pos.x / cjBgImage.rectTransform.sizeDelta.x);
            pos.y = (pos.y / cjBgImage.rectTransform.sizeDelta.y);

            inputVec = new Vector3(pos.x * 2f, 0f, pos.y * 2f);
            inputVec = (inputVec.magnitude > 1f) ? inputVec.normalized : inputVec;

            //Movement of Joystick Img;
            cjJoyImage.rectTransform.anchoredPosition = new Vector3(inputVec.x * (cjBgImage.rectTransform.sizeDelta.x / 3), inputVec.z * (cjBgImage.rectTransform.sizeDelta.y / 3));
        }
    }
    public virtual void OnPointerDown(PointerEventData data)
    {
        OnDrag(data);
    }

    public virtual void OnPointerUp(PointerEventData data)
    {
        inputVec = Vector3.zero;
        mjJoyImage.rectTransform.anchoredPosition = Vector3.zero;
        cjJoyImage.rectTransform.anchoredPosition = Vector3.zero;
    }
    #endregion

    #region My Functions
    public float Horizontal()
    {
        if (inputVec.x != 0)
            return inputVec.x;
        else
            return Input.GetAxisRaw("Horizontal");
    }

    public float Vertical()
    {
        if (inputVec.x != 0)
            return inputVec.z;
        else
            return Input.GetAxisRaw("Vertical");
    }
    #endregion
}
