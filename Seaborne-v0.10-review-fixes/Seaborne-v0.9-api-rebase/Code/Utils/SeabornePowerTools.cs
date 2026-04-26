using System.Reflection;
using MegaCrit.Sts2.Core.Commands;

namespace Seaborne.Utils;

public static class SeabornePowerTools
{
    public static async Task ApplyPowerToPlayer(string powerId, int stacks)
    {
        object? player = SeaborneReflectionTools.GetPlayer();
        if (player is null || stacks == 0)
        {
            return;
        }

        await InvokeCommonApplyPower(player, player, powerId, stacks);
    }

    public static async Task ApplyPowerToTarget(object target, string powerId, int stacks)
    {
        object? player = SeaborneReflectionTools.GetPlayer();
        if (player is null || stacks == 0)
        {
            return;
        }

        await InvokeCommonApplyPower(player, target, powerId, stacks);
    }

    private static async Task InvokeCommonApplyPower(object source, object target, string powerId, int stacks)
    {
        Type? commonActionsType = Type.GetType("BaseLib.Utils.CommonActions, BaseLib");
        if (commonActionsType is not null)
        {
            foreach (string candidateId in SeabornePowerIdAliases.GetAliases(powerId))
            {
                if (await TryInvokeCommonAction(commonActionsType, source, target, candidateId, stacks))
                {
                    return;
                }
            }
        }

        SeaborneReflectionTools.AddPowerStacks(target, powerId, stacks);
    }

    private static async Task<bool> TryInvokeCommonAction(Type commonActionsType, object source, object target, string powerId, int stacks)
    {
        MethodInfo[] methods = commonActionsType
            .GetMethods(BindingFlags.Public | BindingFlags.Static)
            .Where(method => method.Name.Contains("Apply", StringComparison.OrdinalIgnoreCase)
                             && method.Name.Contains("Power", StringComparison.OrdinalIgnoreCase))
            .OrderByDescending(ScoreApplyPowerMethod)
            .ToArray();

        foreach (MethodInfo method in methods)
        {
            object?[]? args = BuildApplyPowerArgs(method, source, target, powerId, stacks);
            if (args is null)
            {
                continue;
            }

            try
            {
                object? result = method.Invoke(null, args);
                if (result is Task task)
                {
                    await task;
                }

                return true;
            }
            catch
            {
                // Try the next overload; BaseLib has changed these signatures between builds.
            }
        }

        return false;
    }

    private static int ScoreApplyPowerMethod(MethodInfo method)
    {
        ParameterInfo[] parameters = method.GetParameters();
        int score = 0;
        score += parameters.Any(parameter => parameter.ParameterType == typeof(string)) ? 10 : 0;
        score += parameters.Any(parameter => parameter.ParameterType == typeof(int)) ? 10 : 0;
        score += parameters.Length is >= 3 and <= 5 ? 5 : 0;
        return score;
    }

    private static object?[]? BuildApplyPowerArgs(MethodInfo method, object source, object target, string powerId, int stacks)
    {
        ParameterInfo[] parameters = method.GetParameters();
        object?[] args = new object?[parameters.Length];
        bool usedSource = false;
        bool usedTarget = false;
        bool usedPowerId = false;
        bool usedStacks = false;

        for (int index = 0; index < parameters.Length; index++)
        {
            Type parameterType = parameters[index].ParameterType;
            string parameterName = parameters[index].Name ?? string.Empty;

            if (parameterType == typeof(string) && !usedPowerId)
            {
                args[index] = powerId;
                usedPowerId = true;
            }
            else if (parameterType == typeof(int) && !usedStacks)
            {
                args[index] = stacks;
                usedStacks = true;
            }
            else if (!usedSource && (parameterName.Contains("source", StringComparison.OrdinalIgnoreCase) || parameterName.Contains("owner", StringComparison.OrdinalIgnoreCase)))
            {
                args[index] = source;
                usedSource = true;
            }
            else if (!usedTarget && (parameterName.Contains("target", StringComparison.OrdinalIgnoreCase) || parameterName.Contains("creature", StringComparison.OrdinalIgnoreCase)))
            {
                args[index] = target;
                usedTarget = true;
            }
            else if (!usedTarget && parameterType.IsInstanceOfType(target))
            {
                args[index] = target;
                usedTarget = true;
            }
            else if (!usedSource && parameterType.IsInstanceOfType(source))
            {
                args[index] = source;
                usedSource = true;
            }
            else if (parameterType.IsValueType)
            {
                args[index] = Activator.CreateInstance(parameterType);
            }
            else
            {
                args[index] = null;
            }
        }

        return usedPowerId && usedStacks ? args : null;
    }
}
