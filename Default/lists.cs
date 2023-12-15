global using Lists;
using Nodes;

namespace Lists
{
    public static class LE
    {
        /// <summary>
        /// A dictionary of predefined mathematical and statistical functions.
        /// </summary>
        public static readonly Dictionary<string, Func<double[], double>> predefinedFunctions = new()
        {
            // Trigonometric functions
            { "Sin", args => Math.Sin(args[0]) },
            { "Cos", args => Math.Cos(args[0]) },
            { "Tan", args => Math.Tan(args[0]) },
            // Square root function
            { "Sqrt", args => Math.Sqrt(args[0]) },
            // Power function
            { "Pow", args => Math.Pow(args[0], args[1]) },
            // Absolute value function
            { "Abs", args => Math.Abs(args[0]) },
            // Logarithmic functions
            { "Log", args => args.Length == 1 ? Math.Log(args[0]) : Math.Log(args[0], args[1]) },
            { "Log10", args => Math.Log10(args[0]) },
            // Exponential function
            { "Exp", args => Math.Exp(args[0]) },
            // Factorial function
            { "Fact", args => Enumerable.Range(1, (int)args[0]).Aggregate(1, (p, item) => p * item) },
            // Random number generator between first and second arguments
            { "Random", args => new Random().Next((int)args[0], (int)args[1])}
        };
        /// <summary>
        /// A list of constant declarations, each represented as a ConstDeclarationNode.
        /// </summary>
        public static readonly List<ConstDeclarationNode> CDN = new()
        {
            // PI constant, approximately equal to 3.14159
            new ConstDeclarationNode("PI", new ValueNode(Math.PI)),
            // E constant, approximately equal to 2.71828
            new ConstDeclarationNode("E", new ValueNode(Math.E)),
            // Gravitational constant, approximately equal to 6.67430
            new ConstDeclarationNode("G", new ValueNode(6.67430)),
        };
    }
}