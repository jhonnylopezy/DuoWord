﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuoWord.SharedKernel
{
    public abstract class DomainEventBase:INotification
    {

        public DateTime DateOccurred { get; protected set; } = DateTime.UtcNow;
    }
}
