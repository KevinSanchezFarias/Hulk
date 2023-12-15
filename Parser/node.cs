namespace Nodes;
/// <summary>
/// Abstract base class for all nodes in the abstract syntax tree.
/// </summary>
public abstract class Node { }

/// <summary>
/// Represents a variable declaration node in the abstract syntax tree.
/// </summary>
public class VariableDeclarationNode : Node
{
    /// <summary>
    /// The identifier of the variable being declared.
    /// </summary>
    public string Identifier { get; }

    /// <summary>
    /// The value assigned to the variable.
    /// </summary>
    public Node Value { get; }

    /// <summary>
    /// The body of the program following the variable declaration.
    /// </summary>
    public Node Body { get; }

    public VariableDeclarationNode(string identifier, Node value, Node body)
    {
        Identifier = identifier;
        Value = value;
        Body = body;
    }
}

/// <summary>
/// Represents a node for multiple variable declarations in the abstract syntax tree.
/// </summary>
public class MultipleVariableDeclarationNode : Node
{
    /// <summary>
    /// The list of variable declarations.
    /// </summary>
    public List<VariableDeclarationNode> Declarations { get; }

    /// <summary>
    /// The body of the program following the variable declarations.
    /// </summary>
    public Node Body { get; }

    public MultipleVariableDeclarationNode(List<VariableDeclarationNode> declarations, Node body)
    {
        Declarations = declarations;
        Body = body;
    }
}

/// <summary>
/// Represents an identifier node in the abstract syntax tree.
/// </summary>
public class IdentifierNode : Node
{
    /// <summary>
    /// The identifier.
    /// </summary>
    public string Identifier { get; }

    public IdentifierNode(string identifier)
    {
        Identifier = identifier;
    }
}

/// <summary>
/// Represents a constant declaration node in the abstract syntax tree.
/// </summary>
public class ConstDeclarationNode : Node
{
    /// <summary>
    /// The identifier of the constant being declared.
    /// </summary>
    public string Identifier { get; }

    /// <summary>
    /// The value assigned to the constant.
    /// </summary>
    public Node Value { get; }

    public ConstDeclarationNode(string identifier, Node value)
    {
        Identifier = identifier;
        Value = value;
    }
}

/// <summary>
/// Represents a binary expression node in the abstract syntax tree.
/// </summary>
public class BinaryExpressionNode : Node
{
    /// <summary>
    /// The left operand of the binary expression.
    /// </summary>
    public Node Left { get; }

    /// <summary>
    /// The operator of the binary expression.
    /// </summary>
    public string Operator { get; }

    /// <summary>
    /// The right operand of the binary expression.
    /// </summary>
    public Node Right { get; }

    public BinaryExpressionNode(Node left, string @operator, Node right)
    {
        Left = left;
        Operator = @operator;
        Right = right;
    }
}

/// <summary>
/// Represents an if expression node in the abstract syntax tree.
/// </summary>
public class IfExpressionNode : Node
{
    /// <summary>
    /// The condition of the if expression.
    /// </summary>
    public Node Condition { get; }

    /// <summary>
    /// The body of the program to be executed if the condition is true.
    /// </summary>
    public Node ThenBody { get; }

    /// <summary>
    /// The body of the program to be executed if the condition is false.
    /// </summary>
    public Node ElseBody { get; }

    public IfExpressionNode(Node condition, Node thenBody, Node elseBody)
    {
        Condition = condition;
        ThenBody = thenBody;
        ElseBody = elseBody;
    }
}

/// <summary>
/// Represents a predefined function call node in the abstract syntax tree.
/// </summary>
public class FunctionPredefinedNode : Node
{
    /// <summary>
    /// The name of the predefined function.
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// The arguments passed to the predefined function.
    /// </summary>
    public List<Node> Args { get; }

    public FunctionPredefinedNode(string name, List<Node> args)
    {
        Name = name;
        Args = args;
    }
}
/// <summary>
/// Represents a function call node in the abstract syntax tree.
/// </summary>
public class FunctionCallNode : Node
{
    /// <summary>
    /// The name of the function being called.
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// The arguments passed to the function.
    /// </summary>
    public List<Node> Args { get; }

    public FunctionCallNode(string name, List<Node> args)
    {
        Name = name;
        Args = args;
    }
}

/// <summary>
/// Represents a print function node in the abstract syntax tree.
/// </summary>
public class PrintNode : Node
{
    /// <summary>
    /// The expression to be printed.
    /// </summary>
    public Node Body { get; }  

    public PrintNode(Node body)
    {
        Body = body;
    } 
}

/// <summary>
/// Represents a function declaration node in the abstract syntax tree.
/// </summary>
public class FunctionDeclarationNode : Node
{
    /// <summary>
    /// The name of the function being declared.
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// The arguments of the function.
    /// </summary>
    public List<string> Args { get; }

    /// <summary>
    /// The body of the function.
    /// </summary>
    public Node Body { get; }

    public FunctionDeclarationNode(string name, List<string> args, Node body)
    {
        Name = name;
        Args = args;
        Body = body;
    }
}

/// <summary>
/// Represents an end node in the abstract syntax tree.
/// This node is used to represent the end of a program or a block of code.
/// </summary>
public class EndNode : Node { }

/// <summary>
/// Represents a value node in the abstract syntax tree.
/// This node is used to represent a literal value in the source code.
/// </summary>
public class ValueNode : Node
{
    /// <summary>
    /// The literal value that this node represents.
    /// </summary>
    public object Value { get; }

    public ValueNode(object value)
    {
        Value = value;
    }
}