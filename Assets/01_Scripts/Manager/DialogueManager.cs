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

    private Queue<string> sentences; //큐

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
        sentences = new Queue<string>(); //sentences 초기화
    }

    private void Ondialogue(string[] lines)
    {
        sentences.Clear();
        foreach (string line in lines)
        {
            sentences.Enqueue(line);
        }
        dialoguegroup.alpha = 1;
        dialoguegroup.blocksRaycasts = true; //blockRaycasts = true일떄는 마우스이벤트 감지

        NextSentence();
    }

    private void NextSentence()
    {
        if (sentences.Count != 0)
        {
            currentSentence = sentences.Dequeue(); //Dequeue:큐에 존재하는 데이터 중 가장 먼저 들어온 데이터 반환 및 해당 데이터 제거
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
        //TocharArray:문자열을 char형 배열로 변환
        foreach (char letter in line.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    private void Update()
    {
        // dialoueText == currentSentence 대사 한줄 끝
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