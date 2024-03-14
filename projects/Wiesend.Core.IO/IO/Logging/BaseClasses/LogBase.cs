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
using System.Collections.Generic;
using Wiesend.Core.DataTypes;
using Wiesend.Core.DataTypes.Patterns.BaseClasses;
using Wiesend.Core.IO.Logging.Enums;
using Wiesend.Core.IO.Logging.Interfaces;

namespace Wiesend.Core.IO.Logging.BaseClasses
{
    /// <summary>
    /// Delegate used to format the message
    /// </summary>
    /// <param name="Message">Message to format</param>
    /// <param name="Type">Type of message</param>
    /// <param name="args">Args to insert into the message</param>
    /// <returns>The formatted message</returns>
    public delegate string Format(string Message, MessageType Type, params object[] args);

    /// <summary>
    /// Base class for logs
    /// </summary>
    /// <typeparam name="LogType">Log type</typeparam>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Naming", "CA1715:Identifiers should have correct prefix", Justification = "<Pending>")]
    public abstract class LogBase<LogType> : SafeDisposableBaseClass, ILog
        where LogType : LogBase<LogType>
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="Name">Name of the log</param>
        protected LogBase(string Name)
        {
            this.Name = Name;
            this.Log = new ConcurrentDictionary<MessageType, Action<string>>();
        }

        /// <summary>
        /// Name of the log
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Called when the log is "closed"
        /// </summary>
        protected Action<LogType> End { get; set; }

        /// <summary>
        /// Format message function
        /// </summary>
        protected Format FormatMessage { get; set; }

        /// <summary>
        /// Called to log the current message
        /// </summary>
        protected IDictionary<MessageType, Action<string>> Log { get; private set; }

        /// <summary>
        /// Called when the log is "opened"
        /// </summary>
        protected Action<LogType> Start { get; set; }

        /// <summary>
        /// Logs a message
        /// </summary>
        /// <param name="Message">Message to log</param>
        /// <param name="Type">Type of message</param>
        /// <param name="args">args to format/insert into the message</param>
        public virtual void LogMessage(string Message, MessageType Type, params object[] args)
        {
            Message = FormatMessage(Message, Type, args);
            if (Log.ContainsKey(Type))
                Log.GetValue(Type, x => { })(Message);
        }

        /// <summary>
        /// String representation of the logger
        /// </summary>
        /// <returns>The name of the logger</returns>
        public override string ToString()
        {
            return Name;
        }

        /// <summary>
        /// Disposes of the objects
        /// </summary>
        /// <param name="Managed">
        /// True to dispose of all resources, false only disposes of native resources
        /// </param>
        protected override void Dispose(bool Managed)
        {
            if (Managed)
                End((LogType)this);
        }
    }
}