//Jose Velazquez
//GamaManager

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField]private GameObject _apple;
    [SerializeField]private GameObject _canvas;
    [SerializeField]private Text _textApple;
    [SerializeField]private Text _textTail;

    void Start()
    {
        _canvas.SetActive(false);
        SpawnApple();
    }

    public void SpawnApple()
    {
        Vector3 SpawnLocation;
        SpawnLocation = new Vector3(Random.Range(-4.45f, 4.45f), Random.Range(-4.45f, 4.45f), 0);
        Instantiate(_apple, SpawnLocation, Quaternion.identity);
    }

    public void GameOver(int Tail , int Apple)
    {
        _canvas.SetActive(true);
        _textTail.text = Tail.ToString();
        _textApple.text = Apple.ToString();
    }

    public void Reset()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }

}
