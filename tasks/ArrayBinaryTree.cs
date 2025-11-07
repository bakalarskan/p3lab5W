using System.Collections;

namespace tasks;

/// <summary>
/// Interface for an array-based binary tree.
/// </summary>
public interface IArrayBinaryTree<T> : IEnumerable<T>
{
    /// <summary>
    /// Gets the number of nodes in the tree.
    /// </summary>
    int Count { get; }

    /// <summary>
    /// Gets the index of the root.
    /// </summary>
    int RootIndex { get; }

    /// <summary>
    /// Sets the value of the root node.
    /// </summary>
    void SetRoot(T value);

    /// <summary>
    /// Returns the indices of the left and right children for a given parent.
    /// </summary>
    (int leftIndex, int rightIndex) GetChildrenIndices(int parentIndex);

    /// <summary>
    /// Sets the value of the left child of a given parent.
    /// </summary>
    void SetLeftChild(int parentIndex, T value);

    /// <summary>
    /// Sets the value of the right child of a given parent.
    /// </summary>
    void SetRightChild(int parentIndex, T value);

    /// <summary>
    /// Gets the node at the specified index.
    /// </summary>
    T this[int index] { get; }

    /// <summary>
    /// Checks if a node at the specified index exists.
    /// </summary>
    bool Exists(int index);

    /// <summary>
    /// Clears all nodes from the tree.
    /// </summary>
    void Clear();
}

/// <summary>
/// An implementation of a binary tree based on an array.
/// Indices are calculated as:
/// - Left Child: 2 * i + 1
/// - Right Child: 2 * i + 2
/// - Parent: (i - 1) / 2
/// </summary>
public class ArrayBinaryTree<T> : IArrayBinaryTree<T>
{
    private T[] _nodes;
    private bool[] _present;

    public int Count { get; private set; }
    public int RootIndex => 0;
    public ArrayBinaryTree(int capacity = 10)
    {
        if (capacity < 1)
        {
            capacity = 10;
        }
        _nodes  = new T[capacity];
        Count = 0;
        _present = new bool[capacity];
    }

    private void EnsureCapacity(int index)
    {
        if (index < _nodes.Length)
        {
            return;
        }
        var newSize = _nodes.Length;
        if (newSize == 0)
        {
            newSize = 1;
        }
        while (index >= newSize)
        {
            newSize *= 2;
        }
        T[] temp = new T[newSize];
        for (int i = 0; i < _nodes.Length; i++)
        {
            temp[i] = _nodes[i];
        }
        _nodes = temp;
        //Array.Resize(ref _nodes, newSize);
        Array.Resize(ref _present, newSize);
    }
    public void SetAt(int index, T value)
    {
        if (index < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(index));
        }
        if (index != RootIndex)
        {
            var parentIndex = (index - 1) / 2;
            if (!Exists(parentIndex))
            {
                throw new ArgumentOutOfRangeException(nameof(index));
            }
        }
        EnsureCapacity(index);
        if (!_present[index])
        {
            _present[index] = true;
            Count++;
        }
        _nodes[index] = value;
    }
    public void SetRoot(T value)
    {
        SetAt(RootIndex, value);
    }
    public (int leftIndex, int rightIndex) GetChildrenIndices(int parentIndex)
    {
        if (parentIndex < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(parentIndex));
        }
        return (parentIndex * 2 + 1, parentIndex * 2 + 2);

    }
    public void SetLeftChild(int parentIndex, T value)
    {
        if (!Exists(parentIndex))
        {
            throw new ArgumentOutOfRangeException(nameof(parentIndex));
        }
        var left = GetChildrenIndices(parentIndex).leftIndex;
        SetAt(left, value);
    }

    public void SetRightChild(int parentIndex, T value)
    {
        if (!Exists(parentIndex))
        {
            throw new ArgumentOutOfRangeException(nameof(parentIndex));
        }
        var right = GetChildrenIndices(parentIndex).rightIndex;
        SetAt(right, value);
    }
    public T this[int index]
    {
        get
        {
            if (!Exists(index))
            {
                throw new ArgumentOutOfRangeException(nameof(index));
            }
            return _nodes[index];
        }
    }

    public void Clear()
    {
        Array.Clear(_nodes, 0 , _nodes.Length);
        Array.Clear(_present, 0 , _present.Length);
        Count = 0;
    }

    public bool Exists(int index)
    {
        if (index >= RootIndex && index < _present.Length && _present[index])
        {
            return true;
        }
        return false;
    }

    public IEnumerator<T> GetEnumerator()
    {
        return InOrder(RootIndex).GetEnumerator();
    }

    private IEnumerable<T> InOrder(int index)
    {
        if (!Exists(index))
        {
            yield break;
        }
        var (left, right) = GetChildrenIndices(index);
        foreach (var child in InOrder(left))
        {
            yield return child;
        }
        yield return _nodes[index];
        foreach (var child in InOrder(right))
        {
            yield return child;
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
