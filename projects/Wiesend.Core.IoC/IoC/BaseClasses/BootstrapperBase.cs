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
using System.Linq;
using System.Reflection;
using Wiesend.Core.IoC.Interfaces;

namespace Wiesend.Core.IoC.BaseClasses
{
    /// <summary>
    /// Bootstrapper base class
    /// </summary>
    /// <typeparam name="Container">The actual IoC object</typeparam>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Naming", "CA1715:Identifiers should have correct prefix", Justification = "<Pending>")]
    public abstract class BootstrapperBase<Container> : IBootstrapper
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="assemblies">The assemblies.</param>
        /// <param name="types">The types.</param>
        protected BootstrapperBase(IEnumerable<Assembly> assemblies, IEnumerable<Type> types)
        {
            this.Assemblies = assemblies.ToList();
            this.Types = types.ToList();
        }

        /// <summary>
        /// Name of the bootstrapper
        /// </summary>
        public abstract string Name { get; }

        /// <summary>
        /// The IoC container
        /// </summary>
        protected abstract Container AppContainer { get; }

        /// <summary>
        /// Gets the types.
        /// </summary>
        /// <value>The types.</value>
        protected List<Type> Types { get; private set; }

        /// <summary>
        /// Gets the assemblies.
        /// </summary>
        /// <value>The assemblies.</value>
        private List<Assembly> Assemblies { get; set; }

        /// <summary>
        /// Adds the assembly.
        /// </summary>
        /// <param name="Assemblies">The assemblies.</param>
        public void AddAssembly(params Assembly[] Assemblies)
        {
            if (Assemblies == null || Assemblies.Length == 0)
                return;

            foreach (Assembly Assembly in Assemblies)
            {
                if (!this.Assemblies.Contains(Assembly))
                {
                    try
                    {
                        this.Types.AddRange(Assembly.GetTypes());
                        this.Assemblies.Add(Assembly);
                    }
                    catch (ReflectionTypeLoadException) { }
                }
            }
        }

        /// <summary>
        /// Disposes of the object
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Registers an object with the bootstrapper
        /// </summary>
        /// <typeparam name="T">Object type</typeparam>
        /// <param name="Object">Object to register</param>
        /// <param name="Name">Name associated with the object</param>
        public abstract void Register<T>(T Object, string Name = "")
            where T : class;

        /// <summary>
        /// Registers a type with the default constructor
        /// </summary>
        /// <typeparam name="T">Object type to register</typeparam>
        /// <param name="Name">Name associated with the object</param>
        public abstract void Register<T>(string Name = "")
            where T : class,new();

        /// <summary>
        /// Registers a type with the default constructor of a child class
        /// </summary>
        /// <typeparam name="T1">Base class/interface type</typeparam>
        /// <typeparam name="T2">Child class type</typeparam>
        /// <param name="Name">Name associated with the object</param>
        public abstract void Register<T1, T2>(string Name = "")
            where T2 : class,T1
            where T1 : class;

        /// <summary>
        /// Registers a type with a function
        /// </summary>
        /// <typeparam name="T">Type that the function returns</typeparam>
        /// <param name="Name">Name associated with the object</param>
        /// <param name="Function">Function to register with the type</param>
        public abstract void Register<T>(Func<T> Function, string Name = "")
            where T : class;

        /// <summary>
        /// Registers all objects of a certain type with the bootstrapper
        /// </summary>
        /// <typeparam name="T">Object type</typeparam>
        public abstract void RegisterAll<T>()
            where T : class;

        /// <summary>
        /// Resolves the object based on the type specified
        /// </summary>
        /// <typeparam name="T">Type to resolve</typeparam>
        /// <param name="DefaultObject">Default object to return if the type can not be resolved</param>
        /// <returns>An object of the specified type</returns>
        public abstract T Resolve<T>(T DefaultObject = default)
            where T : class;

        /// <summary>
        /// Resolves the object based on the type specified
        /// </summary>
        /// <typeparam name="T">Type to resolve</typeparam>
        /// <param name="Name">Name associated with the object</param>
        /// <param name="DefaultObject">Default object to return if the type can not be resolved</param>
        /// <returns>An object of the specified type</returns>
        public abstract T Resolve<T>(string Name, T DefaultObject = default)
            where T : class;

        /// <summary>
        /// Resolves the object based on the type specified
        /// </summary>
        /// <param name="ObjectType">Object type</param>
        /// <param name="DefaultObject">Default object to return if the type can not be resolved</param>
        /// <returns>An object of the specified type</returns>
        public abstract object Resolve(Type ObjectType, object DefaultObject = null);

        /// <summary>
        /// Resolves the object based on the type specified
        /// </summary>
        /// <param name="ObjectType">Object type</param>
        /// <param name="Name">Name associated with the object</param>
        /// <param name="DefaultObject">Default object to return if the type can not be resolved</param>
        /// <returns>An object of the specified type</returns>
        public abstract object Resolve(Type ObjectType, string Name, object DefaultObject = null);

        /// <summary>
        /// Resolves the objects based on the type specified
        /// </summary>
        /// <typeparam name="T">Type to resolve</typeparam>
        /// <returns>A list of objects of the specified type</returns>
        public abstract IEnumerable<T> ResolveAll<T>()
            where T : class;

        /// <summary>
        /// Resolves all objects based on the type specified
        /// </summary>
        /// <param name="ObjectType">Object type</param>
        /// <returns>A list of objects of the specified type</returns>
        public abstract IEnumerable<object> ResolveAll(Type ObjectType);

        /// <summary>
        /// Disposes of the object
        /// </summary>
        /// <param name="Managed">
        /// Determines if all objects should be disposed or just managed objects
        /// </param>
        protected virtual void Dispose(bool Managed)
        {
        }

        /// <summary>
        /// Destructor
        /// </summary>
        ~BootstrapperBase()
        {
            Dispose(false);
        }
    }
}