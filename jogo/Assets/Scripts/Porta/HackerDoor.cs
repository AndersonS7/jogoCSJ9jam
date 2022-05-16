using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class HackerDoor : MonoBehaviour
{
    [SerializeField] GameObject door;
    [SerializeField] GameObject progressBar;
    [SerializeField] Image barImg;
    [SerializeField] Text error;
    [SerializeField, Tooltip(">= Número para abrir a porta")] int amountNumberHack;

    private float progressBarCount;
    private float timeCount;
    private int number;
    private bool isHackDoor; //ativa quando bate na porta
    private bool isHaking; //ativa quando está hackeando
    //private bool pointPosHacking; // leva o player para esse ponto quando ele está hackeando

    public bool IsHaking { get => isHaking; }

    void Start()
    {
        error.text = "";
        progressBar.SetActive(false);
        barImg.fillAmount = 0;
        timeCount = 0;
        number = 0;
        isHackDoor = false;
        isHaking = false;
    }

    void Update()
    {
        HackingDoor();
        ProgressBar();//onde acontece o hacking
        SuccesHack();
    }

    //abre a porta
    private void SuccesHack()
    {
        if (number >= amountNumberHack)
        {
            timeCount += Time.deltaTime;
            gameObject.GetComponent<Collider2D>().enabled = false;

            if (timeCount <= 2.5f)
            {
                door.transform.Translate(Vector2.up * 1f * Time.deltaTime);
            }
        }
    }

    //esse método inicia o hack
    private void HackingDoor()
    {
        if (isHackDoor && Input.GetKeyDown(KeyCode.E) && !isHaking)
        {
            isHaking = true;
        }
    }

    private void ProgressBar()
    {
        if (isHaking)
        {
            progressBar.SetActive(true);
            progressBarCount += Time.deltaTime;
            barImg.fillAmount = (progressBarCount / 5);

            if (progressBarCount >= 5)
            {
                number = Random.Range(1, 100);
                isHaking = false;
                progressBarCount = 0;
                progressBar.SetActive(false);

                if (number < amountNumberHack)
                {
                    error.text = "ERROR!";
                    StartCoroutine("Error");
                }
            }
        }
    }

    //courotines
    IEnumerator Error()
    {
        yield return new WaitForSeconds(1f);
        error.text = "";
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isHackDoor = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isHackDoor = false;
        }
    }
}
