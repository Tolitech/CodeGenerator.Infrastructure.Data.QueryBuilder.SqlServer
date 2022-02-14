﻿using System;
using Tolitech.CodeGenerator.Domain.Entities;

namespace Tolitech.CodeGenerator.Infrastructure.Data.QueryBuilder.SqlServer.Tests.Domain.Entities
{
    public class PersonEntity : Entity
    {
        public int? PersonId { get; set; }

        public string? Name { get; set; }

        public int? Age { get; set; }
    }
}
