using Nodes;
using ParserAnalize;

namespace EvaluatorAnalize;
/// <summary>
/// Represents an evaluator that evaluates an abstract syntax tree (AST).
/// </summary>
public class Evaluator
{
    /// <summary>
    /// The AST to evaluate.
    /// </summary>
    private Node AST { get; set; }

    /// <summary>
    /// A dictionary of variables used during the evaluation.
    /// </summary>
    private Dictionary<string, object> variables = new();

    /// <summary>
    /// Initializes a new instance of the Evaluator class.
    /// </summary>
    /// <param name="ast">The AST to evaluate.</param>
    public Evaluator(Node ast)
    {
        AST = ast;
    }

    /// <summary>
    /// Evaluates the AST and returns the result.
    /// </summary>
    /// <returns>The result of the evaluation.</returns>
    public object Evaluate()
    {
        return Visit(AST);
    }
    /// <summary>
    /// Visits a node in the abstract syntax tree (AST) and performs the appropriate action based on the type of the node.
    /// </summary>
    /// <param name="node">The node to visit.</param>
    /// <returns>The result of visiting the node.</returns>
    private object Visit(Node node)
    {
        /* The method uses a switch statement to handle different types of nodes:
         * - EndNode: Returns null.
         * - ValueNode: Returns the value of the node.
         * - FunctionCallNode: Finds the function declaration, checks the number of arguments, binds the arguments to the parameters, evaluates the function body, and restores the old variables.
         * - FunctionPredefinedNode: Finds the predefined function and evaluates it with the given arguments.
         * - BinaryExpressionNode: Evaluates the binary expression based on the operator and the left and right operands.
         * - IfExpressionNode: Evaluates the condition and then the appropriate body based on the result of the condition.
         * - IdentifierNode: Returns the value of the variable with the given identifier.
         * - VariableDeclarationNode: Assigns a value to a variable and then visits the body of the node.
         * - MultipleVariableDeclarationNode: Assigns values to multiple variables and then visits the body of the node.
         * - If the node type is not recognized, the method throws an exception.
        */
        switch (node)
        {
            case EndNode:
                return null!;
            case ValueNode valueNode:
                return valueNode.Value;
            case FunctionCallNode functionCallNode:
                // Find the function declaration
                var functionDeclaration = Parser.FDN.FirstOrDefault(f => f.Name == functionCallNode.Name) ?? throw new Exception($"Undefined function {functionCallNode.Name}");

                // Check the number of arguments
                if (functionDeclaration.Args.Count != functionCallNode.Args.Count)
                {
                    throw new Exception($"Incorrect number of arguments for function {functionCallNode.Name}");
                }

                // Bind the arguments to the parameters
                var oldVariables = new Dictionary<string, object>(variables);
                for (int i = 0; i < functionDeclaration.Args.Count; i++)
                {
                    var argName = functionDeclaration.Args[i];
                    var argValue = Visit(functionCallNode.Args[i]);
                    variables[argName] = argValue;
                }

                // Evaluate the function body
                var result = Visit(functionDeclaration.Body);

                // Restore the old variables
                variables = oldVariables;

                return result;
            case FunctionPredefinedNode functionPredefinedNode:
                if (LE.predefinedFunctions.TryGetValue(functionPredefinedNode.Name, out var function))
                {
                    var argValues = functionPredefinedNode.Args.Select(arg => (double)Visit(arg)).ToArray();
                    return function(argValues);
                }
                else
                {
                    throw new Exception($"Undefined function {functionPredefinedNode.Name}");
                }
            case BinaryExpressionNode binaryExpressionNode:
                var left = Visit(binaryExpressionNode.Left);
                if (left is ValueNode vn) left = vn.Value;
                var right = Visit(binaryExpressionNode.Right);
                if (right is ValueNode vn2) right = vn2.Value;
                if (binaryExpressionNode.Operator == "+" && left is string leftStr && right is string rightStr)
                {
                    return leftStr + rightStr;
                }
                else if (binaryExpressionNode.Operator == "==" && left is string leftStr2 && right is string rightStr2)
                {
                    return leftStr2 == rightStr2;
                }
                else if (binaryExpressionNode.Operator == "!=" && left is string leftStr3 && right is string rightStr3)
                {
                    return leftStr3 != rightStr3;
                }
                else if (left is double leftNum && right is double rightNum)
                {
                    return binaryExpressionNode.Operator switch
                    {
                        "+" => leftNum + rightNum,
                        "-" => leftNum - rightNum,
                        "*" => leftNum * rightNum,
                        "/" => leftNum / rightNum,
                        "^" => Math.Pow(leftNum, rightNum),
                        "<" => leftNum < rightNum,
                        ">" => leftNum > rightNum,
                        "<=" => leftNum <= rightNum,
                        ">=" => leftNum >= rightNum,
                        "==" => leftNum == rightNum,
                        "!=" => leftNum != rightNum,
                        _ => throw new Exception($"Unexpected operator {binaryExpressionNode.Operator}")
                    };
                }
                else
                {
                    throw new Exception($"Invalid operands for operator {binaryExpressionNode.Operator}");
                }
            case IfExpressionNode ifExpressionNode:
                var condition = Visit(ifExpressionNode.Condition);
                if (condition is bool conditionBool)
                {
                    if (conditionBool)
                    {
                        return Visit(ifExpressionNode.ThenBody);
                    }
                    else
                    {
                        return Visit(ifExpressionNode.ElseBody);
                    }
                }
                else
                {
                    throw new Exception($"Invalid condition type {condition.GetType()}");
                }
            case IdentifierNode identifierNode:
                if (variables.TryGetValue(identifierNode.Identifier, out var value))
                {
                    return value;
                }
                else if (LE.CDN.Any(c => c.Identifier == identifierNode.Identifier))
                {
                    return Visit(LE.CDN.First(c => c.Identifier == identifierNode.Identifier).Value); 
                }
                else
                {
                    throw new Exception($"Undefined variable {identifierNode.Identifier}");
                }
            case VariableDeclarationNode varDecl:
                variables[varDecl.Identifier] = Visit(varDecl.Value);
                return Visit(varDecl.Body);
            case MultipleVariableDeclarationNode multipleVarDecl:
                foreach (var varDecl in multipleVarDecl.Declarations)
                {
                    variables[varDecl.Identifier] = Visit(varDecl.Value);
                }
                return Visit(multipleVarDecl.Body);
            default:
                throw new Exception($"Unexpected node type {node.GetType()}");
        }
    }
}