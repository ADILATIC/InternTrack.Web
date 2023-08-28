﻿// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System;
using Xeptions;

namespace InternTrack.Portal.Web.Models.Interns.Exceptions
{
    public class NullInternException : Xeption
    {
        public NullInternException() 
            : base(message: "The Intern is null.") 
        { }

        public NullInternException(string message, Exception innerException) :
            base(message, innerException)
        { }
    }
}
