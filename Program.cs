using InterpreterAnalizer;

static class Program
{
    public static void Main()
    {
        _ = new Interpreter();
        foreach (var item in Interpreter.Interpret(ReadLines("./exp.hk")))
        {
            Console.WriteLine(item);
        }
    }
    private static string ReadLines(string path)
    {
        string fileContents = File.ReadAllText(path);
        string[] lines = fileContents.Split('\n');

        string result = "";
        foreach (string line in lines)
        {
            result += line;
        }
        return result;
    }
}