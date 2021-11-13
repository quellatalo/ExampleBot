using Quellatalo.Nin.TheEyes;
using Quellatalo.Nin.TheEyes.Pattern.CV.Image;
using Quellatalo.Nin.TheHands;

Console.WriteLine("Hello");
// open start menu, launch notepad, and do some typing actions
HandsExample();

// Need preparation: Open Microsoft Paint, with full canvas ready
// This method will:
// - focus on Paint, demonstrate the recognition
// - draw something on canvas
// - duplicate the drawing
// - once again find all similar drawings on the canvas
HandsAndEyesExample();

void HandsExample()
{
    KeyboardHandler keyboard = new KeyboardHandler();
    keyboard.KeyTyping(Keys.LWin);
    Thread.Sleep(2222); // wait (for start menu to appear)
    keyboard.KeyTyping(Keys.N);
    keyboard.StringInput("otepad");
    Thread.Sleep(2222); // wait, it might take some time for slow PCs to find the program
    keyboard.KeyTyping(Keys.Enter);
    Thread.Sleep(2222); // wait (for start menu to appear)
    keyboard.StringInput("Kawaii is Justice!");
    keyboard.KeyTyping(Keys.Enter);
    Thread.Sleep(2222); // wait (just to see what is happening)

    // Set action delay (in milliseconds) for further keyboard inputs
    // in order to have a better feel of human-like actions
    keyboard.DefaultKeyboardActionDelay = 41;

    // Cut all current text to clipboard
    // ctrl + A, ctrl + X
    keyboard.KeyDown(Keys.LControlKey);
    keyboard.KeyTyping(Keys.A);
    keyboard.KeyTyping(Keys.X);
    keyboard.KeyUp(Keys.LControlKey);

    // Type something (Vv)
    keyboard.KeyDown(Keys.LShiftKey);
    keyboard.KeyTyping(Keys.V);
    keyboard.KeyUp(Keys.LShiftKey);
    keyboard.KeyTyping(Keys.V);
    keyboard.KeyTyping(Keys.Enter);

    // Paste the text stored in clipboard
    keyboard.KeyDown(Keys.LControlKey);
    keyboard.KeyTyping(Keys.V);
    keyboard.KeyUp(Keys.LControlKey);

    // Type something again
    keyboard.KeyDown(Keys.LShiftKey);
    keyboard.KeyTyping(Keys.V);
    keyboard.KeyUp(Keys.LShiftKey);
    keyboard.StringInput("ictory");
    keyboard.CharacterInput('!');
}

void HandsAndEyesExample()
{
    // Look for a window which has "paint" in title and bring it to front
    var app = App.GetAppByWindowTitle("paint");
    app.ToFront();

    using Brush brush = new SolidBrush(Color.FromArgb(63, Color.Red));
    // Highlight the Paint's window area
    var area = new Area(app.GetMainWindowRectangle());
    area.Highlight(brush);
    Thread.Sleep(2222); // wait, just to observe
    Area.ClearHighlight(); // clear the highlight

    // Prepare some points to draw in sequence
    Point[] points1 = {
        new(area.Center.X-10,area.Center.Y-10),
        new(area.Center.X+10,area.Center.Y-10),
        new(area.Center.X+10,area.Center.Y+10),
        new(area.Center.X-10,area.Center.Y+10)
    };
    Point[] points2 = {
        new(area.Center.X-14,area.Center.Y-14),
        new(area.Center.X+14,area.Center.Y-14),
        new(area.Center.X+14,area.Center.Y+14),
        new(area.Center.X-14,area.Center.Y+14)
    };

    var mouse = new MouseHandler();
    var keyboard = new KeyboardHandler();

    mouse.DefaultMouseActionDelay = 111; // set some delay after each mouse action, it's easier to observe

    // Draw on Paint's canvas
    mouse.LeftDrag(points1);
    mouse.LeftDrag(points2);

    keyboard.DefaultKeyboardActionDelay = 41; // set some delay after each keyboard action

    // Switch to Paint's select mode
    keyboard.KeyDown(Keys.LControlKey);
    keyboard.KeyTyping(Keys.A);
    keyboard.KeyUp(Keys.LControlKey);
    keyboard.KeyTyping(Keys.Escape);

    // The drawing's area
    var myDrawingArea = new Area(
        new Rectangle(
            new Point(area.Center.X - 16, area.Center.Y - 16),
            new Size(32, 32)));
                
    // In Paint, select the drawing
    mouse.LeftDrag(
        myDrawingArea.TopLeft,
        myDrawingArea.BottomRight);

    // Make a copy of the drawing
    keyboard.KeyDown(Keys.LControlKey);
    keyboard.KeyTyping(Keys.C);
    keyboard.KeyTyping(Keys.V);
    keyboard.KeyUp(Keys.LControlKey);
    keyboard.KeyTyping(Keys.Escape);

    // Find all the same drawings on Paint's window
    // (with 75% threshold by using default Pattern constructor)
    using var pattern = new ImagePattern(myDrawingArea.GetDisplayingImage());
    var matches = area.FindAll(pattern);
    foreach (var match in matches)
    {
        area.SubArea(match.Rectangle).Highlight(brush);
    }
    Thread.Sleep(2222);
    Area.ClearHighlight();
}