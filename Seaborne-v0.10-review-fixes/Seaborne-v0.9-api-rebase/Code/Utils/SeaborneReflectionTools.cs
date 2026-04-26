using System.Collections;
using System.Reflection;

namespace Seaborne.Utils;

public static class SeaborneReflectionTools
{
    public static object? GetPlayer()
    {
        object? instance = GetRunStateInstance();

        return instance?.GetType().GetProperty("Player")?.GetValue(instance)
            ?? instance?.GetType().GetField("Player")?.GetValue(instance);
    }

    public static int GetPowerStacks(object creature, string powerId)
    {
        IEnumerable? powers = GetMemberValue(creature, "Powers") as IEnumerable;
        if (powers is null)
        {
            return 0;
        }

        foreach (object power in powers)
        {
            string? id = GetMemberValue(power, "Id")?.ToString()
                ?? GetMemberValue(power, "ID")?.ToString()
                ?? GetMemberValue(power, "PowerId")?.ToString();

            if (!PowerIdMatches(id, powerId))
            {
                continue;
            }

            object? amount = GetMemberValue(power, "Amount")
                ?? GetMemberValue(power, "Stacks")
                ?? GetMemberValue(power, "StackAmount");

            return amount is int value ? value : 0;
        }

        return 0;
    }

    public static void SetPowerStacks(object creature, string powerId, int stacks)
    {
        IEnumerable? powers = GetMemberValue(creature, "Powers") as IEnumerable;
        if (powers is null)
        {
            return;
        }

        foreach (object power in powers)
        {
            string? id = GetMemberValue(power, "Id")?.ToString()
                ?? GetMemberValue(power, "ID")?.ToString()
                ?? GetMemberValue(power, "PowerId")?.ToString();

            if (!PowerIdMatches(id, powerId))
            {
                continue;
            }

            SetMemberValue(power, "Amount", stacks);
            SetMemberValue(power, "Stacks", stacks);
            SetMemberValue(power, "StackAmount", stacks);
            return;
        }
    }

    public static IEnumerable<object> GetEnemies()
    {
        object? instance = GetRunStateInstance();

        object? combat = instance is null ? null : GetMemberValue(instance, "Combat") ?? GetMemberValue(instance, "CurrentCombat");
        object? enemies = combat is null ? null : GetMemberValue(combat, "Enemies") ?? GetMemberValue(combat, "Monsters");

        if (enemies is IEnumerable enumerable)
        {
            return enumerable.Cast<object>().ToList();
        }

        return [];
    }

    public static object? GetMemberValue(object instance, string name)
    {
        Type type = instance.GetType();
        return type.GetProperty(name, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)?.GetValue(instance)
            ?? type.GetField(name, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)?.GetValue(instance);
    }

    public static bool SetMemberValue(object instance, string name, object? value)
    {
        Type type = instance.GetType();
        PropertyInfo? prop = type.GetProperty(name, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
        if (prop is not null && prop.CanWrite)
        {
            prop.SetValue(instance, value);
            return true;
        }

        FieldInfo? field = type.GetField(name, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
        if (field is not null)
        {
            field.SetValue(instance, value);
            return true;
        }

        return false;
    }
    public static object? GetRunStateInstance()
    {
        string[] candidateTypeNames =
        [
            "MegaCrit.Sts2.Core.Runs.RunState, sts2",
            "MegaCrit.Sts2.Core.Gameplay.RunState, sts2",
            "MegaCrit.Sts2.Core.RunState, sts2"
        ];

        foreach (string typeName in candidateTypeNames)
        {
            Type? runStateType = Type.GetType(typeName);
            object? instance = runStateType?.GetProperty("Current", BindingFlags.Public | BindingFlags.Static)?.GetValue(null)
                ?? runStateType?.GetProperty("Instance", BindingFlags.Public | BindingFlags.Static)?.GetValue(null)
                ?? runStateType?.GetProperty("CurrentRun", BindingFlags.Public | BindingFlags.Static)?.GetValue(null);

            if (instance is not null)
            {
                return instance;
            }
        }

        return AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(assembly => assembly.GetTypes())
            .Where(type => type.FullName is not null && type.FullName.EndsWith(".RunState", StringComparison.Ordinal))
            .Select(type => type.GetProperty("Current", BindingFlags.Public | BindingFlags.Static)?.GetValue(null)
                            ?? type.GetProperty("Instance", BindingFlags.Public | BindingFlags.Static)?.GetValue(null)
                            ?? type.GetProperty("CurrentRun", BindingFlags.Public | BindingFlags.Static)?.GetValue(null))
            .FirstOrDefault(instance => instance is not null);
    }

    public static bool PowerIdMatches(string? actual, string expected)
    {
        if (string.IsNullOrWhiteSpace(actual))
        {
            return false;
        }

        if (string.Equals(actual, expected, StringComparison.OrdinalIgnoreCase))
        {
            return true;
        }

        foreach (string alias in SeabornePowerIdAliases.GetAliases(expected))
        {
            if (string.Equals(actual, alias, StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }
        }

        return false;
    }

    public static void AddPowerStacks(object creature, string powerId, int delta)
    {
        int current = GetPowerStacks(creature, powerId);
        SetPowerStacks(creature, powerId, Math.Max(0, current + delta));
    }

    public static bool TryAddEnergy(object player, int amount)
    {
        foreach (string memberName in new[] { "Energy", "CurrentEnergy", "EnergyThisTurn" })
        {
            object? value = GetMemberValue(player, memberName);
            if (value is int current && SetMemberValue(player, memberName, current + amount))
            {
                return true;
            }

            if (value is not null)
            {
                object? nestedCurrent = GetMemberValue(value, "Current") ?? GetMemberValue(value, "Amount") ?? GetMemberValue(value, "Value");
                if (nestedCurrent is int nested && SetMemberValue(value, "Current", nested + amount))
                {
                    return true;
                }
                if (nestedCurrent is int nested2 && SetMemberValue(value, "Amount", nested2 + amount))
                {
                    return true;
                }
                if (nestedCurrent is int nested3 && SetMemberValue(value, "Value", nested3 + amount))
                {
                    return true;
                }
            }
        }

        return false;
    }

}
