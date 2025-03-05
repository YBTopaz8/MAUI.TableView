using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using System;
using System.Runtime.CompilerServices;
using ListViewSelectionMode = Microsoft.UI.Xaml.Controls.ListViewSelectionMode;

namespace YB.MauiDataGridView.Extensions;

/// <summary>
/// Provides extension methods for the Type class.
/// </summary>
internal static class TypeExtensions
{
    /// <summary>
    /// Determines whether the specified type is a Boolean.
    /// </summary>
    /// <param name="type">The type to check.</param>
    /// <returns>True if the type is Boolean; otherwise, false.</returns>
    public static bool IsBoolean(this Type type)
    {
        return type == typeof(bool) || type == typeof(bool?);
    }

    /// <summary>
    /// Determines whether the specified type is a numeric.
    /// </summary>
    /// <param name="type">The type to check.</param>
    /// <returns>True if the type is numeric; otherwise, false.</returns>
    public static bool IsNumeric(this Type type)
    {
        return type == typeof(byte) || type == typeof(byte?) ||
               type == typeof(sbyte) || type == typeof(sbyte?) ||
               type == typeof(short) || type == typeof(short?) ||
               type == typeof(ushort) || type == typeof(ushort?) ||
               type == typeof(int) || type == typeof(int?) ||
               type == typeof(uint) || type == typeof(uint?) ||
               type == typeof(long) || type == typeof(long?) ||
               type == typeof(ulong) || type == typeof(ulong?) ||
               type == typeof(float) || type == typeof(float?) ||
               type == typeof(double) || type == typeof(double?) ||
               type == typeof(decimal) || type == typeof(decimal?);
    }

    /// <summary>
    /// Determines whether the specified type is a TimeSpan or nullable TimeSpan.
    /// </summary>
    /// <param name="type">The type to check.</param>
    /// <returns>True if the type is TimeSpan or nullable TimeSpan; otherwise, false.</returns>
    public static bool IsTimeSpan(this Type? type)
    {
        return type == typeof(TimeSpan) || type == typeof(TimeSpan?);
    }

    /// <summary>
    /// Determines whether the specified type is a TimeOnly or nullable TimeOnly.
    /// </summary>
    /// <param name="type">The type to check.</param>
    /// <returns>True if the type is TimeOnly or nullable TimeOnly; otherwise, false.</returns>
    public static bool IsTimeOnly(this Type? type)
    {
        return type == typeof(TimeOnly) || type == typeof(TimeOnly?);
    }

    /// <summary>
    /// Determines whether the specified type is a DateOnly or nullable DateOnly.
    /// </summary>
    /// <param name="type">The type to check.</param>
    /// <returns>True if the type is DateOnly or nullable DateOnly; otherwise, false.</returns>
    public static bool IsDateOnly(this Type? type)
    {
        return type == typeof(DateOnly) || type == typeof(DateOnly?);
    }

    /// <summary>
    /// Determines whether the specified type is a DateTime or nullable DateTime.
    /// </summary>
    /// <param name="type">The type to check.</param>
    /// <returns>True if the type is DateTime or nullable DateTime; otherwise, false.</returns>
    public static bool IsDateTime(this Type? type)
    {
        return type == typeof(DateTime) || type == typeof(DateTime?);
    }

    /// <summary>
    /// Determines whether the specified type is a DateTimeOffset or nullable DateTimeOffset.
    /// </summary>
    /// <param name="type">The type to check.</param>
    /// <returns>True if the type is DateTimeOffset or nullable DateTimeOffset; otherwise, false.</returns>
    public static bool IsDateTimeOffset(this Type? type)
    {
        return type == typeof(DateTimeOffset) || type == typeof(DateTimeOffset?);
    }

    /// <summary>
    /// Determines whether the specified type is a nullable type.
    /// </summary>
    public static bool IsNullableType(this Type? type)
    {
        return type is not null && type.GetTypeInfo().IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>);
    }

    /// <summary>
    /// Gets the underlying type argument of a nullable type.
    /// </summary>
    public static Type GetNonNullableType(this Type type)
    {
        return type.IsNullableType() ? Nullable.GetUnderlyingType(type)! : type;
    }

    /// <summary>
    /// Determines whether the specified type is a primitive type.
    /// </summary>
    public static bool IsPrimitive(this Type? dataType)
    {
        return dataType is not null &&
            (dataType.GetTypeInfo().IsPrimitive ||
             dataType == typeof(string) ||
             dataType == typeof(decimal) ||
             dataType == typeof(DateTime));
    }

    /// <summary>
    /// Determines whether the specified type is inherited from <see cref="IComparable"/>.
    /// </summary>
    public static bool IsInheritedFromIComparable(this Type type)
    {
        return type.GetInterfaces().Any(i => i == typeof(IComparable));
    }
}


