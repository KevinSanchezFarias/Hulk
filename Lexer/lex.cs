using Tokens;

namespace LexerAnalize;
/// <summary>
/// Represents a lexer that tokenizes source code.
/// </summary>
public partial class Lexer
{
    /// <summary>
    /// The source code to tokenize.
    /// </summary>
    private string Text { get; set; }
    /// <summary>
    /// The current position in the source code.
    /// </summary>
    private static int Position { get; set; }

    /// <summary>
    /// The current column in the source code.
    /// </summary>
    private int Column { get; set; }

    /// <summary>
    /// The current line in the source code.
    /// </summary>
    private int Line { get; set; }

    /// <summary>
    /// The current character being processed in the source code.
    /// </summary>
    private char CurrentChar { get; set; }

    /// <summary>
    /// Initializes a new instance of the Lexer class.
    /// </summary>
    /// <param name="input">The source code to tokenize.</param>
    public Lexer(string input)
    {
        Text = input;
        Position = 0;
        Column = 1;
        Line = 1;
        CurrentChar = input[Position];
        Tokenize();
    }

    /// <summary>
    /// Scans the source code for a string literal.
    /// </summary>
    /// <returns>
    /// A Token object representing the string literal found.
    /// </returns>
    private Token StringLiteral()
    {
        // Initialize an empty string to store the string literal
        string result = "";
        // Advance past the opening quote
        Advance();

        // Continue scanning characters until a closing quote is found
        while (CurrentChar != '\0' && CurrentChar != '"')
        {
            // Append the current character to the result string
            result += CurrentChar;
            // Move to the next character
            Advance();
        }

        // If the end of the source code is reached before a closing quote is found, throw an exception
        if (CurrentChar == '\0')
        {
            throw new Exception($"Unterminated string literal at line {Line} and column {Column}");
        }

        // Consume the closing quote to advance to the next token
        Advance();

        // Return a new Token object representing the string literal found
        return new Token(TokenType.StringLiteral, result, Line, Column);
    }
    
    /// <summary>
    /// Lexes a print expression
    /// </summary>
    private void LexPrint()
    {
        // Add a token for the print keyword
        LexTokens.Add(new Token(TokenType.Print, "print", Line, Column));
        Advance();
        SkipWhitespace();

        // Expect an opening parenthesis after the print keyword
        if (CurrentChar != '(')
        {
            throw new Exception($"Expected '(' after print keyword, but found '{CurrentChar}' at line {Line} and column {Column}");
        }
        Advance();

        //Tokenize the print expression
        while (CurrentChar != '\0' && CurrentChar != ')')
        {
           Tokenize();
        }
        if (CurrentChar == '\0')
        {
            throw new Exception($"Expected ')' after print keyword, but found '{CurrentChar}' at line {Line} and column {Column}");
        }
        Tokenize();
        
    }

    /// <summary>
    /// Scans the source code for a function declaration.
    /// </summary>
    private void DeclareFunction()
    {
        // Add a token for the function keyword
        LexTokens.Add(new Token(TokenType.FunctionKeyword, "function", Line, Column));
        Advance();
        SkipWhitespace();

        // Scan for the function name
        var functionName = "";
        while (CurrentChar != '\0' && char.IsLetterOrDigit(CurrentChar))
        {
            functionName += CurrentChar;
            Advance();
        }
        // Add a token for the function name
        LexTokens.Add(new Token(TokenType.FIdentifier, functionName, Line, Column));

        // Expect an opening parenthesis after the function name
        if (CurrentChar != '(')
        {
            throw new Exception($"Expected '(' after function name, but found '{CurrentChar}' at line {Line} and column {Column}");
        }
        Advance();

        // Scan for function parameters
        while (CurrentChar != '\0' && CurrentChar != ')')
        {
            SkipWhitespace();
            var parameter = "";
            while (CurrentChar != '\0' && char.IsLetterOrDigit(CurrentChar))
            {
                parameter += CurrentChar;
                Advance();
            }
            // Add a token for each parameter
            LexTokens.Add(new Token(TokenType.Parameter, parameter, Line, Column));
            SkipWhitespace();
            if (CurrentChar == ',')
            {
                Advance();
            }
        }

        // Expect a closing parenthesis after the function parameters
        if (CurrentChar != ')')
        {
            throw new Exception($"Expected ')' after function parameters, but found '{CurrentChar}' at line {Line} and column {Column}");
        }
        Advance();
        SkipWhitespace();

        // Expect '=>' after the function declaration
        if (CurrentChar != '=' && Peek() != '>')
        {
            throw new Exception($"Expected '=>' after function declaration, but found '{CurrentChar}' at line {Line} and column {Column}");
        }
        // Add a token for the '=>' symbol
        LexTokens.Add(new Token(TokenType.Flinq, "=>", Line, Column));
        Advance(); Advance();
        SkipWhitespace();

        // Continue tokenizing the rest of the source code
        Tokenize();
    }

