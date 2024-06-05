using ClassLibraryLabor10;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace labor121
{
    public class MyList<T> where T : IInit, ICloneable, new()
    {
        public Point<T>? beg;
        public Point<T>? end;
        int count = 0;
        public int Count => count;


        public void AddToBegin(T item)
        {
            T newData = (T)item.Clone();
            Point<T> newItem = new Point<T>(newData);
            count++;

            if (beg != null)
            {
                beg.Pred = newItem;
                newItem.Next = beg;
                beg = newItem;
            }
            else
            {
                beg = newItem;
                end = beg;
            }
        }
        public void AddToEnd(T item)
        {
            T newData = (T)item.Clone();
            Point<T> newItem = new Point<T>(newData);
            count++;

            if (end != null)
            {
                end.Next = newItem;
                newItem.Pred = end;
                end = newItem;
            }
            else
            {
                beg = newItem;
                end = beg;
            }
        }
        public MyList() { }
        public MyList(int size)
        {
            if (size <= 0) throw new Exception("size less zero");
            beg = null;
            end = null;
            for (int i = 0; i < size; i++)
            {
                T newItem = Point<T>.MakeRandomItem();
                AddToEnd(newItem);
            }
            count = size;
        }
        public MyList(params T[] collection)
        {
            if (collection == null) throw new Exception("empty collection: null");
            if (collection.Length == 0) throw new Exception("empty collection");
            T newData = (T)collection[0].Clone();
            beg = new Point<T>(newData);
            end = beg;
            for (int i = 1; i < collection.Length; i++)
            {
                AddToEnd(collection[i]);
            }
        }
        public void PrintList()
        {
            if (count == 0)
                Console.WriteLine("the list is empty");
            Point<T>? current = beg;
            while (current != null)
            {
                Console.WriteLine(current);
                current = current.Next;
            }
        }
        public Point<T>? FindItem(T item)
        {
            Point<T>? current = beg;
            while (current != null)
            {
                if (current.Data.Equals(item))
                    return current;
                current = current.Next;
            }
            return null;
        }
        public bool RemoveItem(T item)
        {
            if (beg == null)
                throw new Exception("the empty list");

            Point<T>? pos = FindItem(item);
            if (pos == null) return false;

            count--;

            if (pos.Pred == null)
            {
                beg = beg?.Next;
                if (beg != null) beg.Pred = null;
                else end = null;
                return true;
            }

            if (pos.Next == null)
            {
                end = end?.Pred;
                if (end != null) end.Next = null;
                else beg = null;
                return true;
            }

            Point<T>? next = pos.Next;
            Point<T>? pred = pos.Pred;
            pred.Next = next;
            next.Pred = pred;

            return true;
        }
        public void RemoveLastItemWithFieldValue(T item)
        {
            Point<T>? current = end;

            while (current != null)
            {
                if (current.Data.Equals(item))
                {
                    count--;

                    if (current.Pred == null)
                    {
                        beg = beg?.Next;
                        if (beg != null) beg.Pred = null;
                        else end = null;
                        return;
                    }

                    if (current.Next == null)
                    {
                        end = end?.Pred;
                        if (end != null) end.Next = null;
                        else beg = null;
                        return;
                    }

                    Point<T>? pred = current.Pred;
                    Point<T>? next = current.Next;
                    pred.Next = next;
                    next.Pred = pred;
                    return;
                }

                current = current.Pred;
            }

            throw new Exception($"Элемент с информационным полем {item} не найден.");
        }
        public void AddAfterItem(T afterItem, T newItem)
        {
            Point<T>? current = beg;

            while (current != null)
            {
                if (current.Data.Equals(afterItem))
                {
                    Point<T> newItemNode = new Point<T>(newItem);
                    count++;

                    if (current.Next != null)
                    {
                        Point<T>? nextNode = current.Next;
                        current.Next = newItemNode;
                        newItemNode.Pred = current;
                        newItemNode.Next = nextNode;
                        nextNode.Pred = newItemNode;
                    }
                    else
                    {
                        current.Next = newItemNode;
                        newItemNode.Pred = current;
                        end = newItemNode;
                    }

                    return;
                }

                current = current.Next;
            }

            throw new Exception($"Элемент с информационным полем {afterItem} не найден.");
        }
        public MyList<T> Clone()
        {
            MyList<T> newList = new MyList<T>();

            Point<T>? current = beg;
            while (current != null)
            {
                T newData = (T)current.Data.Clone();
                newList.AddToEnd(newData);
                current = current.Next;
            }

            return newList;
        }
        public void Clear()
        {
            Point<T>? current = beg;
            while (current != null)
            {
                Point<T>? temp = current;
                current = current.Next;
                temp.Data = default(T);
                temp.Next = null;
                temp.Pred = null;
            }
            beg = null;
            end = null;
            count = 0;

        }


    }
}
