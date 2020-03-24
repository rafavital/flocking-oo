using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TMP_Text numberOfAgentsText;
    private FlockManager flockManager;

    private void Start() {
        flockManager = FlockManager.Instance;
        numberOfAgentsText.text = "agentes: " + flockManager.boidNum;
    }
}