    /// <summary>
    /// Scans the source code for a number.
    /// </summary>
    /// <returns>
    /// A Token object representing the number found.
    /// </returns>
    private Token Number()
    {
        // Initialize an empty string to store the number
        string result = "";

        // If the current character is a negative sign, append it to the result string
        if (CurrentChar == '-')
        {
            result += CurrentChar;
            Advance();
        }

        // Continue scanning characters as long as they are digits
        while (CurrentChar != '\0' && char.IsDigit(CurrentChar))
        {
            result += CurrentChar;
            Advance();
        }

        // If the current character is a decimal point, append it to the result string
        if (CurrentChar == '.')
        {
            result += CurrentChar;
            Advance();

            // Continue scanning characters as long as they are digits
            while (CurrentChar != '\0' && char.IsDigit(CurrentChar))
            {
                result += CurrentChar;
                Advance();
            }
        }

        // Return a new Token object representing the number found
        return new Token(TokenType.Number, result, Line, Column);
    }

    /// <summary>
    /// Scans the source code for a keyword or identifier.
    /// </summary>
    /// <returns>
    /// A Token object representing the keyword or identifier found.
    /// </returns>
    private Token Keyword()
    {
        // Initialize an empty string to store the keyword or identifier
        string result = "";

        // Continue scanning characters as long as they are letters or digits
        while (CurrentChar != '\0' && char.IsLetterOrDigit(CurrentChar))
        {
            // Append the current character to the result string
            result += CurrentChar;

            // Move to the next character
            Advance();
        }

        // Return a new Token object based on the keyword or identifier found
        return result switch
        {
            "print" => new Token(TokenType.Print, result, Line, Column),
            "const" => new Token(TokenType.Const, result, Line, Column),
            "flinq" => new Token(TokenType.Flinq, result, Line, Column),
            "llinq" => new Token(TokenType.LLinq, result, Line, Column),
            "let" => new Token(TokenType.LetKeyword, result, Line, Column),
            "function" => new Token(TokenType.FunctionKeyword, result, Line, Column),
            "if" => new Token(TokenType.IfKeyword, result, Line, Column),
            "then" => new Token(TokenType.ThenKeyword, result, Line, Column),
            "else" => new Token(TokenType.ElseKeyword, result, Line, Column),
            "in" => new Token(TokenType.InKeyword, result, Line, Column),
            // If the result string is not a recognized keyword, it is treated as an identifier
            _ => new Token(TokenType.Identifier, result, Line, Column),
        };
    }
    /// <summary>
    /// Advances the position of the lexer to the next character in the input text.
    /// </summary>
    private void Advance()
    {
        Position++;
        Column++;
        if (Position > Text.Length - 1)
        {
            CurrentChar = '\0';
        }
        else
        {
            CurrentChar = Text[Position];
        }
    }
    /// <summary>
    /// Skips all whitespace characters until a non-whitespace character is found.
    /// </summary>
    private void SkipWhitespace()
    {
        while (CurrentChar != '\0' && char.IsWhiteSpace(CurrentChar))
        {
            Advance();
        }
    }
    /// <summary>
    /// Returns the next character in the input string without consuming it.
    /// </summary>
    /// <returns>The next character in the input string, or '\0' if there are no more characters.</returns>
    public char Peek()
    {
        var peek_pos = Position + 1;
        if (peek_pos >= Text.Length)
        {
            return '\0';
        }
        else
        {
            return Text[peek_pos];
        }
    }
}