using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using Random = System.Random;

public class Phone : MonoBehaviour
{
    private static Random rng = new Random();
    
    [SerializeField]
    private TextMeshPro phoneScreen;
    
    [SerializeField]
    private string desiredPhoneNumber = "8773934448";
    
    [SerializeField]
    private string currentPhoneNumber;

    [SerializeField]
    private float numberChangeDelaySeconds = 0.5f;

    [SerializeField]
    private int nextButtonChangeIndex = 0;
    
    private List<Button> buttons;

    private float lastNumberChangeTime;
    

    // Start is called before the first frame update
    void Start()
    {
        var allButtons = GetComponentsInChildren<Button>();
        buttons = new List<Button>();

        foreach (var button in allButtons)
        {
            if (!button.isBackspace)
            {
                buttons.Add((button));
            }
        }
        
        buttons = buttons.OrderBy(_ => rng.Next()).ToList();

        StartGame();
    }

    // Update is called once per frame
    void Update()
    {
        lastNumberChangeTime += Time.deltaTime;
        
        if (lastNumberChangeTime > numberChangeDelaySeconds)
        {
            lastNumberChangeTime = 0;
            buttons[nextButtonChangeIndex].ChangeToNumber(rng.Next(1, 10)); // Pure Random Strategy
            nextButtonChangeIndex++;


            if (nextButtonChangeIndex == buttons.Count)
            {
                buttons = buttons.OrderBy(_ => rng.Next()).ToList();
                nextButtonChangeIndex = 0;
            }
        }
    }

    public void StartGame()
    {
        phoneScreen.text = "";
        currentPhoneNumber = "";
    }

    public void OnClick(InputAction.CallbackContext ctx)
    {
        Debug.Log("Test");
        if (!ctx.started)
        {
            return;
        }

        
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray.origin, ray.direction, out hit))
        {
            Button button = hit.collider.gameObject.GetComponent<Button>();
            if (button == null)
            {
                return;
            }

            if (button.isBackspace)
            {
                OnBackspacePressed();
            }
            else
            {
                OnNumberPressed(button.number);
            }
        }

    }
    
    public void OnNumberPressed(int number)
    {
        if (currentPhoneNumber.Length >= 10)
        {
            // Todo: Screenshake and proper way to telll player the phonenumber is too long
            Debug.LogError("Phone number too long!");
            return;
        }
        
        currentPhoneNumber += number;
        phoneScreen.text = FormatPhoneNumber(currentPhoneNumber);
    }

    public void OnBackspacePressed()
    {
        if (currentPhoneNumber.Length == 0)
        {
            return;
        }

        currentPhoneNumber = currentPhoneNumber.Substring(0, currentPhoneNumber.Length - 1);
        phoneScreen.text = FormatPhoneNumber(currentPhoneNumber);
    }

    private static string FormatPhoneNumber(string rawPhoneNumber)
    {
        if (rawPhoneNumber.Length <= 3)
        {
            return rawPhoneNumber;
        }
        

        if (rawPhoneNumber.Length <= 7)
        {
            return rawPhoneNumber.Substring(0, 3) + "-" + rawPhoneNumber.Substring(3);
        }
        
        return "(" + rawPhoneNumber.Substring(0, 3) + ")" + rawPhoneNumber.Substring(3, 3) + "-" + rawPhoneNumber.Substring(6);
    }
    
}
