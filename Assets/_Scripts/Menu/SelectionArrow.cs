using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SelectionArrow : MonoBehaviour
{
    [SerializeField] private RectTransform[] options;
    [SerializeField] private AudioSettings audioSettingsScript;
    private RectTransform _rect;
    private int _currentPosition;
    private int _previousPosition;

    private void Awake()
    {
        _rect = GetComponent<RectTransform>();
        audioSettingsScript = GameObject.FindWithTag("Audio").GetComponent<AudioSettings>();
    }

    private void Update()
    {
        //Change position of the selection bubble
        if(Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            ChangePosition(-1);
        else if(Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
            ChangePosition(+1);
        
        //IF bubble is in the position of the music slider or sfx slider, change corresponding volumes
        if(Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
            //Decrease volume by 1
        if(Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
            //Increase volume by 1
        
        // Interact with options
        if (Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.Return))
            Interact();
    }

    private void ChangePosition(int change)
    {
        _currentPosition += change;
        _previousPosition = _currentPosition - change;

        if (_currentPosition < 0)
            _currentPosition = options.Length - 1;
        else if (_currentPosition > options.Length - 1)
            _currentPosition = 0;
        
        if (_previousPosition < 0)
            _previousPosition = options.Length - 1;
        else if (_previousPosition > options.Length - 1)
            _previousPosition = 0;
        
        //Assign the Y position of the current option to the arrow 
        _rect.position = new Vector3(_rect.position.x, options[_currentPosition].position.y, 0);
        options[_currentPosition].GetComponent<Image>().sprite = options[_currentPosition].GetComponent<Button>().spriteState.highlightedSprite;
        options[_previousPosition].GetComponent<Image>().sprite = options[_previousPosition].GetComponent<Button>().spriteState.disabledSprite;
    }

    private void Interact()
    {
        // Access the button component on each option and call its function
        options[_currentPosition].GetComponent<Button>().Select();
        options[_currentPosition].GetComponent<Button>().onClick.Invoke();
        
    }
}
