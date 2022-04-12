using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static Utility;


public class AnswerDetection : MonoBehaviour
{
    [SerializeField] GameObject[] currentRow;
    [SerializeField] GameObject[] answerKey;
    [SerializeField] GameObject[] pins;
    [SerializeField] GameObject hintGrid;
    [SerializeField] bool randomize;

    GameHandler gameManager;

    List<GameObject> sortedPins = new List<GameObject>();
    
    List<GameObject> randomizedPins = new List<GameObject>();

    void Start()
    {
        gameManager = GetComponent<GameHandler>();
        if (gameManager != null)
        {

        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Evaluate();
        }
    }

    private void Evaluate()
    {
        Material[] answerMats = new Material[answerKey.Length];
        for (int i = 0; i < answerKey.Length; i++)
        {
            Material temp = answerKey[i].GetComponent<MeshRenderer>().material;
            answerMats[i] = temp;
        }

        Material[] currentMats = new Material[currentRow.Length];
        for (int i = 0; i < currentRow.Length; i++)
        {
            Material temp = currentRow[i].GetComponent<MeshRenderer>().material;
            currentMats[i] = temp;
        }
        Report(answerMats, currentMats);
    }

    void Report(Material[] answerMats, Material[] currentMats)
    {
        // Clear lists so that new objects can be instantiated, remove when using multiple lists
        randomizedPins.Clear();
        sortedPins.Clear();

        int[] answerValues = new int[currentMats.Length];
        List<Material> compMats = answerMats.ToList();
        List<Color> colorAnswers = new List<Color>();

        foreach (var item in compMats)
        {
            colorAnswers.Add(item.color);
        }

        for (int i = 0; i < currentMats.Length; i++)
        {
            if (currentMats[i].color == answerMats[i].color)
            {
                answerValues[i] = 1;
                InstantiatePin(0, hintGrid.transform.GetChild(i).transform);
            }
            else if (colorAnswers.Contains(currentMats[i].color))
            {
                answerValues[i] = 0;
                InstantiatePin(1, hintGrid.transform.GetChild(i).transform);
            }
            else
            {
                answerValues[i] = -1;
                InstantiatePin(2, hintGrid.transform.GetChild(i).transform);
            }
            Debug.Log(answerValues[i]);
        }

        if (randomize)
        {
            RandomizePins();
        }
    }

    void InstantiatePin(int index, Transform transform)
    {
        GameObject pin = Instantiate(pins[index]);
        pin.transform.position = transform.position;
        sortedPins.Add(pin);
    }

    void RandomizePins()
    {
        randomizedPins = sortedPins.OrderBy(x => random.Next()).ToList();

        for (int i = 0; i < randomizedPins.Count; i++)
        {
            randomizedPins[i].transform.position = hintGrid.transform.GetChild(i).transform.position;
        }
    }

    void MoveToNextRow()
    {

    }

    void AdvanceIndex()
    {

    }
}
