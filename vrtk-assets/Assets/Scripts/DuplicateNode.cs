// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using TMPro;

// public class DuplicateNode : MonoBehaviour
// {
//     public GameObject node;
//     public TMP_Text textComponent;

//     private Vector3 firstNodeVector;
//     private Vector3 lastNodeVector;
//     private int textCounter = 1;
//     private bool isAdding = true;
//     private List<GameObject> containers = new List<GameObject>();
//     private Vector3 initialPosition; // Variável para armazenar a posição inicial de criação dos containers

//     void Start()
//     {
//         firstNodeVector = new Vector3(node.transform.position.x,
//             node.transform.position.y, node.transform.position.z);
//         lastNodeVector = new Vector3(firstNodeVector.x, firstNodeVector.y, firstNodeVector.z);
//         initialPosition = lastNodeVector; // Armazenar a posição inicial
//     }

//     void Update()
//     {
//         if (Input.GetKeyDown(KeyCode.T))
//         {
//             if (textCounter == 6)
//             {
//                 RemovePreviousContainers();
//                 textCounter = 1; // Reiniciar o contador para 1 após atingir 6
//                 isAdding = true; // Retomar a duplicação normal
//                 lastNodeVector = initialPosition; // Voltar à posição inicial de criação
//             }

//             lastNodeVector.x -= isAdding ? 0.659f : -0.659f;

//             // Criar novo objeto "container"
//             GameObject newContainer = new GameObject("Container" + textCounter);
//             newContainer.transform.position = lastNodeVector;

//             // Criar novo nó com o texto atualizado como filho do objeto "container"
//             GameObject newNode = Instantiate(node, newContainer.transform);
//             newNode.transform.localPosition = Vector3.zero; // Defina a posição do nó localmente para (0, 0, 0)
//             newNode.name = "Node" + textCounter;

//             TMP_Text newTextComponent = newNode.GetComponentInChildren<TMP_Text>();
//             if (newTextComponent != null)
//             {
//                 newTextComponent.text = textCounter.ToString();
//             }

//             // Adicionar o novo objeto "container" na lista
//             containers.Add(newContainer);

//             // Incrementar o contador
//             textCounter++;
//         }
//     }

//     // Remover os objetos anteriores criados, mas manter a lista de containers vazia para reiniciar a duplicação
//     private void RemovePreviousContainers()
//     {
//         foreach (GameObject container in containers)
//         {
//             Destroy(container);
//         }
//         containers.Clear();
//     }
// }

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DuplicateNode : MonoBehaviour
{
    public GameObject node;
    public TMP_Text textComponent;

    private Vector3 firstNodeVector;
    private Vector3 lastNodeVector;
    private int textCounter = 1;
    private List<GameObject> containers = new List<GameObject>();
    private Vector3 initialPosition; // Variável para armazenar a posição inicial de criação dos containers

    void Start()
    {
        firstNodeVector = new Vector3(node.transform.position.x,
            node.transform.position.y, node.transform.position.z);
        lastNodeVector = new Vector3(firstNodeVector.x, firstNodeVector.y, firstNodeVector.z);
        initialPosition = lastNodeVector; // Armazenar a posição inicial
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            if (textCounter == 7)
            {
                if (containers.Count > 0)
                {
                    RemoveAllContainers(); // Remover todos os containers antes de reiniciar
                }
                textCounter = 1; // Reiniciar o contador para 1
                lastNodeVector = initialPosition; // Voltar à posição inicial de criação
            }

            // Criar novo objeto "container"
            GameObject newContainer = new GameObject("Node" + textCounter);
            newContainer.transform.position = lastNodeVector;

            // Criar novo nó com o texto atualizado como filho do objeto "container"
            GameObject newNode = Instantiate(node, newContainer.transform);
            newNode.transform.localPosition = Vector3.zero; // Defina a posição do nó localmente para (0, 0, 0)
            newNode.name = "Node" + textCounter;

            TMP_Text newTextComponent = newNode.GetComponentInChildren<TMP_Text>();
            if (newTextComponent != null)
            {
                newTextComponent.text = textCounter.ToString();
            }

            // Adicionar o novo objeto "container" na lista
            containers.Add(newContainer);

            // Atualizar a posição para a próxima criação
            lastNodeVector.x -= 0.659f;

            // Incrementar o contador
            textCounter++;
        }
    }

    // Remover todos os objetos criados anteriormente e limpar a lista de containers
    private void RemoveAllContainers()
    {
        foreach (GameObject container in containers)
        {
            Destroy(container);
        }
        containers.Clear();
    }
}


