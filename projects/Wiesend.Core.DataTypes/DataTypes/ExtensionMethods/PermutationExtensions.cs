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
    /// Permutation extensions
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public static class PermutationExtensions
    {
        /// <summary>
        /// Finds all permutations of the items within the list
        /// </summary>
        /// <typeparam name="T">Object type in the list</typeparam>
        /// <param name="Input">Input list</param>
        /// <returns>The list of permutations</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0180:Use tuple to swap values", Justification = "<Pending>")]
        public static ListMapping<int, T> Permute<T>([NotNull] this IEnumerable<T> Input)
        {
            if (Input == null) throw new ArgumentNullException(nameof(Input));
            var Current = new List<T>();
            Current.AddRange(Input);
            var ReturnValue = new ListMapping<int, T>();
            var Max = (Input.Count() - 1).Factorial();
            int CurrentValue = 0;
            for (int x = 0; x < Input.Count(); ++x)
            {
                int z = 0;
                while (z < Max)
                {
                    int y = Input.Count() - 1;
                    while (y > 1)
                    {
                        T TempHolder = Current[y - 1];
                        Current[y - 1] = Current[y];
                        Current[y] = TempHolder;
                        --y;
                        foreach (T Item in Current)
                            ReturnValue.Add(CurrentValue, Item);
                        ++z;
                        ++CurrentValue;
                        if (z == Max)
                            break;
                    }
                }
                if (x + 1 != Input.Count())
                {
                    Current.Clear();
                    Current.AddRange(Input);
                    T TempHolder2 = Current[0];
                    Current[0] = Current[x + 1];
                    Current[x + 1] = TempHolder2;
                }
            }
            return ReturnValue;
        }
    }
}