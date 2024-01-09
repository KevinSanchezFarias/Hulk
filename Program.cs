using InterpreterAnalizer;

static class Program
{
    public static void Main()
    {
        Console.Write("> ");
        _ = new Interpreter();
        while(true)
        {
            string item = Console.ReadLine();
            if(item == "")
            {
                break;
            }
            Console.WriteLine("> {0}", Interpreter.Interpret(item)[0]);
            Console.Write("> ");
        }
    }
    
}