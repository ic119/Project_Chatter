using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.EventSystems;

public class JoyStickController : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    #region Private variable
    private Vector3 joystick_pos;
    private Vector3 joystick_vec;
    private float joystick_radius;
    private bool isTouch = false;
    private Transform player_Tr;
    #endregion

    [SerializeField] Transform joystick_tr;

    #region LifeCycle
    private void Start()
    {
        player_Tr = GameObject.FindGameObjectWithTag("Player").transform;

        joystick_radius = this.gameObject.GetComponent<RectTransform>().sizeDelta.y * 0.5f;
        joystick_pos = joystick_tr.transform.position;

        float canvas_size = transform.parent.GetComponent<RectTransform>().localPosition.x;
        joystick_radius *= canvas_size;
    }

    private void Update()
    {
        if (isTouch)
        {
            player_Tr.transform.Translate(Vector3.forward * Time.deltaTime * 5.0f);
        }
    }
    #endregion

    #region IEvent
    /// <summary>
    /// 배경 터치 시 해당위치로 조이스틱 핸들 이동처리
    /// </summary>
    /// <param name="eventData"></param>
    public void OnDrag(PointerEventData eventData)
    {
        isTouch = true;
        PointerEventData _eventData = eventData as PointerEventData;
        Vector3 pos = _eventData.position;

        joystick_vec = (pos - joystick_pos).normalized;

        float dis = Vector3.Distance(pos, joystick_pos);
        if ( dis < joystick_radius)
        {
            joystick_tr.position = joystick_pos + joystick_vec * dis;
        }
        else
        {
            joystick_tr.position = joystick_pos + joystick_vec * joystick_radius;
        }
        player_Tr.eulerAngles = new Vector3(0, Mathf.Atan2(joystick_vec.x, joystick_vec.y) * Mathf.Rad2Deg, 0);
    }

    /// <summary>
    /// 터치 중일 때 OnDrag함수에 eventData를 인수로 삼아 처리
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerDown(PointerEventData eventData)
    {
        OnDrag(eventData);
    }

    /// <summary>
    /// 터치 중 손을 떼었을 경우에 조이스틱 핸들 위치 처리
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerUp(PointerEventData eventData)
    {
        joystick_tr.position = joystick_pos;
        joystick_vec = Vector3.zero;
        isTouch = false;
    }
    #endregion
}
