using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MenuManager : MonoBehaviour
{
    public enum MenuType
    {
        MENUTYPE_VERTICAL,
        MENUTYPE_HORIZONTAL
    }

    /*It is smarter to split the creation of the private variable and the property that is associated with it
    Unity does not allow for the direct serialization of properties as this can cause unintended problems depending on
    the behaviors given to the properties in question
    Check out: https://gamedev.stackexchange.com/questions/194623/why-doesnt-unity-use-properties-from-c-scripts-instead-of-fields */

    [SerializeField] private PlayerInput _playerInput;
    public PlayerInput PlayerInputProp{ get { return _playerInput; } }

    [SerializeField] private string _menuName;
    public string MenuName { get { return _menuName; } }
    [SerializeField] private MenuType _menuType;
    public MenuType TypeOfMenu { get { return _menuType; } }

    [SerializeField]  private int _currentPosition;

    [SerializeField] private string _currentOptionName;

    private MenuOption _currentOption;
    public MenuOption CurrentOption { get { return _currentOption; } }

    public void Awake()
    {
        _currentPosition = 0;
        UpdateMenuStats();
    }

    private void OnEnable()
    {
        _playerInput.actions["Move"].Enable();
        _playerInput.actions["Select"].Enable();
        _playerInput.actions["Move"].started += MoveInMenu;
        _playerInput.actions["Select"].started += SelectOption;
    }

    public void Test(string test)
    {
        Debug.Log(test);
    }

    private void UpdateMenuStats()
    {
        if (transform.childCount > 0)
        {
            _currentOption = transform.GetChild(_currentPosition).GetComponent<MenuOption>();
        }
        if (_currentOption != null)
        {
            _currentOptionName = _currentOption.OptionName;
        }
        else
        {
            _currentOptionName = "No Option Highlighted!";
        }
    }

    public void AppendOption(GameObject optionToBeAdded)
    {
        bool isFound = false;
        MenuOption menuOption = optionToBeAdded.GetComponent<MenuOption>();
        if(menuOption == null)
        {
            Debug.LogError(optionToBeAdded.name + " tried to add a Menu Option to " + name+
              " despite not having a Menu Option component!");
            return;
        }
        else if(menuOption != null)
        {
            for(int i = 0; i < transform.childCount; i++)
            {
                MenuOption currentOption = optionToBeAdded.GetComponent<MenuOption>();
                if(currentOption != null && menuOption.Equals(currentOption))
                {
                    isFound = true;
                }
            }
            if(!isFound)
            {
                optionToBeAdded.transform.parent = transform;
            }
            else
            {
                Debug.LogError("This Menu Option has already been added to "+name+"!");
            }
        }
    }

    public void RemoveOption(int index, string optionName)
    {
        if(index < 0 || index >= transform.childCount)
        {
            Debug.LogError(name + " tried to remove a Menu Option that was out of bounds!");
            return;
        }

        MenuOption option = transform.GetChild(index).GetComponent<MenuOption>();

        if (!option.OptionName.Equals(optionName)) {
            Debug.LogError("The Menu Option trying to be removed does not exist! (incorrect" +
              " index or/and optionName)");
        }   
        else
        {
            Destroy(option.gameObject);
        }
    }

    public virtual void SelectOption(InputAction.CallbackContext context)
    {
        if(transform.childCount == 0)
        {
            Debug.LogError("This menu has no options in it!");
        }
        else if (transform.childCount > 0 && _currentOption != null)
        {
            Debug.Log("Selected");
            _currentOption.OnMenuAction.Invoke();
        }
    }

    public virtual void MoveInMenu(InputAction.CallbackContext context)
    {
        Vector2 input = context.ReadValue<Vector2>();
        if(transform.childCount != 0)
        {
            switch (input.y)
            {
                case -1:
                    if (_currentPosition == transform.childCount - 1) 
                    {
                        _currentPosition = 0;
                    }
                    else
                    {
                        _currentPosition++;
                    }
                    break;
                case 1:
                    if (_currentPosition == 0)
                    {
                        _currentPosition = transform.childCount - 1;
                    }
                    else
                    {
                        _currentPosition--;
                    }
                    break;
            }
        }
        UpdateMenuStats();
    }

    public void OnDisable()
    {
        _playerInput.actions["Move"].started -= MoveInMenu;
        _playerInput.actions["Select"].started -= SelectOption;
        _playerInput.actions["Move"].Disable();
        _playerInput.actions["Select"].Disable();
    }
}
