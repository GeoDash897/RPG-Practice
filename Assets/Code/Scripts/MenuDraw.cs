using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(MenuManager))]
public class MenuDraw : MonoBehaviour
{
    [SerializeField] private bool _isVisible;
    public bool IsVisible { get { return _isVisible; } set { _isVisible = value; } }

    /* Note that this _currentOption can be out of sync with the _currentOption of MenuManager.
     * As a result, in order to get the current MenuOption that the user is highlighting,
     * this class' _currentOption has to be updated to match the one of MenuManager.)
     */
    [SerializeField] private MenuOption _currentOption;
    private PlayerInput _playerInput;
    
    [SerializeField] private Sprite _cursor;
    public Sprite Cursor { get { return _cursor; } }

    private void OnEnable()
    {
        _playerInput = GetComponent<MenuManager>().PlayerInputProp;
        _playerInput.actions["Move"].started += UpdateCursor;
    }

    // Start is called before the first frame update
    void Start()
    {
        //Allow for MenuManager to be created first
        _currentOption = GetComponent<MenuManager>().CurrentOption;
        _currentOption.GetComponent<SpriteRenderer>().color = Color.red;
    }

    public void UpdateCursor(InputAction.CallbackContext context)
    {
        /* This method is called after MoveInMenu() in MenuManager. As a result,
         * note that _currentOption is at first = the previous _currentOption of Menu Manager.
         */
        _currentOption.GetComponent<SpriteRenderer>().color = Color.white;
        _currentOption = GetComponent<MenuManager>().CurrentOption;
        _currentOption.GetComponent<SpriteRenderer>().color = Color.red;
    }
    
    public void DrawMenu()
    {
        if(!_isVisible)
        {
            for(int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.SetActive(false);
            }
        }
    }

    private void OnDisable()
    {
        _playerInput.actions["Move"].started -= UpdateCursor;
    }
}
