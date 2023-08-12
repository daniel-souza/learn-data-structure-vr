using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DuplicateNode : MonoBehaviour
{
    public GameObject node;
    public TMP_InputField userInputField; // Referência ao InputField do Unity onde o usuário digita o texto

    private Vector3 firstNodeVector;
    private Vector3 lastNodeVector;
    private Vector3 lastNodeVector2;
    private int textCounter = 1;
    private int textCounter2 = 1;
    private List<GameObject> nodeList = new List<GameObject>();
    private Vector3 initialPosition; // Variável para armazenar a posição inicial de criação dos containers

    void Start()
    {
        firstNodeVector = new Vector3(node.transform.position.x,
            node.transform.position.y, node.transform.position.z);
        lastNodeVector = new Vector3(firstNodeVector.x, firstNodeVector.y, firstNodeVector.z);
        lastNodeVector2 = new Vector3(firstNodeVector.x, firstNodeVector.y, firstNodeVector.z);
        initialPosition = lastNodeVector; // Armazenar a posição inicial
    }

    void Update()
    {
        // Verificar se o usuário pressionou "T" para criar o nó
        if (Input.GetKeyDown(KeyCode.T))
        {
            CreateNewNode();
        }

        // Verificar se o usuário pressionou "R" para duplicar o nó do lado contrário
        if (Input.GetKeyDown(KeyCode.R))
        {
            DuplicateNodeOpposite();
        }
    }

    private void CreateNewNode()
    {
        if (nodeList.Count == 10)
            ResetNodes();

        if (textCounter >= 7)
            return; // Remover todos os nós antes de reiniciar

        GameObject newNode = Instantiate(node, lastNodeVector, Quaternion.identity);
        newNode.SetActive(true);
        newNode.transform.parent = transform;
        newNode.name = "Node" + textCounter;

        TMP_Text newTextComponent = newNode.GetComponentInChildren<TMP_Text>();
        if (newTextComponent != null)
        {
            if (!string.IsNullOrEmpty(userInputField.text))
            {
                newTextComponent.text = userInputField.text;
                userInputField.text = ""; // Limpar o input após atribuir o texto ao nó
            }
            else
            {
                newTextComponent.text = textCounter.ToString();
            }
        }

        // Adicionar o novo objeto "container" na lista
        nodeList.Add(newNode);

        // Atualizar a posição para a próxima criação
        lastNodeVector.x -= 0.659f;

        // Incrementar o contador
        textCounter++;
    }

    private void DuplicateNodeOpposite()
    {
        if (nodeList.Count == 10)
            ResetNodes();

        if(textCounter2 >= 5)
            return;

        GameObject newNode = Instantiate(node, lastNodeVector2, Quaternion.identity);
        newNode.SetActive(true);
        newNode.transform.parent = transform;
        newNode.name = "Node" + textCounter2;

        TMP_Text newTextComponent = newNode.GetComponentInChildren<TMP_Text>();
        if (newTextComponent != null)
        {
            if (!string.IsNullOrEmpty(userInputField.text))
            {
                newTextComponent.text = userInputField.text;
                userInputField.text = ""; // Limpar o input após atribuir o texto ao nó
            }
            else
            {
                newTextComponent.text = textCounter2.ToString();
            }
        }

        nodeList.Add(newNode);

        lastNodeVector2.x += 0.659f;

        textCounter2++;
    }

    // Remover todos os objetos criados anteriormente e limpar a lista de nós
    private void ResetNodes()
    {
        foreach (GameObject node in nodeList)
        {
            Destroy(node);
        }
        nodeList.Clear();
        textCounter = 1; // Reiniciar o contador para 1
        textCounter2 = 1;
        lastNodeVector = initialPosition; // Voltar à posição inicial de criação
        lastNodeVector2 = initialPosition;
    }
}
