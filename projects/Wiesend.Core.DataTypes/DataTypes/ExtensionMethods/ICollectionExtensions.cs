﻿#region Project Description [About this]
// =================================================================================
//            The whole Project is Licensed under the MIT License
// =================================================================================
// =================================================================================
//    Wiesend's Dynamic Link Library is a collection of reusable code that 
//    I've written, or found throughout my programming career. 
//
//    I tried my very best to mention all of the original copyright holders. 
//    I hope that all code which I've copied from others is mentioned and all 
//    their copyrights are given. The copied code (or code snippets) could 
//    already be modified by me or others.
// =================================================================================
#endregion of Project Description
#region Original Source Code [Links to all original source code]
// =================================================================================
//          Original Source Code [Links to all original source code]
// =================================================================================
// https://github.com/JaCraig/Craig-s-Utility-Library
// =================================================================================
//    I didn't wrote this source totally on my own, this class could be nearly a 
//    clone of the project of James Craig, I did highly get inspired by him, so 
//    this piece of code isn't totally mine, shame on me, I'm not the best!
// =================================================================================
#endregion of where is the original source code
#region Licenses [MIT Licenses]
#region MIT License [James Craig]
// =================================================================================
//    Copyright(c) 2014 <a href="http://www.gutgames.com">James Craig</a>
//    
//    Permission is hereby granted, free of charge, to any person obtaining a copy
//    of this software and associated documentation files (the "Software"), to deal
//    in the Software without restriction, including without limitation the rights
//    to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
//    copies of the Software, and to permit persons to whom the Software is
//    furnished to do so, subject to the following conditions:
//    
//    The above copyright notice and this permission notice shall be included in all
//    copies or substantial portions of the Software.
//    
//    THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//    IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//    FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//    AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
//    LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
//    OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
//    SOFTWARE.
// =================================================================================
#endregion of MIT License [James Craig] 
#region MIT License [Dominik Wiesend]
// =================================================================================
//    Copyright(c) 2018 Dominik Wiesend. All rights reserved.
//    
//    Permission is hereby granted, free of charge, to any person obtaining a copy
//    of this software and associated documentation files (the "Software"), to deal
//    in the Software without restriction, including without limitation the rights
//    to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
//    copies of the Software, and to permit persons to whom the Software is
//    furnished to do so, subject to the following conditions:
//    
//    The above copyright notice and this permission notice shall be included in all
//    copies or substantial portions of the Software.
//    
//    THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//    IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//    FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//    AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
//    LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
//    OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
//    SOFTWARE.
// =================================================================================
#endregion of MIT License [Dominik Wiesend] 
#endregion of Licenses [MIT Licenses]

using System;
using System.Collections.Generic;
using System.ComponentModel;
using JetBrains.Annotations;
using System.Linq;

