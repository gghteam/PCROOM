using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Food : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    protected bool isDrag = false;
    public int num { get; private set; }
    public int mode; //일단 만들어놓은거임 0:음식물 없음, 1:음식물 있음
    private int test;
    private int index;
    public bool isFood;
    [SerializeField]
    protected Canvas canvas = null;
    protected Image food = null;
    protected RectTransform rectTransform = null;
    protected Rigidbody2D rigid = null;
    protected Vector3 startPos;

    protected void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        rigid = GetComponent<Rigidbody2D>();
        food = GetComponent<Image>();
    }

    //쓰레기에 위치를 드래그로 변경합니다.
    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }                             
    public void OnBeginDrag(PointerEventData eventData)
    {
        startPos = rectTransform.anchoredPosition;
        food.color = new Color(1, 1, 1, 0.6f);
        isDrag = true;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        isDrag = false;
        food.color = new Color(1, 1, 1, 1);
        Invoke("Limited", 0.1f);

    }


    protected void FixedUpdate()
    {
        HitGarbage();
    }

    //조건에 맞으면 쓰레기를 치웁니다.
    protected void HitGarbage()
    {
        //쓰레기 치우기
        RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, Vector2.right, 13, LayerMask.GetMask("Box"));
        if (rayHit.collider != null)
        {
            if (!isDrag)
            {
                if (rayHit.transform.CompareTag("Trash") && mode == 0)
                {
                    //쓰레기 버리기
                    this.gameObject.SetActive(false);
                    CheckGarbage(GameManager.Instance.uiManager.currentsit);
                }
            }
            if (rayHit.transform.CompareTag("Food_Waste") && mode == 1)
            {
                //음식물 버리기
                GameManager.Instance.garbageControllrer.ChangeSprite(index);
                mode = 0;
            }
        }
    }

    protected void Limited()
    {
        if(rectTransform.anchoredPosition.x < -900 || rectTransform.anchoredPosition.x > -360 ||
            rectTransform.anchoredPosition.y > -230 || rectTransform.anchoredPosition.y < -520)
        {
            rectTransform.anchoredPosition = startPos;
        }
    }

    private void CheckGarbage(int current)
    {
        GameManager.Instance.uiManager.isclean[current]--;
        if(isFood)
        {
            GameManager.Instance.garbageControllrer.CancelFood(current);
            return;
        }
        for (int i = 0; i < 2; i++) {
            GameManager.Instance.garbageControllrer.CancelGarbage(current, num);
        }
    }

    public void ChangeNum(int i)
    {
        num = i;
    }

    public void ChangeMode(bool check)
    {
        if (check)
            mode = 1;
        else
            mode = 0;
    }

    public void Saveindex(int c)
    {
        index = c;
    }
}
