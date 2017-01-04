open System
// Part One - *Very* Basic F# Syntax

// This is a single line comment, same syntax as C#

(* 
	Multiline comments are wrapped in this syntax 
*)

// F# uses type inference, so declaring types aren't needed.
// Functions and objects are both instantiated with "let" syntax.

class demoClass =
    member this.name = "name"
    member mutable this.ID = 0

let t = new demoClass()
t.ID <- 123
t.name <- "bob"


let theQuestionMark = "?"
let aNormalInt = 123456
let aNormalFloat = 1.0
let aUnit = () 

// Hover your mouse over values to see their different inferred types.

// This is an example function, you write the function name first, then follow with your parameters.
let addOne parameter secondParameter thirdParameter = 
    let result = parameter + 1
    let finalLine = "concatenate" + result.ToString() + secondParameter
    finalLine

// NB: there's no curly braces. F# uses whitespace for determining code blocks
// NB: there's no "return" statement. F# evaluates and returns the last line in a code block

// Types are inferred from evaluating the expression on the right side of the equal sign.
// If there's insufficient information to evaluate the expression and infer the type, then F# will warn you
// and allow you to specify the type manually.

let castsAnInt (parameter:IDisposable) = 
    let line = "This was an int" + parameter.ToString()
    Console.WriteLine line

// F# prefers to use Modules for organizing code
module ContainsTheAnswerOfLife = 
    let answerOfLife = 42
    // This instance of theQuestion has a scope that's internal to the module so there's no naming conflict.
    let theQuestion = "The question is unknown."
    
// To use a module in another module, use the "open" syntax
open ContainsTheAnswerOfLife

// F# uses dot syntax for accessing members of modules or members of objects
let theAnswer = ContainsTheAnswerOfLife.answerOfLife

// If you need to use an object, like for working with C#, then you can use "new" syntax and constructors
let myRandom = new Random(42)
let randomNumber = myRandom.Next(0, Int32.MaxValue)
randomNumber = myRandom.Next()

// Normally F# uses immutable values. Once a value is declared then it's normally only able to be read. You can designate
// an value as modifiable by using the "mutable" modifier and the value-setting syntax.

let mutable changeMe = 0
changeMe <- myRandom.Next(0, Int32.MaxValue)
changeMe <- myRandom.Next(0, Int32.MaxValue)
changeMe <- myRandom.Next(0, Int32.MaxValue)

// F# includes a powerful method of flow-control called "pattern matching". In its most basic form, you can use pattern matching
// as a replacement for if-then-else statements.
let city = "Portland"
let oregonOrWashington chosenCity = 
    let result = 
        match chosenCity with
        | "Portland" -> "Oregon"
        | "Seattle" -> "Washington"
        | _ -> "Unknown"
    result

// Because F# is expression oriented, not object oriented, functions can be combined! 
// If you have two functions with matching type signatures, then those two functions can be joined together to make another function

let addsTwo p = p + 2
let addsThree p = p + 3
let addsFive = addsTwo >> addsThree >> addsTwo

// This function building process can be used to chain functions together for building shopping cart operations, website processing flows, and so on.

let taxOR price = 
let taxWA price = 3 * (2 * price + 1)
let noTax price = price

obj.func(obj2.funct2())

let calcTax stateName = 
    match stateName with
    | "OR" -> taxOR
    | "WA" -> taxOR >> taxWA >> taxOR
    // taxWA ( taxOR ( price )) 
    // 3 * ( 2 * 1 + 1 )
    | _ -> noTax
    
let calculatesTaxes stateName price= 
    let calc = calcTax stateName 
    calc price

open System
open System.Diagnostics
let testFunction = 
    try
        raise(IndexOutOfRangeException("text"))
    with
    | :? IndexOutOfRangeException -> "error 1"
    | _ -> "error 2"

// ============================================================================================================
// ============================================================================================================
//  ============================================================================================================
 

// Now that you have a basic idea of syntax, run the follow code to draw a fractal, then experiment to see what you can make!

open System
open System.Drawing
open System.Windows.Forms

// Create a form to display the graphics
let width, height = 500, 500         
let form = new Form(Width = width, Height = height)
let box = new PictureBox(BackColor = Color.White, Dock = DockStyle.Fill)
let image = new Bitmap(width, height)
let graphics = Graphics.FromImage(image)
//The following line produces higher quality images, 
//at the expense of speed. Uncomment it if you want
//more beautiful images, even if it's slower.
//Thanks to https://twitter.com/AlexKozhemiakin for the tip!
//graphics.SmoothingMode <- System.Drawing.Drawing2D.SmoothingMode.HighQuality
let brush = new SolidBrush(Color.FromArgb(0, 0, 0))

box.Image <- image
form.Controls.Add(box) 

// Compute the endpoint of a line
// starting at x, y, going at a certain angle
// for a certain length. 
let endpoint x y angle length =
    x + length * cos angle,
    y + length * sin angle

let flip x = (float)height - x

// Utility function: draw a line of given width, 
// starting from x, y
// going at a certain angle, for a certain length.
let drawLine (target : Graphics) (brush : Brush) 
             (x : float) (y : float) 
             (angle : float) (length : float) (width : float) =
    let x_end, y_end = endpoint x y angle length
    let origin = new PointF((single)x, (single)(y |> flip))
    let destination = new PointF((single)x_end, (single)(y_end |> flip))
    let pen = new Pen(brush, (single)width)
    target.DrawLine(pen, origin, destination)

let draw x y angle length width = 
    drawLine graphics brush x y angle length width

let pi = Math.PI

let maxDepth = 2
let ``Testing funct that you can use spaces in the name blah blah blah`` = 321

let rec branch (curDepth:int) (x : float) (y : float) (ang : float) (len : float) (wid : float) =
    // we draw the current segment
    draw x y ang len wid
    // if max depth hasn't been reached yet,
    // we create 2 branches and keep going
    if (curDepth > maxDepth)
    then ignore ()
    else
        // compute end coordinates of current segment
        let x',y' = endpoint x y ang len
        // go left
        branch (curDepth + 1) x' y' (ang + 0.3) (len * 0.8) (wid * 0.7)
        // go right
        branch (curDepth + 1) x' y' (ang - 0.3) (len * 0.8) (wid * 0.7)
                       
branch 0 250. 50. (pi*(0.5)) 90.0 10.
// form.ShowDialog()









