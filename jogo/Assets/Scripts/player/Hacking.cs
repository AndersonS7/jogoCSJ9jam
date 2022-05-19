using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Hacking : MonoBehaviour
{
    //[SerializeField] private GameObject msgPanelDoor;

    private GameObject systemDoor;
    private Transform door;

    private bool hack; //mostra quando o player está hackeando 
    private bool collided;
    private float timeCount; //tempo para chamar a sena
    private float timeOpenDoor; //tempo para abrir a porta

    public bool Hack { get => hack; }

    // Start is called before the first frame update
    void Start()
    {
        //PlayerPrefs.DeleteAll();
        //PlayerPrefs.Save();
        LoadPosition();
    }

    // Update is called once per frame
    void Update()
    {
        //DetectCollider();
        CheckHack();
        Invader();

        if (hack) //chama a sena do minigame
        {
            timeCount += Time.deltaTime;

            if (timeCount > 1f)
            {
                SceneManager.LoadScene("MiniGame");
            }
        }
    }

    //controla para o player ir para o minigame
    private void Invader()
    {
        if (collided && Input.GetKeyDown(KeyCode.E))
        {
            //indica se o jogador já foi para o minigame ou não
            PlayerPrefs.SetString("minigame", "true");
            PlayerPrefs.Save();

            //chama a sena do hack | o contador no update chama a sena
            hack = true;
            //msgPanelDoor.SetActive(true);
            transform.position = systemDoor.transform.GetChild(1).transform.position;
            systemDoor.transform.GetChild(2).transform.GetChild(0).gameObject.SetActive(true);
            //transform.position = posHackDoor.position;

            //guardar a posição do player
            PlayerPrefs.SetFloat("PosX", transform.position.x);
            PlayerPrefs.SetFloat("PosY", transform.position.y);
            PlayerPrefs.Save();

            //salva o nome da porta para poder usar quando voltar do hack
            door = systemDoor.transform.GetChild(0);
            PlayerPrefs.SetString("currentdoor", door.name);
        }
    }

    //restaura a posição do player quando ele voltar do minigame
    private void LoadPosition()
    {
        if (PlayerPrefs.GetString("minigame") == "true")
        {
            float posX = PlayerPrefs.GetFloat("PosX");
            float posY = PlayerPrefs.GetFloat("PosY");

            transform.position = new Vector2(posX, posY);

            //apaga a posição quando carrega uma vez
            PlayerPrefs.DeleteKey("minigame");
            PlayerPrefs.DeleteKey("PosX");
            PlayerPrefs.DeleteKey("PosY");
            PlayerPrefs.Save();
        }
    }

    //verifica qual objeto foi hackeado
    private void CheckHack()
    {
        if (PlayerPrefs.GetString("hack") == "door")
        {
            OpenDoor();
        }
    }

    private void OpenDoor()
    {
        timeOpenDoor += Time.deltaTime;

        door = GameObject.Find($"{PlayerPrefs.GetString("currentdoor")}").gameObject.transform;

        if (timeOpenDoor < 1.5f)
        {
            door.transform.Translate(Vector2.up * 1.8f * Time.deltaTime);
        }
        if (timeOpenDoor > 1.5f)
        {
            //restaurar o valor do hack
            PlayerPrefs.DeleteKey("hack");
            PlayerPrefs.DeleteKey("currentdoor");
            PlayerPrefs.Save();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("DoorSistem"))
        {
            collided = true;
            systemDoor = collision.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("DoorSistem"))
        {
            collided = false;
            systemDoor = null;
        }
    }
}
