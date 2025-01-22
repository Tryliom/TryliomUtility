# Tryliom Utility
A set of various utilities for Unity like:
- Conditional Field and Header attributes: display a field/header if a variable/func return true
- MinMaxRange type: a range in float/int with min and max range
- Utility for string and math
- RectMover: a tool that simplify the placement of RectTransform around other RectTransform
- Variable: A scriptable object that contains a type
- Reference: A field that can accept a Variable or a constant type

## Install
In Unity, go to `Window` menu -> `Package Manager` -> `+` -> `install package from git url` and put this url:
```
https://github.com/Tryliom/TryliomUtility.git
```

## How to use
### Conditional Fields
Add the `ConditionalField` attribute to a field or a header and put the name of the variable/func that will be used to display the field/header.
The second parameter is used to invert the condition.
```c#
public class Example : MonoBehaviour
{
    public bool ShowField;
    [ConditionalField(nameof(ShowField))]
    public int Field;
    
    public int Value;
    private bool IsGreaterThan0() => Value > 0;
    
    [ConditionalHeader(nameof(IsGreaterThan0), "Greater than 0")]
    [ConditionalField(nameof(IsGreaterThan0))] public int GreaterThan0;
    
    [ConditionalHeader(nameof(IsGreaterThan0), "Less than 0", true)]
    [ConditionalField(nameof(IsGreaterThan0), false)] public int NotGreaterThan0;
}
```

**Will not work for class that doesn't inherit from `MonoBehaviour` or `ScriptableObject`**

### MinMaxRange
Create a new `MinMaxRange` with the type you want and set:
- The default min value
- The default max value
- The min range
- The max range

```c#
public class Example : MonoBehaviour
{
    public MinMaxRange<float> PlayerStartHp = new (0.3f, 0.7f, 0f, 1f);
    
    private void Start()
    {
        Debug.Log(PlayerStartHp.RandomInRange());
    }
}
```

**Will not work for class that doesn't inherit from `MonoBehaviour` or `ScriptableObject`**

## RectMover
Add the `RectMover` component to a RectTransform, call the `MoveTo` method with the target RectTransform and the `MoveTo` method will move the RectTransform to the target RectTransform.

```c#
public class Example : MonoBehaviour
{
    public RectTransform Target;
    public RectMover Mover;
    
    private void Start()
    {
        Mover.MoveTo(Target);
    }
}
```