/// <summary>
/// Provides attached dependency properties for the <see cref="ListViewBase"/>
/// </summary>
public static partial class ListViewExtensions
{
    /// <summary>
    /// Deselects the provided item.
    /// </summary>
    /// <typeparam name="T">Type of item.</typeparam>
    /// <param name="list"><see cref="ListViewBase"/></param>
    /// <param name="item">Item to deselect.</param>
    public static void DeselectItem<T>(this ListViewBase list, T item)
        where T : DependencyObject
    {
        switch (list.SelectionMode)
        {
            case ListViewSelectionMode.Single:
                if (list.SelectedItem == (object)item)
                {
                    list.SelectedItem = null;
                }

                break;
            case ListViewSelectionMode.Multiple:
            case ListViewSelectionMode.Extended:
                list.DeselectRange(new ItemIndexRange(list.IndexFromContainer(item), 1));
                break;
        }
    }

    /// <summary>
    /// Deselects all items in list.
    /// </summary>
    /// <param name="list"><see cref="ListViewBase"/></param>
    public static void DeselectAll(this ListViewBase list)
    {
        switch (list.SelectionMode)
        {
            case ListViewSelectionMode.Single:
                list.SelectedItem = null;
                break;
            case ListViewSelectionMode.Multiple:
            case ListViewSelectionMode.Extended:
                list.DeselectRange(new ItemIndexRange(0, (uint)list.Items.Count));
                break;
        }
    }

    /// <summary>
    /// Selects all items in the list (or first one), if possible.
    /// </summary>
    /// <param name="list"><see cref="ListViewBase"/></param>
    public static void SelectAllSafe(this ListViewBase list)
    {
        switch (list.SelectionMode)
        {
            case ListViewSelectionMode.Single:
                list.SelectedItem = list.Items.FirstOrDefault();
                break;
            case ListViewSelectionMode.Multiple:
            case ListViewSelectionMode.Extended:
                list.SelectRange(new ItemIndexRange(0, (uint)list.Items.Count));
                break;
        }
    }
}

/// <summary>
/// Provides attached dependency properties for the <see cref="DependencyObject"/> type.
/// </summary>
public static class DependencyObjectExtensions
{
    /// <summary>
    /// Find the first descendant of type <see cref="FrameworkElement"/> with a given name, using a depth-first search.
    /// </summary>
    /// <param name="element">The root element.</param>
    /// <param name="name">The name of the element to look for.</param>
    /// <param name="comparisonType">The comparison type to use to match <paramref name="name"/>.</param>
    /// <returns>The descendant that was found, or <see langword="null"/>.</returns>
    public static FrameworkElement? FindDescendant(this DependencyObject element, string name, StringComparison comparisonType = StringComparison.Ordinal)
    {
        PredicateByName predicateByName = new(name, comparisonType);

        return FindDescendant<FrameworkElement, PredicateByName>(element, ref predicateByName);
    }

    /// <summary>
    /// Find the first descendant element of a given type, using a depth-first search.
    /// </summary>
    /// <typeparam name="T">The type of elements to match.</typeparam>
    /// <param name="element">The root element.</param>
    /// <returns>The descendant that was found, or <see langword="null"/>.</returns>
    public static T? FindDescendant<T>(this DependencyObject element)
#if HAS_UNO
		where T : class, DependencyObject // Note: In Uno, DependencyObject is an interface, background: https://github.com/unoplatform/uno/issues/25
#else
        where T : notnull, DependencyObject
#endif
    {
        PredicateByAny<T> predicateByAny = default;

        return FindDescendant<T, PredicateByAny<T>>(element, ref predicateByAny);
    }

