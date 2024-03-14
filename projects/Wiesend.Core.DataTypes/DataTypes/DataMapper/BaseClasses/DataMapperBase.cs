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
using System.Collections.Concurrent;
using Wiesend.Core.DataTypes.DataMapper.Interfaces;

namespace Wiesend.Core.DataTypes.DataMapper.BaseClasses
{
    /// <summary>
    /// Data mapper base class
    /// </summary>
    public abstract class DataMapperBase : IDataMapper
    {
        /// <summary>
        /// Constructor
        /// </summary>
        protected DataMapperBase()
        {
            this.Mappings = new ConcurrentDictionary<Tuple<Type, Type>, ITypeMapping>();
        }

        /// <summary>
        /// The name of the data mapper
        /// </summary>
        public abstract string Name { get; }

        /// <summary>
        /// Mappings
        /// </summary>
        protected ConcurrentDictionary<Tuple<Type, Type>, ITypeMapping> Mappings { get; private set; }

        /// <summary>
        /// Adds or returns a mapping between two types
        /// </summary>
        /// <typeparam name="Left">Left type</typeparam>
        /// <typeparam name="Right">Right type</typeparam>
        /// <returns>A mapping object for the two types specified</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Naming", "CA1715:Identifiers should have correct prefix", Justification = "<Pending>")]
        public ITypeMapping<Left, Right> Map<Left, Right>()
        {
            var Key = new Tuple<Type, Type>(typeof(Left), typeof(Right));
            Mappings.AddOrUpdate(Key, x => CreateTypeMapping<Left, Right>(), (x, y) => y);
            Mappings.TryGetValue(Key, out ITypeMapping ReturnValue);
            return (ITypeMapping<Left, Right>)ReturnValue;
        }

        /// <summary>
        /// Adds or returns a mapping between two types
        /// </summary>
        /// <param name="Left">Left type</param>
        /// <param name="Right">Right type</param>
        /// <returns>A mapping object for the two types specified</returns>
        public ITypeMapping Map(Type Left, Type Right)
        {
            var Key = new Tuple<Type, Type>(Left, Right);
            Mappings.AddOrUpdate(Key, x => CreateTypeMapping(x.Item1, x.Item2), (x, y) => y);
            Mappings.TryGetValue(Key, out ITypeMapping ReturnValue);
            return ReturnValue;
        }

        /// <summary>
        /// The name of the data mapper
        /// </summary>
        /// <returns>The name of the data mapper</returns>
        public override string ToString()
        {
            return Name;
        }

        /// <summary>
        /// Used internally to create type mappings
        /// </summary>
        /// <typeparam name="Left">Left type</typeparam>
        /// <typeparam name="Right">Right type</typeparam>
        /// <returns>A mapping object for the two types specified</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Naming", "CA1715:Identifiers should have correct prefix", Justification = "<Pending>")]
        protected abstract ITypeMapping<Left, Right> CreateTypeMapping<Left, Right>();

        /// <summary>
        /// Used internally to create type mappings
        /// </summary>
        /// <param name="Left">Left type</param>
        /// <param name="Right">Right type</param>
        /// <returns>A mapping object for the two types specified</returns>
        protected abstract ITypeMapping CreateTypeMapping(Type Left, Type Right);
    }
}