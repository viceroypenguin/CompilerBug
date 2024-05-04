using Microsoft.CodeAnalysis;

namespace Generator1;

[Generator]
public sealed class Generator : IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        var names = context.SyntaxProvider.ForAttributeWithMetadataName(
            "Immediate.Validations.Shared.ValidateAttribute",
            (_, _) => true,
            (n, ct) => ((INamedTypeSymbol)n.TargetSymbol).Name
        );

        context.RegisterSourceOutput(
            names,
            (spc, n) =>
                spc.AddSource(
                    $"Generator.{n}.g.cs",
                    $$"""
                    
                    using System.Collections.Generic;
                    using Immediate.Validations.Shared;

                    #pragma warning disable CS1591

                    namespace Immediate.Validations.FunctionalTests.IntegrationTests;

                    partial class SaveRecord
                    {

                        partial record Command : IValidationTarget<Command>
                        {
                            public static List<ValidationError> Validate(Command target)
                            {
                                var errors = new List<ValidationError>();

                                {

                                    if (
                                        global::Immediate.Validations.Shared.NotNullAttribute.ValidateProperty(
                                            target.Name
                                        ) is (true, var message)
                                    )
                                    {
                                        errors.Add(new()
                                        {
                                            PropertyName = "Name",
                                            ErrorMessage = null ?? message,
                                        });
                                    }
                                }
                                {

                                    if (
                                        global::Immediate.Validations.Shared.NotEmptyOrWhiteSpaceAttribute.ValidateProperty(
                                            target.Name
                                        ) is (true, var message)
                                    )
                                    {
                                        errors.Add(new()
                                        {
                                            PropertyName = "Name",
                                            ErrorMessage = "Name must be provided." ?? message,
                                        });
                                    }
                                }
                                {

                                    if (
                                        global::Immediate.Validations.Shared.EnumValueAttribute.ValidateProperty(
                                            target.Status
                                        ) is (true, var message)
                                    )
                                    {
                                        errors.Add(new()
                                        {
                                            PropertyName = "Status",
                                            ErrorMessage = null ?? message,
                                        });
                                    }
                                }
                                {

                                    if (
                                        global::Immediate.Validations.FunctionalTests.IntegrationTests.SaveRecord.GreaterThanAttribute.ValidateProperty(
                                            target.Value
                                            , operand: 0
                                        ) is (true, var message)
                                    )
                                    {
                                        errors.Add(new()
                                        {
                                            PropertyName = "Value",
                                            ErrorMessage = null ?? message,
                                        });
                                    }
                                }

                                return errors;
                            }
                        }

                    }
                    
                    """
                )
            );
    }
}