    /// <summary>
    /// Find the first descendant element of a given type, using a depth-first search.
    /// </summary>
    /// <param name="element">The root element.</param>
    /// <param name="type">The type of element to match.</param>
    /// <returns>The descendant that was found, or <see langword="null"/>.</returns>
    public static DependencyObject? FindDescendant(this DependencyObject element, Type type)
    {
        PredicateByType predicateByType = new(type);

        return FindDescendant<DependencyObject, PredicateByType>(element, ref predicateByType);
    }

    /// <summary>
    /// Find the first descendant element matching a given predicate, using a depth-first search.
    /// </summary>
    /// <typeparam name="T">The type of elements to match.</typeparam>
    /// <param name="element">The root element.</param>
    /// <param name="predicate">The predicatee to use to match the descendant nodes.</param>
    /// <returns>The descendant that was found, or <see langword="null"/>.</returns>
    public static T? FindDescendant<T>(this DependencyObject element, Func<T, bool> predicate)
#if HAS_UNO
        where T : class, DependencyObject
#else
        where T : notnull, DependencyObject
#endif
    {
        PredicateByFunc<T> predicateByFunc = new(predicate);

        return FindDescendant<T, PredicateByFunc<T>>(element, ref predicateByFunc);
    }

    /// <summary>
    /// Find the first descendant element matching a given predicate, using a depth-first search.
    /// </summary>
    /// <typeparam name="T">The type of elements to match.</typeparam>
    /// <typeparam name="TState">The type of state to use when matching nodes.</typeparam>
    /// <param name="element">The root element.</param>
    /// <param name="state">The state to give as input to <paramref name="predicate"/>.</param>
    /// <param name="predicate">The predicatee to use to match the descendant nodes.</param>
    /// <returns>The descendant that was found, or <see langword="null"/>.</returns>
    public static T? FindDescendant<T, TState>(this DependencyObject element, TState state, Func<T, TState, bool> predicate)
#if HAS_UNO
		where T : class, DependencyObject
#else
        where T : notnull, DependencyObject
#endif
    {
        PredicateByFunc<T, TState> predicateByFunc = new(state, predicate);

        return FindDescendant<T, PredicateByFunc<T, TState>>(element, ref predicateByFunc);
    }

    /// <summary>
    /// Find the first descendant element matching a given predicate, using a depth-first search.
    /// </summary>
    /// <typeparam name="T">The type of elements to match.</typeparam>
    /// <typeparam name="TPredicate">The type of predicate in use.</typeparam>
    /// <param name="element">The root element.</param>
    /// <param name="predicate">The predicatee to use to match the descendant nodes.</param>
    /// <returns>The descendant that was found, or <see langword="null"/>.</returns>
    private static T? FindDescendant<T, TPredicate>(this DependencyObject element, ref TPredicate predicate)
#if HAS_UNO
        where T : class, DependencyObject
#else
        where T : notnull, DependencyObject
#endif
        where TPredicate : struct, IPredicate<T>
    {
        int childrenCount = VisualTreeHelper.GetChildrenCount(element);

        for (var i = 0; i < childrenCount; i++)
        {
            DependencyObject child = VisualTreeHelper.GetChild(element, i);

            if (child is T result && predicate.Match(result))
            {
                return result;
            }

            T? descendant = FindDescendant<T, TPredicate>(child, ref predicate);

            if (descendant is not null)
            {
                return descendant;
            }
        }

        return null;
    }

    /// <summary>
    /// Find the first descendant (or self) of type <see cref="FrameworkElement"/> with a given name, using a depth-first search.
    /// </summary>
    /// <param name="element">The root element.</param>
    /// <param name="name">The name of the element to look for.</param>
    /// <param name="comparisonType">The comparison type to use to match <paramref name="name"/>.</param>
    /// <returns>The descendant (or self) that was found, or <see langword="null"/>.</returns>
    public static FrameworkElement? FindDescendantOrSelf(this DependencyObject element, string name, StringComparison comparisonType = StringComparison.Ordinal)
    {
        if (element is FrameworkElement result && name.Equals(result.Name, comparisonType))
        {
            return result;
        }

        return FindDescendant(element, name, comparisonType);
    }

