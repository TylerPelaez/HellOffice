using DG.Tweening;
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

    public void ChangeToNumber(int newNumber)
    {
        
        
        transform.DOScale(new Vector3(0, 0, 0), 0.2f)
            .OnComplete(() => OnNumberShrinkComplete(newNumber));
    }

    private void OnNumberShrinkComplete(int newNumber)
    {
        number = newNumber;
        textMesh.text = isBackspace ? "CLR" : number.ToString();

        transform.DOScale(new Vector3(1, 1, 1), 0.2f);
    }
    
}
