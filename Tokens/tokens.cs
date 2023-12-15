namespace Tokens;
public enum TokenType
{
    Print,
    Const,
    Flinq,
    LLinq,
    LParen,
    RParen,
    LBrace,
    RBrace,
    LBracket,
    RBracket,
    Operator,
    Punctuation,
    Point,
    Comma,
    ComparisonOperator,
    Identifier,
    FIdentifier,
    Number,
    Parameter,
    StringLiteral,
    LetKeyword,
    IfKeyword,
    ThenKeyword,
    ElseKeyword,
    InKeyword,
    FunctionKeyword,
    EOL,
    EOF
}
/// <summary>
/// Represents a token in the program.
/// </summary>
/// <param name="type">The type of the token.</param>
/// <param name="value">The value of the token.</param>
/// <param name="line">The line number where the token appears.</param>
/// <param name="column">The column number where the token appears.</param>
public class Token
{
    public TokenType Type { get; set; }
    public string Value { get; set; }
    public int Line { get; set; }
    public int Column { get; set; }

    public Token(TokenType type, string value, int line, int column)
    {
        Type = type;
        Value = value;
        Line = line;
        Column = column;
    }
}