/*
 namespace SomethingAboutActions;

interface IActionRequest{}
interface IActionResult{}

delegate IActionResult Action(IActionRequest request);

class ActionException : Exception, IActionResult
{
        public ActionException()
        {
        }

        public ActionException(string message)
            : base(message)
        {
        }

        public ActionException(string message, Exception inner)
            : base(message, inner)
        {
        }
}

struct ResultOk : IActionResult {}

interface IInteractable
{
    IActionResult Interact(IActionRequest action);
}

//

record struct Hit(string Tool, uint Power = 0) : IActionRequest {}

record struct Get : IActionRequest
{
    public int X { get; init; }
    
    public record struct Result : IActionResult
    {
        public int Y { get; init; }
    }
}

//

class Tree : IInteractable
{
    public IActionResult Interact(IActionRequest action)
    {
        return action switch
        {
            Hit act => Interact(act),
            Get act => new Get.Result() { Y = act.X*2 + 5 },
            _ => new ActionException("wrong action"),
        };
    }

    public IActionResult Interact(Hit action)
    {
        return action.Tool == "axe" ? new ResultOk() : new ActionException("wrong tool");
    }
}

static class Example
{
    public static void DoExample()
    {
        IInteractable it = new Tree();
        
        Console.WriteLine(
            it.Interact(new Get{ X = 10 }) switch
            {
                Get.Result result => result.Y,
                ActionException e => throw e,
                _ => "something else",
            }
        );

        Console.WriteLine(
            it.Interact(new Hit(Tool: "axe", Power: 0)) switch
            {
                ResultOk _ => "okey",
                ActionException e => throw e,
            }
        );
        
        Console.WriteLine(
            it.Interact(new Hit(Tool: "pickaxe", Power: 0)) switch
            {
                ResultOk _ => "okey",
                ActionException e => throw e,
            }
        );
    }
}
*/
