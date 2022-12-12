using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{

    //scroller
    public RawImage Image;
    public float x;
    public float scrollSpeed;

    public AudioMixer audioMixer;

    public void SetVolumn(float volume){
        audioMixer.SetFloat("MasterVolume",volume);
        Debug.Log(volume);

    }

    public void PlayGame(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }

    public void PlayGameAgain(){
        SceneManager.LoadScene("Forest");
    }
    public void quitToMenu(){
        SceneManager.LoadScene("MainMenu");

    }
    public void QuitGame(){
        Debug.Log("Application Quit");
        Application.Quit();

    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Image.uvRect = new Rect(Image.uvRect.position + new Vector2(x,0) * (Time.deltaTime* 1/scrollSpeed),Image.uvRect.size); 

        
    }
}
