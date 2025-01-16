using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class gamesession : MonoBehaviour
{
   [SerializeField] int playerlives = 3;
   [SerializeField] TextMeshProUGUI lives;
   [SerializeField] TextMeshProUGUI scoretext;
   [SerializeField] int score = 0;
    private void Awake()
    {
        int NumGameSessions = FindObjectsOfType<gamesession>().Length;
        if(NumGameSessions>1)
        {
            Destroy(gameObject);
        }
        else
        {
              DontDestroyOnLoad(gameObject);
        }
    }
   void Start()
    {
       lives.text = playerlives.ToString();
       scoretext.text = score.ToString();
    }
    public void addtoscore(int pointstoadd)
    {
        score += pointstoadd;
        scoretext.text = score.ToString();
    }
    public void playerdeathprocess()
    {
        if(playerlives>1)
        {
            takelife();
        }
        else
        {
          resetgamesession();
        }
    }
    void takelife()
    {
         playerlives--;
         int currentsceneindex = SceneManager.GetActiveScene().buildIndex;
         SceneManager.LoadScene(currentsceneindex);
         lives.text = playerlives.ToString();
    }
    void resetgamesession()
    {
        FindObjectOfType<scenepersist>().resetscenepersist();
        SceneManager.LoadScene(0);
        Destroy(gameObject);
    }
   
}
