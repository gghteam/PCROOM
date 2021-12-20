using UnityEngine;
using UnityEngine.UI;

public class DragAndDropContainer : MonoBehaviour
{
    public Image image;

    private Rigidbody2D rigid;

    private void Start()
    {
        gameObject.SetActive(false);
        rigid = GetComponent<Rigidbody2D>();
    }

    public RaycastHit2D RaycastChack()
    {
        RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, Vector2.right, 13);
        return rayHit;
    }
}