    /// <summary>
    /// Find the first descendant (or self) element of a given type, using a depth-first search.
    /// </summary>
    /// <typeparam name="T">The type of elements to match.</typeparam>
    /// <param name="element">The root element.</param>
    /// <returns>The descendant (or self) that was found, or <see langword="null"/>.</returns>
    public static T? FindDescendantOrSelf<T>(this DependencyObject element)
#if HAS_UNO
		where T : class, DependencyObject
#else
        where T : notnull, DependencyObject
#endif
    {
        if (element is T result)
        {
            return result;
        }

        return FindDescendant<T>(element);
    }

    /// <summary>
    /// Find the first descendant (or self) element of a given type, using a depth-first search.
    /// </summary>
    /// <param name="element">The root element.</param>
    /// <param name="type">The type of element to match.</param>
    /// <returns>The descendant (or self) that was found, or <see langword="null"/>.</returns>
    public static DependencyObject? FindDescendantOrSelf(this DependencyObject element, Type type)
    {
        if (element.GetType() == type)
        {
            return element;
        }

        return FindDescendant(element, type);
    }

    /// <summary>
    /// Find the first descendant (or self) element matching a given predicate, using a depth-first search.
    /// </summary>
    /// <typeparam name="T">The type of elements to match.</typeparam>
    /// <param name="element">The root element.</param>
    /// <param name="predicate">The predicatee to use to match the descendant nodes.</param>
    /// <returns>The descendant (or self) that was found, or <see langword="null"/>.</returns>
    public static T? FindDescendantOrSelf<T>(this DependencyObject element, Func<T, bool> predicate)
#if HAS_UNO
		where T : class, DependencyObject
#else
        where T : notnull, DependencyObject
#endif
    {
        if (element is T result && predicate(result))
        {
            return result;
        }

        return FindDescendant(element, predicate);
    }

    /// <summary>
    /// Find the first descendant (or self) element matching a given predicate, using a depth-first search.
    /// </summary>
    /// <typeparam name="T">The type of elements to match.</typeparam>
    /// <typeparam name="TState">The type of state to use when matching nodes.</typeparam>
    /// <param name="element">The root element.</param>
    /// <param name="state">The state to give as input to <paramref name="predicate"/>.</param>
    /// <param name="predicate">The predicatee to use to match the descendant nodes.</param>
    /// <returns>The descendant (or self) that was found, or <see langword="null"/>.</returns>
    public static T? FindDescendantOrSelf<T, TState>(this DependencyObject element, TState state, Func<T, TState, bool> predicate)
#if HAS_UNO
		where T : class, DependencyObject
#else
        where T : notnull, DependencyObject
#endif
    {
        if (element is T result && predicate(result, state))
        {
            return result;
        }

        return FindDescendant(element, state, predicate);
    }

    /// <summary>
    /// Find all descendant elements of the specified element. This method can be chained with
    /// LINQ calls to add additional filters or projections on top of the returned results.
    /// <para>
    /// This method is meant to provide extra flexibility in specific scenarios and it should not
    /// be used when only the first item is being looked for. In those cases, use one of the
    /// available <see cref="FindDescendant{T}(DependencyObject)"/> overloads instead, which will
    /// offer a more compact syntax as well as better performance in those cases.
    /// </para>
    /// </summary>
    /// <param name="element">The root element.</param>
    /// <returns>All the descendant <see cref="DependencyObject"/> instance from <paramref name="element"/>.</returns>
    public static IEnumerable<DependencyObject> FindDescendants(this DependencyObject element)
    {
        int childrenCount = VisualTreeHelper.GetChildrenCount(element);

        for (var i = 0; i < childrenCount; i++)
        {
            DependencyObject child = VisualTreeHelper.GetChild(element, i);

            yield return child;

            foreach (DependencyObject childOfChild in FindDescendants(child))
            {
                yield return childOfChild;
            }
        }
    }

    /// <summary>
    /// Find the first ascendant of type <see cref="FrameworkElement"/> with a given name.
    /// </summary>
    /// <param name="element">The starting element.</param>
    /// <param name="name">The name of the element to look for.</param>
    /// <param name="comparisonType">The comparison type to use to match <paramref name="name"/>.</param>
    /// <returns>The ascendant that was found, or <see langword="null"/>.</returns>
    public static FrameworkElement? FindAscendant(this DependencyObject element, string name, StringComparison comparisonType = StringComparison.Ordinal)
    {
        PredicateByName predicateByName = new(name, comparisonType);

        return FindAscendant<FrameworkElement, PredicateByName>(element, ref predicateByName);
    }

