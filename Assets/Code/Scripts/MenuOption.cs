using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MenuOption : MonoBehaviour
{
    [SerializeField] private string _optionName;
    public string OptionName { get { return _optionName; } set { _optionName = value; } }
    [SerializeField] private Sprite _optionSprite;
    public Sprite OptionSprite { get { return _optionSprite; } set { _optionSprite = value; } }

    public EventHandler OnMenuAction;

    public override bool Equals(object obj)
    {
        MenuOption other = obj as MenuOption;
        if(_optionName == null && other.OptionName == null && 
          _optionSprite == null && other.OptionSprite == null)
        {
            return true;
        }
        else if(_optionName != null && _optionName.Equals(other.OptionName)
          && _optionSprite != null && _optionSprite.Equals(other.OptionSprite))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }

    public override string ToString()
    {
        return base.ToString();
    }
}
