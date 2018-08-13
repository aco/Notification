
# Notification

An old project I found, Notification is a flexible, easy to use notification assistant for your WinForms project. This can be compiled as a dll.

## Demo
<img src="https://github.com/cewpur/Notification/blob/master/demoassets/demo.gif" />
*Gif is choppy - animation uses native APIs and is super smooth IRL*

## Usage
The project includes a test form with customizable fields demonstrating its use. Basically, this is all you need:


```c#
var notification = new Notification.Control.Notification(Style.Slide, 250, Direction.Left)
{
    Title = "Hello World",
    Body = "Your body",

    TitleColor = Color.Black,
    ForeColor = Color.Black,
    BackColor = Color.White,

    ScreenPadding = 10,

    Icon = new Notification.Model.Icon()
    {
        Image = Image.FromFile(@".."),
        Padding = 5,
        BackColor = Color.LightGray
    }
};

notification.Show();
```

Forcing dismissal is possible with ```notification.Dismiss()```, you can choose to play out the exit animation or have it perish instantaneously through the bool param ```instant```.

### Show with lifespan
Method ```show()``` supports an overload with an int param where the notification's lifespan can be marked. Hovering over the notification will suspend the countdown to EOL. e.g.

```c#
var notification = ...;

notification.Show(3000); // displays for 3s/3000ms
```

### Further customization
The notification control itself inherits from System.Windows.Forms.Form and exposes all properties alongside. You can attach handlers, configure dimensions, etc. as normal.


## License
MIT
