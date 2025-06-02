namespace Delegates;

public static class FunctionExtensions
{
    public static IEnumerable<T> GenerateProgression<T>(T first, Func<T, T>? formula, int count)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(count);
        ArgumentNullException.ThrowIfNull(formula);

        return GeneratorCore();

        IEnumerable<T> GeneratorCore()
        {
            T current = first;
            yield return current;
            for (int i = 1; i < count; i++)
            {
                current = formula(current);
                yield return current;
            }
        }
    }

    public static IEnumerable<T> GenerateProgression<T>(T first, Func<T, T>? formula, Predicate<T>? finished)
    {
        ArgumentNullException.ThrowIfNull(formula);
        ArgumentNullException.ThrowIfNull(finished);

        return GeneratorCore();

        IEnumerable<T> GeneratorCore()
        {
            T current = first;
            while (!finished(current))
            {
                yield return current;
                current = formula(current);
            }
        }
    }

    public static T GetElement<T>(T first, Func<T, T>? formula, int number)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(number);
        ArgumentNullException.ThrowIfNull(formula);

        T result = first;
        for (int i = 1; i < number; i++)
        {
            result = formula(result);
        }

        return result;
    }

    public static T Calculate<T>(T first, Func<T, T>? formula, Func<T, T, T>? operation, int count)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(count);
        ArgumentNullException.ThrowIfNull(formula);
        ArgumentNullException.ThrowIfNull(operation);

        T current = first;
        T value = current;
        for (int i = 1; i < count; i++)
        {
            current = formula(current);
            value = operation(value, current);
        }

        return value;
    }

    public static IEnumerable<T> GenerateSequence<T>(T first, T second, Func<T, T, T>? formula, int count)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(count);
        ArgumentNullException.ThrowIfNull(formula);

        return GeneratorCore();

        IEnumerable<T> GeneratorCore()
        {
            if (count >= 1)
            {
                yield return first;
            }

            if (count >= 2)
            {
                yield return second;
            }

            if (count <= 2)
            {
                yield break;
            }

            T prev2 = first;
            T prev1 = second;
            for (int i = 2; i < count; i++)
            {
                T next = formula(prev2, prev1);
                yield return next;
                prev2 = prev1;
                prev1 = next;
            }
        }
    }

    public static Predicate<T> CombinePredicates<T>(params Predicate<T>[]? predicates)
    {
        ArgumentNullException.ThrowIfNull(predicates);

        return x =>
        {
            foreach (var pred in predicates)
            {
                if (pred == null)
                {
                    continue;
                }

                if (!pred(x))
                {
                    return false;
                }
            }

            return true;
        };
    }

    public static T FindMax<T>(T lhs, T rhs, Comparison<T>? comparer)
    {
        ArgumentNullException.ThrowIfNull(comparer);
        return comparer(lhs, rhs) >= 0 ? lhs : rhs;
    }
}
