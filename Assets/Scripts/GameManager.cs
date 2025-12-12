using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static bool iniciarDirecto = false;

    [Header("UI")]
    [SerializeField] private GameObject menuPrincipal;
    [SerializeField] private GameObject menuJuegoTerminado;
    [SerializeField] private GameObject menuGanaste;
    [SerializeField] private TextMeshProUGUI scoreText;


    [Header("Game")]
    [SerializeField] private GameObject player;
    [SerializeField] private Spawner spawner1;
    [SerializeField] private SpawnerEnemy2 spawner2;

    [Header("Puntos")]
    [SerializeField] private int puntosParaGanar = 50;
    private int puntos = 0;

    private void Start()
    {
        if (iniciarDirecto)
        {
            // Venimos de un "Reintentar"
            menuPrincipal.SetActive(false);
            menuJuegoTerminado.SetActive(false);

            player.SetActive(true);
            spawner1.gameObject.SetActive(true);
            spawner2.gameObject.SetActive(true);
            spawner1.IniciarSpawn();
            spawner2.IniciarSpawn();

            // Importantísimo: reseteamos el flag
            iniciarDirecto = false;
        }
        else
        {
            // Inicio normal (cuando abres el juego / escena por primera vez)
            menuPrincipal.SetActive(true);
            menuJuegoTerminado.SetActive(false);

            player.SetActive(false);
            spawner1.gameObject.SetActive(false);
            spawner2.gameObject.SetActive(false);
        }

        if (menuGanaste != null)
        {
            menuGanaste.SetActive(false);
        }
        ActualizarUI();

    }

    public void OnPlayButton()
    {
        //Cerrar Menu
        menuPrincipal.SetActive(false);

        // Activar Player
        player.SetActive(true);
        spawner1.gameObject.SetActive(true);
        spawner2.gameObject.SetActive(true);

        // Arrancar el Coroutine Spawners
        spawner1.IniciarSpawn();
        spawner2.IniciarSpawn();
    }

    public void GameOver()
    {
        menuJuegoTerminado.SetActive(true);
        
        if (spawner1 && spawner2 != null)
        {
            spawner1.StopAllCoroutines();
            spawner2.StopAllCoroutines();
            spawner1.enabled = false;
            spawner2.enabled = false;
        }

        if (player != null)
        {
            player.SetActive(false);
        }
    }

    public void OnRestartButton()
    {
        iniciarDirecto = true;

        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.buildIndex);
    }

    public void SumarPuntos(int cantidad)
    {
        puntos += cantidad;
        ActualizarUI();

        if (puntos >= puntosParaGanar)
        {
            Ganaste();
        }
    }

    private void ActualizarUI()
    {
        if (scoreText != null)
            scoreText.text = $"Puntos: {puntos}/{puntosParaGanar}";
    }

    public void Ganaste()
    {
        if (menuGanaste != null) menuGanaste.SetActive(true);

        // detener juego (simple)
        if (spawner1 != null) spawner1.StopAllCoroutines();
        if (spawner2 != null) spawner2.StopAllCoroutines();
        if (player != null) player.SetActive(false);
    }
}