    /// <summary>
    /// Find the first ascendant element of a given type.
    /// </summary>
    /// <typeparam name="T">The type of elements to match.</typeparam>
    /// <param name="element">The starting element.</param>
    /// <returns>The ascendant that was found, or <see langword="null"/>.</returns>
    public static T? FindAscendant<T>(this DependencyObject element)
#if HAS_UNO
		where T : class, DependencyObject
#else
        where T : notnull, DependencyObject
#endif
    {
        PredicateByAny<T> predicateByAny = default;

        return FindAscendant<T, PredicateByAny<T>>(element, ref predicateByAny);
    }

    /// <summary>
    /// Find the first ascendant element of a given type.
    /// </summary>
    /// <param name="element">The starting element.</param>
    /// <param name="type">The type of element to match.</param>
    /// <returns>The ascendant that was found, or <see langword="null"/>.</returns>
    public static DependencyObject? FindAscendant(this DependencyObject element, Type type)
    {
        PredicateByType predicateByType = new(type);

        return FindAscendant<DependencyObject, PredicateByType>(element, ref predicateByType);
    }

    /// <summary>
    /// Find the first ascendant element matching a given predicate.
    /// </summary>
    /// <typeparam name="T">The type of elements to match.</typeparam>
    /// <param name="element">The starting element.</param>
    /// <param name="predicate">The predicatee to use to match the ascendant nodes.</param>
    /// <returns>The ascendant that was found, or <see langword="null"/>.</returns>
    public static T? FindAscendant<T>(this DependencyObject element, Func<T, bool> predicate)
#if HAS_UNO
		where T : class, DependencyObject
#else
        where T : notnull, DependencyObject
#endif
    {
        PredicateByFunc<T> predicateByFunc = new(predicate);

        return FindAscendant<T, PredicateByFunc<T>>(element, ref predicateByFunc);
    }

    /// <summary>
    /// Find the first ascendant element matching a given predicate.
    /// </summary>
    /// <typeparam name="T">The type of elements to match.</typeparam>
    /// <typeparam name="TState">The type of state to use when matching nodes.</typeparam>
    /// <param name="element">The starting element.</param>
    /// <param name="state">The state to give as input to <paramref name="predicate"/>.</param>
    /// <param name="predicate">The predicatee to use to match the ascendant nodes.</param>
    /// <returns>The ascendant that was found, or <see langword="null"/>.</returns>
    public static T? FindAscendant<T, TState>(this DependencyObject element, TState state, Func<T, TState, bool> predicate)
#if HAS_UNO
		where T : class, DependencyObject
#else
        where T : notnull, DependencyObject
#endif
    {
        PredicateByFunc<T, TState> predicateByFunc = new(state, predicate);

        return FindAscendant<T, PredicateByFunc<T, TState>>(element, ref predicateByFunc);
    }

    /// <summary>
    /// Find the first ascendant element matching a given predicate.
    /// </summary>
    /// <typeparam name="T">The type of elements to match.</typeparam>
    /// <typeparam name="TPredicate">The type of predicate in use.</typeparam>
    /// <param name="element">The starting element.</param>
    /// <param name="predicate">The predicatee to use to match the ascendant nodes.</param>
    /// <returns>The ascendant that was found, or <see langword="null"/>.</returns>
    private static T? FindAscendant<T, TPredicate>(this DependencyObject element, ref TPredicate predicate)
#if HAS_UNO
		where T : class, DependencyObject
#else
        where T : notnull, DependencyObject
#endif
        where TPredicate : struct, IPredicate<T>
    {
        while (true)
        {
            DependencyObject? parent = VisualTreeHelper.GetParent(element);

            if (parent is null)
            {
                return null;
            }

            if (parent is T result && predicate.Match(result))
            {
                return result;
            }

            element = parent;
        }
    }

