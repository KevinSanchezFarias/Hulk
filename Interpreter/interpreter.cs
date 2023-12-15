using LexerAnalize;
using ParserAnalize;
using EvaluatorAnalize;
namespace InterpreterAnalizer;
public class Interpreter
{
    public static List<string> Interpret(string input)
    {
        // Split the string into lines
        var lines = input.Split(";\r", StringSplitOptions.RemoveEmptyEntries);
        List<string> outp = new();

        foreach (var line in lines)
        {
            var lexer = new Lexer(line);
            var parser = new Parser(lexer.LexTokens);
            var evaluator = new Evaluator(parser.Parse());
            var lineResult = evaluator.Evaluate().ToString();
            if (lineResult is null or "")
                continue;
            else
                outp.Add(lineResult);
        }
        return outp;
    }
}