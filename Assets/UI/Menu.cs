using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class Menu : MonoBehaviour
{

    public AudioMixer audioMixer;

    public void SetVolumn(float volume){
        audioMixer.SetFloat("MasterVolume",volume);
        Debug.Log(volume);

    }

    public void PlayGame(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
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
        
    }
}