    /// <summary>
    /// Find the first ascendant (or self) of type <see cref="FrameworkElement"/> with a given name.
    /// </summary>
    /// <param name="element">The starting element.</param>
    /// <param name="name">The name of the element to look for.</param>
    /// <param name="comparisonType">The comparison type to use to match <paramref name="name"/>.</param>
    /// <returns>The ascendant (or self) that was found, or <see langword="null"/>.</returns>
    public static FrameworkElement? FindAscendantOrSelf(this DependencyObject element, string name, StringComparison comparisonType = StringComparison.Ordinal)
    {
        if (element is FrameworkElement result && name.Equals(result.Name, comparisonType))
        {
            return result;
        }

        return FindAscendant(element, name, comparisonType);
    }

    /// <summary>
    /// Find the first ascendant (or self) element of a given type.
    /// </summary>
    /// <typeparam name="T">The type of elements to match.</typeparam>
    /// <param name="element">The starting element.</param>
    /// <returns>The ascendant (or self) that was found, or <see langword="null"/>.</returns>
    public static T? FindAscendantOrSelf<T>(this DependencyObject element)
#if HAS_UNO
		where T : class, DependencyObject
#else
        where T : notnull, DependencyObject
#endif
    {
        if (element is T result)
        {
            return result;
        }

        return FindAscendant<T>(element);
    }

    /// <summary>
    /// Find the first ascendant (or self) element of a given type.
    /// </summary>
    /// <param name="element">The starting element.</param>
    /// <param name="type">The type of element to match.</param>
    /// <returns>The ascendant (or self) that was found, or <see langword="null"/>.</returns>
    public static DependencyObject? FindAscendantOrSelf(this DependencyObject element, Type type)
    {
        if (element.GetType() == type)
        {
            return element;
        }

        return FindAscendant(element, type);
    }

    /// <summary>
    /// Find the first ascendant (or self) element matching a given predicate.
    /// </summary>
    /// <typeparam name="T">The type of elements to match.</typeparam>
    /// <param name="element">The starting element.</param>
    /// <param name="predicate">The predicatee to use to match the ascendant nodes.</param>
    /// <returns>The ascendant (or self) that was found, or <see langword="null"/>.</returns>
    public static T? FindAscendantOrSelf<T>(this DependencyObject element, Func<T, bool> predicate)
#if HAS_UNO
        where T : class, DependencyObject
#else
        where T : notnull, DependencyObject
#endif
    {
        if (element is T result && predicate(result))
        {
            return result;
        }

        return FindAscendant(element, predicate);
    }

    /// <summary>
    /// Find the first ascendant (or self) element matching a given predicate.
    /// </summary>
    /// <typeparam name="T">The type of elements to match.</typeparam>
    /// <typeparam name="TState">The type of state to use when matching nodes.</typeparam>
    /// <param name="element">The starting element.</param>
    /// <param name="state">The state to give as input to <paramref name="predicate"/>.</param>
    /// <param name="predicate">The predicatee to use to match the ascendant nodes.</param>
    /// <returns>The ascendant (or self) that was found, or <see langword="null"/>.</returns>
    public static T? FindAscendantOrSelf<T, TState>(this DependencyObject element, TState state, Func<T, TState, bool> predicate)
#if HAS_UNO
		where T : class, DependencyObject
#else
        where T : notnull, DependencyObject
#endif
    {
        if (element is T result && predicate(result, state))
        {
            return result;
        }

        return FindAscendant(element, state, predicate);
    }

    /// <summary>
    /// Find all ascendant elements of the specified element. This method can be chained with
    /// LINQ calls to add additional filters or projections on top of the returned results.
    /// <para>
    /// This method is meant to provide extra flexibility in specific scenarios and it should not
    /// be used when only the first item is being looked for. In those cases, use one of the
    /// available <see cref="FindAscendant{T}(DependencyObject)"/> overloads instead, which will
    /// offer a more compact syntax as well as better performance in those cases.
    /// </para>
    /// </summary>
    /// <param name="element">The root element.</param>
    /// <returns>All the descendant <see cref="DependencyObject"/> instance from <paramref name="element"/>.</returns>
    public static IEnumerable<DependencyObject> FindAscendants(this DependencyObject element)
    {
        while (true)
        {
            DependencyObject? parent = VisualTreeHelper.GetParent(element);

            if (parent is null)
            {
                yield break;
            }

            yield return parent;

            element = parent;
        }
    }
    /// <summary>
    /// An <see cref="IPredicate{T}"/> type matching all instances of a given type.
    /// </summary>
    /// <typeparam name="T">The type of items to match.</typeparam>
    internal readonly struct PredicateByAny<T> : IPredicate<T>
        where T : class
    {
        /// <inheritdoc/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Match(T element)
        {
            return true;
        }
    }
    /// <summary>
    /// An interface representing a predicate for items of a given type.
    /// </summary>
    /// <typeparam name="T">The type of items to match.</typeparam>
    internal interface IPredicate<in T>
        where T : class
    {
        /// <summary>
        /// Performs a match with the current predicate over a target <typeparamref name="T"/> instance.
        /// </summary>
        /// <param name="element">The input element to match.</param>
        /// <returns>Whether the match evaluation was successful.</returns>
        bool Match(T element);
    }

