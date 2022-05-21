using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Hacking : MonoBehaviour
{
    [SerializeField] private Color openDoorColor;

    private GameObject systemDoor;
    private Transform door;

    private bool hack; //mostra quando o player está hackeando 
    private bool collided;
    private float timeCount; //tempo para chamar a sena

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
        UpdateDoor(); //guarda as portas que já foram abertas e as matem abertas
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
            systemDoor.transform.GetChild(0).transform.GetChild(0).gameObject.SetActive(true);

            //guardar a posição do player
            PlayerPrefs.SetFloat("PosX", transform.position.x);
            PlayerPrefs.SetFloat("PosY", transform.position.y);
            PlayerPrefs.Save();

            //salva o nome da porta para poder usar quando voltar do hack
            door = systemDoor.transform.GetChild(2);
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
        if (PlayerPrefs.GetString("currentdoor") != "")
        {
            door = GameObject.Find($"{PlayerPrefs.GetString("currentdoor")}").gameObject.transform;
            door.GetComponent<SpriteRenderer>().color = openDoorColor;
            door.GetComponent<Collider2D>().enabled = false;

            //guarda o nome da porta na lista de portas
            string arrayTemp = PlayerPrefs.GetString("listDoor");
            arrayTemp = arrayTemp + door.name + ",";
            PlayerPrefs.SetString("listDoor", arrayTemp);
            PlayerPrefs.Save();

            //restaurar o valor do hack
            PlayerPrefs.DeleteKey("hack");
            PlayerPrefs.DeleteKey("currentdoor");
            PlayerPrefs.Save();
        }
    }

    private void UpdateDoor()
    {
        if (PlayerPrefs.GetString("listDoor") != null && PlayerPrefs.GetString("listDoor") != "")
        {
            string[] arrayTemp = PlayerPrefs.GetString("listDoor").Split(',');

            foreach (string item in arrayTemp)
            {
                if (GameObject.Find(item) != null)
                {
                    GameObject.Find(item).GetComponent<SpriteRenderer>().color = openDoorColor;
                    GameObject.Find(item).GetComponent<Collider2D>().enabled = false;
                }   
            }
        }
    }

    //colisões
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
