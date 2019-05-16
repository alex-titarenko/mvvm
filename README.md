# TAlex.Mvvm
[![Build status](https://ci.appveyor.com/api/projects/status/fs0fugvd4b743sdv?svg=true)](https://ci.appveyor.com/project/alex-titarenko/mvvm)

Lightweight MVVM framework for C#.

## Structure
* **TAlex.Mvvm** - provides base view model and relay command classes.
* **TAlex.Mvvm.Wpf** - WPF depended implementation of message service, application class extensions and some commands.

## Example of usage
Defining view model:
```C#
// View Model
public class TestViewModel : ViewModelBase
{
    private int _intProp;
    private string _strProp;


    public int IntegerProperty
    {
        get { return _intProp; }
        set { Set(ref _intProp, value); }
    }

    public string StringProperty
    {
        get { return _strProp; }
        set { Set(() => StringProperty, ref _strProp, value); }
    }
}

// PropertyChanged event
var target = new TestViewModel();
string actualPropName = null;
target.PropertyChanged += (e, a) => { actualPropName = a.PropertyName; };

target.IntegerProperty = 3;

Console.WriteLine(actualPropName);
```

## Get it on NuGet!

    Install-Package TAlex.Mvvm
    Install-Package TAlex.Mvvm.Wpf

## License
TAlex.Common is under the [MIT license](LICENSE.md).
