using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DialogueManager : MonoBehaviour, IPointerDownHandler
{
    [SerializeField]
    private Text dialogueText;
    [SerializeField]
    private GameObject nextIcon;
    private CanvasGroup dialoguegroup;

    private Queue<string> sentences; //ť

    private string currentSentence;

    [SerializeField]
    private float typingSpeed = 0.1f;

    private bool isTyping;


    private void Awake()
    {
        dialoguegroup = GetComponent<CanvasGroup>();
    }
    private void Start()
    {
        sentences = new Queue<string>(); //sentences �ʱ�ȭ
    }

    private void Ondialogue(string[] lines)
    {
        sentences.Clear();
        foreach (string line in lines)
        {
            sentences.Enqueue(line);
        }
        dialoguegroup.alpha = 1;
        dialoguegroup.blocksRaycasts = true; //blockRaycasts = true�ϋ��� ���콺�̺�Ʈ ����

        NextSentence();
    }

    private void NextSentence()
    {
        if (sentences.Count != 0)
        {
            currentSentence = sentences.Dequeue(); //Dequeue:ť�� �����ϴ� ������ �� ���� ���� ���� ������ ��ȯ �� �ش� ������ ����
            isTyping = true;
            nextIcon.SetActive(false);
            StartCoroutine(Typing(currentSentence));

        }
        else
        {
            dialoguegroup.alpha = 0;
            dialoguegroup.blocksRaycasts = false;
        }
    }

    private IEnumerator Typing(string line)
    {
        dialogueText.text = "";
        //TocharArray:���ڿ��� char�� �迭�� ��ȯ
        foreach (char letter in line.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    private void Update()
    {
        // dialoueText == currentSentence ��� ���� ��
        if (dialogueText.text.Equals(currentSentence))
        {
            nextIcon.SetActive(true);
            isTyping = false;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!isTyping)
            NextSentence();
    }
}