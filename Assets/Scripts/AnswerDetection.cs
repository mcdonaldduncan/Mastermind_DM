using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AnswerDetection : MonoBehaviour
{
    [SerializeField] GameObject[] currentRow;
    [SerializeField] GameObject[] answerKey;

    void Start()
    {
        
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
            }
            else if (colorAnswers.Contains(currentMats[i].color))
            {
                answerValues[i] = 0;
            }
            else
            {
                answerValues[i] = -1;
            }
            Debug.Log(answerValues[i]);
        }
    }
}