    /// <summary>
    /// An <see cref="IPredicate{T}"/> type matching items of a given type.
    /// </summary>
    /// <typeparam name="T">The type of items to match.</typeparam>
    internal readonly struct PredicateByFunc<T> : IPredicate<T>
        where T : class
    {
        /// <summary>
        /// The predicatee to use to match items.
        /// </summary>
        private readonly Func<T, bool> predicate;

        /// <summary>
        /// Initializes a new instance of the <see cref="PredicateByFunc{T}"/> struct.
        /// </summary>
        /// <param name="predicate">The predicatee to use to match items.</param>
        public PredicateByFunc(Func<T, bool> predicate)
        {
            this.predicate = predicate;
        }

        /// <inheritdoc/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Match(T element)
        {
            return this.predicate(element);
        }
    }


    /// <summary>
    /// An <see cref="IPredicate{T}"/> type matching items of a given type.
    /// </summary>
    /// <typeparam name="T">The type of items to match.</typeparam>
    /// <typeparam name="TState">The type of state to use when matching items.</typeparam>
    internal readonly struct PredicateByFunc<T, TState> : IPredicate<T>
        where T : class
    {
        /// <summary>
        /// The state to give as input to <see name="predicate"/>.
        /// </summary>
        private readonly TState state;

        /// <summary>
        /// The predicatee to use to match items.
        /// </summary>
        private readonly Func<T, TState, bool> predicate;

        /// <summary>
        /// Initializes a new instance of the <see cref="PredicateByFunc{T, TState}"/> struct.
        /// </summary>
        /// <param name="state">The state to give as input to <paramref name="predicate"/>.</param>
        /// <param name="predicate">The predicatee to use to match items.</param>
        public PredicateByFunc(TState state, Func<T, TState, bool> predicate)
        {
            this.state = state;
            this.predicate = predicate;
        }

        /// <inheritdoc/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Match(T element)
        {
            return this.predicate(element, state);
        }
    }

    /// <summary>
    /// An <see cref="IPredicate{T}"/> type matching items of a given type.
    /// </summary>
    internal readonly struct PredicateByType : IPredicate<object>
    {
        /// <summary>
        /// The type of element to match.
        /// </summary>
        private readonly Type type;

        /// <summary>
        /// Initializes a new instance of the <see cref="PredicateByType"/> struct.
        /// </summary>
        /// <param name="type">The type of element to match.</param>
        public PredicateByType(Type type)
        {
            this.type = type;
        }

        /// <inheritdoc/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Match(object element)
        {
            return element.GetType() == this.type;
        }
    }

    /// <summary>
    /// An <see cref="IPredicate{T}"/> type matching <see cref="FrameworkElement"/> instances by name.
    /// </summary>
    internal readonly struct PredicateByName : IPredicate<FrameworkElement>
    {
        /// <summary>
        /// The name of the element to look for.
        /// </summary>
        private readonly string name;

        /// <summary>
        /// The comparison type to use to match <see name="name"/>.
        /// </summary>
        private readonly StringComparison comparisonType;

        /// <summary>
        /// Initializes a new instance of the <see cref="PredicateByName"/> struct.
        /// </summary>
        /// <param name="name">The name of the element to look for.</param>
        /// <param name="comparisonType">The comparison type to use to match <paramref name="name"/>.</param>
        public PredicateByName(string name, StringComparison comparisonType)
        {
            this.name = name;
            this.comparisonType = comparisonType;
        }

        /// <inheritdoc/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Match(FrameworkElement element)
        {
            return element.Name?.Equals(this.name, this.comparisonType) ?? false;
        }
    }

}
