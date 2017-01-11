using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cake.MetadataGenerator.Documentation;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Cake.MetadataGenerator.SyntaxRewriters
{
    public class CakeMethodBodySyntaxRewriter : CSharpSyntaxRewriter
    {
        private readonly SemanticModel _semanticModel;
        private readonly IDocumentationProvider _documentationProvider;

        public CakeMethodBodySyntaxRewriter(SemanticModel semanticModel, IDocumentationProvider documentationProvider)
        {
            _semanticModel = semanticModel;
            _documentationProvider = documentationProvider;
        }

        public override SyntaxNode VisitMethodDeclaration(MethodDeclarationSyntax node)
        {
            var methodSymbol = _semanticModel.GetDeclaredSymbol(node);
            var comment = methodSymbol.GetDocumentationCommentId();

            var statements = new List<StatementSyntax>();

            if (node.AttributeLists.Any(list => list.Attributes.Any(attr => attr.Name == IdentifierName(""))))
            {

            }

            var outParams = node.ParameterList.Parameters
                                              .Where(val => _semanticModel.GetDeclaredSymbol(val).RefKind == RefKind.Out)
                                              .ToList();

            if (outParams.Any())
            {
                var outAssignments = outParams.Select(val => ExpressionStatement(AssignmentExpression(
                    SyntaxKind.SimpleAssignmentExpression,
                    IdentifierName(val.Identifier),
                    DefaultExpression(val.Type))));

                statements.AddRange(outAssignments);
            }

            if (node.ReturnType != PredefinedType(Token(SyntaxKind.VoidKeyword)))
            {
                statements.Add(ReturnStatement(DefaultExpression(node.ReturnType)));
            }
            var text = _documentationProvider.Get(comment);

            var syntaxToken = Token(TriviaList(Comment(text)), SyntaxKind.None, TriviaList(CarriageReturn));
            var syntaxTokenList =
                new SyntaxTokenList().Add(syntaxToken)
                    .AddRange(node.Modifiers);

            node = node.WithParameterList(ParameterList(SeparatedList(node.ParameterList.Parameters.Skip(1))))
                        .WithAttributeLists(new SyntaxList<AttributeListSyntax>())
                        .WithBody(Block(statements))
                        .WithModifiers(syntaxTokenList);

            return base.VisitMethodDeclaration(node);
        }

        public static string GetFullMetadataName(INamespaceOrTypeSymbol symbol)
        {
            ISymbol s = symbol;
            var sb = new StringBuilder(s.MetadataName);

            var last = s;
            s = s.ContainingSymbol;
            while (!IsRootNamespace(s))
            {
                if (s is ITypeSymbol && last is ITypeSymbol)
                {
                    sb.Insert(0, '+');
                }
                else
                {
                    sb.Insert(0, '.');
                }
                sb.Insert(0, s.MetadataName);
                s = s.ContainingSymbol;
            }

            return sb.ToString();
        }

        private static bool IsRootNamespace(ISymbol s)
        {
            return s is INamespaceSymbol && ((INamespaceSymbol)s).IsGlobalNamespace;
        }
    }
}
