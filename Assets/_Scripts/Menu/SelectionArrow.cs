using System;
using UnityEngine;
using UnityEngine.UI;

public class SelectionArrow : MonoBehaviour
{
    [SerializeField] private RectTransform[] options;
    private RectTransform _rect;
    private int currentPosition;

    private void Awake()
    {
        _rect = GetComponent<RectTransform>();
    }

    private void Update()
    {
        //Change position of the selection bubble
        if(Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            ChangePosition(-1);
        else if(Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
            ChangePosition(+1);
        
        // Interact with options
        if (Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.Return))
            Interact();
    }

    private void ChangePosition(int change)
    {
        currentPosition += change;

        if (currentPosition < 0)
            currentPosition = options.Length - 1;
        else if (currentPosition > options.Length - 1)
            currentPosition = 0;
        
        //Assign the Y position of the current option to the arrow 
        _rect.position = new Vector3(_rect.position.x, options[currentPosition].position.y, 0);
    }

    private void Interact()
    {
        // Access the button component on each option and call its function
        options[currentPosition].GetComponent<Button>().onClick.Invoke();
    }
}
