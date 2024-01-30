using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class GameManager : MonoBehaviour
{
    public static int lives = 3;
    private int pigsToKill;
    private int leftPig = 0;
    public TextMeshPro msg; // Reference to the Level Passed Text
    public TextMeshPro livesTxt; 
    [SerializeField] string victorySceneName; // Name of the victory scene
    [SerializeField] string scenceLose; // Name of the lose scene
    [SerializeField] float delayFromScence = 0.5f; // Delay before going to the victory scene


    // Start is called before the first frame update
    void Start()
    {
    
        GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("Pig");
        pigsToKill = gameObjects.Length;
    
        if (msg == null )
        {
            Debug.LogError("Level Passed Text is null");
        }
        if (livesTxt == null)
        {
            Debug.LogError("Lives Text is null");
        }

        UpdateLivesText(); // Update lives text initially
    }

    public void KillPig()
    {
        leftPig++;
       
        if (leftPig == pigsToKill)
        {
            StartCoroutine(DelayedGoToVictory()); // Call the DelayedGoToVictory function when all pigs are killed
        }
    }

    IEnumerator DelayedGoToVictory()
    {
        yield return new WaitForSeconds(delayFromScence);
        SceneManager.LoadScene(victorySceneName);
    }

    public void KillBird()
    {
        if (leftPig < pigsToKill)
        {
            lives--;

            if (lives <= 0)
            {
                UpdateLivesText();
                msg.text = $"Game Over";
                StartCoroutine(DelayedGoToLose());
            }
            else
            {
                UpdateLivesText(); // Update Lives Text
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
    }

    IEnumerator DelayedGoToLose()
    {
        yield return new WaitForSeconds(delayFromScence);
        SceneManager.LoadScene(scenceLose);
    }

    void UpdateLivesText()
    {
        // Update Lives Text
        if (livesTxt != null)
        {
            livesTxt.text = $"Lives: {lives}";
        }
    }
}
