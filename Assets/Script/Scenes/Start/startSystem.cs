using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class startSystem : MonoBehaviour
{
    [SerializeField] private Button startButton;
    void Start()
    {
        startButton.onClick.AddListener(() =>
        {
            SceneManager.LoadScene("SelectCharacter");
        });
    }
}
