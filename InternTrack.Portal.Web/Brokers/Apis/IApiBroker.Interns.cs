﻿// -------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE FOR THE WORLD
// -------------------------------------------------------

using InternTrack.Portal.Web.Models.Interns;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InternTrack.Portal.Web.Brokers.Apis
{
    public partial interface IApiBroker
    {
        ValueTask<Intern> PostInternAsync(Intern intern);
        ValueTask<List<Intern>> GetAllInternsAsync();
        ValueTask<Intern> GetInternByIdAsync(Guid internId);
        ValueTask<Intern> PutInternAsync(Intern intern);
        ValueTask<Intern> DeleteInternByIdAsync(Guid internId);
    }
}
