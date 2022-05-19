using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Hacking : MonoBehaviour
{
    [SerializeField] private GameObject msgPanelDoor;
    [SerializeField] private Transform posHackDoor; //nova posição quando ta hackeando

    private bool hack; //mostra quando o player está hackeando 
    private bool collided;
    private float timeCount;

    public bool Hack { get => hack; }

    // Start is called before the first frame update
    void Start()
    {
        LoadPosition();
    }

    // Update is called once per frame
    void Update()
    {
        if (collided && Input.GetKeyDown(KeyCode.E))
        {
            Invader();
        }

        if (hack) //chama a sena do minigame
        {
            timeCount += Time.deltaTime;

            if (timeCount > 1f)
            {
                SceneManager.LoadScene("MiniGame");
            }
        }
    }

    private void Invader()
    {
        //indica se o jogador já foi para o minigame ou não
        PlayerPrefs.SetString("minigame", "true");
        PlayerPrefs.Save();

        //guardar a posição do player
        PlayerPrefs.SetFloat("PosX", transform.position.x);
        PlayerPrefs.SetFloat("PosY", transform.position.y);
        PlayerPrefs.Save();

        //chama a sena do hack | o contador no update chama a sena
        hack = true;
        msgPanelDoor.SetActive(true);
        transform.position = posHackDoor.position;
    }

    //restaura a posição do player quando ele voltar do minigame
    private void LoadPosition()
    {
        if (PlayerPrefs.GetString("minigame") == "true")
        {
            float posX = PlayerPrefs.GetFloat("PosX");
            float posY = PlayerPrefs.GetFloat("PosY");

            transform.position = new Vector2(posX, posY);

            PlayerPrefs.SetString("minigame", "false");
            PlayerPrefs.Save();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("DoorSistem"))
        {
            collided = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("DoorSistem"))
        {
            collided = false;
        }
    }
}
