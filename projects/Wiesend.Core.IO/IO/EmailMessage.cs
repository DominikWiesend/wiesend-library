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

using System.Collections.Generic;
using System.Net.Mail;
using Wiesend.Core.IO.Messaging;
using Wiesend.Core.IO.Messaging.BaseClasses;
using Wiesend.Core.IO.Messaging.Interfaces;

namespace Wiesend.Core.IO
{
    /// <summary>
    /// Email message class
    /// </summary>
    public class EmailMessage : MessageBase, IMessage
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public EmailMessage()
            : base(IoC.Manager.Bootstrapper.Resolve<Manager>().MessagingSystems[typeof(EmailMessage)])
        {
            Attachments = new List<Attachment>();
            EmbeddedResources = new List<LinkedResource>();
            Priority = MailPriority.Normal;
            Port = 25;
        }

        /// <summary>
        /// Attachments
        /// </summary>
        public ICollection<Attachment> Attachments { get; private set; }

        /// <summary>
        /// BCC
        /// </summary>
        public string Bcc { get; set; }

        /// <summary>
        /// CC
        /// </summary>
        public string CC { get; set; }

        /// <summary>
        /// Embedded resource
        /// </summary>
        public ICollection<LinkedResource> EmbeddedResources { get; private set; }

        /// <summary>
        /// Password for the user
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Port to use
        /// </summary>
        public int Port { get; set; }

        /// <summary>
        /// Priority
        /// </summary>
        public MailPriority Priority { get; set; }

        /// <summary>
        /// Server
        /// </summary>
        public string Server { get; set; }

        /// <summary>
        /// User name for the user
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Use SSL?
        /// </summary>
        public bool UseSSL { get; set; }

        /// <summary>
        /// Disposes of the objects
        /// </summary>
        /// <param name="Managed">
        /// True to dispose of all resources, false only disposes of native resources
        /// </param>
        protected override void Dispose(bool Managed)
        {
            if (Attachments != null)
            {
                foreach (Attachment Attachment in Attachments)
                    Attachment.Dispose();
                Attachments = null;
            }
            if (EmbeddedResources != null)
            {
                foreach (LinkedResource Resource in EmbeddedResources)
                    Resource.Dispose();
                EmbeddedResources = null;
            }
        }
    }
}