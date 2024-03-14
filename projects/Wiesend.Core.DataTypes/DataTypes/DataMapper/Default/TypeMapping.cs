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
using System.Linq;
using System.Linq.Expressions;
using Wiesend.Core.DataTypes.DataMapper.BaseClasses;
using Wiesend.Core.DataTypes.DataMapper.Interfaces;

namespace Wiesend.Core.DataTypes.DataMapper.Default
{
    /// <summary>
    /// Type mapping default class
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Naming", "CA1715:Identifiers should have correct prefix", Justification = "<Pending>")]
    public class TypeMapping<Left, Right> : TypeMappingBase<Left, Right>
    {
        /// <summary>
        /// Adds a mapping
        /// </summary>
        /// <param name="LeftExpression">Left expression</param>
        /// <param name="RightExpression">Right expression</param>
        /// <returns>This</returns>
        public override ITypeMapping<Left, Right> AddMapping(Expression<Func<Left, object>> LeftExpression, Expression<Func<Right, object>> RightExpression)
        {
            this.Mappings.Add(new Mapping<Left, Right>(LeftExpression, RightExpression));
            return this;
        }

        /// <summary>
        /// Adds a mapping
        /// </summary>
        /// <param name="LeftGet">Left get function</param>
        /// <param name="LeftSet">Left set action</param>
        /// <param name="RightExpression">Right expression</param>
        /// <returns>This</returns>
        public override ITypeMapping<Left, Right> AddMapping(Func<Left, object> LeftGet, Action<Left, object> LeftSet, Expression<Func<Right, object>> RightExpression)
        {
            this.Mappings.Add(new Mapping<Left, Right>(LeftGet, LeftSet, RightExpression));
            return this;
        }

        /// <summary>
        /// Adds a mapping
        /// </summary>
        /// <param name="LeftExpression">Left expression</param>
        /// <param name="RightGet">Right get function</param>
        /// <param name="RightSet">Right set function</param>
        /// <returns>This</returns>
        public override ITypeMapping<Left, Right> AddMapping(Expression<Func<Left, object>> LeftExpression, Func<Right, object> RightGet, Action<Right, object> RightSet)
        {
            this.Mappings.Add(new Mapping<Left, Right>(LeftExpression, RightGet, RightSet));
            return this;
        }

        /// <summary>
        /// Adds a mapping
        /// </summary>
        /// <param name="LeftGet">Left get function</param>
        /// <param name="LeftSet">Left set function</param>
        /// <param name="RightGet">Right get function</param>
        /// <param name="RightSet">Right set function</param>
        /// <returns>This</returns>
        public override ITypeMapping<Left, Right> AddMapping(Func<Left, object> LeftGet, Action<Left, object> LeftSet, Func<Right, object> RightGet, Action<Right, object> RightSet)
        {
            this.Mappings.Add(new Mapping<Left, Right>(LeftGet, LeftSet, RightGet, RightSet));
            return this;
        }

        /// <summary>
        /// Copies from the source to the destination
        /// </summary>
        /// <param name="Source">Source object</param>
        /// <param name="Destination">Destination object</param>
        public override void Copy(Left Source, Right Destination)
        {
            foreach (Mapping<Left, Right> Mapping in Mappings.OfType<Mapping<Left, Right>>())
                Mapping.Copy(Source, Destination);
        }

        /// <summary>
        /// Copies from the source to the destination
        /// </summary>
        /// <param name="Source">Source object</param>
        /// <param name="Destination">Destination object</param>
        public override void Copy(Right Source, Left Destination)
        {
            foreach (Mapping<Left, Right> Mapping in Mappings.OfType<Mapping<Left, Right>>())
                Mapping.Copy(Source, Destination);
        }

        /// <summary>
        /// Copies from the source to the destination (used in instances when both Left and Right
        /// are the same type and thus Copy is ambiguous)
        /// </summary>
        /// <param name="Source">Source</param>
        /// <param name="Destination">Destination</param>
        public override void CopyLeftToRight(Left Source, Right Destination)
        {
            foreach (Mapping<Left, Right> Mapping in Mappings.OfType<Mapping<Left, Right>>())
                Mapping.CopyLeftToRight(Source, Destination);
        }

        /// <summary>
        /// Copies from the source to the destination (used in instances when both Left and Right
        /// are the same type and thus Copy is ambiguous)
        /// </summary>
        /// <param name="Source">Source</param>
        /// <param name="Destination">Destination</param>
        public override void CopyRightToLeft(Right Source, Left Destination)
        {
            foreach (Mapping<Left, Right> Mapping in Mappings.OfType<Mapping<Left, Right>>())
                Mapping.CopyRightToLeft(Source, Destination);
        }
    }
}