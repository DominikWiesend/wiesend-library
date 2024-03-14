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

using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using Wiesend.Core.DataTypes.Conversion;
using Wiesend.Core.DataTypes.DataMapper.Interfaces;

namespace Wiesend.Core.DataTypes
{
    /// <summary>
    /// Extensions converting between types, checking if something is null, etc.
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public static class TypeConversionExtensions
    {
        /// <summary>
        /// Calls the object's ToString function passing in the formatting
        /// </summary>
        /// <param name="Input">Input object</param>
        /// <param name="Format">Format of the output string</param>
        /// <returns>The formatted string</returns>
        public static string FormatToString(this object Input, string Format)
        {
            if (Input == null)
                return "";
            return !string.IsNullOrEmpty(Format) ? Input.Call<string>("ToString", Format) : Input.ToString();
        }

        /// <summary>
        /// Sets up a mapping between two types
        /// </summary>
        /// <param name="LeftType">Left type</param>
        /// <param name="RightType">Right type</param>
        /// <returns>The type mapping</returns>
        public static ITypeMapping MapTo([NotNull] this Type LeftType, [NotNull] Type RightType)
        {
            if (LeftType == null) throw new ArgumentNullException(nameof(LeftType));
            if (RightType == null) throw new ArgumentNullException(nameof(RightType));
            var TempManager = IoC.Manager.Bootstrapper.Resolve<DataTypes.DataMapper.Manager>();
            if (TempManager == null)
                return null;
            return TempManager.Map(LeftType, RightType);
        }

        /// <summary>
        /// Sets up a mapping between two types
        /// </summary>
        /// <typeparam name="Left">Left type</typeparam>
        /// <typeparam name="Right">Right type</typeparam>
        /// <param name="Object">Object to set up mapping for</param>
        /// <returns>The type mapping</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "<Pending>")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Naming", "CA1715:Identifiers should have correct prefix", Justification = "<Pending>")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Naming", "CA1720:Identifier contains type name", Justification = "<Pending>")]
        public static ITypeMapping<Left, Right> MapTo<Left, Right>(this Left Object)
        {
            var TempManager = IoC.Manager.Bootstrapper.Resolve<DataTypes.DataMapper.Manager>();
            if (TempManager == null)
                return null;
            return TempManager.Map<Left, Right>();
        }

        /// <summary>
        /// Sets up a mapping between two types
        /// </summary>
        /// <typeparam name="Left">Left type</typeparam>
        /// <typeparam name="Right">Right type</typeparam>
        /// <param name="ObjectType">Object type to set up mapping for</param>
        /// <returns>The type mapping</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "<Pending>")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Naming", "CA1715:Identifiers should have correct prefix", Justification = "<Pending>")]
        public static ITypeMapping<Left, Right> MapTo<Left, Right>(this Type ObjectType)
        {
            var TempManager = IoC.Manager.Bootstrapper.Resolve<DataTypes.DataMapper.Manager>();
            if (TempManager == null)
                return null;
            return TempManager.Map<Left, Right>();
        }

        /// <summary>
        /// Attempts to convert the DataTable to a list of objects
        /// </summary>
        /// <typeparam name="T">Type the objects should be in the list</typeparam>
        /// <param name="Data">DataTable to convert</param>
        /// <param name="Creator">Function used to create each object</param>
        /// <returns>The DataTable converted to a list of objects</returns>
        public static List<T> To<T>(this DataTable Data, Func<T> Creator)
            where T : class,new()
        {
            if (Data == null)
                return new List<T>();
            Creator = Creator.Check(() => new T());
            Type TType = typeof(T);
            var Properties = TType.GetProperties();
            var Results = new List<T>();
            for (int x = 0; x < Data.Rows.Count; ++x)
            {
                var RowObject = Creator();
                for (int y = 0; y < Data.Columns.Count; ++y)
                {
                    var Property = Properties.FirstOrDefault(z => z.Name == Data.Columns[y].ColumnName);
#if NET45
                    Property?.SetValue(RowObject, Data.Rows[x][Data.Columns[y]].To(Property.PropertyType, null), new object[] { });
#else
                    Property?.SetValue(RowObject, Data.Rows[x][Data.Columns[y]].To(Property.PropertyType, null), Array.Empty<object>());
#endif
                }
                Results.Add(RowObject);
            }
            return Results;
        }

        /// <summary>
        /// Attempts to convert the object to another type and returns the value
        /// </summary>
        /// <typeparam name="T">Type to convert from</typeparam>
        /// <typeparam name="R">Return type</typeparam>
        /// <param name="Object">Object to convert</param>
        /// <param name="DefaultValue">
        /// Default value to return if there is an issue or it can't be converted
        /// </param>
        /// <returns>
        /// The object converted to the other type or the default value if there is an error or
        /// can't be converted
        /// </returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Naming", "CA1715:Identifiers should have correct prefix", Justification = "<Pending>")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Naming", "CA1720:Identifier contains type name", Justification = "<Pending>")]
        public static R To<T, R>(this T Object, R DefaultValue = default)
        {
            return Manager.To(Object, DefaultValue);
        }

        /// <summary>
        /// Attempts to convert the object to another type and returns the value
        /// </summary>
        /// <typeparam name="T">Type to convert from</typeparam>
        /// <param name="ResultType">Result type</param>
        /// <param name="Object">Object to convert</param>
        /// <param name="DefaultValue">
        /// Default value to return if there is an issue or it can't be converted
        /// </param>
        /// <returns>
        /// The object converted to the other type or the default value if there is an error or
        /// can't be converted
        /// </returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Naming", "CA1720:Identifier contains type name", Justification = "<Pending>")]
        public static object To<T>(this T Object, Type ResultType, object DefaultValue = null)
        {
            return Manager.To(Object, ResultType, DefaultValue);
        }
    }
}