namespace Wiesend.Core.DataTypes
{
    /// <summary>
    /// ICollection extensions
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public static class ICollectionExtensions
    {
        /// <summary>
        /// Adds a list of items to the collection
        /// </summary>
        /// <typeparam name="T">The type of the items in the collection</typeparam>
        /// <param name="Collection">Collection</param>
        /// <param name="Items">Items to add</param>
        /// <returns>The collection with the added items</returns>
        public static ICollection<T> Add<T>([NotNull] this ICollection<T> Collection, IEnumerable<T> Items)
        {
            if (Collection == null) throw new ArgumentNullException(nameof(Collection));
            if (Items == null)
                return Collection;
            Items.ForEach(x => Collection.Add(x));
            return Collection;
        }

        /// <summary>
        /// Adds a list of items to the collection
        /// </summary>
        /// <typeparam name="T">The type of the items in the collection</typeparam>
        /// <param name="Collection">Collection</param>
        /// <param name="Items">Items to add</param>
        /// <returns>The collection with the added items</returns>
        public static ICollection<T> Add<T>([NotNull] this ICollection<T> Collection, params T[] Items)
        {
            if (Collection == null) throw new ArgumentNullException(nameof(Collection));
            if (Items == null)
                return Collection;
            Items.ForEach(x => Collection.Add(x));
            return Collection;
        }

        /// <summary>
        /// Adds an item to a list and returns the item
        /// </summary>
        /// <typeparam name="T">Item type</typeparam>
        /// <param name="Collection">Collection to add to</param>
        /// <param name="Item">Item to add to the collection</param>
        /// <returns>The original item</returns>
        public static T AddAndReturn<T>([NotNull] this ICollection<T> Collection, T Item)
        {
            if (Collection == null) throw new ArgumentNullException(nameof(Collection));
            Collection.Add(Item);
            return Item;
        }

        /// <summary>
        /// Adds items to the collection if it passes the predicate test
        /// </summary>
        /// <typeparam name="T">Collection type</typeparam>
        /// <param name="Collection">Collection to add to</param>
        /// <param name="Items">Items to add to the collection</param>
        /// <param name="Predicate">Predicate that an item needs to satisfy in order to be added</param>
        /// <returns>True if any are added, false otherwise</returns>
        public static bool AddIf<T>([NotNull] this ICollection<T> Collection, [NotNull] Predicate<T> Predicate, params T[] Items)
        {
            if (Collection == null) throw new ArgumentNullException(nameof(Collection));
            if (Predicate == null) throw new ArgumentNullException(nameof(Predicate));
            if (Items == null)
                return true;
            bool ReturnValue = false;
            foreach (T Item in Items)
            {
                if (Predicate(Item))
                {
                    Collection.Add(Item);
                    ReturnValue = true;
                }
            }
            return ReturnValue;
        }

        /// <summary>
        /// Adds an item to the collection if it isn't already in the collection
        /// </summary>
        /// <typeparam name="T">Collection type</typeparam>
        /// <param name="Collection">Collection to add to</param>
        /// <param name="Items">Items to add to the collection</param>
        /// <param name="Predicate">Predicate that an item needs to satisfy in order to be added</param>
        /// <returns>True if it is added, false otherwise</returns>
        public static bool AddIf<T>([NotNull] this ICollection<T> Collection, [NotNull] Predicate<T> Predicate, IEnumerable<T> Items)
        {
            if (Collection == null) throw new ArgumentNullException(nameof(Collection));
            if (Predicate == null) throw new ArgumentNullException(nameof(Predicate));
            if (Items == null)
                return true;
            return Collection.AddIf(Predicate, Items.ToArray());
        }

        /// <summary>
        /// Adds an item to the collection if it isn't already in the collection
        /// </summary>
        /// <typeparam name="T">Collection type</typeparam>
        /// <param name="Collection">Collection to add to</param>
        /// <param name="Items">Items to add to the collection</param>
        /// <returns>True if it is added, false otherwise</returns>
        public static bool AddIfUnique<T>([NotNull] this ICollection<T> Collection, params T[] Items)
        {
            if (Collection == null) throw new ArgumentNullException(nameof(Collection));
            if (Items == null)
                return true;
            return Collection.AddIf(x => !Collection.Contains(x), Items);
        }

        /// <summary>
        /// Adds an item to the collection if it isn't already in the collection
        /// </summary>
        /// <typeparam name="T">Collection type</typeparam>
        /// <param name="Collection">Collection to add to</param>
        /// <param name="Items">Items to add to the collection</param>
        /// <param name="Predicate">
        /// Predicate used to determine if two values are equal. Return true if they are the same,
        /// false otherwise
        /// </param>
        /// <returns>True if it is added, false otherwise</returns>
        public static bool AddIfUnique<T>([NotNull] this ICollection<T> Collection, [NotNull] Func<T, T, bool> Predicate, params T[] Items)
        {
            if (Collection == null) throw new ArgumentNullException(nameof(Collection));
            if (Predicate == null) throw new ArgumentNullException(nameof(Predicate));
            if (Items == null)
                return true;
            return Collection.AddIf(x => !Collection.Any(y => Predicate(x, y)), Items);
        }

        /// <summary>
        /// Adds an item to the collection if it isn't already in the collection
        /// </summary>
        /// <typeparam name="T">Collection type</typeparam>
        /// <param name="Collection">Collection to add to</param>
        /// <param name="Items">Items to add to the collection</param>
        /// <returns>True if it is added, false otherwise</returns>
        public static bool AddIfUnique<T>([NotNull] this ICollection<T> Collection, IEnumerable<T> Items)
        {
            if (Collection == null) throw new ArgumentNullException(nameof(Collection));
            if (Items == null)
                return true;
            return Collection.AddIf(x => !Collection.Contains(x), Items);
        }

        /// <summary>
        /// Adds an item to the collection if it isn't already in the collection
        /// </summary>
        /// <typeparam name="T">Collection type</typeparam>
        /// <param name="Collection">Collection to add to</param>
        /// <param name="Items">Items to add to the collection</param>
        /// <param name="Predicate">
        /// Predicate used to determine if two values are equal. Return true if they are the same,
        /// false otherwise
        /// </param>
        /// <returns>True if it is added, false otherwise</returns>
        public static bool AddIfUnique<T>([NotNull] this ICollection<T> Collection, [NotNull] Func<T, T, bool> Predicate, IEnumerable<T> Items)
        {
            if (Collection == null) throw new ArgumentNullException(nameof(Collection));
            if (Predicate == null) throw new ArgumentNullException(nameof(Predicate));
            if (Items == null)
                return true;
            return Collection.AddIf(x => !Collection.Any(y => Predicate(x, y)), Items);
        }

        /// <summary>
        /// Does an action for each item in the IEnumerable between the start and end indexes
        /// </summary>
        /// <typeparam name="T">Object type</typeparam>
        /// <param name="List">IEnumerable to iterate over</param>
        /// <param name="Start">Item to start with</param>
        /// <param name="End">Item to end with</param>
        /// <param name="Action">Action to do</param>
        /// <returns>The original list</returns>
        public static IList<T> For<T>([NotNull] this IList<T> List, int Start, int End, [NotNull] Action<int, T> Action)
        {
            if (List == null) throw new ArgumentNullException(nameof(List));
            if (Action == null) throw new ArgumentNullException(nameof(Action));
            if (!(End + 1 - Start >= 0)) throw new InvalidOperationException("End must be greater than start");
            if (End >= List.Count)
                End = List.Count - 1;
            if (Start < 0)
                Start = 0;
            for (int x = Start; x <= End; ++x)
                Action(x, List[x]);
            return List;
        }

        /// <summary>
        /// Does a function for each item in the IEnumerable between the start and end indexes and
        /// returns an IEnumerable of the results
        /// </summary>
        /// <typeparam name="T">Object type</typeparam>
        /// <typeparam name="R">Return type</typeparam>
        /// <param name="List">IEnumerable to iterate over</param>
        /// <param name="Start">Item to start with</param>
        /// <param name="End">Item to end with</param>
        /// <param name="Function">Function to do</param>
        /// <returns>The resulting list</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Naming", "CA1715:Identifiers should have correct prefix", Justification = "<Pending>")]
        public static IList<R> For<T, R>([NotNull] this IList<T> List, int Start, int End, [NotNull] Func<int, T, R> Function)
        {
            if (List == null) throw new ArgumentNullException(nameof(List));
            if (Function == null) throw new ArgumentNullException(nameof(Function));
            if (!(End + 1 - Start >= 0)) throw new InvalidOperationException("End must be greater than start");
            var ReturnValues = new List<R>();
            if (End >= List.Count)
                End = List.Count - 1;
            if (Start < 0)
                Start = 0;
            for (int x = Start; x <= End; ++x)
                ReturnValues.Add(Function(x, List[x]));
            return ReturnValues;
        }

        /// <summary>
        /// Removes all items that fit the predicate passed in
        /// </summary>
        /// <typeparam name="T">The type of the items in the collection</typeparam>
        /// <param name="Collection">Collection to remove items from</param>
        /// <param name="Predicate">Predicate used to determine what items to remove</param>
        public static ICollection<T> Remove<T>([NotNull] this ICollection<T> Collection, Func<T, bool> Predicate)
        {
            if (Collection == null) throw new ArgumentNullException(nameof(Collection));
            return Collection.Where(x => !Predicate(x)).ToList();
        }

        /// <summary>
        /// Removes all items in the list from the collection
        /// </summary>
        /// <typeparam name="T">The type of the items in the collection</typeparam>
        /// <param name="Collection">Collection</param>
        /// <param name="Items">Items to remove</param>
        /// <returns>The collection with the items removed</returns>
        public static ICollection<T> Remove<T>([NotNull] this ICollection<T> Collection, IEnumerable<T> Items)
        {
            if (Collection == null) throw new ArgumentNullException(nameof(Collection));
            if (Items == null)
                return Collection;
            return Collection.Where(x => !Items.Contains(x)).ToList();
        }
    }
}