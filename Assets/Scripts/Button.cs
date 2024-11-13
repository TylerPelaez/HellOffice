using TMPro;
using UnityEngine;

public class Button : MonoBehaviour
{
    [SerializeField]
    public bool isBackspace;
    
    [SerializeField]
    public int number;
    

    private TextMeshPro textMesh;
    
    // Start is called before the first frame update
    void Start()
    {
        textMesh = GetComponentInChildren<TextMeshPro>();

        textMesh.text = isBackspace ? "CLR" : number.ToString();
    }
}
