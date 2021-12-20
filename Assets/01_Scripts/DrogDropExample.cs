using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class DrogDropExample : MonoBehaviour,IDragHandler,IBeginDragHandler,IEndDragHandler
{
    public Image data;
    public DragAndDropContainer dragAndDropContainer;
    public RectTransform rectTransform;
    [SerializeField]
    private PoolObjectType ingredienttype;

    bool isDragging = false;

    Rigidbody2D rigid;

    private void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
    }
    // 드래그 오브젝트에서 발생
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (data.sprite == null)
        {
            return;
        }

        // Activate Container
        dragAndDropContainer.gameObject.SetActive(true);
        // Set Data 
        dragAndDropContainer.image.sprite = data.sprite;
        isDragging = true;
    }
    // 드래그 오브젝트에서 발생
    public void OnDrag(PointerEventData eventData)
    {
        if (isDragging)
        {
            Debug.Log(eventData.position);
            dragAndDropContainer.GetComponent<RectTransform>().anchoredPosition = eventData.position;
        }
    }
    // 드래그 오브젝트에서 발생
    public void OnEndDrag(PointerEventData eventData)
    {
        if (isDragging)
        {
            if (dragAndDropContainer.image.sprite != null)
            {
                // set data from dropped object  
                data.sprite = dragAndDropContainer.image.sprite;
            }
            else
            {
                // Clear Data
                data.sprite = null;
            }
        }

        isDragging = false;
        // Reset Contatiner
        dragAndDropContainer.image.sprite = null;
        dragAndDropContainer.gameObject.SetActive(false);
        Rayhitck(dragAndDropContainer.RaycastChack());
    }


    private void Rayhitck(RaycastHit2D rayHit)
    {
        if (rayHit.collider == null)
        {
            Debug.Log(">");
        }
        else if (rayHit.collider.CompareTag("CuttingBoard"))
        {
            IngredientsSet(rayHit, 5);
        }
        else if (rayHit.collider.CompareTag("Pot 1"))
        {
            IngredientsSet(rayHit, 0);
        }
        else if (rayHit.collider.CompareTag("Pot 2"))
        {
            IngredientsSet(rayHit, 1);
        }
        else if (rayHit.collider.CompareTag("Pot 3"))
        {
            IngredientsSet(rayHit, 2);
        }
        else if (rayHit.collider.CompareTag("Pot 4"))
        {
            IngredientsSet(rayHit, 3);
        }
        else if (rayHit.collider.CompareTag("MicroMove"))
        {
            IngredientsSet(rayHit, 4);
        }
        else if (rayHit.collider.CompareTag("Machine"))
        {
            IngredientsSet(rayHit, 6);
        }
        else
        {
            Debug.Log("?");
        }
    }

    private void IngredientsSet(RaycastHit2D rayHit, int i)
    {
        GameObject ingredient = ObjectPool.Instance.GetObject(ingredienttype);
        RectTransform rayrectTransform
         = rayHit.collider.GetComponent<RectTransform>();
        ingredient.transform.SetParent(rayrectTransform.transform);
        ingredient.transform.localPosition = new Vector3(0, 0, 0);    
        ingredient.transform.localScale = new Vector3(1, 1,1);
        GameManager.Instance.AddIngredients();
        GameManager.Instance.recipe.currentingredientstype[i].Add(ingredienttype);
    }
}
