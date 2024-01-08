using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heap<T> where T : IHeapItem<T>, IComparable{
    T[] items;
    int currentItemCount;

    public int Count{
        get{
            return currentItemCount;
        }
    }

    public Heap(int maxCappacity){
        items = new T[maxCappacity];
    }

    public void Add(T item){
        item.heapIndex = currentItemCount;
        items[currentItemCount] = item;
        SortUp(item);
        currentItemCount++;
    }

    public T Pop(){
        T firstItem = items[0];
        currentItemCount--;
        items[0] = items[currentItemCount];
        items[0].heapIndex = 0;
        SortDown(items[0]);
        return firstItem;
    }

    void SortUp(T item){
        int parentIndex = (item.heapIndex - 1)/2;
        while(true){
            T parentItem = items[parentIndex];
            if(item.CompareTo(parentItem) > 0)
                Swap(item, parentItem);
            else {
                break;
            }
            parentIndex = (item.heapIndex - 1)/2;
        }
    }

    void SortDown(T item){
        int leftChildIndex = item.heapIndex * 2 + 1;
        int rightChildIndex = item.heapIndex * 2 + 2;
        int swapIndex = 0;

        if(leftChildIndex < currentItemCount){
            swapIndex = leftChildIndex;
            if(rightChildIndex < currentItemCount){
                if(items[leftChildIndex].CompareTo(items[rightChildIndex]) < 0)
                    swapIndex = rightChildIndex;
            }

            if(item.CompareTo(items[swapIndex]) < 0)
                Swap(item, items[swapIndex]);
            else{
                return;
            }
        }
        else{
            return;
        }
    }

    void Swap(T itemA, T itemB){
        items[itemA.heapIndex] = itemB;
        items[itemB.heapIndex] = itemA;
        int itemAIndex = itemA.heapIndex;
        itemA.heapIndex = itemB.heapIndex;
        itemB.heapIndex = itemAIndex;
    }   

    public void UpdateItem(T item){
        SortUp(item);
    }

    public bool Contains(T item){
        return Equals(item, items[item.heapIndex]);
    }
}

public interface IHeapItem<T>{
    public int heapIndex{get;set;}
}